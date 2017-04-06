using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Markdig.SyntaxHighlighting.Tests
{
    public class IntegrationTests
    {

        [Fact]
        public void ShouldUseDefaultRendererIfLanguageIsNotIndicated() {
            string testString = @"
# This is a test

```
{
    ""jsonProperty"": 1
}
```";
            var pipeline = new MarkdownPipelineBuilder()
                .UseAdvancedExtensions()
                .UseSyntaxHighlighting()
                .Build();
            var html = Markdown.ToHtml(testString, pipeline);
            Assert.True(html.Contains("<pre><code>"));
            Assert.True(html.Contains("jsonProperty"));
            Assert.False(html.Contains("lang-"));
        }

        [Fact]
        public void ShouldColorizeSyntaxWhenLanguageIsIndicated()
        {
            string testString = @"
# This is a test

```json
{
    ""jsonProperty"": 1
}
```";
            var pipeline = new MarkdownPipelineBuilder()
                .UseAdvancedExtensions()
                .UseSyntaxHighlighting()
                .Build();
            var html = Markdown.ToHtml(testString, pipeline);
            Assert.True(html.Contains("<div"));
            Assert.True(html.Contains("jsonProperty"));
            Assert.True(html.Contains("lang-"));
        }

    }
}
