This is before the table

| Heading 1 | Heading 2 |
| --------- | --------- |
| Row 1     | Cell 2    |

This is after the table

```csharp
// ©2015 Amido Limited (https://www.amido.com), Licensed under the terms of the Apache 2.0 Licence (http://www.apache.org/licenses/LICENSE-2.0)

namespace Amido.VersionDashboard.Web.Domain {
    public interface IConfigProvider {
        string GetSetting(string appSetting);
    }
}
```
