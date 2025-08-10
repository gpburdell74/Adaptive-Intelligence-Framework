using Adaptive.Intelligence.Shared.IO;
using System.Text;

namespace Adaptive.Intelligence.Shared.Test.IO
{
    public class TextFileTests
    {
        [Fact]
        public void ConstructorTest()
        {
            var file = new TextFile();
            file.Dispose();
        }
        [Fact]
        public void ConstructorTest2()
        {
            var file = new TextFile(@"C:\Temp\TextFIleText.txt");
            file.Dispose();
        }
        [Fact]
        public void DisposeTest()
        {
            string fileName = FileNameRenderer.RenderFileNameInUserPath("TestFileText.txt");
            
            var file = new TextFile(fileName);
            file.Dispose();
            file.Dispose();
            file.Dispose();
            file.Dispose();
        }

        [Fact]
        public void CreateTest()
        {
            var fileName = @"C:\Temp\TextFIleText.txt";

            if (File.Exists(fileName))
                File.Delete(fileName);

            var file = new TextFile(fileName);
            file.Create();
            file.Close();
            file.Dispose();

            Assert.True(File.Exists(fileName));
            File.Delete(fileName);
        }
        [Fact]
        public void CreateTestPart2()
        {
            var fileName =
                FileNameRenderer.RenderFileNameInUserPath("TestFileTest2.txt");
            
            if (File.Exists(fileName))
                File.Delete(fileName);

            var file = new TextFile();
            file.Create(fileName);
            file.Close();
            file.Dispose();

            Assert.True(File.Exists(fileName));
            File.Delete(fileName);
        }

        [Fact]
        public void DeleteTest()
        {
            var fileName = $@"{System.Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\DeleteTextFileText.txt";
            if (!System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows))
                fileName = $@"{System.Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}/DeleteTextFileText.txt";
            var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            var writer = new StreamWriter(fileStream);
            writer.Write(DateTime.Now.ToString());
            writer.Dispose();
            fileStream.Dispose();

            var textFile = new TextFile(fileName);
            var deleted = textFile.Delete();
            textFile.Dispose();
            Assert.True(deleted);

            Assert.False(File.Exists(fileName));
        }

        [Fact]
        public void DeleteTest2()
        {
            var fileName = FileNameRenderer.RenderFileNameInUserPath(
                @"DeleteTextFileText.txt");

            var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            var w = new StreamWriter(fs);
            w.Write(DateTime.Now.ToString());
            w.Dispose();
            fs.Dispose();

            var textFile = new TextFile();
            var deleted = textFile.Delete(fileName);
            textFile.Dispose();
            Assert.True(deleted);

            Assert.False(File.Exists(fileName));
        }

        [Fact]
        public void DeleteInvalidFileTest()
        {
            var fileName = System.Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) +
                @"\DeleteTextFileText.txt";
            if (File.Exists(fileName))
                File.Delete(fileName);

            var textFile = new TextFile();
            var deleted = textFile.Delete(fileName);
            textFile.Dispose();
            Assert.True(deleted);

            Assert.False(File.Exists(fileName));
        }

        [Fact]
        public void IsOpenTest()
        {
            var fileName = FileNameRenderer.RenderFileNameInUserPath("IsOpenTestFileText.txt");

            var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            var w = new StreamWriter(fs);
            w.Write(DateTime.Now.ToString());
            w.Dispose();
            fs.Dispose();

            var textFile = new TextFile(fileName);
            var open = textFile.IsOpen;
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
            var fileName = FileNameRenderer.RenderFileNameInUserPath(
                @"IsOpenForReadTextFileText.txt");

            var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            var w = new StreamWriter(fs);
            w.Write(DateTime.Now.ToString());
            w.Dispose();
            fs.Dispose();

            var textFile = new TextFile(fileName);
            var open = textFile.IsOpen;
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
            var fileName = FileNameRenderer.RenderFileNameInUserPath(
                @"IsOpenForWriteTextFileText.txt");

            var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            var w = new StreamWriter(fs);
            w.Write(DateTime.Now.ToString());
            w.Dispose();
            fs.Dispose();

            var textFile = new TextFile(fileName);
            var open = textFile.IsOpen;
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
            string fileName = string.Empty;
            
            if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows))
                fileName = System.Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) +
                @"\FileNameTextFileText.txt";
            else
                fileName = System.Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) +
                    "/FileNameTextFileText.txt";
            
            var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            var w = new StreamWriter(fs);
            w.Write(DateTime.Now.ToString());
            w.Dispose();
            fs.Dispose();

            var textFile = new TextFile(fileName);
            Assert.Equal(fileName, textFile.FileName);
            textFile.Close();
            textFile.Delete();
            textFile.Dispose();
        }

        [Fact]
        public void ReadTest()
        {
            var fileName = FileNameRenderer.RenderFileNameInUserPath(
                @"ReadLinesTextFileText.txt");

            var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            var w = new StreamWriter(fs);
            w.WriteLine(DateTime.Now.ToString());
            w.WriteLine(DateTime.Now.ToString());
            w.WriteLine(DateTime.Now.ToString());
            w.Dispose();
            fs.Dispose();

            var textFile = new TextFile(fileName);
            var open = textFile.IsOpen;
            Assert.False(open);

            textFile.OpenForRead();
            Assert.True(textFile.IsOpen);
            Assert.True(textFile.CanRead);

            var lineA = textFile.ReadLine();
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
            var fileName = FileNameRenderer.RenderFileNameInUserPath(
                @"WriteLinesTextFileText.txt");

            var textFile = new TextFile(fileName);
            textFile.OpenForWrite();
            Assert.True(textFile.IsOpen);
            Assert.True(textFile.CanWrite);

            textFile.WriteLine(DateTime.Now.ToString());
            textFile.WriteLine(DateTime.Now.ToString());
            textFile.WriteLine(DateTime.Now.ToString());

            textFile.Close();
            textFile.Dispose();

            var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            var r = new StreamReader(fs);

            var data = r.ReadLine();
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
            var fileName = FileNameRenderer.RenderFileNameInUserPath(@"ReadAllTextFileText.txt");

            var builder = new StringBuilder();
            builder.AppendLine(DateTime.Now.ToString());
            builder.AppendLine(DateTime.Now.ToString());
            builder.AppendLine(DateTime.Now.ToString());

            var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            var w = new StreamWriter(fs);
            w.Write(builder.ToString());
            w.Dispose();
            fs.Dispose();

            var textFile = new TextFile(fileName);
            var open = textFile.IsOpen;
            Assert.False(open);

            textFile.OpenForRead();
            Assert.True(textFile.IsOpen);
            Assert.True(textFile.CanRead);

            var data = textFile.ReadAll();
            Assert.NotNull(data);
            Assert.Equal(builder.ToString(), data);

            textFile.Close();
            textFile.Delete(fileName);
            textFile.Dispose();
        }
    }
}
