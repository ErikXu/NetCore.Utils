using System.Text;
using NetCore.Utils.Conversion;
using Xunit;

namespace NetCore.Utils.Tests.Conversion
{
    public class BytesExtensionsTests
    {
        [Fact]
        public void BytesToString_UTF8()
        {
            const string str = "Test String";
            var bytes = Encoding.UTF8.GetBytes(str);

            var result = bytes.BytesToString();

            Assert.Equal(str, result);
        }

        [Fact]
        public void BytesToString_ASCII()
        {
            const string str = "Test String";
            var bytes = Encoding.ASCII.GetBytes(str);

            var result = bytes.BytesToString();

            Assert.Equal(str, result);
        }

        [Fact]
        public void BytesToBase64()
        {
            const string str = "Test String";
            var bytes = Encoding.UTF8.GetBytes(str);

            var result = bytes.BytesToBase64();

            //VGVzdCBTdHJpbmc=由其他转换工具生成
            Assert.Equal("VGVzdCBTdHJpbmc=", result);
        }

        [Fact]
        public void BytesToHex()
        {
            const string str = "Test String";
            var bytes = Encoding.UTF8.GetBytes(str);

            var result = bytes.BytesToHex();

            //5465737420537472696e67由其他转换工具生成
            Assert.Equal("5465737420537472696e67", result);
        }

        [Fact]
        public void BytesToHex_Upper()
        {
            const string str = "Test String";
            var bytes = Encoding.UTF8.GetBytes(str);

            var result = bytes.BytesToHex(true);

            Assert.Equal("5465737420537472696E67", result);
        }
    }
}