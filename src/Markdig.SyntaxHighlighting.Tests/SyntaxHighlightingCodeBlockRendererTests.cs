using System.IO;
using System.Text;
using Markdig.Parsers;
using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Syntax;
using Moq;
using Xunit;

namespace Markdig.SyntaxHighlighting.Tests {
    public class SyntaxHighlightingCodeBlockRendererTests {
        [Fact]
        public void ConstructorDoesNotThrow() {
            var renderer = new SyntaxHighlightingCodeBlockRenderer();
            Assert.NotNull(renderer);
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
        public void EditorColorsCssClassAdded()
        {
            var underlyingRendererMock = new Mock<CodeBlockRenderer>();
            underlyingRendererMock
                .Setup(x => x.Write(It.IsAny<HtmlRenderer>(), It.IsAny<CodeBlock>()));
            var renderer = new SyntaxHighlightingCodeBlockRenderer(underlyingRendererMock.Object);
            var builder = new StringBuilder();
            var markdownRenderer = new HtmlRenderer(new StringWriter(builder));
            var codeBlock = new FencedCodeBlock(new FencedCodeBlockParser()) { Info = "language-csharp" };
            renderer.Write(markdownRenderer, codeBlock);
            Assert.Contains("editor-colors", builder.ToString());
        }

        [Fact]
        public void LangCssClassAdded()
        {
            var underlyingRendererMock = new Mock<CodeBlockRenderer>();
            underlyingRendererMock
                .Setup(x => x.Write(It.IsAny<HtmlRenderer>(), It.IsAny<CodeBlock>()));
            var renderer = new SyntaxHighlightingCodeBlockRenderer(underlyingRendererMock.Object);
            var builder = new StringBuilder();
            var markdownRenderer = new HtmlRenderer(new StringWriter(builder));
            var codeBlock = new FencedCodeBlock(new FencedCodeBlockParser()) {Info = "language-csharp"};
            renderer.Write(markdownRenderer, codeBlock);
            Assert.Contains("lang-csharp", builder.ToString());
        }

        [Fact]
        public void DivWritten()
        {
            var underlyingRendererMock = new Mock<CodeBlockRenderer>();
            underlyingRendererMock
                .Setup(x => x.Write(It.IsAny<HtmlRenderer>(), It.IsAny<CodeBlock>()));
            var renderer = new SyntaxHighlightingCodeBlockRenderer(underlyingRendererMock.Object);
            var builder = new StringBuilder();
            var markdownRenderer = new HtmlRenderer(new StringWriter(builder));
            var codeBlock = new FencedCodeBlock(new FencedCodeBlockParser()) { Info = "language-csharp" };
            renderer.Write(markdownRenderer, codeBlock);
            Assert.Contains("<div", builder.ToString());
            Assert.Contains("</div>", builder.ToString());
        }
    }
}