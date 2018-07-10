using System.Text;
using NetCore.Utils.Conversion;
using Xunit;

namespace NetCore.Utils.Tests.Conversion
{
    public class StringExtensionsTest
    {
        [Fact]
        public void StringToBytes_UTF8()
        {
            const string str = "Test String";
            var bytes = Encoding.UTF8.GetBytes(str);

            var result = str.StringToBytes();

            Assert.Equal(bytes, result);
        }

        [Fact]
        public void StringToBytes_ASCII()
        {
            const string str = "Test String";
            var bytes = Encoding.ASCII.GetBytes(str);

            var result = str.StringToBytes();

            Assert.Equal(bytes, result);
        }

        [Fact]
        public void Base64StringToBytes()
        {
            const string str = "Test String";
            var bytes = Encoding.UTF8.GetBytes(str);

            //VGVzdCBTdHJpbmc=由其他转换工具生成
            var result = "VGVzdCBTdHJpbmc=".Base64StringToBytes();

            Assert.Equal(bytes, result);
        }
    }
}