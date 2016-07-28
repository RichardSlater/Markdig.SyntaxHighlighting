using Markdig.Parsers;
using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Syntax;

namespace Markdig.SyntaxHighlighting {
    public class SyntaxHighlightingCodeBlockRenderer : HtmlObjectRenderer<CodeBlock> {
        private readonly CodeBlockRenderer _underlyingRenderer;

        public SyntaxHighlightingCodeBlockRenderer(CodeBlockRenderer underlyingRenderer = null) {
            _underlyingRenderer = underlyingRenderer ?? new CodeBlockRenderer();
        }

        protected override void Write(HtmlRenderer renderer, CodeBlock obj) {
            var fencedCodeBlock = obj as FencedCodeBlock;
            var parser = obj.Parser as FencedCodeBlockParser;
            if (fencedCodeBlock == null || parser == null)
            {
                _underlyingRenderer.Write(renderer, obj);
                return;
            }

            var attributes = obj.TryGetAttributes() ?? new HtmlAttributes();
            attributes.AddClass("editor-colors");

            var languageClass = fencedCodeBlock.Info.Replace(parser.InfoPrefix, "lang-");
            attributes.AddClass(languageClass);

            renderer
                .Write("<div")
                .WriteAttributes(attributes)
                .Write(">");

            renderer.WriteLine("</div>");
        }
    }
}