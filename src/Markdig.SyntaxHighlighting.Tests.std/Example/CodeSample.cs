using System;
using System.IO;
using System.Reflection;
using Xunit;

namespace Markdig.SyntaxHighlighting.Tests.Example {
    public class CodeSample {
        [Fact]
        public void CodeSampleWorks() {
            var codebase = Assembly.GetEntryAssembly().Location;
            var directory = Path.GetDirectoryName(codebase);
            if (directory == null) {
                throw new NullReferenceException("appPath came back null.");
            }
            var appPath = new Uri(directory).LocalPath;
            var folder = Path.Combine(appPath, "Example");
            var inputMarkdown = Path.Combine(folder, "README.md");
            var referenceFile = Path.Combine(folder, "expected.html");
            var expectedHtml = File.ReadAllText(referenceFile);
            var markdown = File.ReadAllText(inputMarkdown);
            var pipeline = new MarkdownPipelineBuilder()
                .UseAdvancedExtensions()
                .UseSyntaxHighlighting()
                .Build();
            var html = Markdown.ToHtml(markdown, pipeline);
            var actualHtml = File.ReadAllText(Path.Combine(folder, "_template.html"))
                .Replace("{{{this}}}", html);
            actualHtml = actualHtml.Replace("\r\n", "\n").Replace("\n", "\r\n");
            expectedHtml = expectedHtml.Replace("\r\n", "\n").Replace("\n", "\r\n");
            File.WriteAllText(Path.Combine(folder, "actual.html"), actualHtml);
            Assert.Equal(expectedHtml, actualHtml);
        }
    }
}