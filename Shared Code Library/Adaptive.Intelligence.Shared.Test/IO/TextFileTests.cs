using Adaptive.Intelligence.Shared.IO;
using System.Text;

namespace Adaptive.Intelligence.Shared.Test.IO
{
    public class TextFileTests
    {
        [Fact]
        public void ConstructorTest()
        {
            TextFile file = new TextFile();
            file.Dispose();
        }
        [Fact]
        public void ConstructorTest2()
        {
            TextFile file = new TextFile(@"C:\Temp\TextFIleText.txt");
            file.Dispose();
        }
        [Fact]
        public void DisposeTest()
        {
            TextFile file = new TextFile(@"C:\Temp\TextFIleText.txt");
            file.Dispose();
            file.Dispose();
            file.Dispose();
            file.Dispose();
        }

        [Fact]
        public void CreateTest()
        {
            string fileName = @"C:\Temp\TextFIleText.txt";

            if (File.Exists(fileName))
                File.Delete(fileName);

            TextFile file = new TextFile(fileName);
            file.Create();
            file.Close();
            file.Dispose();

            Assert.True(File.Exists(fileName));
            File.Delete(fileName);
        }
        [Fact]
        public void CreateTestPart2()
        {
            string fileName = @"C:\Temp\TextFileText2.txt";

            if (File.Exists(fileName))
                File.Delete(fileName);

            TextFile file = new TextFile();
            file.Create(fileName);
            file.Close();
            file.Dispose();

            Assert.True(File.Exists(fileName));
            File.Delete(fileName);
        }

        [Fact]
        public void DeleteTest()
        {
            string fileName = System.Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) +
                @"\DeleteTextFileText.txt";

            FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            StreamWriter w = new StreamWriter(fs);
            w.Write(DateTime.Now.ToString());
            w.Dispose();
            fs.Dispose();

            TextFile textFile = new TextFile(fileName);
            bool deleted = textFile.Delete();
            textFile.Dispose();
            Assert.True(deleted);

            Assert.False(File.Exists(fileName));
        }

        [Fact]
        public void DeleteTest2()
        {
            string fileName = System.Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) +
                @"\DeleteTextFileText.txt";

            FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            StreamWriter w = new StreamWriter(fs);
            w.Write(DateTime.Now.ToString());
            w.Dispose();
            fs.Dispose();

            TextFile textFile = new TextFile();
            bool deleted = textFile.Delete(fileName);
            textFile.Dispose();
            Assert.True(deleted);

            Assert.False(File.Exists(fileName));
        }

        [Fact]
        public void DeleteInvalidFileTest()
        {
            string fileName = System.Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) +
                @"\DeleteTextFileText.txt";
            if (File.Exists(fileName))
                File.Delete(fileName);

            TextFile textFile = new TextFile();
            bool deleted = textFile.Delete(fileName);
            textFile.Dispose();
            Assert.True(deleted);

            Assert.False(File.Exists(fileName));
        }

        [Fact]
        public void IsOpenTest()
        {
            string fileName = System.Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) +
                @"\IsOpenTextFileText.txt";

            FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            StreamWriter w = new StreamWriter(fs);
            w.Write(DateTime.Now.ToString());
            w.Dispose();
            fs.Dispose();

            TextFile textFile = new TextFile(fileName);
            bool open = textFile.IsOpen;
            Assert.False(open);

            textFile.OpenForRead();
            Assert.True(textFile.IsOpen);

            textFile.Close();
            Assert.False(textFile.IsOpen);

            textFile.Delete();
            textFile.Dispose();
        }

        [Fact]
        public void OpenForReadingTest()
        {
            string fileName = System.Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) +
                @"\IsOpenForReadTextFileText.txt";

            FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            StreamWriter w = new StreamWriter(fs);
            w.Write(DateTime.Now.ToString());
            w.Dispose();
            fs.Dispose();

            TextFile textFile = new TextFile(fileName);
            bool open = textFile.IsOpen;
            Assert.False(open);

            textFile.OpenForRead();
            Assert.True(textFile.IsOpen);
            Assert.True(textFile.CanRead);

            textFile.Close();
            Assert.False(textFile.IsOpen);
            Assert.False(textFile.CanRead);

            textFile.Delete();
            textFile.Dispose();

        }

        [Fact]
        public void OpenForWritingTest()
        {
            string fileName = System.Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) +
                @"\IsOpenForWriteTextFileText.txt";

            FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            StreamWriter w = new StreamWriter(fs);
            w.Write(DateTime.Now.ToString());
            w.Dispose();
            fs.Dispose();

            TextFile textFile = new TextFile(fileName);
            bool open = textFile.IsOpen;
            Assert.False(open);

            textFile.OpenForWrite();
            Assert.True(textFile.IsOpen);
            Assert.True(textFile.CanWrite);

            textFile.Close();
            Assert.False(textFile.IsOpen);
            Assert.False(textFile.CanWrite);

            textFile.Delete();
            textFile.Dispose();

        }

        [Fact]
        public void FileNameTest()
        {
            string fileName = System.Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) +
                @"\FileNameTextFileText.txt";

            FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            StreamWriter w = new StreamWriter(fs);
            w.Write(DateTime.Now.ToString());
            w.Dispose();
            fs.Dispose();

            TextFile textFile = new TextFile(fileName);
            Assert.Equal(fileName, textFile.FileName);
            textFile.Close();
            textFile.Delete();
            textFile.Dispose();
        }

        [Fact]
        public void ReadTest()
        {
            string fileName = System.Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) +
    @"\ReadLinesTextFileText.txt";

            FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            StreamWriter w = new StreamWriter(fs);
            w.WriteLine(DateTime.Now.ToString());
            w.WriteLine(DateTime.Now.ToString());
            w.WriteLine(DateTime.Now.ToString());
            w.Dispose();
            fs.Dispose();

            TextFile textFile = new TextFile(fileName);
            bool open = textFile.IsOpen;
            Assert.False(open);

            textFile.OpenForRead();
            Assert.True(textFile.IsOpen);
            Assert.True(textFile.CanRead);

            string? lineA = textFile.ReadLine();
            Assert.NotNull(lineA);

            lineA = textFile.ReadLine();
            Assert.NotNull(lineA);

            lineA = textFile.ReadLine();
            Assert.NotNull(lineA);

            textFile.Close();

            textFile.Delete(fileName);

            textFile.Dispose();


        }

        [Fact]
        public void WriteTest()
        {
            string fileName = System.Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) +
            @"\WriteLinesTextFileText.txt";

            TextFile textFile = new TextFile(fileName);
            textFile.OpenForWrite();
            Assert.True(textFile.IsOpen);
            Assert.True(textFile.CanWrite);

            textFile.WriteLine(DateTime.Now.ToString());
            textFile.WriteLine(DateTime.Now.ToString());
            textFile.WriteLine(DateTime.Now.ToString());

            textFile.Close();
            textFile.Dispose();

            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader r = new StreamReader(fs);

            string? data = r.ReadLine();
            Assert.NotNull(data);
            data = r.ReadLine();
            Assert.NotNull(data);
            data = r.ReadLine();
            Assert.NotNull(data);
            r.Dispose();
            fs.Dispose();
            File.Delete(fileName);
        }

        [Fact]
        public void ReadAllTest()
        {
            string fileName = System.Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) +
    @"\ReadAllTextFileText.txt";

            StringBuilder builder = new StringBuilder();
            builder.AppendLine(DateTime.Now.ToString());
            builder.AppendLine(DateTime.Now.ToString());
            builder.AppendLine(DateTime.Now.ToString());

            FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            StreamWriter w = new StreamWriter(fs);
            w.Write(builder.ToString());
            w.Dispose();
            fs.Dispose();

            TextFile textFile = new TextFile(fileName);
            bool open = textFile.IsOpen;
            Assert.False(open);

            textFile.OpenForRead();
            Assert.True(textFile.IsOpen);
            Assert.True(textFile.CanRead);

            string? data = textFile.ReadAll();
            Assert.NotNull(data);
            Assert.Equal(builder.ToString(), data);

            textFile.Close();
            textFile.Delete(fileName);
            textFile.Dispose();
        }
    }
}
