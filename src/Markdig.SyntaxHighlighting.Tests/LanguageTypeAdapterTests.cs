using Xunit;

namespace Markdig.SyntaxHighlighting.Tests {
    public class LanguageTypeAdapterTests {
        [Theory]
        [InlineData("csharp", "c#")]
        public void CanParseCSharp(string inputLanguage, string expectedId) {
            var adapter = new LanguageTypeAdapter();
            var result = adapter.Parse(inputLanguage);
            Assert.Equal(expectedId, result.Id);
        }
    }
}