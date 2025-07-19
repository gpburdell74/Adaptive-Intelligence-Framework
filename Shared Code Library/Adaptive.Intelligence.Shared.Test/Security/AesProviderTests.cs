using Adaptive.Intelligence.Shared.Security;

namespace Adaptive.Intelligence.Shared.Test.Security
{
    public class AesProviderTests
    {
        [Fact]
        public void CreateTest()
        {
            AesProvider provider = new AesProvider();
            Assert.NotNull(provider);
            provider.Dispose();

        }

        [Fact]
        public void DisposeSafetyTest()
        {
            AesProvider provider = new AesProvider();
            Assert.NotNull(provider);

            provider.Dispose();
            provider.Dispose();
            provider.Dispose();
            provider.GenerateNewKey();
            provider.Dispose();
            provider.GenerateNewKey();
            provider.Dispose();
            provider.Dispose();
            provider.Dispose();
            provider.Dispose();

        }
        [Fact]
        public void DecryptTest()
        {
            AesProvider provider = new AesProvider();

            byte[] originalData = new byte[] { 1, 2, 3, 4, 5, 6, 7 };

            byte[]? encrypted = provider.Encrypt(originalData);

            Assert.NotNull(encrypted);
            byte[]? decrypted = provider.Decrypt(encrypted);

            Assert.NotNull(decrypted);
            Assert.Equal(originalData.Length, decrypted.Length);

            for (int count = 0; count < originalData.Length; count++)
            {
                Assert.Equal(originalData[count], decrypted[count]);
            }
            provider.Dispose();
        }
        [Fact]
        public void EncryptTest()
        {
            AesProvider provider = new AesProvider();

            byte[] originalData = new byte[] { 111, 112, 123, 4, 5, 6, 7 };

            byte[]? encrypted = provider.Encrypt(originalData);

            Assert.NotNull(encrypted);

        }
        [Fact]
        public void EncryptFullDataTest()
        {
            AesProvider provider = new AesProvider();

            byte[] originalData = new byte[256];
            for (int count = 0; count < 256; count++)
                originalData[count] = 255;

            byte[]? encrypted = provider.Encrypt(originalData);

            Assert.NotNull(encrypted);

        }
        [Fact]
        public void EncryptMinDataTest()
        {
            AesProvider provider = new AesProvider();

            byte[] originalData = new byte[256];
            Array.Clear(originalData, 0, 0);

            byte[]? encrypted = provider.Encrypt(originalData);

            Assert.NotNull(encrypted);

        }

        [Fact]
        public void DecryptFromBase64StringTest()
        {
            byte[] content = GetRandomBytes(1024);

            AesProvider provider = new AesProvider();
            byte[]? encrypted = provider.Encrypt(content);
            string encrypted64 = Convert.ToBase64String(encrypted);

            byte[]? results = provider.DecryptFromBase64String(encrypted64);

            Assert.NotNull(results);
            for (int count = 0; count < content.Length; count++)
            {
                Assert.Equal(content[count], results[count]);
            }
            provider.Dispose();
        }

        [Fact]
        public void GetKeyTest()
        {
            AesProvider provider = new AesProvider();
            string? keyIv = provider.GetKey();
            Assert.NotNull(keyIv);
            Assert.Equal(64, keyIv.Length);

            byte[]? keyData = Convert.FromBase64String(keyIv);
            provider.Dispose();
        }

        [Fact]
        public void GetKeyDataTest()
        {
            AesProvider provider = new AesProvider();

            byte[]? data = provider.GetKeyData();
            Assert.NotNull(data);
            Assert.Equal(48, data.Length);

            provider.Dispose();

        }

        [Fact]
        public void SetKeyDataFromBase64Test()
        {
            AesProvider provider = new AesProvider();

            byte[] data = GetRandomBytes(48);
            string keyData = Convert.ToBase64String(data);
            provider.SetKeyIVFromBase64String(keyData);

            provider.Dispose();

        }

        private static byte[] GetRandomBytes(int length)
        {
            byte[] data = new byte[length];
            Random.Shared.NextBytes(data);
            return data;
        }


    }
}
