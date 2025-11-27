// Ignore Spelling: Sql

using Adaptive.SqlServer.Client;

namespace Adaptive.Intelligence.SqlServer.Schema
{
    /// <summary>
    /// Represents a SQL data type.
    /// </summary>
    public sealed class SqlDataType : SqlDataTypeSpecification
    {
        #region Private Constants

        private const string FieldTypeName = "typeName";
        private const string FieldTypeId = "typeId";
        private const string FieldMaxLength = "maxLength";
        private const string FieldPrecision = "precision";
        private const string FieldScale = "scale";
        private const string FieldIsNullable = "isNullable";

        #endregion

        #region Constructor / Dispose Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlDataType"/> class.
        /// </summary>
        public SqlDataType()
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlDataType"/> class.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="ISafeSqlDataReader"/> to read the data content from.
        /// </param>
        public SqlDataType(ISafeSqlDataReader? reader)
        {
            FromReader(reader);
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            Name = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the SQL name of the data type.
        /// </summary>
        /// <value>
        /// A string specifying the SQL name of the data type.
        /// </value>
        public string? Name { get; private set; }
        /// <summary>
        /// Gets the system type ID value.
        /// </summary>
        /// <value>
        /// A byte specifying the system type ID.
        /// </value>
        public byte TypeId { get; private set; }
        #endregion

        #region Public Methods / Functions
        /// <summary>
        /// Populates the instance from the supplied reader.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="ISafeSqlDataReader"/> used to read the data row.
        /// </param>
        public void FromReader(ISafeSqlDataReader? reader)
        {
            if (reader != null)
            {
                Name = reader.GetString(FieldTypeName);
                TypeId = reader.GetByte(FieldTypeId);
                MaxLength = reader.GetInt16(FieldMaxLength);
                Precision = reader.GetByte(FieldPrecision);
                Scale = reader.GetByte(FieldScale);
                IsNullable = reader.GetBoolean(FieldIsNullable);
            }
        }
        /// <summary>
        /// Gets the matching data type in the .NET Framework.
        /// </summary>
        /// <returns>
        /// The corresponding .NET <see cref="Type"/>.
        /// </returns>
        public Type GetDotNetType()
        {
            Type returnType;

            SqlDataTypes myType = (SqlDataTypes)TypeId;
            switch (myType)
            {
                case SqlDataTypes.BigInt:
                    returnType = typeof(long);
                    break;

                case SqlDataTypes.Int:
                    returnType = typeof(int);
                    break;

                case SqlDataTypes.SmallInt:
                    returnType = typeof(short);
                    break;

                case SqlDataTypes.TinyInt:
                    returnType = typeof(byte);
                    break;

                case SqlDataTypes.Decimal:
                case SqlDataTypes.Numeric:
                case SqlDataTypes.Money:
                    returnType = typeof(double);
                    break;
                case SqlDataTypes.Real:
                case SqlDataTypes.SmallMoney:
                    returnType = typeof(float);
                    break;

                case SqlDataTypes.Bit:
                    returnType = typeof(bool);
                    break;

                case SqlDataTypes.Date:
                case SqlDataTypes.DateTime:
                case SqlDataTypes.DateTime2:
                case SqlDataTypes.Time:
                case SqlDataTypes.SmallDateTime:
                    returnType = typeof(DateTime);
                    break;

                case SqlDataTypes.DateTimeOffset:
                    returnType = typeof(DateTimeOffset);
                    break;

                case SqlDataTypes.Char:
                case SqlDataTypes.NChar:
                    returnType = typeof(char);
                    break;

                case SqlDataTypes.VarChar:
                case SqlDataTypes.NVarCharOrSysName:
                    returnType = typeof(string);
                    break;

                case SqlDataTypes.Binary:
                case SqlDataTypes.VarBinary:
                case SqlDataTypes.Image:
                case SqlDataTypes.TimeStamp:
                    returnType = typeof(byte[]);
                    break;

                case SqlDataTypes.SqlVariant:
                    returnType = typeof(object);
                    break;

                case SqlDataTypes.UniqueIdentifier:
                    returnType = typeof(Guid);
                    break;

                default:
                    returnType = typeof(object);
                    break;
            }
            return returnType;
        }
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            if (Name == null)
            {
                return nameof(SqlDataType);
            }

            return Name;
        }
        #endregion
    }
}
