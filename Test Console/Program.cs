using Adaptive.Taz;

namespace Test_Console
{
    internal class Program
    {
        private const string ClearTazFile = @"C:\Temp\Test Data\DX\ClearArchive.taz";
        private const string ClearExtractPath = @"C:\Temp\Test Data\Clear Output\";

        private const string SecureTazFile = @"C:\Temp\Test Data\DX\SecureArchive.taz.secure";
        private const string SecureExtractPath = @"C:\Temp\Test Data\Secure Output\";

        static void Main(string[] args)
        {
            MainAsync().Wait();
        }
        private static async Task MainAsync()
        {
            //	await CreateAndReadClearArchiveTestAsync();
            await CreateAndReadSecureArchiveTestAsync();

        }
        private const string UserID = "gpburdell74";
        private const string Pwd = "7329.Wnyhiq";
        private const string UserPIN = "111362";

        private static async Task CreateAndReadSecureArchiveTestAsync()
        {
            string[] fileList = System.IO.Directory.GetFiles(@"C:\Temp\Test Data");

            if (File.Exists(SecureTazFile))
                File.Delete(SecureTazFile);

            TazFile file = new TazFile(UserID, Pwd, UserPIN);
            await file.CreateArchiveAsync(SecureTazFile, fileList);
            file.Dispose();

            file = new TazFile(UserID, Pwd, UserPIN);
            await file.LoadDirectoryContentAsync(SecureTazFile);
            file.Dispose();

            file = new TazFile(UserID, Pwd, UserPIN);
            await file.ExtractFilesAsync(SecureTazFile, SecureExtractPath);
            file.Dispose();
        }
        private static async Task CreateAndReadClearArchiveTestAsync()
        {
            string[] fileList = System.IO.Directory.GetFiles(@"C:\Temp\Test Data");

            if (File.Exists(ClearTazFile))
                File.Delete(ClearTazFile);

            TazFile file = new TazFile();
            await file.CreateArchiveAsync(ClearTazFile, fileList);
            file.Dispose();

            file = new TazFile();
            await file.LoadDirectoryContentAsync(ClearTazFile);
            file.Dispose();

            file = new TazFile();
            await file.ExtractFilesAsync(ClearTazFile, ClearExtractPath);
            file.Dispose();

        }
    }
}
