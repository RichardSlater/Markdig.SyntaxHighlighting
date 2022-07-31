using Xunit;

namespace MDS.Markdig.SyntaxHighlighting.Tests {
    public class LanguageTypeAdapterTests {
        private const string AspxCsFirstLine = "<%@ Page Language=\"C#\" %>";

        [Theory]
        [InlineData("csharp", "c#", null)]
        [InlineData("cplusplus", "cpp", null)]
        [InlineData("css", "css", null)]
        [InlineData("aspx", "aspx(c#)", AspxCsFirstLine)]
        [InlineData("javascript", "javascript", "var myVar = 1;")]
        public void CanParse(string inputLanguage, string expectedId, string firstLine) {
            var adapter = new LanguageTypeAdapter();
            var result = adapter.Parse(inputLanguage, firstLine);
            Assert.Equal(expectedId, result.Id);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("fubar")]
        public void CanNotParse(string inputLanguage) {
            var adapter = new LanguageTypeAdapter();
            var result = adapter.Parse(inputLanguage);
            Assert.Null(result);
        }
    }
}