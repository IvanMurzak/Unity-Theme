# Unity-Theme
![npm](https://img.shields.io/npm/v/extensions.unity.theme) [![openupm](https://img.shields.io/npm/v/extensions.unity.theme?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/extensions.unity.theme/) ![License](https://img.shields.io/github/license/IvanMurzak/Unity-Theme) [![Stand With Ukraine](https://raw.githubusercontent.com/vshymanskyy/StandWithUkraine/main/badges/StandWithUkraine.svg)](https://stand-with-ukraine.pp.ua)

Create palettes of colors and components for change specific color on a specific visual element. Very useful to UI.

![Unity Theme Settings](https://imgur.com/FKVF2H9.gif)

## Features

- create custom themes
- name color as you want
- add as many colors as you need
- name theme as you want
- change theme any moment by name
- bind color to: Image, SpriteRenderer, TextMeshPro
- ease way to add custom color binder by new C# class with just few lines of code
- ability to rename color even after binding to component, no broken links

# How to install - Option 1 (RECOMMENDED)
- Install [ODIN Inspector](https://odininspector.com/)
- [Install OpenUPM-CLI](https://github.com/openupm/openupm-cli#installation)
- Open command line in Unity project folder
- `openupm add extensions.unity.theme`

# How to install - Option 2
- Install [ODIN Inspector](https://odininspector.com/)
- Add this code to <code>/Packages/manifest.json</code>
```json
{
  "dependencies": {
    "extensions.unity.theme": "0.1.15",
  },
  "scopedRegistries": [
    {
      "name": "package.openupm.com",
      "url": "https://package.openupm.com",
      "scopes": [
        "extensions.unity"
      ]
    }
  ]
}
```

# How to use 

- Go to `Edit/Unity-Theme Preferences...`.
- Customize color pallets as you want
- Add ColorBinder to colorized GameObject

# Color Binder
Each colored component should be binded to specific color.

- ImageColorBinder
- SpriteRendereColorBinder
- TextMeshProColorBinder

![Color Binders](https://imgur.com/AeNC3tF.gif)

# Custom Color Binder

If you want to bind color to something else, you may extends from `BaseColorBinder` as listed below.

```C#
using Sirenix.OdinInspector;
using UnityEngine;
using TMPro;

namespace Unity.Theme.Binders
{
    [AddComponentMenu("Theme/TextMeshPro Color Binder")]
    public class TextMeshProColorBinder : BaseColorBinder
    {
        [SerializeField, Required] TextMeshProUGUI textMeshPro;

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
