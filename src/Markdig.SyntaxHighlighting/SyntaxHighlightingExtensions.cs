namespace Markdig.SyntaxHighlighting {
    public static class SyntaxHighlightingExtensions {
        public static MarkdownPipelineBuilder UseSyntaxHighlighting(this MarkdownPipelineBuilder pipeline) {
            pipeline.Extensions.Add(new SyntaxHighlightingExtension());
            return pipeline;
        }
    }
}