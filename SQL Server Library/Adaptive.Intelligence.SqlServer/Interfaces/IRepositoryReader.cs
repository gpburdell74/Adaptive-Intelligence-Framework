namespace Adaptive.SqlServer.Client
{
	/// <summary>
	/// Provides a signature definition for an object that can supply data to any object for reading
	/// the values for population.
	/// </summary>
	public interface IRepositoryReader
	{
		#region Properties
		/// <summary>
		/// Gets the number of columns in the current row.
		/// </summary>
		/// <value>
		/// When not positioned in a valid record set, 0; otherwise the number of columns in the current row. The default is -1.
		/// </value>
		int FieldCount { get; }
		/// <summary>
		/// Gets a value that indicates whether the <see cref="IRepositoryReader"/>
		/// contains Data.
		/// </summary>
		/// <value>
		/// <b>true</b> if the <see cref="IRepositoryReader"/> contains some data to read;
		/// otherwise <b>false</b>.
		/// </value>
		bool HasData { get; }
		/// <summary>
		/// Gets a value indicating whether this the <see cref="IRepositoryReader"/> is closed.
		/// </summary>
		/// <remarks>
		/// It is not possible to read from a <see cref="IRepositoryReader"/> instance
		/// that is closed.
		/// </remarks>
		/// <value>
		/// <b>true</b> if the <see cref="IRepositoryReader"/> is closed;
		/// otherwise, <b>false</b>.
		/// </value>
		bool IsClosed { get; }
		#endregion

		#region Methods / Functions

		#region Index-Based Column Reading Methods
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
		bool GetBoolean(int index);
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
		byte GetByte(int index);
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
		char GetChar(int index);
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
		DateTime GetDateTime(int index);
		/// <summary>
		/// Gets the value of the specified column as a Date/Time.
		/// </summary>
		/// <param name="index">
		/// The zero-based column ordinal value.
		/// </param>
		/// <returns>
		/// The value of the column as a nullable <see cref="DateTime"/>, or <b>null</b>.
		/// </returns>
		DateTime? GetDateTimeNullable(int index);
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
		DateTimeOffset GetDateTimeOffset(int index);
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
		DateTimeOffset? GetDateTimeOffsetNullable(int index);
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
		double GetDouble(int index);
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
		decimal GetDecimal(int index);
		/// <summary>
		/// Gets the SQL decimal value of the specified column as a float.
		/// </summary>
		/// <param name="index">
		/// The zero-based column ordinal value.
		/// </param>
		/// <returns>
		/// The SQL Decimal value of the column as a float, or <b>0</b> if the
		/// operation fails.
		/// </returns>
		float GetDecimalAsSingle(int index);
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
		float GetFloat(int index);
		/// <summary>
		/// Gets the value of the specified column as a <see cref="Guid"/>.
		/// </summary>
		/// <param name="index">
		/// The zero-based column ordinal value.
		/// </param>
		/// <returns>
		/// The value of the column as a <see cref="Guid"/>, or <see cref="Guid.Empty"/>> if the
		/// operation fails.
		/// </returns>
		Guid GetGuid(int index);
		/// <summary>
		/// Gets the value of the specified column as a <see cref="Guid"/>.
		/// </summary>
		/// <param name="index">
		/// The zero-based column ordinal value.
		/// </param>
		/// <returns>
		/// The value of the column as a <see cref="Guid"/>, or <b>null</b> if the
		/// operation fails.
		/// </returns>
		Guid? GetGuidNullable(int index);
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
		short GetInt16(int index);
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
		int GetInt32(int index);
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
		int? GetInt32Nullable(int index);
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
		long GetInt64(int index);
		/// <summary>
		/// Gets the item at the specified index.
		/// </summary>
		/// <param name="index">
		/// An integer specifying the ordinal column index.
		/// </param>
		/// <returns>
		/// An <see cref="object"/> containing the boxed value of the cell in the row.
		/// </returns>
		object? GetItem(int index);
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
		string? GetString(int index);
		#endregion

		#region Name-Based Column Reading Methods
		/// <summary>
		/// Gets the value of the specified column as a boolean.
		/// </summary>
		/// <param name="columnName">
		/// A string containing the name of the column whose data is to be read.
		/// </param>
		/// <returns>
		/// The value of the column as a boolean, or <b>false</b> if the
		/// operation fails.
		/// </returns>
		bool GetBoolean(string columnName);
		/// <summary>
		/// Gets the value of the specified column as a byte.
		/// </summary>
		/// <param name="columnName">
		/// A string containing the name of the column whose data is to be read.
		/// </param>
		/// <returns>
		/// The value of the column as a byte, or <b>0</b> if the
		/// operation fails.
		/// </returns>
		byte GetByte(string columnName);
		/// <summary>
		/// Gets the value of the specified column as a character.
		/// </summary>
		/// <param name="columnName">
		/// A string containing the name of the column whose data is to be read.
		/// </param>
		/// <returns>
		/// The value of the column as a boolean, or the null character if the
		/// operation fails.
		/// </returns>
		char GetChar(string columnName);
		/// <summary>
		/// Gets the value of the specified column as a Date/Time.
		/// </summary>
		/// <param name="columnName">
		/// A string containing the name of the column whose data is to be read.
		/// </param>
		/// <returns>
		/// The value of the column as a <see cref="DateTime"/>, or <see cref="DateTime.MinValue"/> if the
		/// operation fails.
		/// </returns>
		DateTime GetDateTime(string columnName);
		/// <summary>
		/// Gets the value of the specified column as a Date/Time.
		/// </summary>
		/// <param name="columnName">
		/// A string containing the name of the column whose data is to be read.
		/// </param>
		/// <returns>
		/// The value of the column as a nullable <see cref="DateTime"/>, or <b>null</b>.
		/// </returns>
		DateTime? GetDateTimeNullable(string columnName);
		/// <summary>
		/// Gets the value of the specified column as a Date/Time offset.
		/// </summary>
		/// <param name="columnName">
		/// A string containing the name of the column whose data is to be read.
		/// </param>
		/// <returns>
		/// The value of the column as a <see cref="DateTimeOffset"/>, or <see cref="DateTimeOffset.MinValue"/> if the
		/// operation fails.
		/// </returns>
		DateTimeOffset GetDateTimeOffset(string columnName);
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
		DateTimeOffset? GetDateTimeOffsetNullable(string columnName);
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
		double GetDouble(string columnName);
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
		decimal GetDecimal(string columnName);
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
		float GetDecimalAsSingle(string columnName);
		/// <summary>
		/// Gets the value of the specified column as a float.
		/// </summary>
		/// <param name="columnName">
		/// A string containing the name of the column whose data is to be read.
		/// </param>
		/// <returns>
		/// The value of the column as a float, or <b>0</b> if the
		/// operation fails.
		/// </returns>
		float GetFloat(string columnName);
		/// <summary>
		/// Gets the value of the specified column as a <see cref="Guid"/>.
		/// </summary>
		/// <param name="columnName">
		/// A string containing the name of the column whose data is to be read.
		/// </param>
		/// <returns>
		/// The value of the column as a <see cref="Guid"/>, or <b>0</b> if the
		/// operation fails.
		/// </returns>
		Guid GetGuid(string columnName);
		/// <summary>
		/// Gets the value of the specified column as an integer.
		/// </summary>
		/// <param name="columnName">
		/// A string containing the name of the column whose data is to be read.
		/// </param>
		/// <returns>
		/// The value of the column as a short integer, or <b>0</b> if the
		/// operation fails.
		/// </returns>
		short GetInt16(string columnName);
		/// <summary>
		/// Gets the value of the specified column as an integer.
		/// </summary>
		/// <param name="columnName">
		/// A string containing the name of the column whose data is to be read.
		/// </param>
		/// <returns>
		/// The value of the column as an integer, or <b>0</b> if the
		/// operation fails.
		/// </returns>
		int GetInt32(string columnName);
		/// <summary>
		/// Gets the value of the specified column as an integer.
		/// </summary>
		/// <param name="columnName">
		/// A string containing the name of the column whose data is to be read.
		/// </param>
		/// <returns>
		/// The value of the column as an integer, or <b>0</b> if the
		/// operation fails.
		/// </returns>
		int? GetInt32Nullable(string columnName);
		/// <summary>
		/// Gets the value of the specified column as a long integer.
		/// </summary>
		/// <param name="columnName">
		/// A string containing the name of the column whose data is to be read.
		/// </param>
		/// <returns>
		/// The value of the column as an integer, or <b>0</b> if the
		/// operation fails.
		/// </returns>
		long GetInt64(string columnName);
		/// <summary>
		/// Gets the item at the specified index.
		/// </summary>
		/// <param name="columnName">
		/// A string containing the name of the column whose data is to be read.
		/// </param>
		/// <returns>
		/// An <see cref="object"/> containing the boxed value of the cell in the row.
		/// </returns>
		object? GetItem(string columnName);
		/// <summary>
		/// Gets the value of the specified column as a string.
		/// </summary>
		/// <param name="columnName">
		/// A string containing the name of the column whose data is to be read.
		/// </param>
		/// <returns>
		/// The value of the column as a string, or <b>null</b> if the
		/// operation fails.
		/// </returns>
		string? GetString(string columnName);
		#endregion

		/// <summary>
		/// Gets the values for the specified number of columns.
		/// </summary>
		/// <param name="numberOfExpectedColumns">
		/// An integer specifying the number of expected columns.
		/// </param>
		/// <returns>
		/// An array of objects containing the boxed values.
		/// </returns>
		object[]? GetValues(int numberOfExpectedColumns);
		#endregion
	}
}
