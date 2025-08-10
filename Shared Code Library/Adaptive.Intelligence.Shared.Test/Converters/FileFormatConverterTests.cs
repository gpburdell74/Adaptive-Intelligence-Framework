namespace Adaptive.Intelligence.Shared.Test.Converters
{
    public class FileFormatConverterTests
    {
        private readonly FileFormatConverter _converter;

        public FileFormatConverterTests()
        {
            _converter = new FileFormatConverter();
        }

        [Theory]
        [InlineData(FileFormat.Excel, Constants.ExtExcel)]
        [InlineData(FileFormat.WordDocument, Constants.ExtWordDocument)]
        [InlineData(FileFormat.NotSpecified, "")]
        public void Convert_ShouldReturnCorrectExtension(FileFormat format, string expectedExtension)
        {
            var result = _converter.Convert(format);
            Assert.Equal(expectedExtension, result);
        }

        [Theory]
        [InlineData(Constants.ExtExcel, FileFormat.Excel)]
        [InlineData(Constants.ExtWordDocument, FileFormat.WordDocument)]
        [InlineData("", FileFormat.NotSpecified)]
        public void ConvertBack_ShouldReturnCorrectFileFormat(string extension, FileFormat expectedFormat)
        {
            var result = _converter.ConvertBack(extension);
            Assert.Equal(expectedFormat, result);
        }
        [Fact]
        public void ConvertOddDataTest()
        {
            string fileType = _converter.Convert((FileFormat)1003);
            Assert.Equal(string.Empty, fileType);

            string fileType2 = _converter.Convert((FileFormat)(-3));
            Assert.Equal(string.Empty, fileType2);
        }
    }
}
