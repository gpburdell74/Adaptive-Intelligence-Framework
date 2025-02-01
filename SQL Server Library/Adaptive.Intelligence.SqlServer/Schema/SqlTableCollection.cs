using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.Shared.Logging;

namespace Adaptive.Intelligence.SqlServer.Schema
{
    /// <summary>
    /// Contains a list of <see cref="SqlTable"/> instances.
    /// </summary>
    public sealed class SqlTableCollection : NameIndexCollection<SqlTable>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlTableCollection"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public SqlTableCollection()
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlTableCollection"/> class.
        /// </summary>
        /// <param name="sourceList">
        /// An <see cref="IEnumerable{T}"/> of <see cref="SqlTable"/> list used to populate the collection.
        /// </param>
        public SqlTableCollection(IEnumerable<SqlTable> sourceList)
        {
            AddRange(sourceList);
        }
        #endregion

        #region Public Properties        
        /// <summary>
        /// Gets the <see cref="SqlTable"/> with the specified table name.
        /// </summary>
        /// <value>
        /// The <see cref="SqlTable"/>.
        /// </value>
        /// <param name="tableName">
        /// A string containing the name of the table.</param>
        /// <returns>
        /// The <see cref="SqlTable"/> instance, if found; otherwise, returns <b>null</b>.
        /// </returns>
        public new SqlTable? this[string tableName]
        {
            get
            {
                SqlTable? item = null;

                try
                {
                    item = base[tableName];
                }
                catch (Exception ex)
                {
                    ExceptionLog.LogException(ex);
                }

                if (item == null)
                    item = HeuristicFind(tableName);
                return item;
            }
        }
        #endregion

        #region Protected Method Overrides        
        /// <summary>
        /// Gets the name / key value of the specified instance.
        /// </summary>
        /// <param name="item">
        /// The <see cref="SqlTable"/> item to be stored in the collection.</param>
        /// <returns>
        /// The name / key value of the specified instance.
        /// </returns>
        /// <remarks>
        /// This is called from several methods, including the Add() method, to identify the instance
        /// being added.
        /// </remarks>
        protected override string GetName(SqlTable item)
        {
            return item.TableName!;
        }
        #endregion
        /// <summary>
        /// Attempts to find the SQL table that is closest to the provided name value.
        /// </summary>
        /// <param name="candidateName">
        /// A string containing the whole or partial name of the table to find.
        /// </param>
        /// <returns>
        /// A <see cref="SqlTable"/> whose name is the closest match, if possible; otherwise, 
        /// returns <b>null</b>.
        /// </returns>
        public SqlTable? HeuristicFind(string candidateName)
        {
            List<TableNameComparisonResult> list = new List<TableNameComparisonResult>();
            SqlTable? tableRef = null;

            string originalCandidate = candidateName;
            candidateName = candidateName.ToLower();
            foreach (SqlTable table in this)
            {
                TableNameComparisonResult rs = new TableNameComparisonResult();
                string compName = table.TableName!.ToLower();

                if (compName == candidateName)
                {
                    rs.Sureness = 100;
                    rs.Table = table;
                    rs.CandidateValue = candidateName;
                    rs.ComparedTo = compName;
                    list.Add(rs);
                }
                else if (compName.StartsWith(candidateName))
                {
                    rs.Sureness = 85;
                    rs.Table = table;
                    rs.CandidateValue = candidateName;
                    rs.ComparedTo = compName;
                    list.Add(rs);
                }
                else if (compName.Contains(candidateName))
                {
                    rs.Sureness = 75;
                    rs.Table = table;
                    rs.CandidateValue = candidateName;
                    rs.ComparedTo = compName;
                    list.Add(rs);
                }
                else
                {
                    bool done = false;
                    int sureVal = 70;
                    int stopLen = compName.Length / 2;
                    while (candidateName.Length > stopLen && !done)
                    {
                        candidateName = candidateName.Substring(1, candidateName.Length - 1);
                        if (compName.Contains(candidateName))
                        {
                            rs.Sureness = sureVal;
                            rs.Table = table;
                            rs.CandidateValue = originalCandidate;
                            rs.ComparedTo = compName;
                            list.Add(rs);
                            done = true;
                        }
                        sureVal -= 6;
                    }
                    candidateName = originalCandidate;
                }
            }

            if (list.Count > 0)
            {
                foreach (TableNameComparisonResult item in list)
                {
                    item.Adjust();
                }
                list = list.OrderByDescending(x => x.Sureness).ToList();
                tableRef = list[0].Table;
            }

            return tableRef;
        }
    }
}
