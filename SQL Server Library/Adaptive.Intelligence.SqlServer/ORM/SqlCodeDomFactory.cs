using Adaptive.Intelligence.Shared.Code;
using Adaptive.Intelligence.SqlServer.Analysis;
using Adaptive.Intelligence.SqlServer.Schema;
using Microsoft.SqlServer.Management.Smo;

namespace Adaptive.Intelligence.SqlServer.ORM
{
    /// <summary>
    /// Provides static methods and functions for creating C# code for use with SQL Server Objects.
    /// </summary>
    public static class SqlCodeDomFactory
    {
        #region Public Methods / Functions
        /// <summary>
        /// Generates the property profiles.
        /// </summary>
        /// <param name="dbInfo">
        /// The <see cref="DatabaseInfo"/> instance containing the database information.
        /// </param>
        /// <param name="columnList">
        /// A <see cref="SqlColumnCollection"/> containing the column list.
        /// </param>
        /// <param name="profile">
        /// The <see cref="AdaptiveTableProfile"/> containing the table metadata.
        /// </param>
        /// <returns>
        /// A <see cref="PropertyProfileCollection"/> instance.
        /// </returns>
        public static PropertyProfileCollection? GeneratePropertyProfiles(DatabaseInfo? dbInfo, SqlColumnCollection? columnList, AdaptiveTableProfile? profile)
        {
            PropertyProfileCollection? propertyList = null;

            if (dbInfo != null && columnList != null && profile != null)
            {
                propertyList = new PropertyProfileCollection(columnList.Count);

                // Create Property Profiles for the column definitions.
                foreach (SqlColumn column in columnList)
                {
                    PropertyProfile? propProfile = CreatePropertyProfileForSqlColumn(column, dbInfo);
                    if (propProfile != null)
                        propertyList.Add(propProfile);
                }

                // Create Property Profiles for key fields that reference other tables.
                if (profile.ReferencedTableJoins != null && profile.ReferencedTableJoins.Count > 0)
                {
                    foreach (ReferencedTableJoin tableReference in profile.ReferencedTableJoins)
                    {
                        AdaptiveTableProfile? referencedProfile = dbInfo.GetTableProfile(tableReference.ReferencedTable?.TableName);
                        if (referencedProfile != null && !referencedProfile.NoSubJoins)
                        {
                            PropertyProfile? propProfile = CreatePropertyProfileForReferencedSqlColumn(referencedProfile);
                            if (propProfile != null)
                                propertyList.Add(propProfile);
                        }
                    }
                }

                // Sort in alphabetical order.
                propertyList.SortAlphabetical();
            }

            return propertyList;
        }
        #endregion

        #region Private Static Methods / Functions        
        /// <summary>
        /// Creates the property profile for a standard SQL column in a table.
        /// </summary>
        /// <param name="column">
        /// The <see cref="SqlColumn"/> definition instance.
        /// </param>
        /// <param name="dbInfo">
        /// The <see cref="DatabaseInfo"/> instance containing the database metadata and information.
        /// </param>
        /// <returns>
        /// A <see cref="PropertyProfile"/> instance that can be used to generate C# code for a property definition, 
        /// or <b>null</b>.
        /// </returns>
        private static PropertyProfile? CreatePropertyProfileForSqlColumn(SqlColumn column, DatabaseInfo dbInfo)
        {
            PropertyProfile? propProfile = null;

            string? columnName = column.ColumnName;
            if (columnName != null && dbInfo.Database != null && dbInfo.Database.DataTypes != null)
            {
                // Determine information about the data type for the columns.
                Schema.SqlDataType columnDataType = dbInfo.Database.DataTypes.GetTypeById(column.TypeId);
                Type propertyType = columnDataType.GetDotNetType();
                Type? isDisposable = propertyType.GetInterface("System.IDisposable");

                propProfile = new PropertyProfile();
                propProfile.PropertyName = columnName;
                propProfile.TypeName = propertyType.Name;

                if (!propertyType.IsValueType)
                    propProfile.IsNullable = true;

                if (propertyType.IsClass && isDisposable != null)
                    propProfile.IsDisposable = true;

                if (propertyType.BaseType == typeof(List<>))
                    propProfile.IsList = true;
            }
            return propProfile;
        }
        /// <summary>
        /// Creates the property profile for a SQL column in a table that actually references another SQL table
        /// and data type.
        /// </summary>
        /// <param name="referencedProfile">
        /// The <see cref="AdaptiveTableProfile"/> containing the metadata and profile for the related SQL table.
        /// </param>
        /// <returns>
        /// A <see cref="PropertyProfile"/> instance that can be used to generate C# code for a property definition, 
        /// or <b>null</b>.
        /// </returns>
        private static PropertyProfile? CreatePropertyProfileForReferencedSqlColumn(AdaptiveTableProfile referencedProfile)
        {
            PropertyProfile? propProfile = null;

            if (!referencedProfile.NoSubJoins)
            {
                string name = referencedProfile.SubJoinObjectNamePrefix +
                              referencedProfile.TableName + referencedProfile.SubJoinObjectNameSuffix;

                string? typeName = referencedProfile?.DataDefinitionClassName;

                propProfile = new PropertyProfile
                {
                    TypeName = typeName,
                    PropertyName = name,
                    IsDisposable = true,
                    IsNullable = true
                };
            }
            return propProfile;
        }
        #endregion
    }
}
