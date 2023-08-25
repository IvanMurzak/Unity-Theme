# Unity-Theme

![npm](https://img.shields.io/npm/v/extensions.unity.theme) [![openupm](https://img.shields.io/npm/v/extensions.unity.theme?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/extensions.unity.theme/) ![License](https://img.shields.io/github/license/IvanMurzak/Unity-Theme) [![Stand With Ukraine](https://raw.githubusercontent.com/vshymanskyy/StandWithUkraine/main/badges/StandWithUkraine.svg)](https://stand-with-ukraine.pp.ua)

Create palettes of colors and components to change specific colors on a specific visual element. Very useful for UI.

![Unity Theme Settings](https://imgur.com/FKVF2H9.gif)

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

## Usage

- Go to `Window/Unity-Theme`.
- Customize color pallets as you want
- Add `ColorBinder` components to a target GameObject

### Color Binder

A component that binds a color to a specific target, such as `Image`, `SpriteRenderer`, `TextMeshPro` or anything else. There are list of built-in color binders:

- `ImageColorBinder`
- `SpriteRendererColorBinder`
- `TextMeshProColorBinder`

![Color Binders](https://imgur.com/AeNC3tF.gif)

#### Create a custom `ColorBinder`

When need to bind color to something else, you may extend from `BaseColorBinder` as listed below.

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

### Theme change in Play Time (Runtime)

```C#
using Unity.Theme;

// Change the current theme to a new theme from the existing themes list
Theme.Instance.CurrentThemeIndex = 1;
```
