// Ignore Spelling: Utils Sql Argb

using AutoFixture;
using AutoFixture.Xunit2;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;

namespace Adaptive.Intelligence.Shared.Test.Utility
{

    public class GeneralUtilsTest
    {
        [Fact]
        public void BigKeyTest()
        {
            // Act.
            var key = GeneralUtils.BigKey();

            // Assert.
            Assert.NotNull(key);
            Assert.False(string.IsNullOrEmpty(key));

            Guid.Parse(key);
        }

        [Fact]
        public void BlankDateTest()
        {
            // Act.
            DateTimeOffset dt = GeneralUtils.BlankDate();

            // Assert.
            Assert.Equal(1900, dt.Year);
            Assert.Equal(1, dt.Month);
            Assert.Equal(1, dt.Day);

            Assert.Equal(0, dt.Hour);
            Assert.Equal(0, dt.Minute);
            Assert.Equal(0, dt.Second);

        }

        [Fact]
        public void CleanUpPhoneNumberEmptyTest()
        {
            string result = GeneralUtils.CleanUpPhoneNumber(string.Empty);
            Assert.Equal(string.Empty, result);
        }

        [Theory,
            InlineData("1"),
            InlineData("12"),
            InlineData("1234"),
            InlineData("12345"),
            InlineData("123456"),
            InlineData("1234567"),
            InlineData("123456789")]

        public void CleanUpPhoneNumberNotEnoughDigitsTest(string data)
        {
            // Act.
            string result = GeneralUtils.CleanUpPhoneNumber(data);

            // Assert.
            string expected;
            if (data == "1")
                expected = string.Empty;
            else
                expected = data.Substring(1, data.Length - 1);

            Assert.Equal(expected, result);
        }

        [Theory,
            InlineData("(111) 222 - 3333"),
            InlineData("111) 222 - 3333"),
            InlineData("(111 222 - 3333"),
            InlineData("111-222 - 3333"),
            InlineData("111 -222 - 3333"),
            InlineData("111 - 222 - 3333"),
            InlineData("(111222 - 3333"),
            InlineData("(111)222 - 3333"),
            InlineData("(111) 222- 3333"),
            InlineData("(111) 222-3333"),
            InlineData("(111)222-3333")]
        public void CleanUpPhoneNumberTest(string data)
        {
            string result = GeneralUtils.CleanUpPhoneNumber(data);

            Assert.Equal("112223333", result);
        }

        [Fact]
        public void ConvertToDecimalTest()
        {
            var d = GeneralUtils.ConvertToDecimal("3.2");
            Assert.Equal((decimal)3.2, d);
        }
        [Fact]
        public void ConvertToSignTest()
        {
            var d = GeneralUtils.ConvertToDecimal("$3.21");
            Assert.Equal((decimal)3.21, d);
        }
        [Fact]
        public void ConvertToDecimalEmptyTest()
        {
            var d = GeneralUtils.ConvertToDecimal("");
            Assert.Equal(0, d);
        }
        [Fact]
        public void ConvertToDoubleTest()
        {
            var d = GeneralUtils.ConvertToDouble("3.2");
            Assert.Equal(3.2d, d);
        }
        [Fact]
        public void ConvertToDoubleSignTest()
        {
            double d = GeneralUtils.ConvertToDouble("$3.21");
            Assert.Equal(3.21d, d);
        }
        [Fact]
        public void ConvertToDoubleEmptyTest()
        {
            double d = GeneralUtils.ConvertToDouble("");
            Assert.Equal((double)0, d);
        }
        [Fact]
        public void ConvertToSqlMoneyTest()
        {
            float d = GeneralUtils.ConvertToSQLMoney("3.2");
            Assert.Equal((float)3.2, d);
        }
        [Fact]
        public void ConvertToSqlMoneySignTest()
        {
            float d = GeneralUtils.ConvertToSQLMoney("$3.21");
            Assert.Equal((float)3.21, d);
        }
        [Fact]
        public void ConvertToSqlMoneyEmptyTest()
        {
            float d = GeneralUtils.ConvertToSQLMoney("");
            Assert.Equal((float)0, d);
        }

        [Fact]
        public void CreateGuidIdStringTest()
        {
            string? id = GeneralUtils.CreateGuidIdString();

            Assert.NotNull(id);
            Assert.False(string.IsNullOrEmpty(id));
            Assert.DoesNotContain(id, "-");
            Assert.DoesNotContain(id, " ");

        }

        [Fact]
        public void CreateIterationListsFromIdListNullTest()
        {
            List<string> idList = GeneralUtils.CreateIterationListsFromIdList(null);

            Assert.NotNull(idList);
            Assert.Empty(idList);
        }

        [Fact]
        public void CreateIterationListsFromIdListEmptyTest()
        {
            List<string> idList = GeneralUtils.CreateIterationListsFromIdList(
                new List<string>());

            Assert.NotNull(idList);
            Assert.Empty(idList);
        }

        [Fact]
        public void CreateIterationListsFromIdListTest()
        {
            // Arrange.
            List<string> idList = new List<string>();

            Fixture fixture = new Fixture();
            fixture.AddManyTo<string>(idList, 100);

            string expected = SqlIze(idList);

            // Act.
            List<string> data = GeneralUtils.CreateIterationListsFromIdList(idList);

            // Assert.
            string line = data[0];
            Assert.Equal(expected, line);

        }

        [Fact]
        public void CreateIterationListsFromIdListMultiLineTest()
        {
            // Arrange.
            List<string> masterIdList = new List<string>();

            Fixture fixture = new Fixture();
            fixture.AddManyTo<string>(masterIdList, 505);

            string expectedA = SqlIze(masterIdList, 0, 100);
            string expectedB = SqlIze(masterIdList, 101, 201);
            string expectedC = SqlIze(masterIdList, 202, 302);
            string expectedD = SqlIze(masterIdList, 303, 403);
            string expectedE = SqlIze(masterIdList, 404, 504);

            // Act.
            List<string> data = GeneralUtils.CreateIterationListsFromIdList(masterIdList);

            // Assert.
            Assert.NotNull(data);
            Assert.Equal(5, data.Count);

            Assert.Equal(expectedA, data[0]);
            Assert.Equal(expectedB, data[1]);
            Assert.Equal(expectedC, data[2]);
            Assert.Equal(expectedD, data[3]);
            Assert.Equal(expectedE, data[4]);

        }
        private string SqlIze(List<string> testData)
        {
            int length = testData.Count;

            StringBuilder builder = new StringBuilder();
            int count = 0;
            builder.Append("'");
            foreach (string id in testData)
            {
                builder.Append(testData[count]);
                count++;
                if (count < length)
                    builder.Append(",");
            }
            builder.Append("'");
            return builder.ToString();
        }

        private string SqlIze(List<string> testData, int startIndex, int endIndex)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("'");
            for (int count = startIndex; count <= endIndex; count++)
            {
                builder.Append(testData[count]);
                if (count < endIndex)
                    builder.Append(",");
            }
            builder.Append("'");
            return builder.ToString();
        }
        [Fact]
        public void CreateListBlocks_NullList_ReturnsEmptyList()
        {
            // Arrange
            List<int>? originalList = null;
            int blockSize = 3;

            // Act
            var result = GeneralUtils.CreateListBlocks(originalList, blockSize);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void CreateListBlocks_EmptyList_ReturnsEmptyList()
        {
            // Arrange
            List<int> originalList = new List<int>();
            int blockSize = 3;

            // Act
            var result = GeneralUtils.CreateListBlocks(originalList, blockSize);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.Count);
        }

        [Fact]
        public void CreateListBlocks_ListWithItems_ReturnsCorrectBlocks()
        {
            // Arrange
            List<int> originalList = new List<int> { 1, 2, 3, 4, 5, 6, 7 };
            int blockSize = 3;

            // Act
            List<List<int>> result = GeneralUtils.CreateListBlocks(originalList, blockSize);

            // Assert
            Assert.Equal(3, result.Count);

            Assert.Collection<int>(
                result[0],
                e => Assert.Equal(1, e),
                e => Assert.Equal(2, e),
                e => Assert.Equal(3, e)
            );

            Assert.Collection<int>(
                result[1],
                e => Assert.Equal(4, e),
                e => Assert.Equal(5, e),
                e => Assert.Equal(6, e)
            );

            Assert.Collection<int>(
                result[2],
                e => Assert.Equal(7, e)
            );
        }

        [Fact]
        public void CreateListBlocks_ListWithItems_BlockSizeGreaterThanListSize()
        {
            // Arrange
            List<int> originalList = new List<int> { 1, 2, 3 };
            int blockSize = 5;

            // Act
            var result = GeneralUtils.CreateListBlocks(originalList, blockSize);

            // Assert
            Assert.Single(result);
            Assert.Equal(new List<int> { 1, 2, 3 }, result[0]);
        }

        [Fact]
        public void CreateListBlocks_ListWithItems_BlockSizeOne()
        {
            // Arrange
            List<int> originalList = new List<int> { 1, 2, 3 };
            int blockSize = 1;

            // Act
            List<List<int>> result = GeneralUtils.CreateListBlocks(originalList, blockSize);

            // Assert
            Assert.Equal(3, result.Count);
            Assert.Equal(new List<int> { 1 }, result[0]);
            Assert.Equal(new List<int> { 2 }, result[1]);
            Assert.Equal(new List<int> { 3 }, result[2]);
        }

        [Theory]
        [InlineData("test", "tests")]
        [InlineData("story", "stories")]
        [InlineData("daily", "dailies")]
        [InlineData("day", "days")]
        [InlineData("family", "families")]
        [InlineData("eighty", "eighties")]
        [InlineData("valley", "valleys")]
        [InlineData("beauty", "beauties")]
        [InlineData("opportunity", "opportunities")]
        public void GetEnglishPluralFormTest(string testData, string expected)
        {
            string result = GeneralUtils.GetPluralEnglishForm(testData);
            Assert.NotNull(result);
            Assert.Equal(expected, result);
        }
        [Fact]
        public void GetEnglishPluralFormNullTest()
        {
            string result = GeneralUtils.GetPluralEnglishForm(null);
            Assert.NotNull(result);
            Assert.Equal(string.Empty, result);
        }
        [Fact]
        public void GetEnglishPluralFormEmptyTest()
        {
            string result = GeneralUtils.GetPluralEnglishForm(string.Empty);
            Assert.NotNull(result);
            Assert.Equal(string.Empty, result);
        }
        [Fact]
        public void GetEnglishSingleFormNullTest()
        {
            string result = GeneralUtils.GetSingleEnglishForm(null);
            Assert.NotNull(result);
            Assert.Equal(string.Empty, result);
        }
        [Fact]
        public void GetEnglishSingleFormEmptyTest()
        {
            string result = GeneralUtils.GetSingleEnglishForm(string.Empty);
            Assert.NotNull(result);
            Assert.Equal(string.Empty, result);
        }
        [Theory]
        [InlineData("test", "tests")]
        [InlineData("story", "stories")]
        [InlineData("daily", "dailies")]
        [InlineData("day", "days")]
        [InlineData("family", "families")]
        [InlineData("eighty", "eighties")]
        [InlineData("valley", "valleys")]
        [InlineData("beauty", "beauties")]
        [InlineData("opportunity", "opportunities")]
        public void GetEnglishSingleFormTest(string expected, string testData)
        {
            string result = GeneralUtils.GetSingleEnglishForm(testData);
            Assert.NotNull(result);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(0, "0 inches")]
        [InlineData(1, "1 inch")]
        [InlineData(2, "2 inches")]
        [InlineData(3, "3 inches")]
        [InlineData(4, "4 inches")]
        [InlineData(5, "5 inches")]
        [InlineData(6, "6 inches")]
        [InlineData(7, "7 inches")]
        [InlineData(8, "8 inches")]
        [InlineData(9, "9 inches")]
        [InlineData(10, "10 inches")]
        [InlineData(11, "11 inches")]
        public void EnglishPluralTest(int value, string expected)
        {
            string? result = GeneralUtils.EnglishPlural(value, "inch", "es");
            Assert.NotNull(result);
            Assert.Equal(expected, result);

        }

        [Theory]
        [InlineData(new string[] { "" }, "")]
        [InlineData(new string[] { "one" }, "one")]
        [InlineData(new string[] { "one", "two" }, "one and two")]
        [InlineData(new string[] { "one", "two", "three" }, "one, two and three")]
        [InlineData(new string[] { "1", "2", "3", "4" }, "1, 2, 3 and 4")]
        public void EnglishStringAppendTest(string[] words, string expected)
        {
            string? result = GeneralUtils.EnglishStringAppend(words);
            Assert.NotNull(result);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(10)]
        [InlineData(11)]
        [InlineData(12)]
        [InlineData(-1)]
        [InlineData(-2)]
        [InlineData(-3)]
        [InlineData(-4)]
        [InlineData(-5)]
        [InlineData(-6)]
        [InlineData(-7)]
        [InlineData(-8)]
        [InlineData(-9)]
        [InlineData(-10)]
        [InlineData(-11)]
        [InlineData(-12)]
        public void FindTimeZoneForOffsetTest(int offset)
        {
            TimeZoneInfo current = TimeZoneInfo.Local;

            TimeZoneInfo? info = GeneralUtils.FindTimeZoneForOffset(offset);
            Assert.NotNull(info);

            int hours = (int)info.BaseUtcOffset.TotalHours;
            Assert.Equal(offset, hours);
        }

        [Theory]
        [InlineData("FF", "FF", "FF", "FF", 255, 255, 255, 255)]
        [InlineData("00", "00", "00", "00", 0, 0, 0, 0)]
        [InlineData("FF", "00", "00", "00", 255, 0, 0, 0)]
        [InlineData("00", "FF", "00", "00", 0, 255, 0, 0)]
        [InlineData("00", "FF", "AA", "22", 0, 255, 170, 34)]
        public void FromArgbStringTestOne(string a, string r, string g, string b,
            int expectedA, int expectedR, int expectedG, int expectedB)
        {
            Color? color = GeneralUtils.FromArgbString(a, r, g, b);
            Assert.NotNull(color);

            Assert.Equal(color.Value.A, expectedA);
            Assert.Equal(color.Value.R, expectedR);
            Assert.Equal(color.Value.G, expectedG);
            Assert.Equal(color.Value.B, expectedB);

        }
        [Theory, AutoHexData]
        public void FromArgbString_ShouldReturnCorrectColor_WhenValidHexValuesProvided(string a, string r, string g, string b)
        {
            // Arrange
            a = a.Substring(0, 2);
            r = r.Substring(0, 2);
            g = g.Substring(0, 2);
            b = b.Substring(0, 2);

            int aValue = Convert.ToInt32(a, 16);
            int rValue = Convert.ToInt32(r, 16);
            int gValue = Convert.ToInt32(g, 16);
            int bValue = Convert.ToInt32(b, 16);

            // Act
            Color result = GeneralUtils.FromArgbString(a, r, g, b);

            // Assert
            Assert.Equal(aValue, result.A);
            Assert.Equal(rValue, result.R);
            Assert.Equal(gValue, result.G);
            Assert.Equal(bValue, result.B);
        }

        [Theory, AutoHexData]
        public void FromArgbString_ShouldReturnDefaultColor_WhenEmptyStringsProvided()
        {
            // Arrange
            string a = string.Empty;
            string r = string.Empty;
            string g = string.Empty;
            string b = string.Empty;

            // Act
            Color result = GeneralUtils.FromArgbString(a, r, g, b);

            // Assert
            Assert.Equal(255, result.A);
            Assert.Equal(255, result.R);
            Assert.Equal(255, result.G);
            Assert.Equal(255, result.B);
        }

        [Theory, AutoHexData]
        public void FromArgbString_ShouldReturnCorrectColor_WhenPartialHexValuesProvided(string r, string g, string b)
        {
            // Arrange
            r = r.Substring(0, 2);
            g = g.Substring(0, 2);
            b = b.Substring(0, 2);

            int rValue = Convert.ToInt32(r, 16);
            int gValue = Convert.ToInt32(g, 16);
            int bValue = Convert.ToInt32(b, 16);

            // Act
            Color result = GeneralUtils.FromArgbString(null, r, g, b);

            // Assert
            Assert.Equal(255, result.A);
            Assert.Equal(rValue, result.R);
            Assert.Equal(gValue, result.G);
            Assert.Equal(bValue, result.B);
        }

        [Fact]
        public void HashStringEmptyTest()
        {
            string? hash = GeneralUtils.HashString(string.Empty);
            Assert.Null(hash);
        }

        [Theory]
        [AutoData]
        public void HashStringTest(string testData)
        {
            // Arrange.
            byte[] data = Encoding.UTF8.GetBytes(testData);
            byte[] output;
            using (SHA512 hasher = SHA512.Create())
            {
                output = hasher.ComputeHash(data);
            }

            // Act.
            string? hash = GeneralUtils.HashString(testData);

            // Assert.
            Assert.NotNull(hash);
            byte[] testArray = Convert.FromBase64String(hash);
            Assert.Equal(output, testArray);

        }

        [Fact]
        public void IsNullOrEmptyTestInt()
        {
            bool isempty = GeneralUtils.IsListNullOrEmpty<int>(new List<int>());
            Assert.True(isempty);
        }
        [Fact]
        public void IsNullOrEmptyTestStr()
        {
            bool isempty = GeneralUtils.IsListNullOrEmpty<string>(new List<string>());
            Assert.True(isempty);
        }
        [Fact]
        public void IsNullOrEmptyTestNullStr()
        {
            bool isempty = GeneralUtils.IsListNullOrEmpty<string>(null);
            Assert.True(isempty);
        }
        [Fact]
        public void IsNullOrEmptyTestNotStr()
        {
            List<string> list = new List<string>();
            list.Add("X");
            bool isempty = GeneralUtils.IsListNullOrEmpty<string>(list);
            Assert.False(isempty);
        }

        [Fact]
        public void IsNullOrEmptyAutoTest()
        {
            Fixture fixture = new Fixture();
            bool isEmpty = GeneralUtils.IsListNullOrEmpty(fixture.Create<List<string>>());
            Assert.False(isEmpty);
        }

        [Fact]
        public void CreateGuidIdString_ShouldReturnValidGuidString()
        {
            // Act
            string result = GeneralUtils.CreateGuidIdString();

            // Assert
            Assert.False(string.IsNullOrEmpty(result));
            Assert.Equal(32, result.Length); // GUID without dashes has 32 characters
        }

        [Fact]
        public void FindTimeZoneForOffset_String_ShouldReturnCorrectTimeZone()
        {
            // Act
            TimeZoneInfo result = GeneralUtils.FindTimeZoneForOffset("-5");

            // Assert
            Assert.Equal("Eastern Standard Time", result.Id);
        }

        [Fact]
        public void FindTimeZoneForOffset_Int_ShouldReturnCorrectTimeZone()
        {
            // Act
            TimeZoneInfo result = GeneralUtils.FindTimeZoneForOffset(
                DateTimeOffset.Now.Offset.Hours);

            // Assert
            Assert.Equal("Eastern Standard Time", result.Id);
        }

        [Fact]
        public void BigKey_ShouldReturnValidGuidString()
        {
            // Act
            string result = GeneralUtils.BigKey();

            // Assert
            Assert.False(string.IsNullOrEmpty(result));
            Assert.Equal(32, result.Length); // GUID without dashes has 32 characters
        }

        [Fact]
        public void BlankDate_ShouldReturnCorrectDate()
        {
            // Act
            DateTimeOffset result = GeneralUtils.BlankDate();

            // Assert
            Assert.Equal(new DateTimeOffset(1900, 1, 1, 0, 0, 0, TimeSpan.Zero), result);
        }

        [Fact]
        public void CleanUpPhoneNumber_ShouldReturnCleanedPhoneNumber()
        {
            // Arrange
            string phoneNumber = "1-800-555-1234";

            // Act
            string result = GeneralUtils.CleanUpPhoneNumber(phoneNumber);

            // Assert
            Assert.Equal("8005551234", result);
        }

        [Fact]
        public void ConvertToSQLMoney_ShouldReturnFloatValue()
        {
            // Arrange
            string moneyText = "$123.45";

            // Act
            float result = moneyText.ConvertToSQLMoney();

            // Assert
            Assert.Equal(123.45f, result);
        }

        [Fact]
        public void ConvertToDouble_ShouldReturnDoubleValue()
        {
            // Arrange
            string doubleText = "$123.45";

            // Act
            double result = doubleText.ConvertToDouble();

            // Assert
            Assert.Equal(123.45, result);
        }

        [Fact]
        public void ConvertToDecimal_ShouldReturnDecimalValue()
        {
            // Arrange
            string decimalText = "$123.45";

            // Act
            decimal result = decimalText.ConvertToDecimal();

            // Assert
            Assert.Equal(123.45m, result);
        }

        [Fact]
        public void FromArgbString_ShouldReturnCorrectColor()
        {
            // Act
            Color result = GeneralUtils.FromArgbString("FF", "00", "00", "FF");

            // Assert
            Assert.Equal(Color.FromArgb(255, 0, 0, 255), result);
        }

        [Fact]
        public void HashString_ShouldReturnHashedString()
        {
            // Arrange
            string originalContent = "Hello, World!";

            // Act
            string? result = GeneralUtils.HashString(originalContent);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void LaunchProcess_ShouldNotThrowException()
        {
            // Arrange
            string fileName = "notepad.exe";

            // Act & Assert
            var exception = Record.Exception(() => GeneralUtils.LaunchProcess(fileName));
            Assert.Null(exception);
        }

        [Fact]
        public void MarkupPhoneNumber_ShouldReturnFormattedPhoneNumber()
        {
            // Arrange
            string phone = "8005551234";

            // Act
            string result = GeneralUtils.MarkupPhoneNumber(phone);

            // Assert
            Assert.Equal("(800) 555-1234", result);
        }

        [Fact]
        public void ParseColor_ShouldReturnCorrectColor()
        {
            // Act
            Color result = GeneralUtils.ParseColor("#FF0000FF");

            // Assert
            Assert.Equal(Color.FromArgb(255, 0, 0, 255), result);
        }

        [Fact]
        public void SmartTrim_ShouldReturnTrimmedString()
        {
            // Arrange
            string data = "  Hello, World!  ";

            // Act
            string result = data.SmartTrim(5);

            // Assert
            Assert.Equal("Hello", result);
        }

        [Fact]
        public void ToCustomTextIfNull_ShouldReturnFormattedDate()
        {
            // Arrange
            DateTimeOffset value = new DateTimeOffset(2023, 10, 1, 0, 0, 0, TimeSpan.Zero);
            string noDateText = "No Date";

            // Act
            string result = value.ToCustomTextIfNull(noDateText);

            // Assert
            Assert.Equal("10/01/2023", result); // Assuming Constants.DateFormat is "MM/dd/yyyy"
        }

        [Fact]
        public void ToCustomTextIfNullWithTime_ShouldReturnFormattedDateTime()
        {
            // Arrange
            DateTimeOffset value = new DateTimeOffset(2023, 10, 1, 12, 0, 0, TimeSpan.Zero);
            string noDateText = "No Date";

            // Act
            string result = value.ToCustomTextIfNullWithTime(noDateText);

            // Assert
            Assert.Equal("10/01/2023 12:00 PM", result); // Assuming Constants.DateTimeFormat is "MM/dd/yyyy hh:mm tt"
        }

        [Fact]
        public void ToDefaultStringIfNull_ShouldReturnDefaultValue()
        {
            // Arrange
            string value = null;
            string defaultValue = "Default";

            // Act
            string result = value.ToDefaultStringIfNull(defaultValue);

            // Assert
            Assert.Equal(defaultValue, result);
        }

        [Fact]
        public void ToNoDateTextIfNull_DateTimeOffset_ShouldReturnEmptyString()
        {
            // Arrange
            DateTimeOffset value = new DateTimeOffset(1900, 1, 1, 0, 0, 0, TimeSpan.Zero);

            // Act
            string result = value.ToNoDateTextIfNull();

            // Assert
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void ToNoDateTextIfNull_DateTime_ShouldReturnEmptyString()
        {
            // Arrange
            DateTime value = new DateTime(1900, 1, 1);

            // Act
            string result = value.ToNoDateTextIfNull();

            // Assert
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void ToUserTimeZone_ShouldReturnLocalTime()
        {
            // Arrange
            DateTimeOffset value = new DateTimeOffset(2023, 10, 1, 0, 0, 0, TimeSpan.Zero);

            // Act
            DateTimeOffset result = value.ToUserTimeZone();

            // Assert
            Assert.Equal(value.ToLocalTime(), result);
        }

        [Fact]
        public void ToUserTimeZone_WithOffset_ShouldReturnLocalTime()
        {
            // Arrange
            DateTimeOffset value = new DateTimeOffset(2023, 10, 1, 0, 0, 0, TimeSpan.Zero);
            int offset = -5;

            // Act
            DateTimeOffset result = value.ToUserTimeZone(offset);

            // Assert
            Assert.Equal(value.AddHours(-offset), result);
        }

        [Fact]
        public void ToUserTimeZone_WithStringOffset_ShouldReturnLocalTime()
        {
            // Arrange
            DateTimeOffset value = new DateTimeOffset(2023, 10, 1, 0, 0, 0, TimeSpan.Zero);
            string offset = "-5";

            // Act
            DateTimeOffset result = value.ToUserTimeZone(offset);

            // Assert
            Assert.Equal(value.AddHours(Convert.ToInt32(offset)), result);
        }

        [Fact]
        public void ToSqlListString_ShouldReturnSqlListString()
        {
            // Arrange
            List<string> valueList = new List<string> { "1", "2", "3" };

            // Act
            string result = GeneralUtils.ToSqlListString(valueList);

            // Assert
            Assert.Equal("1,2,3", result);
        }

        [Fact]
        public void SplitList_ShouldReturnSplitLists()
        {
            // Arrange
            List<int> originalList = new List<int> { 1, 2, 3, 4, 5 };
            int subListSize = 2;

            // Act
            List<List<int>> result = GeneralUtils.SplitList(originalList, subListSize);

            // Assert
            Assert.Equal(3, result.Count);
            Assert.Equal(new List<int> { 1, 2 }, result[0]);
            Assert.Equal(new List<int> { 3, 4 }, result[1]);
            Assert.Equal(new List<int> { 5 }, result[2]);
        }

        [Fact]
        public void ToStream_ByteArray_ShouldReturnMemoryStream()
        {
            // Arrange
            byte[] originalData = new byte[] { 1, 2, 3 };

            // Act
            MemoryStream result = GeneralUtils.ToStream(originalData);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(originalData, result.ToArray());
        }

        [Fact]
        public void ToStream_String_ShouldReturnMemoryStream()
        {
            // Arrange
            string originalData = "Hello, World!";

            // Act
            MemoryStream result = GeneralUtils.ToStream(originalData);

            // Assert
            Assert.NotNull(result);
            using (StreamReader reader = new StreamReader(result))
            {
                string resultString = reader.ReadToEnd();
                Assert.Equal(originalData, resultString);
            }
        }
    }
}
