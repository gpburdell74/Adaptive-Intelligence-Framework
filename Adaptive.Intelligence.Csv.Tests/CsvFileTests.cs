namespace Adaptive.Intelligence.Csv.Tests
{
    public class CsvFileTests : IDisposable
    {
        private string _testFile;
        private string _testFileWithHeader;
        private string _outputFile;
        private CsvFile _csvFile;

        public CsvFileTests()
        {
            _testFile = Path.GetTempFileName();
            _testFileWithHeader = Path.GetTempFileName();
            _outputFile = Path.GetTempFileName();
            File.WriteAllLines(_testFile, new[] { "1,Alpha", "2,Beta" });
            File.WriteAllLines(_testFileWithHeader, new[] { "Id,Name", "1,Alpha", "2,Beta" });
            _csvFile = new CsvFile(_testFile);
        }

        public void Dispose()
        {
            _csvFile.Dispose();
            if (File.Exists(_testFile)) File.Delete(_testFile);
            if (File.Exists(_testFileWithHeader)) File.Delete(_testFileWithHeader);
            if (File.Exists(_outputFile)) File.Delete(_outputFile);
        }

        [Fact]
        public void Constructor_InitializesFileName()
        {
            Assert.Equal(_testFile, _csvFile.FileName);
        }

        [Fact]
        public void LoadContent_LoadsRowsWithoutHeader()
        {
            _csvFile.LoadContent(_testFile);
            Assert.Equal(2, _csvFile.RowCount);
            Assert.Equal(2, _csvFile.ColumnCount);
        }

        [Fact]
        public void LoadContent_LoadsRowsWithHeader()
        {
            _csvFile.LoadContent(_testFileWithHeader, true);
            Assert.Equal(2, _csvFile.RowCount);
            Assert.Equal(2, _csvFile.ColumnCount);
        }

        [Fact]
        public async Task LoadContentAsync_LoadsRowsWithHeader()
        {
            await _csvFile.LoadContentAsync(_testFileWithHeader, true);
            Assert.Equal(2, _csvFile.RowCount);
            Assert.Equal(2, _csvFile.ColumnCount);
        }

        [Fact]
        public void SaveAs_SavesLoadedContent()
        {
            _csvFile.LoadContent(_testFile);
            _csvFile.SaveAs(_outputFile);
            Assert.True(File.Exists(_outputFile));
            var lines = File.ReadAllLines(_outputFile);
            Assert.Equal(new[] { "1,Alpha", "2,Beta" }, lines);
        }

        [Fact]
        public void ValidateColumnCounts_ReturnsEmptyForValidRows()
        {
            _csvFile.LoadContent(_testFile);
            var invalidRows = _csvFile.ValidateColumnCounts();
            Assert.Empty(invalidRows);
        }

        [Fact]
        public void ValidateColumnCounts_ReturnsInvalidRows()
        {
            var badFile = Path.GetTempFileName();
            File.WriteAllLines(badFile, new[] { "1,Alpha", "2" });
            var file = new CsvFile(badFile);
            file.LoadContent(badFile);
            var invalidRows = file.ValidateColumnCounts();
            Assert.Single(invalidRows);
            Assert.Equal(1, invalidRows[0]);
            file.Dispose();
            File.Delete(badFile);
        }

        [Fact]
        public void Properties_AreCorrect()
        {
            _csvFile.LoadContent(_testFile);
            Assert.Equal(_testFile, _csvFile.FileName);
            Assert.True(_csvFile.Length > 0);
            Assert.Equal(2, _csvFile.RowCount);
            Assert.Equal(2, _csvFile.ColumnCount);
            Assert.Equal(65536, _csvFile.MaximumCellSize);
        }

        [Fact]
        public void Close_ClearsData()
        {
            _csvFile.LoadContent(_testFile);
            _csvFile.Close();
            Assert.Equal(-1, _csvFile.RowCount);
            Assert.Equal(-1, _csvFile.ColumnCount);
        }

        [Fact]
        public void CompareTo_ReturnsExpected()
        {
            var fileA = new CsvFile(_testFile);
            var fileB = new CsvFile(_testFile);
            Assert.Equal(0, fileA.CompareTo(fileB));
        }
    }
}