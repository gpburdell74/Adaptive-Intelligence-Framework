// Ignore Spelling: Sql

namespace Adaptive.Intelligence.SqlServer.Schema
{
    /// <summary>
    /// Lists the types of system data types in SQL Server.
    /// </summary>
    public enum SqlDataTypes
    {
        /// <summary>
        /// Indicates variable-length binary data for storing images.
        /// </summary>
        Image = 34,
        /// <summary>
        /// Indicates variable-length non-Unicode data in the code page of the server.
        /// </summary>
        Text = 35,
        /// <summary>
        /// Indicates a 16-byte GUID value.
        /// </summary>
        UniqueIdentifier = 36,
        /// <summary>
        /// Indicates a date value.
        /// </summary>
        Date = 40,
        /// <summary>
        /// Indicates a time value.
        /// </summary>
        Time = 41,
        /// <summary>
        /// Indicates a date that is combined with a time of day that is based on
        /// 24-hour clock.
        /// </summary>
        DateTime2 = 42,
        /// <summary>
        /// Indicates a date that is combined with a time of a day that has time
        /// zone awareness and is based on a 24-hour clock.
        /// </summary>
        DateTimeOffset = 43,
        /// <summary>
        /// Indicates an 8-bit integer (byte)
        /// </summary>
        TinyInt = 48,
        /// <summary>
        /// Indicates a 16-bit integer
        /// </summary>
        SmallInt = 52,
        /// <summary>
        /// Indicates a 32-bit integer
        /// </summary>
        Int = 56,
        /// <summary>
        /// Indicates date that is combined with a time of day. The time is based on a
        /// 24-hour day, with seconds always zero (:00) and without fractional seconds.
        /// </summary>
        SmallDateTime = 58,
        /// <summary>
        /// Indicates a single-precision floating-point value where n is the number of bits that are
        /// used to store the mantissa of the float number in scientific notation and,
        /// therefore, dictates the precision and storage size. If n is specified, it
        /// must be a value between 1 and 53. The default value of n is 53.
        /// </summary>
        Real = 59,
        /// <summary>
        /// Indicates a data types that represents monetary or currency value in
        /// 8 bytes.
        /// </summary>
        Money = 60,
        /// <summary>
        /// Indicates a date that is combined with a time of day with fractional seconds that is based on a 24-hour clock.
        /// </summary>
        DateTime = 61,
        /// <summary>
        /// Indicates a double-precision floating-point value where n is the number of bits that are
        /// used to store the mantissa of the float number in scientific notation and,
        /// therefore, dictates the precision and storage size. If n is specified, it
        /// must be a value between 1 and 53. The default value of n is 53.
        /// </summary>
        Float = 62,
        /// <summary>
        /// Indicates a data type (sql_variant) that stores values of various SQL
        /// Server-supported data types.
        /// </summary>
        SqlVariant = 98,
        /// <summary>
        /// Indicates variable-length Unicode data with a maximum string length of
        /// (1,073,741,823) bytes. Storage size, in bytes, is two times the string
        /// length that is entered. The ISO synonym for ntext is national text.
        /// </summary>
        NText = 99,
        /// <summary>
        /// Indicates a boolean data type.
        /// </summary>
        Bit = 104,
        /// <summary>
        /// Indicates a numeric data type that has fixed precision and scale, where
        /// the size of the data is based on the precision value.
        /// </summary>
        Decimal = 106,
        /// <summary>
        /// Indicates a numeric data type that has fixed precision and scale, where
        /// the size of the data is based on the precision value.
        /// </summary>
        Numeric = 108,
        /// <summary>
        /// Indicates a data types that represents monetary or currency value in
        /// 4 bytes.
        /// </summary>
        SmallMoney = 122,
        /// <summary>
        /// Indicates a 64-bit integer
        /// </summary>
        BigInt = 127,
        /// <summary>
        /// Indicates a binary data type of variable length.
        /// </summary>
        VarBinary = 165,
        /// <summary>
        /// Indicates a string data type of variable length.
        /// </summary>
        VarChar = 167,
        /// <summary>
        /// Indicates a binary data type of fixed length.
        /// </summary>
        Binary = 173,
        /// <summary>
        /// Indicates a string data type of fixed length.
        /// </summary>
        Char = 175,
        /// <summary>
        /// Indicates a data type that exposes automatically generated, unique binary numbers within a database.
        /// </summary>
        TimeStamp = 189,
        /// <summary>
        /// Indicates a Unicode string data type of variable length.
        /// </summary>
        NVarCharOrSysName = 231,
        /// <summary>
        /// Indicates a Unicode string data type of fixed length.
        /// </summary>
        NChar = 239,
        /// <summary>
        /// Indicates a spatial type.
        /// </summary>
        SpatialType = 240,
        /// <summary>
        /// Indicates a data type that stores XML data.
        /// </summary>
        Xml = 241
    }
}
