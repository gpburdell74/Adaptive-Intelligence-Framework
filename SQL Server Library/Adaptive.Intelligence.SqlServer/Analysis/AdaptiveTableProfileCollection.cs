using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.Shared.IO;
using Adaptive.Intelligence.Shared.Logging;
using Adaptive.Intelligence.SqlServer.Schema;

namespace Adaptive.Intelligence.SqlServer.Analysis
{
    /// <summary>
    /// Provides the definition for a collection implementation used to manage user-defined profiles for 
    /// specific SQL data tables in the EasyVote business domain.
    /// </summary>
    public sealed class AdaptiveTableProfileCollection : NameIndexCollection<AdaptiveTableProfile>, ITableProfileCollection
    {
        #region Private Member Declarations        
        /// <summary>
        /// The file name in which the profile content is to be stored.
        /// </summary>
        private string? _fileName;
        #endregion

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>
        /// A string containing the fully-qualified path and name of the file.
        /// </value>
        public string? FileName
        {
            get => _fileName;
            set => _fileName = value;
        }
        
        /// <summary>
        /// Gets the name / key value of the specified instance.
        /// </summary>
        /// <param name="item">The <see cref="AdaptiveTableProfile"/> item top be stored in the collection.</param>
        /// <returns>
        /// The name / key value of the specified instance.
        /// </returns>
        /// <remarks>
        /// This is called from several methods, including the Add() method, to identify the instance
        /// being added.
        /// </remarks>
        protected override string GetName(AdaptiveTableProfile? item)
        {
            if (item == null)
                return string.Empty; 

            return item.TableName!;
        }
        /// <summary>
        /// Attempts to load the local table profile file, if present, and populates the collection with
        /// <see cref="ITableProfile" /> instances for each table in the provided collection, even if the local
        /// file does not contain an entry for each table.
        /// </summary>
        /// <param name="tableList">The <see cref="SqlTableCollection" /> containing the SQL table schema information for the tables
        /// being represented.</param>
        public void CreateContentForTables(SqlTableCollection? tableList)
        {
            // Load data, if present.
            Load();

            if (tableList != null)
            {
                foreach (SqlTable table in tableList)
                {
                    AdaptiveTableProfile? profile = this[table.TableName!];
                    if (profile != null)
                        profile.TableReference = table;
                    else
                    {
                        profile = new AdaptiveTableProfile(table);
                        Add(profile);
                    }
                }
            }
        }
        /// <summary>
        /// Attempts to load the local table profile file, if present, and populates the collection with
        /// <see cref="ITableProfile" /> instances for each table in the provided collection, even if the local
        /// file does not contain an entry for each table.
        /// </summary>
        /// <param name="tableList">The <see cref="SqlTableCollection" /> containing the SQL table schema information for the tables
        /// being represented.</param>
        /// <returns></returns>
        public async Task CreateContentForTablesAsync(SqlTableCollection? tableList)
        {
            // Load data, if present.
            await LoadAsync().ConfigureAwait(false);

            if (tableList != null)
            {
                foreach (SqlTable table in tableList)
                {
                    AdaptiveTableProfile? profile = this[table.TableName!];
                    if (profile != null)
                        profile.TableReference = table;
                    else
                    {
                        profile = new AdaptiveTableProfile(table);
                        Add(profile);
                    }
                }
            }
        }
        /// <summary>
        /// Gets the table profile instance for the specified table.
        /// </summary>
        /// <param name="tableName">
        /// A string containing the name of the table.
        /// </param>
        /// <returns>
        /// The <see cref="AdaptiveTableProfile"/> instnace for the specified table, or <b>null</b> if not present.
        /// </returns>
        public AdaptiveTableProfile? GetProfile(string? tableName)
        {
            AdaptiveTableProfile? profile = null;
            if (!string.IsNullOrEmpty(tableName) && Contains(tableName))
            {
                profile = this[tableName];
            }
            return profile;
        }
        /// <summary>
        /// Attempts to load the local table profile file.
        /// </summary>
        /// <returns>
        ///   <b>true</b> if the load operation is successful; otherwise, returns <b>false</b>.
        /// </returns>
        public bool Load()
        {
            bool success = false;
            if (!string.IsNullOrEmpty(_fileName))
            {
                if (File.Exists(_fileName))
                {
                    FileStream? inStream = null;
                    try
                    {
                        inStream = new FileStream(_fileName, FileMode.Open, FileAccess.Read);  
                    }
                    catch(Exception ex)
                    {
                        ExceptionLog.LogException(ex);
                    }

                    if (inStream != null)
                    {
                        try
                        {
                            BinaryReader reader = new BinaryReader(inStream);
                            int length = reader.ReadInt32();
                            for (int count = 0; count < length; count++)
                            {
                                AdaptiveTableProfile profile = new AdaptiveTableProfile();
                                profile.Load(reader);
                                Add(profile);
                            }
                            reader.Close();
                            reader.Dispose();
                            success = true;
                        }
                        catch (Exception ex)
                        {
                            ExceptionLog.LogException(ex);
                        }
                        inStream.Close();
                        inStream.Dispose();
                    }
                }
            }
            return success;
        }
        /// <summary>
        /// Attempts to load the local table profile file.
        /// </summary>
        /// <returns>
        ///   <b>true</b> if the load operation is successful; otherwise, returns <b>false</b>.
        /// </returns>
        public async Task<bool> LoadAsync()
        {
            bool success = false;

            await Task.Yield();

            if (!string.IsNullOrEmpty(_fileName))
            {
                if (File.Exists(_fileName))
                {
                    FileStream? inStream = null;
                    try
                    {
                        inStream = new FileStream(_fileName, FileMode.Open, FileAccess.Read);
                    }
                    catch (Exception ex)
                    {
                        ExceptionLog.LogException(ex);
                    }

                    if (inStream != null)
                    {
                        try
                        {
                            BinaryReader reader = new BinaryReader(inStream);
                            int length = reader.ReadInt32();
                            for (int count = 0; count < length; count++)
                            {
                                AdaptiveTableProfile profile = new AdaptiveTableProfile();
                                profile.Load(reader);
                                Add(profile);
                            }
                            reader.Close();
                            reader.Dispose();
                            success = true;
                        }
                        catch (Exception ex)
                        {
                            ExceptionLog.LogException(ex);
                        }
                        inStream.Close();
                        inStream.Dispose();
                    }
                }
            }
            return success;
        }
        /// <summary>
        /// Attempts to save the content of the collection to a local table profile file.
        /// </summary>
        /// <returns>
        ///   <b>true</b> if the save operation is successful; otherwise, returns <b>false</b>.
        /// </returns>
        public bool Save()
        {
            bool success = false;

            if (!string.IsNullOrEmpty(_fileName))
            {
                if (File.Exists(_fileName))
                    SafeIO.DeleteFile(_fileName);

                FileStream? outStream = null;
                try
                {
                    outStream = new FileStream(_fileName, FileMode.CreateNew, FileAccess.Write);
                }
                catch(Exception ex)
                {
                    ExceptionLog.LogException(ex);
                }
                if (outStream != null)
                {
                    try
                    {
                        BinaryWriter writer = new BinaryWriter(outStream);
                        writer.Write(Count);
                        foreach (AdaptiveTableProfile profile in this)
                        {
                            profile.Save(writer);
                            writer.Flush();
                        }
                        writer.Close();
                        writer.Dispose();
                        success = true;
                    }
                    catch (Exception ex)
                    {
                        ExceptionLog.LogException(ex);
                    }
                    outStream.Close();
                    outStream.Dispose();
                }
            }
            return success;
        }
        /// <summary>
        /// Attempts to save the content of the collection to a local table profile file.
        /// </summary>
        /// <returns>
        ///   <b>true</b> if the save operation is successful; otherwise, returns <b>false</b>.
        /// </returns>
        public async Task<bool> SaveAsync()
        {
            bool success = false;

            await Task.Yield();
            if (!string.IsNullOrEmpty(_fileName))
            {
                if (File.Exists(_fileName))
                    SafeIO.DeleteFile(_fileName);

                FileStream? outStream = null;
                try
                {
                    outStream = new FileStream(_fileName, FileMode.CreateNew, FileAccess.Write);
                }
                catch (Exception ex)
                {
                    ExceptionLog.LogException(ex);
                }
                if (outStream != null)
                {
                    try
                    {
                        BinaryWriter writer = new BinaryWriter(outStream);
                        writer.Write(Count);
                        foreach (AdaptiveTableProfile profile in this)
                        {
                            profile.Save(writer);
                            writer.Flush();
                        }
                        writer.Close();
                        writer.Dispose();
                        success = true;
                    }
                    catch (Exception ex)
                    {
                        ExceptionLog.LogException(ex);
                    }
                    outStream.Close();
                    outStream.Dispose();
                }
            }
            return success;
        }

    }
}