[![GitHub issues](https://img.shields.io/github/issues/RichardSlater/Markdig.SyntaxHighlighting.svg?style=flat-square)](https://github.com/RichardSlater/Markdig.SyntaxHighlighting/issues)
[![GitHub forks](https://img.shields.io/github/forks/RichardSlater/Markdig.SyntaxHighlighting.svg?style=flat-square)](https://github.com/RichardSlater/Markdig.SyntaxHighlighting/network)
[![GitHub stars](https://img.shields.io/github/stars/RichardSlater/Markdig.SyntaxHighlighting.svg?style=flat-square)](https://github.com/RichardSlater/Markdig.SyntaxHighlighting/stargazers)
[![GitHub license](https://img.shields.io/badge/license-Apache%202-blue.svg?style=flat-square)](https://raw.githubusercontent.com/RichardSlater/Markdig.SyntaxHighlighting/master/LICENSE.md)
[![AppVeyor](https://img.shields.io/appveyor/ci/richard-slater/markdig-syntaxhighlighting.svg?style=flat-square)](https://ci.appveyor.com/project/richard-slater/markdig-syntaxhighlighting)

# Syntax Highlighting extension for Markdig

An extension that adds Syntax Highlighting, also known as code colourization,
to a [Markdig][markdig] pipeline through the power of [ColorCode][colorcode].
By simply adding this extension to your pipeline, you can add colour and style
to your source code:

## Demonstration

### Before

```
namespace Amido.VersionDashboard.Web.Domain {
    public interface IConfigProvider {
        string GetSetting(string appSetting);
    }
}
```

### After

```csharp
namespace Amido.VersionDashboard.Web.Domain {
    public interface IConfigProvider {
        string GetSetting(string appSetting);
    }
}
```

## Usage

Simply import the nuget package, add a using statement for `Markdig.SyntaxHighlighting` and add to your pipeline:

```csharp
var pipeline = new MarkdownPipelineBuilder()
    .UseAdvancedExtensions()
    .UseSyntaxHighlighting()
    .Build();
```

## Future Updates

*   [ ] Upgrade ColorCode to support .NET Core / .NET Standard.
*   [ ] Upgrade Markdig.SyntaxHighlighting to support .NET Core / .NET Standard.


  [markdig]: https://github.com/lunet-io/markdig
  [colorcode]: https://colorcode.codeplex.com/
