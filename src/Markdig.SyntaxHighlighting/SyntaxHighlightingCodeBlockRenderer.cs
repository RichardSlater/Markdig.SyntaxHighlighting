using System.IO;
using System.Text;
using ColorCode;
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
            if (fencedCodeBlock == null || parser == null) {
                _underlyingRenderer.Write(renderer, obj);
                return;
            }

            var attributes = obj.TryGetAttributes() ?? new HtmlAttributes();

            var languageMoniker = fencedCodeBlock.Info.Replace(parser.InfoPrefix, string.Empty);
            attributes.AddClass($"lang-{languageMoniker}");
            attributes.Classes.Remove($"language-{languageMoniker}");

            attributes.AddClass("editor-colors");

            string firstLine;
            var code = GetCode(obj, out firstLine);

            renderer
                .Write("<div")
                .WriteAttributes(attributes)
                .Write(">");

            var markup = ApplySyntaxHighlighting(languageMoniker, firstLine, code);

            renderer.WriteLine(markup);
            renderer.WriteLine("</div>");
        }

        private static string ApplySyntaxHighlighting(string languageMoniker, string firstLine, string code) {
            var languageTypeAdapter = new LanguageTypeAdapter();
            var language = languageTypeAdapter.Parse(languageMoniker, firstLine);
            var codeBuilder = new StringBuilder();
            var codeWriter = new StringWriter(codeBuilder);
            var styleSheet = StyleSheets.Default;
            var colourizer = new CodeColorizer();
            colourizer.Colorize(code, language, Formatters.Default, styleSheet, codeWriter);
            return codeBuilder.ToString();
        }

        private static string GetCode(LeafBlock obj, out string firstLine) {
            var code = new StringBuilder();
            firstLine = null;
            foreach (var line in obj.Lines.Lines) {
                var slice = line.Slice;
                if (slice.Text == null) {
                    continue;
                }

                var lineText = slice.Text.Substring(slice.Start, slice.Length);

                if (firstLine == null) {
                    firstLine = lineText;
                }

                code.AppendLine(lineText);
            }
            return code.ToString();
        }
    }
}