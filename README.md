# Unity-Theme

![npm](https://img.shields.io/npm/v/extensions.unity.theme) [![openupm](https://img.shields.io/npm/v/extensions.unity.theme?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/extensions.unity.theme/) ![License](https://img.shields.io/github/license/IvanMurzak/Unity-Theme) [![Stand With Ukraine](https://raw.githubusercontent.com/vshymanskyy/StandWithUkraine/main/badges/StandWithUkraine.svg)](https://stand-with-ukraine.pp.ua)

Create palettes of colors and components to change specific colors on a specific visual element. Very useful for UI.

![Unity-Theme-1](https://github.com/IvanMurzak/Unity-Theme/assets/9135028/1c545d11-aea4-4cd2-8aaa-75539bbb6699)

## Features

- ✔️ Name colors
- ✔️ Create custom themes with names
- ✔️ Add as many colors as you need
- ✔️ Name a theme as you want
- ✔️ Change theme any moment by name
- ✔️ Bind color to: `Image`, `SpriteRenderer`, `TextMeshPro`, etc
- ✔️ Easy way to add custom color binder by new C# class with just a few lines of code
- ✔️ Rename color even after binding to a component, with no broken links

## Installation

- [Install OpenUPM-CLI](https://github.com/openupm/openupm-cli#installation)
- Open command line in Unity project folder

```bash
openupm add extensions.unity.theme`
```

## Usage

- Go to `Window/Unity-Theme`
- Customize color palettes as you want
- Add `ColorBinder` components to a target GameObject

### Color Binder

A component that binds a color to a specific target, such as `Image`, `SpriteRenderer`, `TextMeshPro`, or anything else. 

There is a list of built-in color binders:

- `ImageColorBinder`
- `SpriteRendererColorBinder`
- `TextMeshProColorBinder`

![Unity-Theme-Binder](https://github.com/IvanMurzak/Unity-Theme/assets/9135028/6198af48-9f0e-4cda-b5e9-40508bbd5c45)

## C# Api

### Colors

```csharp
using Unity.Theme;

Theme.Instance.AddColor("Primary", "#6750A4");
Theme.Instance.AddColor("Primary", Color.white);

Theme.Instance.SetColor("Primary", "#6750A4");
Theme.Instance.SetColor("Primary", Color.white);

Theme.Instance.SetOrAddColor("Primary", "#6750A4");
Theme.Instance.SetOrAddColor("Primary", Color.white);

Theme.Instance.RemoveColorByName("Primary");
Theme.Instance.RemoveColor(colorData);
Theme.Instance.RemoveAllColors();
```

### Themes

```csharp
using Unity.Theme;

Theme.Instance.AddTheme("Light");
Theme.Instance.SetOrAddTheme("Light");

Theme.Instance.CurrentThemeName = "Light";
Theme.Instance.CurrentThemeIndex = 0;

Theme.Instance.RemoveTheme("Light");
Theme.Instance.RemoveAllThemes();
```

### Create a custom `ColorBinder`

If you need to bind color to something else, you may extend from `BaseColorBinder` as listed below.

```C#
using UnityEngine;
using TMPro;

namespace Unity.Theme.Binders
{
    [AddComponentMenu("Theme/TextMeshPro Color Binder")]
    public class TextMeshProColorBinder : BaseColorBinder
    {
        [SerializeField] TextMeshProUGUI textMeshPro;

        protected override void Awake()
        {
            if (textMeshPro == null) textMeshPro = GetComponent<TextMeshProUGUI>();
            base.Awake();
        }
        protected override void SetColor(Color color)
        {
            textMeshPro.color = color;
        }
    }
}
```

## Other

- **[Color palette builder](https://m3.material.io/theme-builder#/custom)** by Google's Material Design V3
