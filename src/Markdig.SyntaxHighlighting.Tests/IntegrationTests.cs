using Markdig;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MDS.Markdig.SyntaxHighlighting.Tests
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
            Assert.Contains("<pre><code>", html);
            Assert.Contains("jsonProperty", html);
            Assert.DoesNotContain("lang-", html);
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
            Assert.Contains("<div", html);
            Assert.Contains("jsonProperty", html);
            Assert.Contains("lang-", html);
        }

    }
}
