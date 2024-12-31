using Adaptive.Intelligence.Shared.IO;
using AutoFixture;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adaptive.Intelligence.Shared.Test.IO
{
    public class SafeBinaryWriterTests
    {
        [Fact]
        public void Test()
        {
            MemoryStream ms = new MemoryStream();
            SafeBinaryWriter writer = new SafeBinaryWriter(ms);

            writer.Write(true);
            writer.Write(false);
            writer.Flush();

            ms.Position = 0;
            SafeBinaryReader reader = new SafeBinaryReader(ms);
            bool value = reader.ReadBoolean();
            Assert.True(value);
            value = reader.ReadBoolean();
            Assert.False(value);

            writer.Dispose();
            ms.Dispose();
            writer.Dispose();
        }

    }
}
