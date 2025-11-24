# [Unity-Theme (Color Palette)](https://github.com/IvanMurzak/Unity-Theme)

[![openupm](https://img.shields.io/npm/v/extensions.unity.theme?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/extensions.unity.theme/)
[![r](https://github.com/IvanMurzak/Unity-Theme/workflows/release/badge.svg 'Tests Passed')](https://github.com/IvanMurzak/Unity-Theme/actions/workflows/release.yml)
[![Unity Editor](https://img.shields.io/badge/Editor-X?style=flat&logo=unity&labelColor=333A41&color=49BC5C 'Unity Editor supported')](https://unity.com/releases/editor/archive)
[![Unity Runtime](https://img.shields.io/badge/Runtime-X?style=flat&logo=unity&labelColor=333A41&color=49BC5C 'Unity Runtime supported')](https://unity.com/releases/editor/archive)

[![Stars](https://img.shields.io/github/stars/IvanMurzak/Unity-Theme 'Stars')](https://github.com/IvanMurzak/Unity-Theme/stargazers)
[![License](https://img.shields.io/github/license/IvanMurzak/Unity-Theme)](https://github.com/IvanMurzak/Unity-Theme/blob/main/LICENSE)
[![Stand With Ukraine](https://raw.githubusercontent.com/vshymanskyy/StandWithUkraine/main/badges/StandWithUkraine.svg)](https://stand-with-ukraine.pp.ua)

Create palettes of colors and components to change specific colors on a specific visual element. Very useful for UI.

![Unity-Theme-1](https://github.com/IvanMurzak/Unity-Theme/assets/9135028/1c545d11-aea4-4cd2-8aaa-75539bbb6699)

<div align="center" width="100%">

<b>[中文](https://github.com/IvanMurzak/Unity-Theme/blob/main/docs/README.zh-CN.md) | [日本語](https://github.com/IvanMurzak/Unity-Theme/blob/main/docs/README.ja.md) | [Español](https://github.com/IvanMurzak/Unity-Theme/blob/main/docs/README.es.md)</b>

</div>

## Features

- ✔️ Name colors
- ✔️ Create custom themes with names
- ✔️ Add as many colors as you need
- ✔️ Name a theme as you want
- ✔️ Change theme any moment by name
- ✔️ Bind color to: `Image`, `SpriteRenderer`, `TextMeshPro`, etc
- ✔️ Easy way to add custom color binder by new C# class with just a few lines of code
- ✔️ Rename color even after binding to a component, with no broken links

## Stability status

| Unity Version | Editor test | Player test | Build test |
|---------------|-------------|-------------|------------|
| 2022.3.57f1   | ![2022.3.57f1](https://github.com/IvanMurzak/Unity-Theme/workflows/release/badge.svg?job=test-unity-2022-3-57f1-editmode) | ![2022.3.57f1](https://github.com/IvanMurzak/Unity-Theme/workflows/release/badge.svg?job=test-unity-2022-3-57f1-playmode) | ![2022.3.57f1](https://github.com/IvanMurzak/Unity-Theme/workflows/release/badge.svg?job=test-unity-2022-3-57f1-standalone) |
| 2023.1.20f1   | ![2023.1.20f1](https://github.com/IvanMurzak/Unity-Theme/workflows/release/badge.svg?job=test-unity-2023-1-20f1-editmode) | ![2023.1.20f1](https://github.com/IvanMurzak/Unity-Theme/workflows/release/badge.svg?job=test-unity-2023-1-20f1-playmode) | ![2023.1.20f1](https://github.com/IvanMurzak/Unity-Theme/workflows/release/badge.svg?job=test-unity-2023-1-20f1-standalone) |
| 2023.2.20f1   | ![2023.2.20f1](https://github.com/IvanMurzak/Unity-Theme/workflows/release/badge.svg?job=test-unity-2023-2-20f1-editmode) | ![2023.2.20f1](https://github.com/IvanMurzak/Unity-Theme/workflows/release/badge.svg?job=test-unity-2023-2-20f1-playmode) | ![2023.2.20f1](https://github.com/IvanMurzak/Unity-Theme/workflows/release/badge.svg?job=test-unity-2023-2-20f1-standalone) |
| 6000.0.37f1   | ![6000.0.37f1](https://github.com/IvanMurzak/Unity-Theme/workflows/release/badge.svg?job=test-unity-6000-0-37f1-editmode) | ![6000.0.37f1](https://github.com/IvanMurzak/Unity-Theme/workflows/release/badge.svg?job=test-unity-6000-0-37f1-playmode) | ![6000.0.37f1](https://github.com/IvanMurzak/Unity-Theme/workflows/release/badge.svg?job=test-unity-6000-0-37f1-standalone) |

# Installation

### Option 1 - Installer

- **[⬇️ Download Installer](https://github.com/IvanMurzak/Unity-Theme/releases/download/4.2.0/Unity-Theme-Installer.unitypackage)**
- **📂 Import installer into Unity project**
  > - You may use double click on the file - Unity will open it
  > - OR: You may open Unity Editor first, then click on `Assets/Import Package/Custom Package`, then choose the file

### Option 2 - OpenUPM-CLI

- [⬇️ Install OpenUPM-CLI](https://github.com/openupm/openupm-cli#installation)
- 📟 Open command line in Unity project folder

```bash
openupm add extensions.unity.theme
```

### Option 3 - Asset Store

- **[▶️ Open Asset Store](https://u3d.as/3DQp)**

---

# Usage

- Go to `Window/Unity-Theme`
- Customize color palettes as you want
- Add `ColorBinder` components to a target GameObject

### Color Binder

A component that binds a color to a specific target, such as `Image`, `SpriteRenderer`, `TextMeshPro`, or anything else.

There is a list of built-in color binders:

- ✔️ Light
- ✔️ Image
- ✔️ Button
- ✔️ Toggle
- ✔️ Material
- ✔️ Selectable
- ✔️ TextMeshPro
- ✔️ SpriteRenderer

![Unity-Theme-Binder](https://github.com/IvanMurzak/Unity-Theme/assets/9135028/6198af48-9f0e-4cda-b5e9-40508bbd5c45)

### Color palettes

Modify palettes, instant response with all connected GameObjects.

![Unity-Theme-Palettes](https://github.com/IvanMurzak/Unity-Theme/assets/9135028/179215af-23f1-4a8e-bb29-a7169f3433a5)

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
    public class TextMeshProColorBinder : GenericColorBinder<TextMeshProUGUI>
    {
        protected override void SetColor(TextMeshProUGUI target, Color color)
            => target.color = color;

        protected override Color? GetColor(TextMeshProUGUI target)
            => target.color;
    }
}
```

## Other

- **[Color palette builder](https://m3.material.io/theme-builder#/custom)** by Google's Material Design V3

---

## Migration from `2.x.x` version

The version `3.x.x` or newer has a different database structure, so you need to migrate your data manually if you want to keep existed binders to still be connected to a right color. The binders are using GUIDs to connect to colors.

> ❗❗❗**Please follow the exact steps order**

### Step 1

Close Unity project to avoid any data loss.

### Step 2 - backup

- Make a backup copy of `Assets/Resources/Unity-Theme Database.asset`, put it outside of a project.
- Make a screenshot of colors in each theme to reproduce them later with color picker.

### Step 3 - database template

- Download [Unity-Theme-Database.json](https://raw.githubusercontent.com/IvanMurzak/Unity-Theme/refs/heads/main/Assets/Resources/Unity-Theme-Database.json).
- Save it at `Assets/Resources/Unity-Theme-Database.json`.
- Use it as a template. Feel free to delete all existed colors if you want.

### Step 4 - data migration

**You need to copy GUIDs** from `Assets/Resources/Unity-Theme Database.asset`  to `Assets/Resources/Unity-Theme-Database.json`. To do that, please take a look at the samples below. You may see what is data format of old and new databases, and how to copy GUIDs. You may ignore color for now, it is easier to change color values later using color picker tool and the screenshot of old color palette.

#### Sample of `Assets/Resources/Unity-Theme Database.asset` - old file (source)

Take a look at the `guid: 6b934efb-0b9b-42fd-82fd-7a0dbd1de53c`, the file contains it 3 times, depends on your setup. You would need to copy the guid to the `Unity-Theme-Database.json` file.

```yaml
  colors:
  - guid: 6b934efb-0b9b-42fd-82fd-7a0dbd1de53c
    name: Primary
  - guid: 520b0288-c5e4-4106-95ae-095ad2dcceb8
    name: Primary Text
  - guid: 465741bc-25d8-4722-a981-7e4a18074d83
    name: Primary Container
  themes:
  - guid: 6d1fce4e-1938-4d6d-93b0-b4b9f6497293
    expanded: 1
    themeName: Light
    colors:
    - guid: 6b934efb-0b9b-42fd-82fd-7a0dbd1de53c
      color: {r: 0.40392157, g: 0.3137255, b: 0.6431373, a: 1}
    - guid: 520b0288-c5e4-4106-95ae-095ad2dcceb8
      color: {r: 1, g: 1, b: 1, a: 1}
    - guid: 465741bc-25d8-4722-a981-7e4a18074d83
      color: {r: 0.91764706, g: 0.8666667, b: 1, a: 1}
  - guid: 54c71f36-6023-4d84-bce7-c8192cf7ba40
    expanded: 1
    themeName: Dark
    colors:
    - guid: 6b934efb-0b9b-42fd-82fd-7a0dbd1de53c
      color: {r: 0, g: 0.4784314, b: 1, a: 1}
    - guid: 520b0288-c5e4-4106-95ae-095ad2dcceb8
      color: {r: 1, g: 1, b: 1, a: 1}
    - guid: 465741bc-25d8-4722-a981-7e4a18074d83
      color: {r: 0.15294118, g: 0.15294118, b: 0.15686275, a: 1}
```

### Sample of `Assets/Resources/Unity-Theme-Database.json` - new file (destination)

That is the file that you need to make in the end of the migration process.
When you done, you may need to close & open Unity project to let Unity-Theme to reload the data.

```json
{
  "debugLevel": 2,
  "currentThemeIndex": 1,
  "colors": [
    { "guid": "6b934efb-0b9b-42fd-82fd-7a0dbd1de53c", "name": "Primary" },
    { "guid": "520b0288-c5e4-4106-95ae-095ad2dcceb8", "name": "Primary Text" },
    { "guid": "465741bc-25d8-4722-a981-7e4a18074d83", "name": "Primary Container" }
  ],
  "themes": [
    {
      "guid": "4cfe4185-bc3e-4247-969f-1da1d3f2bdec",
      "expanded": true,
      "themeName": "Light",
      "colors": [
        { "guid": "6b934efb-0b9b-42fd-82fd-7a0dbd1de53c", "colorHex": "#6750A4FF" },
        { "guid": "520b0288-c5e4-4106-95ae-095ad2dcceb8", "colorHex": "#FFFFFFFF" },
        { "guid": "465741bc-25d8-4722-a981-7e4a18074d83", "colorHex": "#EADDFFFF" }
      ]
    },
    {
      "guid": "dca52c83-4f79-4fee-854e-0defe9ccbe07",
      "expanded": true,
      "themeName": "Dark",
      "colors": [
        { "guid": "6b934efb-0b9b-42fd-82fd-7a0dbd1de53c", "colorHex": "#007AFFFF" },
        { "guid": "520b0288-c5e4-4106-95ae-095ad2dcceb8", "colorHex": "#FFFFFFFF" },
        { "guid": "465741bc-25d8-4722-a981-7e4a18074d83", "colorHex": "#272728FF" }
      ]
    }
  ]
}
```

### Step 5 - package update

- Delete Unity-Theme `2.x.x` from the project while Unity is closed

```bash
openupm remove extensions.unity.theme
```

- Install latest Unity-Theme

```bash
openupm add extensions.unity.theme
```

### Step 6 - cleaning up

- Delete the `Assets/Resources/Unity-Theme Database.asset` file (original and copy).
- ✅ migration completed, well done!
