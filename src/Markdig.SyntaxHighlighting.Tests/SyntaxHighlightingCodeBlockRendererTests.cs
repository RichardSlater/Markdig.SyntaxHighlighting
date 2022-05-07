using System.IO;
using System.Text;
using Markdig.Helpers;
using Markdig.Parsers;
using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Syntax;
using Moq;
using Xunit;

namespace MarkdownServer.Markdig.SyntaxHighlighting.Tests {
    public class SyntaxHighlightingCodeBlockRendererTests {
        public string scriptBlock = @"```csharp
var desktop = Environment.SpecialFolder.DesktopDirectory;
```";

        private static FencedCodeBlock GetFencedCodeBlock(string language = "language-csharp") {
            return new FencedCodeBlock(new FencedCodeBlockParser()) {
                Info = language,
                Lines = new StringLineGroup(3) {
                    new StringSlice("```csharp"),
                    new StringSlice("var desktop = Environment.SpecialFolder.DesktopDirectory;"),
                    new StringSlice("```")
                }
            };
        }

        [Fact]
        public void ConstructorDoesNotThrow() {
            var renderer = new SyntaxHighlightingCodeBlockRenderer();
            Assert.NotNull(renderer);
        }

        [Fact]
        public void DivWritten() {
            var underlyingRendererMock = new Mock<CodeBlockRenderer>();
            underlyingRendererMock
                .Setup(x => x.Write(It.IsAny<HtmlRenderer>(), It.IsAny<CodeBlock>()));
            var renderer = new SyntaxHighlightingCodeBlockRenderer(underlyingRendererMock.Object);
            var builder = new StringBuilder();
            var markdownRenderer = new HtmlRenderer(new StringWriter(builder));
            var codeBlock = GetFencedCodeBlock();
            renderer.Write(markdownRenderer, codeBlock);
            Assert.Contains("<div", builder.ToString());
            Assert.Contains("</div>", builder.ToString());
        }

        [Fact]
        public void DivWrittenUnrecognisedLanguage()
        {
            var underlyingRendererMock = new Mock<CodeBlockRenderer>();
            underlyingRendererMock
                .Setup(x => x.Write(It.IsAny<HtmlRenderer>(), It.IsAny<CodeBlock>()));
            var renderer = new SyntaxHighlightingCodeBlockRenderer(underlyingRendererMock.Object);
            var builder = new StringBuilder();
            var markdownRenderer = new HtmlRenderer(new StringWriter(builder));
            var codeBlock = GetFencedCodeBlock("language-made-up-language"); //
            renderer.Write(markdownRenderer, codeBlock);
            Assert.Contains("<div", builder.ToString());
            Assert.Contains("</div>", builder.ToString());
        }

        [Fact]
        public void EditorColorsCssClassAdded() {
            var underlyingRendererMock = new Mock<CodeBlockRenderer>();
            underlyingRendererMock
                .Setup(x => x.Write(It.IsAny<HtmlRenderer>(), It.IsAny<CodeBlock>()));
            var renderer = new SyntaxHighlightingCodeBlockRenderer(underlyingRendererMock.Object);
            var builder = new StringBuilder();
            var markdownRenderer = new HtmlRenderer(new StringWriter(builder));
            var codeBlock = GetFencedCodeBlock();
            renderer.Write(markdownRenderer, codeBlock);
            Assert.Contains("editor-colors", builder.ToString());
        }

        [Fact]
        public void LangCssClassAdded() {
            var underlyingRendererMock = new Mock<CodeBlockRenderer>();
            underlyingRendererMock
                .Setup(x => x.Write(It.IsAny<HtmlRenderer>(), It.IsAny<CodeBlock>()));
            var renderer = new SyntaxHighlightingCodeBlockRenderer(underlyingRendererMock.Object);
            var builder = new StringBuilder();
            var markdownRenderer = new HtmlRenderer(new StringWriter(builder));
            var codeBlock = GetFencedCodeBlock();
            renderer.Write(markdownRenderer, codeBlock);
            Assert.Contains("lang-csharp", builder.ToString());
        }

        [Fact]
        public void UnderlyingRendererCalledIfNotFencedCodeBlock() {
            var underlyingRendererMock = new Mock<CodeBlockRenderer>();
            underlyingRendererMock
                .Setup(x => x.Write(It.IsAny<HtmlRenderer>(), It.IsAny<CodeBlock>()))
                .Verifiable("Write was not called on the underlying renderer mock.");
            var renderer = new SyntaxHighlightingCodeBlockRenderer(underlyingRendererMock.Object);
            var writer = new StringWriter();
            var markdownRenderer = new HtmlRenderer(writer);
            var codeBlock = new CodeBlock(new IndentedCodeBlockParser());
            renderer.Write(markdownRenderer, codeBlock);
            underlyingRendererMock.VerifyAll();
        }

        [Fact]
        public void WritesOutCode() {
            var underlyingRendererMock = new Mock<CodeBlockRenderer>();
            underlyingRendererMock
                .Setup(x => x.Write(It.IsAny<HtmlRenderer>(), It.IsAny<CodeBlock>()));
            var renderer = new SyntaxHighlightingCodeBlockRenderer(underlyingRendererMock.Object);
            var builder = new StringBuilder();
            var markdownRenderer = new HtmlRenderer(new StringWriter(builder));
            var codeBlock = GetFencedCodeBlock();
            renderer.Write(markdownRenderer, codeBlock);
            Assert.Contains("var", builder.ToString());
        }

        [Fact]
        public void WritesOutColouredCode() {
            var underlyingRendererMock = new Mock<CodeBlockRenderer>();
            underlyingRendererMock
                .Setup(x => x.Write(It.IsAny<HtmlRenderer>(), It.IsAny<CodeBlock>()));
            var renderer = new SyntaxHighlightingCodeBlockRenderer(underlyingRendererMock.Object);
            var builder = new StringBuilder();
            var markdownRenderer = new HtmlRenderer(new StringWriter(builder));
            var codeBlock = GetFencedCodeBlock();
            renderer.Write(markdownRenderer, codeBlock);
            Assert.Contains("<span style=\"color:Blue;\">var</span>", builder.ToString());
        }
    }
}