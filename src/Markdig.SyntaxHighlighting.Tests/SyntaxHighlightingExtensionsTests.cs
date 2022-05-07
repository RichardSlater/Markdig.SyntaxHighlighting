using System;
using System.IO;
using Markdig.Renderers;
using Markdig.Renderers.Html;
using Xunit;

namespace MarkdownServer.Markdig.SyntaxHighlighting.Tests {
    public class SyntaxHighlightingExtensionsTests {
        private class FakeRenderer : TextRendererBase<FakeRenderer> {
            public FakeRenderer(TextWriter writer) : base(writer) {}
        }

        [Fact]
        public void CodeBlockRendererReplaced() {
            var extension = new SyntaxHighlightingExtension();
            var writer = new StringWriter();
            var markdownRenderer = new HtmlRenderer(writer);

            var oldRendererCount = markdownRenderer.ObjectRenderers.Count;
            Assert.Equal(1,
                markdownRenderer.ObjectRenderers.FindAll(x => x.GetType() == typeof(CodeBlockRenderer)).Count);
            extension.Setup(null, markdownRenderer);
            Assert.Equal(0,
                markdownRenderer.ObjectRenderers.FindAll(x => x.GetType() == typeof(CodeBlockRenderer)).Count);
            Assert.Equal(1,
                markdownRenderer.ObjectRenderers.FindAll(x => x.GetType() == typeof(SyntaxHighlightingCodeBlockRenderer))
                    .Count);
            Assert.Equal(oldRendererCount, markdownRenderer.ObjectRenderers.Count);
        }

        [Fact]
        public void DoesntThrowWhenSetupPipeline() {
            var extension = new SyntaxHighlightingExtension();
            extension.Setup(new MarkdownPipelineBuilder());
        }

        [Fact]
        public void PipelineChangedIfHtmlRenderer() {
            var extension = new SyntaxHighlightingExtension();
            var writer = new StringWriter();
            var markdownRenderer = new HtmlRenderer(writer);
            markdownRenderer.ObjectRenderers.RemoveAll(x => true);
            extension.Setup(null, markdownRenderer);
            Assert.Equal(1, markdownRenderer.ObjectRenderers.Count);
        }

        [Fact]
        public void PipelineChangedIfHtmlRendererUsingExtensionMethod() {
            var pipelineBuilder = new MarkdownPipelineBuilder();
            pipelineBuilder.UseSyntaxHighlighting();
            var pipeline = pipelineBuilder.Build();
            var writer = new StringWriter();
            var markdownRenderer = new HtmlRenderer(writer);
            pipeline.Setup(markdownRenderer);
            var renderer = markdownRenderer.ObjectRenderers.FindExact<SyntaxHighlightingCodeBlockRenderer>();
            Assert.NotNull(renderer);
        }

        [Fact]
        public void PipelineIntactIfNotHtmlRenderer() {
            var extension = new SyntaxHighlightingExtension();
            var writer = new StringWriter();
            var markdownRenderer = new FakeRenderer(writer);
            var oldRendererCount = markdownRenderer.ObjectRenderers.Count;
            extension.Setup(null, markdownRenderer);
            Assert.Equal(oldRendererCount, markdownRenderer.ObjectRenderers.Count);
        }

        [Fact]
        public void ThrowsIfRendererIsNull() {
            var extension = new SyntaxHighlightingExtension();
            var extensionSetup = new Action(() => extension.Setup(null, null));
            Assert.Throws<ArgumentNullException>(extensionSetup);
        }
    }
}