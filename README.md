# GoogleTranslateI18nText

Meant for Blazor I18nText, its able to translate json files with minimum effort, runs on VS2022 NET7

Very basic implementation but works fine


# Why
1. FREE
2. Uses Google API
3. Auto Generates needed new files
4. Translates only Values (keys will be left invariant)
5. Modules can be reused easily to create other behaviours

# Languages Supported:
1. ANY

# Simple Sample:
```csharp

var content = new GoogleTranslate().Translate("hello", "en", "ru");
Console.WriteLine(content);

```
