using AutoFixture;
using System.Text;

namespace Adaptive.Intelligence.Shared.Test.Utility
{

    public class GeneralUtilsTest
    {
        [Fact]
        public void BigKeyTest()
        {
            // Act.
            string? key = GeneralUtils.BigKey();

            // Assert.
            Assert.NotNull(key);
            Assert.False(string.IsNullOrEmpty(key));

            Guid guid = Guid.Parse(key);

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
            decimal d = GeneralUtils.ConvertToDecimal("3.2");
            Assert.Equal((decimal)3.2, d);
        }
        [Fact]
        public void ConvertToSignTest()
        {
            decimal d = GeneralUtils.ConvertToDecimal("$3.21");
            Assert.Equal((decimal)3.21, d);
        }
        [Fact]
        public void ConvertToDecimalEmptyTest()
        {
            decimal d = GeneralUtils.ConvertToDecimal("");
            Assert.Equal((decimal)0, d);
        }
        [Fact]
        public void ConvertToDoubleTest()
        {
            double d = GeneralUtils.ConvertToDouble("3.2");
            Assert.Equal((double)3.2, d);
        }
        [Fact]
        public void ConvertToDoubleSignTest()
        {
            double d = GeneralUtils.ConvertToDouble("$3.21");
            Assert.Equal((double)3.21, d);
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
            Assert.Equal(0, result.Count);
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
        public void Other()
        {
            //GeneralUtils.EnglishPlural();
            //GeneralUtils.EnglishStringAppend();
            //GeneralUtils.FindTimezoneForOffset();
            //GeneralUtils.FromArgbString();
            //GeneralUtils.GetPluralEnglishForm();
            //GeneralUtils.GetSingleEnglishForm();
            //GeneralUtils.HashString();
            //GeneralUtils.IsListNullOrEmpty();
            //GeneralUtils.LaunchProcess();
            //GeneralUtils.MarkupPhoneNumber();
            //GeneralUtils.ParseColor();
            //GeneralUtils.SmartTrim();
            //GeneralUtils.SplitList();
            //GeneralUtils.ToCustomTextIfNull();
            //GeneralUtils.ToCustomTextIfNullWithTime();
            //GeneralUtils.ToDefaultStringIfNull();
            //GeneralUtils.ToEmptyStringIfNull();
            //GeneralUtils.ToNoDateTextIfNull();
            //GeneralUtils.ToSqlListString();
            //GeneralUtils.ToStream();
            //GeneralUtils.ToUserTimeZone();
        }
    }
}