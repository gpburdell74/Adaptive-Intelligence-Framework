using Adaptive.Intelligence.Shared.Logging;
using Adaptive.SqlServer.Client;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.TransactSql.ScriptDom;
using System.Collections.Specialized;
using System.Text;

namespace Adaptive.Intelligence.SqlServer.CodeDom
{
    /// <summary>
    /// Contains static methods and functions for performing Native (MS) T-SQL Code Dom and
    /// script generation operations.
    /// </summary>
    internal static class NativeTSqlCodeDom
    {
        #region Public Methods / Functions
        /// <summary>
        /// Parses the SQL text for errors.
        /// </summary>
        /// <param name="sqlQuery">
        /// A string containing the SQL text to be tested and parsed.
        /// </param>
        /// <returns>
        /// A <see cref="SqlQueryErrorCollection"/> containing the errors, if present;
        /// otherwise, returns <b>null</b>.  A <b>null</b> response indicates a successful
        /// parsing of the query.
        /// </returns>
        public static SqlQueryErrorCollection? ParseSql(string sqlQuery)
        {
            SqlQueryErrorCollection? errorList = null;

            StringReader reader = new StringReader(sqlQuery);
            TSql120Parser parser = new TSql120Parser(true);

            try
            {
                // Perform the parsing operation.
                parser.Parse(reader, out IList<ParseError> list);

                // Copy errors, if any are returned.
                if (list != null && list.Count > 0)
                {
                    errorList = new SqlQueryErrorCollection();
                    foreach (ParseError error in list)
                    {
                        SqlQueryError localError = new SqlQueryError
                        {
                            Column = error.Column,
                            LineNumber = error.Line,
                            Message = error.Message,
                            Number = error.Number,
                            Offset = error.Offset
                        };
                        errorList.Add(localError);
                    }
                }
            }
            catch (Exception ex)
            {
                errorList = new SqlQueryErrorCollection
                {
                    new SqlQueryError
                    {
                        Message = ex.Message,
                        Class = 30,
                        Source = "NativeTSqlCodeDom::ParseSql()"
                    }
                };
            }

            reader.Dispose();
            return errorList;
        }
        /// <summary>
        /// Gets the list of SQL statements from the provided query string.
        /// </summary>
        /// <param name="sqlQuery">
        /// A string containing the SQL query code.
        /// </param>
        /// <returns>
        /// A <see cref="List{T}"/> of <see cref="TSqlStatement"/> instances if successful. 
        /// Otherwise, returns <b>null</b>. 
        /// </returns>
        public static List<TSqlStatement>? ParseStatements(string sqlQuery)
        {
            List<TSqlStatement>? parsedStatements = null;

            // Create reader and parser instances.
            StringReader reader = new StringReader(sqlQuery);
            TSql120Parser parser = new TSql120Parser(true);

            TSqlFragment? sqlFragment = null;
            TSqlScript? sqlScript = null;

            // Try to parse the content.
            try
            {
                sqlFragment = parser.Parse(reader, out IList<ParseError> list);
                sqlScript = sqlFragment as TSqlScript;
            }
            catch (Exception ex)
            {
                // TODO: Error logging.
                ExceptionLog.LogException(ex);
            }

            if (sqlScript != null && sqlScript.Batches.Count > 0)
            {
                IList<TSqlStatement> statementList = sqlScript.Batches[0].Statements;
                if (statementList.Count > 0)
                {
                    parsedStatements = new List<TSqlStatement>();
                    parsedStatements.AddRange(statementList);
                }
            }

            reader.Dispose();

            return parsedStatements;
        }
        /// <summary>
        /// Determines whether the specified SQL Query has a select statement.
        /// </summary>
        /// <param name="sqlQuery">
        /// The SQL query to be examined.
        /// </param>
        /// <returns>
        ///   <c>true</c> if the provided text has a selected statement; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasSelectStatement(string sqlQuery)
        {
            bool hasSelect = false;

            StringReader reader = new StringReader(sqlQuery);
            TSql120Parser parser = new TSql120Parser(true);
            SqlQueryErrorCollection? errorList = null;

            try
            {
                StatementList statements = parser.ParseStatementList(reader, out IList<ParseError> list);

                // Copy errors, if any are returned.
                if (list != null && list.Count > 0)
                {
                    errorList = new SqlQueryErrorCollection();
                    foreach (ParseError error in list)
                    {
                        SqlQueryError localError = new SqlQueryError
                        {
                            Column = error.Column,
                            LineNumber = error.Line,
                            Message = error.Message,
                            Number = error.Number,
                            Offset = error.Offset
                        };
                        errorList.Add(localError);
                    }
                }
                else
                {
                    int length = statements.Statements.Count;
                    for (int count = 0; count < length; count++)
                    {
                        if (statements.Statements[count] is SelectStatement)
                            hasSelect = true;
                    }
                }
            }
            catch (Exception ex)
            {
                errorList = new SqlQueryErrorCollection
                {
                    new SqlQueryError
                    {
                        Message = ex.Message,
                        Class = 30,
                        Source = "NativeTSqlCodeDom::HasSelectStatement()"
                    }
                };
            }

            reader.Dispose();

            return hasSelect;
        }
        /// <summary>
        /// Generates the create table script.
        /// </summary>
        /// <param name="provider">
        /// The <see cref="SqlDataProvider"/> instance used to connect to the database.
        /// </param>
        /// <param name="tableName">
        /// A string containing the name of the table.
        /// </param>
        /// <returns>
        /// A string containing the resulting SQL query used to create the table.
        /// </returns>
        public static string? GenerateCreateTableScript(SqlDataProvider provider, string tableName)
        {
            string? genScript = null;

            bool initialized = SMOProviderFactory.Initialize(provider);
            if (initialized)
            {
                // Script the specified table.
                try
                {
                    Microsoft.SqlServer.Management.Smo.Table? table = SMOProviderFactory.Tables?[tableName];
                    if (table == null)
                    {
                        table = SMOProviderFactory.FindTable(tableName);
                    }

                    if (table != null)
                    {
                        ScriptingOptions opts = new ScriptingOptions
                        {
                            AllowSystemObjects = true,
                            AnsiPadding = false,
                            DdlBodyOnly = true,
                            IncludeHeaders = false,
                            SchemaQualify = true
                        };

                        StringCollection tableScript = table.Script(opts);
                        StringBuilder builder = new StringBuilder();
                        foreach (string? line in tableScript)
                        {
                            if (line != null)
                                builder.AppendLine(line);
                        }
                        tableScript.Clear();

                        genScript = builder.ToString();
                    }
                }
                catch (Exception ex)
                {
                    ExceptionLog.LogException(ex);
                }
            }
            return genScript;
        }
        #endregion
    }
}

