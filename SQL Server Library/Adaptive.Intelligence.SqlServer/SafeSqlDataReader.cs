// Ignore Spelling: Sql Nullable

using Adaptive.Intelligence.Shared;
using Adaptive.SqlServer.Client;
using Microsoft.Data.SqlClient;

namespace Adaptive.Intelligence.SqlServer
{
    /// <summary>
    /// Provides a wrapper for the <see cref="SqlDataReader"/> class.  This is used to simplify the
    /// reading of data fields.
    /// </summary>
    /// <remarks>
    /// Provides a way of reading a forward-only stream of rows from a SQL Server database.
    /// </remarks>
    public sealed class SafeSqlDataReader : ExceptionTrackingBase, ISafeSqlDataReader
    {
        #region Private Member Declarations
        /// <summary>
        /// The associated command instance, if provided.
        /// </summary>
        private SqlCommand? _command;
        /// <summary>
        /// The reader instance.
        /// </summary>
        private SqlDataReader? _reader;
        #endregion

        #region Constructor / Dispose Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="SafeSqlDataReader"/> class.
        /// </summary>
        /// <param name="reader">
        /// The source <see cref="SqlDataReader"/> instance.
        /// </param>
        public SafeSqlDataReader(SqlDataReader reader)
        {
            _reader = reader;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SafeSqlDataReader"/> class.
        /// </summary>
        /// <param name="reader">
        /// The source <see cref="SqlDataReader"/> instance.
        /// </param>
        /// <param name="command">
        /// The associated <see cref="SqlCommand"/> instance.
        /// </param>
        public SafeSqlDataReader(SqlDataReader reader, SqlCommand command)
        {
            _reader = reader;
            _command = command;
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed && disposing)
            {
                Close();
            }

            _reader = null;
            _command = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the number of columns in the current row.
        /// </summary>
        /// <value>
        /// When not positioned in a valid record set, 0; otherwise the number of columns in the current row. The default is -1.
        /// </value>
        public int FieldCount
        {
            get
            {
                if (_reader == null)
                {
                    return 0;
                }
                else
                {
                    return _reader.FieldCount;
                }
            }
        }
        /// <summary>
        /// Gets a value that indicates whether the <see cref="SafeSqlDataReader"/>
        /// contains one or more rows.
        /// </summary>
        /// <value>
        /// <b>true</b> if the <see cref="SafeSqlDataReader"/> contains one or more rows;
        /// otherwise <b>false</b>.
        /// </value>
        public bool HasData => HasRows;
        /// <summary>
        /// Gets a value that indicates whether the <see cref="SafeSqlDataReader"/>
        /// contains one or more rows.
        /// </summary>
        /// <value>
        /// <b>true</b> if the <see cref="SafeSqlDataReader"/> contains one or more rows;
        /// otherwise <b>false</b>.
        /// </value>
        public bool HasRows
        {
            get
            {
                if (_reader == null)
                {
                    return false;
                }
                else
                {
                    return _reader.HasRows;
                }
            }
        }
        /// <summary>
        /// Gets a value indicating whether this the <see cref="SafeSqlDataReader"/> is closed.
        /// </summary>
        /// <remarks>
        /// It is not possible to read from a <see cref="SafeSqlDataReader"/> instance
        /// that is closed.
        /// </remarks>
        /// <value>
        /// <b>true</b> if the <see cref="SafeSqlDataReader"/> is closed;
        /// otherwise, <b>false</b>.
        /// </value>
        public bool IsClosed
        {
            get
            {
                if (_reader == null)
                {
                    return true;
                }
                else
                {
                    return _reader.IsClosed;
                }
            }
        }
        /// <summary>
        /// Gets the value of the specified column in its native format given the column name.
        /// </summary>
        /// <param name="name">
        /// A string containing the column name.
        /// </param>
        /// <returns>
        /// The value of the specified column in its native format.
        /// </returns>
        public object? this[string name]
        {
            get
            {
                object? returnValue = null;

                if (_reader != null)
                {
                    try
                    {
                        returnValue = _reader[name];
                    }
                    catch (Exception ex)
                    {
                        AddException(ex);
                    }
                }
                return returnValue;
            }
        }
        #endregion

        #region Public Methods / Functions
        /// <summary>
        /// Closes the <see cref="SafeSqlDataReader"/> object.
        /// </summary>
        /// <remarks>
        /// You must explicitly call the <see cref="Close"/> method when you are through using
        /// the <see cref="SafeSqlDataReader"/> to use the associated <see cref="SqlConnection"/>
        /// for any other purpose.
        ///
        /// The <b>Close</b> method disposes of the underlying
        /// <see cref="SqlDataReader"/> instance.
        /// </remarks>
        public void Close()
        {
            SqlConnection? underlyingConnection = null;

            if (_command != null)
            {
                // Capture the connection object for later closing.
                underlyingConnection = _command.Connection;

                // Cancel any existing operations.
                try
                {
                    _command.Cancel();
                }
                catch (Exception ex)
                {
                    AddException(ex);
                }

                // Release the reference to the connection.
                if (_command != null)
                {
                    _command.Connection = null;
                }
            }

            if (_reader != null)
            {
                // Close first...
                try
                {
                    if (!_reader.IsClosed)
                    {
                        _reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    AddException(ex);
                }

                // Then dispose....
                try
                {
                    _reader.Dispose();
                }
                catch (Exception ex)
                {
                    AddException(ex);
                }
                _reader = null;
            }

            // Ensure the connection is closed.
            underlyingConnection?.Dispose();

            // Ensure the command object is disposed.
            _command?.Dispose();
            _command = null;
        }
        /// <summary>
        /// Gets the value of the specified column as a boolean.
        /// </summary>
        /// <param name="index">
        /// The zero-based column ordinal value.
        /// </param>
        /// <returns>
        /// The value of the column as a boolean, or <b>false</b> if the
        /// operation fails.
        /// </returns>
        public bool GetBoolean(int index)
        {
            bool returnValue = false;

            if ((_reader != null) && (index > -1))
            {
                try
                {
                    if (!_reader.IsDBNull(index))
                    {
                        returnValue = _reader.GetBoolean(index);
                    }
                }
                catch (Exception ex) { AddException(ex); }
            }
            return returnValue;
        }
        /// <summary>
        /// Gets the value of the specified column as a boolean.
        /// </summary>
        /// <param name="columnName">
        /// The name of the column to be read.
        /// </param>
        /// <returns>
        /// The value of the column as a boolean, or <b>false</b> if the
        /// operation fails.
        /// </returns>
        public bool GetBoolean(string columnName)
        {
            bool returnValue = false;

            if (_reader != null)
            {
                returnValue = GetBoolean(GetOrdinal(columnName));
            }
            return returnValue;
        }
        /// <summary>
        /// Gets the value of the specified column as a byte.
        /// </summary>
        /// <param name="index">
        /// The zero-based column ordinal value.
        /// </param>
        /// <returns>
        /// The value of the column as a byte, or <b>0</b> if the
        /// operation fails.
        /// </returns>
        public byte GetByte(int index)
        {
            byte returnValue = 0;

            if ((_reader != null) && (index > -1))
            {
                try
                {
                    if (_reader.IsDBNull(index))
                    {
                        returnValue = 0;
                    }
                    else
                    {
                        returnValue = _reader.GetByte(index);
                    }
                }
                catch (Exception ex) { AddException(ex); }
            }
            return returnValue;
        }
        /// <summary>
        /// Gets the value of the specified column as a byte.
        /// </summary>
        /// <param name="columnName">
        /// The name of the column to be read.
        /// </param>
        /// <returns>
        /// The value of the column as a byte, or <b>false</b> if the
        /// operation fails.
        /// </returns>
        public byte GetByte(string columnName)
        {
            byte returnValue = 0;

            if (_reader != null)
            {
                returnValue = GetByte(GetOrdinal(columnName));
            }
            return returnValue;
        }
        /// <summary>
        /// Gets the value of the specified column as a character.
        /// </summary>
        /// <param name="index">
        /// The zero-based column ordinal value.
        /// </param>
        /// <returns>
        /// The value of the column as a boolean, or the null character if the
        /// operation fails.
        /// </returns>
        public char GetChar(int index)
        {
            char returnValue = '\0';

            if ((_reader != null) && (index > -1))
            {
                try
                {
                    if (_reader.IsDBNull(index))
                    {
                        returnValue = '\0';
                    }
                    else
                    {
                        returnValue = _reader.GetChar(index);
                    }
                }
                catch (Exception ex) { AddException(ex); }
            }
            return returnValue;
        }
        /// <summary>
        /// Gets the value of the specified column as a character.
        /// </summary>
        /// <param name="columnName">
        /// The name of the column to be read.
        /// </param>
        /// <returns>
        /// The value of the column as a character, or the null character if the
        /// operation fails.
        /// </returns>
        public char GetChar(string columnName)
        {
            char returnValue = '\0';

            if (_reader != null)
            {
                returnValue = GetChar(GetOrdinal(columnName));
            }
            return returnValue;
        }
        /// <summary>
        /// Gets the name of the column at the specified index.
        /// </summary>
        /// <param name="index">The ordinal index of the column.</param>
        /// <returns>
        /// The name of the column, or <b>null</b>.
        /// </returns>
        public string? GetColumnName(int index)
        {
            string? columnName = null;

            if (_reader != null)
            {
                try
                {
                    columnName = _reader.GetName(index);
                }
                catch (Exception ex) { AddException(ex); }
            }
            return columnName;

        }
        /// <summary>
        /// Gets the value of the specified column as a Date/Time.
        /// </summary>
        /// <param name="index">
        /// The zero-based column ordinal value.
        /// </param>
        /// <returns>
        /// The value of the column as a <see cref="DateTime"/>, or <see cref="DateTime.MinValue"/> if the
        /// operation fails.
        /// </returns>
        public DateTime GetDate(int index)
        {
            DateTime returnValue = DateTime.MinValue;

            if ((_reader != null) && (index > -1))
            {
                try
                {
                    if (!_reader.IsDBNull(index))
                    {
                        returnValue = _reader.GetDateTime(index).Date;
                    }
                }
                catch (Exception ex) { AddException(ex); }
            }
            return returnValue;
        }
        /// <summary>
        /// Gets the value of the specified column as a Date/Time.
        /// </summary>
        /// <param name="index">
        /// The zero-based column ordinal value.
        /// </param>
        /// <returns>
        /// The value of the column as a <see cref="DateTime"/>, or <b>null</b>.
        /// </returns>
        public DateTime? GetDateNullable(int index)
        {
            DateTime? returnValue = null;

            if ((_reader != null) && (index > -1))
            {
                try
                {
                    if (!_reader.IsDBNull(index))
                    {
                        returnValue = _reader.GetDateTime(index).Date;
                    }
                }
                catch (Exception ex) { AddException(ex); }
            }
            return returnValue;
        }
        /// <summary>
        /// Gets the value of the specified column as a Date/Time.
        /// </summary>
        /// <param name="index">
        /// The zero-based column ordinal value.
        /// </param>
        /// <returns>
        /// The value of the column as a <see cref="DateTime"/>, or <see cref="DateTime.MinValue"/> if the
        /// operation fails.
        /// </returns>
        public DateTime GetDateTime(int index)
        {
            DateTime returnValue = DateTime.MinValue;

            if ((_reader != null) && (index > -1))
            {
                try
                {
                    if (!_reader.IsDBNull(index))
                    {
                        returnValue = _reader.GetDateTime(index);
                    }
                }
                catch (Exception ex) { AddException(ex); }
            }
            return returnValue;
        }
        /// <summary>
        /// Gets the value of the specified column as a Date/Time.
        /// </summary>
        /// <param name="columnName">
        /// The name of the column to be read.
        /// </param>
        /// <returns>
        /// The value of the column as a <see cref="DateTime"/>, or <see cref="DateTime.MinValue"/> if the
        /// operation fails.
        /// </returns>
        public DateTime GetDateTime(string columnName)
        {
            DateTime returnValue = DateTime.MinValue;

            if (_reader != null)
            {
                returnValue = GetDateTime(GetOrdinal(columnName));
            }
            return returnValue;
        }

        /// <summary>
        /// Gets the value of the specified column as a Date/Time.
        /// </summary>
        /// <param name="index">
        /// The zero-based column ordinal value.
        /// </param>
        /// <returns>
        /// The value of the column as a nullable <see cref="DateTime"/>, or <b>null</b>.
        /// </returns>
        public DateTime? GetDateTimeNullable(int index)
        {
            DateTime? returnValue = null;

            if ((_reader != null) && (index > -1))
            {
                try
                {
                    if (!_reader.IsDBNull(index))
                    {
                        returnValue = _reader.GetDateTime(index);
                    }
                }
                catch (Exception ex) { AddException(ex); }
            }
            return returnValue;
        }
        /// <summary>
        /// Gets the value of the specified column as a Date/Time.
        /// </summary>
        /// <param name="columnName">
        /// The name of the column to be read.
        /// </param>
        /// <returns>
        /// The value of the column as a <see cref="DateTime"/>, or <see cref="DateTime.MinValue"/> if the
        /// operation fails.
        /// </returns>
        public DateTime? GetDateTimeNullable(string columnName)
        {
            DateTime? returnValue = null;

            if (_reader != null)
            {
                returnValue = GetDateTimeNullable(GetOrdinal(columnName));
            }
            return returnValue;
        }
        /// <summary>
        /// Gets the value of the specified column as a Date/Time offset.
        /// </summary>
        /// <param name="index">
        /// The zero-based column ordinal value.
        /// </param>
        /// <returns>
        /// The value of the column as a <see cref="DateTimeOffset"/>, or <see cref="DateTimeOffset.MinValue"/> if the
        /// operation fails.
        /// </returns>
        public DateTimeOffset GetDateTimeOffset(int index)
        {
            DateTimeOffset returnValue = DateTimeOffset.MinValue;

            if ((_reader != null) && (index > -1))
            {
                try
                {
                    if (_reader.IsDBNull(index))
                    {
                        returnValue = DateTimeOffset.MinValue;
                    }
                    else
                    {
                        returnValue = (_reader).GetDateTimeOffset(index);
                    }
                }
                catch (Exception ex) { AddException(ex); }
            }
            return returnValue;
        }
        /// <summary>
        /// Gets the value of the specified column as a Date/Time offset.
        /// </summary>
        /// <param name="columnName">
        /// The name of the column to be read.
        /// </param>
        /// <returns>
        /// The value of the column as a <see cref="DateTimeOffset"/>, or <see cref="DateTime.MinValue"/> if the
        /// operation fails.
        /// </returns>
        public DateTimeOffset GetDateTimeOffset(string columnName)
        {
            DateTimeOffset returnValue = DateTimeOffset.MinValue;

            if (_reader != null)
            {
                returnValue = GetDateTimeOffset(GetOrdinal(columnName));
            }
            return returnValue;
        }

        /// <summary>
        /// Gets the value of the specified column as a nullable Date/Time offset.
        /// </summary>
        /// <param name="index">
        /// The zero-based column ordinal value.
        /// </param>
        /// <returns>
        /// The value of the column as a <see cref="DateTimeOffset"/>, or <see cref="DateTimeOffset.MinValue"/> if the
        /// operation fails.
        /// </returns>
        public DateTimeOffset? GetDateTimeOffsetNullable(int index)
        {
            DateTimeOffset? returnValue = null;

            if ((_reader != null) && (index > -1))
            {
                try
                {
                    if (!_reader.IsDBNull(index))
                    {
                        returnValue = (_reader).GetDateTimeOffset(index);
                    }
                }
                catch (Exception ex) { AddException(ex); }
            }
            return returnValue;
        }
        /// <summary>
        /// Gets the value of the specified column as a nullable Date/Time offset.
        /// </summary>
        /// <param name="columnName">
        /// A string containing the name of the column whose data is to be read.
        /// </param>
        /// <returns>
        /// The value of the column as a <see cref="DateTimeOffset"/>, or <see cref="DateTimeOffset.MinValue"/> if the
        /// operation fails.
        /// </returns>
        public DateTimeOffset? GetDateTimeOffsetNullable(string columnName)
        {
            DateTimeOffset? returnValue = null;

            if (_reader != null)
            {
                returnValue = GetDateTimeOffsetNullable(GetOrdinal(columnName));
            }
            return returnValue;
        }
        /// <summary>
        /// Gets the value of the specified column as a double.
        /// </summary>
        /// <param name="index">
        /// The zero-based column ordinal value.
        /// </param>
        /// <returns>
        /// The value of the column as a double, or <b>0</b> if the
        /// operation fails.
        /// </returns>
        public decimal GetDecimal(int index)
        {
            decimal returnValue = 0;

            if ((_reader != null) && (index > -1))
            {
                try
                {
                    if (_reader.IsDBNull(index))
                    {
                        returnValue = 0;
                    }
                    else
                    {
                        returnValue = _reader.GetDecimal(index);
                    }
                }
                catch (Exception ex) { AddException(ex); }
            }
            return returnValue;
        }

        /// <summary>
        /// Gets the value of the specified column as a double.
        /// </summary>
        /// <param name="columnName">
        /// A string containing the name of the column whose data is to be read.
        /// </param>
        /// <returns>
        /// The value of the column as a double, or <b>0</b> if the
        /// operation fails.
        /// </returns>
        public decimal GetDecimal(string columnName)
        {
            decimal returnValue = 0;

            if (_reader != null)
            {
                returnValue = _reader.GetDecimal(GetOrdinal(columnName));
            }

            return returnValue;
        }

        /// <summary>
        /// Gets the SQL decimal value of the specified column as a double.
        /// </summary>
        /// <param name="index">
        /// The zero-based column ordinal value.
        /// </param>
        /// <returns>
        /// The value of the column as a double, or <b>0</b> if the
        /// operation fails.
        /// </returns>
        public double GetDecimalAsDouble(int index)
        {
            double returnValue = 0;

            if ((_reader != null) && (index > -1))
            {
                try
                {
                    if (_reader.IsDBNull(index))
                    {
                        returnValue = 0;
                    }
                    else
                    {
                        returnValue = (double)_reader.GetDecimal(index);
                    }
                }
                catch (Exception ex) { AddException(ex); }
            }
            return returnValue;
        }
        /// <summary>
        /// Gets the SQL decimal value of the specified column as a single.
        /// </summary>
        /// <param name="index">
        /// The zero-based column ordinal value.
        /// </param>
        /// <returns>
        /// The value of the column as a single, or <b>0</b> if the
        /// operation fails.
        /// </returns>
        public float GetDecimalAsSingle(int index)
        {
            float returnValue = 0;

            if ((_reader != null) && (index > -1))
            {
                try
                {
                    if (_reader.IsDBNull(index))
                    {
                        returnValue = 0;
                    }
                    else
                    {
                        returnValue = (float)_reader.GetDecimal(index);
                    }
                }
                catch (Exception ex) { AddException(ex); }
            }
            return returnValue;
        }

        /// <summary>
        /// Gets the SQL decimal value of the specified column as a float.
        /// </summary>
        /// <param name="columnName">
        /// A string containing the name of the column whose data is to be read.
        /// </param>
        /// <returns>
        /// The SQL Decimal value of the column as a float, or <b>0</b> if the
        /// operation fails.
        /// </returns>
        public float GetDecimalAsSingle(string columnName)
        {
            float returnValue = 0;

            if (_reader != null)
            {
                returnValue = GetDecimalAsSingle(GetOrdinal(columnName));
            }

            return returnValue;
        }

        /// <summary>
        /// Gets the value of the specified column as a double.
        /// </summary>
        /// <param name="index">
        /// The zero-based column ordinal value.
        /// </param>
        /// <returns>
        /// The value of the column as a double, or <b>0</b> if the
        /// operation fails.
        /// </returns>
        public double GetDouble(int index)
        {
            double returnValue = 0;

            if ((_reader != null) && (index > -1))
            {
                try
                {
                    if (_reader.IsDBNull(index))
                    {
                        returnValue = 0;
                    }
                    else
                    {
                        returnValue = _reader.GetDouble(index);
                    }
                }
                catch (Exception ex) { AddException(ex); }
            }
            return returnValue;
        }
        /// <summary>
        /// Gets the value of the specified column as a double.
        /// </summary>
        /// <param name="columnName">
        /// The name of the column to be read.
        /// </param>
        /// <returns>
        /// The value of the column as a double, or <b>0</b> if the
        /// operation fails.
        /// </returns>
        public double GetDouble(string columnName)
        {
            double returnValue = 0;

            if (_reader != null)
            {
                returnValue = GetDouble(GetOrdinal(columnName));
            }
            return returnValue;
        }
        /// <summary>
        /// Gets the list of field names in sequential order.
        /// </summary>
        /// <returns>
        /// A <see cref="List{T}"/> of <see cref="string"/> containing the field names.
        /// </returns>
        public List<string> GetFieldNames()
        {
            List<string> list = new List<string>(30);

            if (_reader != null)
            {
                int length = _reader.FieldCount;
                for (int count = 0; count < length; count++)
                {
                    string? name = GetColumnName(count);
                    if (!string.IsNullOrEmpty(name))
                    {
                        list.Add(name);
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// Gets the value of the specified column as a float.
        /// </summary>
        /// <param name="index">
        /// The zero-based column ordinal value.
        /// </param>
        /// <returns>
        /// The value of the column as a float, or <b>0</b> if the
        /// operation fails.
        /// </returns>
        public float GetFloat(int index)
        {
            float returnValue = 0;

            if ((_reader != null) && (index > -1))
            {
                try
                {
                    if (_reader.IsDBNull(index))
                    {
                        returnValue = 0;
                    }
                    else
                    {
                        returnValue = _reader.GetFloat(index);
                    }
                }
                catch (Exception ex) { AddException(ex); }
            }
            return returnValue;
        }
        /// <summary>
        /// Gets the value of the specified column as a float.
        /// </summary>
        /// <param name="columnName">
        /// The name of the column to be read.
        /// </param>
        /// <returns>
        /// The value of the column as a boolean, or <b>0</b> if the
        /// operation fails.
        /// </returns>
        public float GetFloat(string columnName)
        {
            float returnValue = 0;

            if (_reader != null)
            {
                returnValue = GetFloat(GetOrdinal(columnName));
            }
            return returnValue;
        }

        /// <summary>
        /// Gets the value of the specified column as a <see cref="Guid" />.
        /// </summary>
        /// <param name="index">The zero-based column ordinal value.</param>
        /// <returns>
        /// The value of the column as a <see cref="Guid" />, or <b>0</b> if the
        /// operation fails.
        /// </returns>
        public Guid GetGuid(int index)
        {
            Guid returnValue = Guid.Empty;

            if ((_reader != null) && (index > -1))
            {
                try
                {
                    if (_reader.IsDBNull(index))
                    {
                        returnValue = Guid.Empty;
                    }
                    else
                    {
                        returnValue = _reader.GetGuid(index);
                    }
                }
                catch (Exception ex) { AddException(ex); }
            }
            return returnValue;
        }
        /// <summary>
        /// Gets the value of the specified column as a <see cref="Guid" />.
        /// </summary>
        /// <param name="columnName">A string containing the name of the column whose data is to be read.</param>
        /// <returns>
        /// The value of the column as a <see cref="Guid" />, or <b>0</b> if the
        /// operation fails.
        /// </returns>
        public Guid GetGuid(string columnName)
        {
            Guid returnValue = Guid.Empty;

            if (_reader != null)
            {
                returnValue = GetGuid(GetOrdinal(columnName));
            }
            return returnValue;
        }

        /// <summary>
        /// Gets the value of the specified column as a <see cref="Guid" />.
        /// </summary>
        /// <param name="index">The zero-based column ordinal value.</param>
        /// <returns>
        /// The value of the column as a <see cref="Guid" />, or <b>0</b> if the
        /// operation fails.
        /// </returns>
        public Guid? GetGuidNullable(int index)
        {
            Guid? returnValue = null;

            if ((_reader != null) && (index > -1))
            {
                try
                {
                    if (_reader.IsDBNull(index))
                    {
                        returnValue = null;
                    }
                    else
                    {
                        returnValue = _reader.GetGuid(index);
                    }
                }
                catch (Exception ex) { AddException(ex); }
            }
            return returnValue;
        }
        /// <summary>
        /// Gets the value of the specified column as an integer.
        /// </summary>
        /// <param name="index">
        /// The zero-based column ordinal value.
        /// </param>
        /// <returns>
        /// The value of the column as a short integer, or <b>0</b> if the
        /// operation fails.
        /// </returns>
        public short GetInt16(int index)
        {
            short returnValue = 0;

            if ((_reader != null) && (index > -1))
            {
                try
                {
                    if (_reader.IsDBNull(index))
                    {
                        returnValue = 0;
                    }
                    else
                    {
                        returnValue = _reader.GetInt16(index);
                    }
                }
                catch (Exception ex) { AddException(ex); }
            }
            return returnValue;
        }
        /// <summary>
        /// Gets the value of the specified column as an integer.
        /// </summary>
        /// <param name="columnName">
        /// The name of the column to be read.
        /// </param>
        /// <returns>
        /// The value of the column as a short integer, or <b>0</b> if the
        /// operation fails.
        /// </returns>
        public short GetInt16(string columnName)
        {
            short returnValue = 0;

            if (_reader != null)
            {
                returnValue = GetInt16(GetOrdinal(columnName));
            }
            return returnValue;
        }
        /// <summary>
        /// Gets the value of the specified column as an integer.
        /// </summary>
        /// <param name="index">
        /// The zero-based column ordinal value.
        /// </param>
        /// <returns>
        /// The value of the column as an integer, or <b>0</b> if the
        /// operation fails.
        /// </returns>
        public int GetInt32(int index)
        {
            int returnValue = 0;

            if ((_reader != null) && (index > -1))
            {
                try
                {
                    if (!_reader.IsDBNull(index))
                    {
                        returnValue = _reader.GetInt32(index);
                    }
                }
                catch (Exception ex) { AddException(ex); }
            }
            return returnValue;
        }
        /// <summary>
        /// Gets the value of the specified column as an integer.
        /// </summary>
        /// <param name="columnName">
        /// The name of the column to be read.
        /// </param>
        /// <returns>
        /// The value of the column as an integer, or <b>0</b> if the
        /// operation fails.
        /// </returns>
        public int GetInt32(string columnName)
        {
            int returnValue = 0;

            if (_reader != null)
            {
                returnValue = GetInt32(GetOrdinal(columnName));
            }
            return returnValue;
        }
        /// <summary>
        /// Gets the value of the specified column as an integer.
        /// </summary>
        /// <param name="index">
        /// The zero-based column ordinal value.
        /// </param>
        /// <returns>
        /// The value of the column as an integer, or <b>0</b> if the
        /// operation fails.
        /// </returns>
        public int? GetInt32Nullable(int index)
        {
            int? returnValue = 0;

            if ((_reader != null) && (index > -1))
            {
                try
                {
                    if (!_reader.IsDBNull(index))
                    {
                        returnValue = _reader.GetInt32(index);
                    }
                }
                catch (Exception ex) { AddException(ex); }
            }
            return returnValue;
        }
        /// <summary>
        /// Gets the value of the specified column as an integer.
        /// </summary>
        /// <param name="columnName">
        /// The name of the column to be read.
        /// </param>
        /// <returns>
        /// The value of the column as an integer, or <b>0</b> if the
        /// operation fails.
        /// </returns>
        public int? GetInt32Nullable(string columnName)
        {
            int? returnValue = 0;

            if (_reader != null)
            {
                returnValue = GetInt32(GetOrdinal(columnName));
            }
            return returnValue;
        }
        /// <summary>
        /// Gets the value of the specified column as a long integer.
        /// </summary>
        /// <param name="index">
        /// The zero-based column ordinal value.
        /// </param>
        /// <returns>
        /// The value of the column as an integer, or <b>0</b> if the
        /// operation fails.
        /// </returns>
        public long GetInt64(int index)
        {
            long returnValue = 0;

            if ((_reader != null) && (index > -1))
            {
                try
                {
                    if (!_reader.IsDBNull(index))
                    {
                        returnValue = _reader.GetInt64(index);
                    }
                }
                catch (Exception ex) { AddException(ex); }
            }
            return returnValue;
        }
        /// <summary>
        /// Gets the value of the specified column as an integer.
        /// </summary>
        /// <param name="columnName">
        /// The name of the column to be read.
        /// </param>
        /// <returns>
        /// The value of the column as an integer, or <b>0</b> if the
        /// operation fails.
        /// </returns>
        public long GetInt64(string columnName)
        {
            long returnValue = 0;

            if (_reader != null)
            {
                returnValue = GetInt64(GetOrdinal(columnName));
            }
            return returnValue;
        }
        /// <summary>
        /// Gets the item at the specified index.
        /// </summary>
        /// <param name="index">
        /// An integer specifying the ordinal column index.
        /// </param>
        /// <returns>
        /// An <see cref="object"/> containing the boxed value of the cell in the row.
        /// </returns>
        public object? GetItem(int index)
        {
            object? returnValue = null;

            if ((_reader != null) && (index > -1))
            {
                try
                {
                    returnValue = _reader.GetValue(index);
                }
                catch (Exception ex)
                {
                    AddException(ex);
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Gets the item from the row for the column with the specified name.
        /// </summary>
        /// <param name="columnName">
        /// A string containing the column name.
        /// </param>
        /// <returns>
        /// An <see cref="object"/> containing the boxed value of the cell in the row.
        /// </returns>
        public object? GetItem(string columnName)
        {
            return GetItem(GetOrdinal(columnName));
        }
        /// <summary>
        /// Gets the value of the specified column as a string.
        /// </summary>
        /// <param name="index">
        /// The zero-based column ordinal value.
        /// </param>
        /// <returns>
        /// The value of the column as a string, or <b>null</b> if the
        /// operation fails.
        /// </returns>
        public string? GetString(int index)
        {
            string? returnValue = null;

            if ((_reader != null) && (index > -1))
            {
                try
                {
                    if (!_reader.IsDBNull(index))
                    {
                        returnValue = _reader.GetString(index);
                    }
                }
                catch (Exception ex) { AddException(ex); }
            }
            return returnValue;
        }
        /// <summary>
        /// Gets the value of the specified column as a string.
        /// </summary>
        /// <param name="columnName">
        /// The name of the column to be read.
        /// </param>
        /// <returns>
        /// The value of the column as a string, or <b>null</b> if the
        /// operation fails.
        /// </returns>
        public string? GetString(string columnName)
        {
            string? returnValue = null;

            if (_reader != null)
            {
                returnValue = GetString(GetOrdinal(columnName));
            }
            return returnValue;
        }

        /// <summary>
        /// Gets the value of the specified column as a string.
        /// </summary>
        /// <param name="index">
        /// The zero-based column ordinal value.
        /// </param>
        /// <returns>
        /// The value of the column as a string, or <b>null</b> if the
        /// operation fails.
        /// </returns>
        public string? GetStringUnsafe(int index)
        {
            if (_reader == null)
            {
                return null;
            }

            if (_reader.IsDBNull(index))
            {
                return null;
            }
            else
            {
                return _reader.GetString(index);
            }
        }
        /// <summary>
        /// Gets the values for the specified number of columns.
        /// </summary>
        /// <param name="numberOfExpectedColumns">
        /// An integer specifying the number of expected columns.
        /// </param>
        /// <returns>
        /// An array of objects containing the boxed values.
        /// </returns>
        public object[]? GetValues(int numberOfExpectedColumns)
        {
            object[]? values = null;
            if (_reader != null)
            {
                values = new object[numberOfExpectedColumns];
                _reader.GetProviderSpecificValues(values);
            }
            return values;
        }
        /// <summary>
        /// Gets a value that indicates whether the column contains non-existent or
        /// missing values.
        /// </summary>
        /// <param name="index">
        /// The zero-based column ordinal.
        /// </param>
        /// <returns>
        /// <b>true</b> if the specified column value is equivalent to DBNull;
        /// otherwise <b>false</b>.
        /// </returns>
        public bool IsDBNull(int index)
        {
            bool isNull = true;
            if ((_reader != null) && (index > -1))
            {
                try
                {
                    isNull = _reader.IsDBNull(index);
                }
                catch (Exception ex) { AddException(ex); }
            }
            return isNull;
        }
        /// <summary>
        /// Gets a value that indicates whether the column contains non-existent or
        /// missing values.
        /// </summary>
        /// <param name="columnName">
        /// The name of the column.
        /// </param>
        /// <returns>
        /// <b>true</b> if the specified column value is equivalent to DBNull;
        /// otherwise <b>false</b>.
        /// </returns>
        public bool IsDBNull(string? columnName)
        {
            bool isNull = true;
            if (_reader != null && !string.IsNullOrEmpty(columnName))
            {
                isNull = _reader.IsDBNull(GetOrdinal(columnName));
            }
            return isNull;
        }
        /// <summary>
        ///  Advances the reader to the next result when reading the results of a batch of
        ///  statements.
        /// </summary>
        /// <returns>
        /// <b>true</b> if there are more result sets; otherwise, <b>false</b>.
        /// </returns>
        public bool NextResult()
        {
            if (_reader == null)
            {
                return false;
            }
            else
            {
                return _reader.NextResult();
            }
        }
        /// <summary>
        /// Advances the <see cref="SafeSqlDataReader"/> to the next record.
        /// </summary>
        /// <returns>
        /// <b>true</b> if there are more rows; otherwise <b>false</b>.
        /// </returns>
        public bool Read()
        {
            bool result = false;
            if (_reader != null)
            {
                try
                {
                    result = _reader.Read();
                }
                catch (Exception ex) { AddException(ex); }
            }
            return result;
        }
        /// <summary>
        /// An asynchronous version of <see cref="Read"/>, which advances the reader to the next
        /// record in a result set. This method invokes ReadAsync
        /// </summary>
        /// <returns>
        /// <b>true</b> if there are more rows; otherwise <b>false</b>.
        /// </returns>
        public async Task<bool> ReadAsync()
        {
            bool success = false;

            if (_reader != null)
            {
                success = await _reader.ReadAsync().ConfigureAwait(false);
            }

            return success;
        }
        #endregion

        #region Private Methods / Functions
        /// <summary>
        /// Gets the column ordinal, given the name of the column.
        /// </summary>
        /// <param name="columnName">
        /// The name of the column.
        /// </param>
        /// <returns>
        /// The zero-based column ordinal; or -1 if the column does not exist
        /// or the operation fails.
        /// </returns>
        private int GetOrdinal(string columnName)
        {
            int result = -1;
            if ((!string.IsNullOrEmpty(columnName)) && (_reader != null))
            {
                try
                {
                    result = _reader.GetOrdinal(columnName);
                }
                catch (Exception ex) { AddException(ex); }
            }
            return result;
        }
        #endregion
    }
}
