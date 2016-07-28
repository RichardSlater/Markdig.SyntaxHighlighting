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

* [ ] Upgrade ColorCode to support .NET Core / .NET Standard.
* [ ] Upgrade Markdig.SyntaxHighlighting to support .NET Core / .NET Standard.

  [markdig]: https://github.com/lunet-io/markdig
  [colorcode]: https://colorcode.codeplex.com/
