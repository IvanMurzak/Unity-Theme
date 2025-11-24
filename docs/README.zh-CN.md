# [Unity-Theme（调色板）](https://github.com/IvanMurzak/Unity-Theme)

[![openupm](https://img.shields.io/npm/v/extensions.unity.theme?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/extensions.unity.theme/)
[![r](https://github.com/IvanMurzak/Unity-Theme/workflows/release/badge.svg '测试通过')](https://github.com/IvanMurzak/Unity-Theme/actions/workflows/release.yml)
[![Unity Editor](https://img.shields.io/badge/Editor-X?style=flat&logo=unity&labelColor=333A41&color=49BC5C 'Unity 编辑器支持')](https://unity.com/releases/editor/archive)
[![Unity Runtime](https://img.shields.io/badge/Runtime-X?style=flat&logo=unity&labelColor=333A41&color=49BC5C 'Unity 运行时支持')](https://unity.com/releases/editor/archive)

[![Stars](https://img.shields.io/github/stars/IvanMurzak/Unity-Theme 'Stars')](https://github.com/IvanMurzak/Unity-Theme/stargazers)
[![License](https://img.shields.io/github/license/IvanMurzak/Unity-Theme)](https://github.com/IvanMurzak/Unity-Theme/blob/main/LICENSE)
[![Stand With Ukraine](https://raw.githubusercontent.com/vshymanskyy/StandWithUkraine/main/badges/StandWithUkraine.svg)](https://stand-with-ukraine.pp.ua)

创建颜色调色板和组件，以更改特定视觉元素上的特定颜色。对 UI 非常有用。

![Unity-Theme-1](https://github.com/IvanMurzak/Unity-Theme/assets/9135028/1c545d11-aea4-4cd2-8aaa-75539bbb6699)

<div align="center" width="100%">

<b>[English](https://github.com/IvanMurzak/Unity-Theme/blob/main/README.md) | [日本語](https://github.com/IvanMurzak/Unity-Theme/blob/main/docs/README.ja.md) | [Español](https://github.com/IvanMurzak/Unity-Theme/blob/main/docs/README.es.md)</b>

</div>

## 功能特性

- ✔️ 为颜色命名
- ✔️ 创建带名称的自定义主题
- ✔️ 添加任意数量的颜色
- ✔️ 自由命名主题
- ✔️ 随时通过名称更改主题
- ✔️ 将颜色绑定到：`Image`、`SpriteRenderer`、`TextMeshPro` 等
- ✔️ 通过几行代码创建新的 C# 类，轻松添加自定义颜色绑定器
- ✔️ 绑定到组件后也可重命名颜色，不会破坏链接

## 稳定性状态

| Unity 版本    | 编辑器测试 | 播放器测试 | 构建测试 |
|---------------|-------------|-------------|------------|
| 2022.3.57f1   | ![2022.3.57f1](https://github.com/IvanMurzak/Unity-Theme/workflows/release/badge.svg?job=test-unity-2022-3-57f1-editmode) | ![2022.3.57f1](https://github.com/IvanMurzak/Unity-Theme/workflows/release/badge.svg?job=test-unity-2022-3-57f1-playmode) | ![2022.3.57f1](https://github.com/IvanMurzak/Unity-Theme/workflows/release/badge.svg?job=test-unity-2022-3-57f1-standalone) |
| 2023.1.20f1   | ![2023.1.20f1](https://github.com/IvanMurzak/Unity-Theme/workflows/release/badge.svg?job=test-unity-2023-1-20f1-editmode) | ![2023.1.20f1](https://github.com/IvanMurzak/Unity-Theme/workflows/release/badge.svg?job=test-unity-2023-1-20f1-playmode) | ![2023.1.20f1](https://github.com/IvanMurzak/Unity-Theme/workflows/release/badge.svg?job=test-unity-2023-1-20f1-standalone) |
| 2023.2.20f1   | ![2023.2.20f1](https://github.com/IvanMurzak/Unity-Theme/workflows/release/badge.svg?job=test-unity-2023-2-20f1-editmode) | ![2023.2.20f1](https://github.com/IvanMurzak/Unity-Theme/workflows/release/badge.svg?job=test-unity-2023-2-20f1-playmode) | ![2023.2.20f1](https://github.com/IvanMurzak/Unity-Theme/workflows/release/badge.svg?job=test-unity-2023-2-20f1-standalone) |
| 6000.0.37f1   | ![6000.0.37f1](https://github.com/IvanMurzak/Unity-Theme/workflows/release/badge.svg?job=test-unity-6000-0-37f1-editmode) | ![6000.0.37f1](https://github.com/IvanMurzak/Unity-Theme/workflows/release/badge.svg?job=test-unity-6000-0-37f1-playmode) | ![6000.0.37f1](https://github.com/IvanMurzak/Unity-Theme/workflows/release/badge.svg?job=test-unity-6000-0-37f1-standalone) |

# 安装

### 选项 1 - 安装程序

- **[⬇️ 下载安装程序](https://github.com/IvanMurzak/Unity-Theme/releases/download/4.2.0/Unity-Theme-Installer.unitypackage)**
- **📂 将安装程序导入 Unity 项目**
  > - 您可以双击文件 - Unity 将打开它
  > - 或者：您可以先打开 Unity 编辑器，然后点击 `Assets/Import Package/Custom Package`，然后选择该文件

### 选项 2 - OpenUPM-CLI

- [⬇️ 安装 OpenUPM-CLI](https://github.com/openupm/openupm-cli#installation)
- 📟 在 Unity 项目文件夹中打开命令行

```bash
openupm add extensions.unity.theme
```

### 选项 3 - Asset Store

- **[▶️ 打开 Asset Store](https://u3d.as/3DQp)**

---

# 使用方法

- 转到 `Window/Unity-Theme`
- 根据需要自定义调色板
- 将 `ColorBinder` 组件添加到目标 GameObject

### 颜色绑定器

一个将颜色绑定到特定目标的组件，例如 `Image`、`SpriteRenderer`、`TextMeshPro` 或其他任何对象。

内置颜色绑定器列表：

- ✔️ Light
- ✔️ Image
- ✔️ Button
- ✔️ Shadow
- ✔️ Toggle
- ✔️ Outline
- ✔️ Renderer
- ✔️ Selectable
- ✔️ TextMeshPro
- ✔️ LineRenderer
- ✔️ MeshRenderer
- ✔️ SpriteRenderer
- ✔️ SkinnedMeshRenderer
- ✔️ SpriteShapeRenderer

![Unity-Theme-Binder](https://github.com/IvanMurzak/Unity-Theme/assets/9135028/6198af48-9f0e-4cda-b5e9-40508bbd5c45)

### 调色板

修改调色板，所有连接的 GameObjects 会立即响应。

![Unity-Theme-Palettes](https://github.com/IvanMurzak/Unity-Theme/assets/9135028/179215af-23f1-4a8e-bb29-a7169f3433a5)

## C# API

### 颜色

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

### 主题

```csharp
using Unity.Theme;

Theme.Instance.AddTheme("Light");
Theme.Instance.SetOrAddTheme("Light");

Theme.Instance.CurrentThemeName = "Light";
Theme.Instance.CurrentThemeIndex = 0;

Theme.Instance.RemoveTheme("Light");
Theme.Instance.RemoveAllThemes();
```

### 创建自定义 `ColorBinder`

如果您需要将颜色绑定到其他对象，可以如下所示从 `BaseColorBinder` 扩展。

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

## 其他

- **[调色板构建器](https://m3.material.io/theme-builder#/custom)**，由 Google 的 Material Design V3 提供

---

## 从 `2.x.x` 版本迁移

版本 `3.x.x` 或更高版本具有不同的数据库结构，因此如果您想保持现有绑定器仍然连接到正确的颜色，则需要手动迁移数据。绑定器使用 GUID 连接到颜色。

> ❗❗❗**请严格按照以下步骤顺序操作**

### 步骤 1

关闭 Unity 项目以避免任何数据丢失。

### 步骤 2 - 备份

- 制作 `Assets/Resources/Unity-Theme Database.asset` 的备份副本，将其放在项目外部。
- 截取每个主题中颜色的屏幕截图，以便稍后使用颜色选择器重现它们。

### 步骤 3 - 数据库模板

- 下载 [Unity-Theme-Database.json](https://raw.githubusercontent.com/IvanMurzak/Unity-Theme/refs/heads/main/Assets/Resources/Unity-Theme-Database.json)。
- 将其保存在 `Assets/Resources/Unity-Theme-Database.json`。
- 将其用作模板。如果需要，可以删除所有现有颜色。

### 步骤 4 - 数据迁移

**您需要将 GUID 复制**从 `Assets/Resources/Unity-Theme Database.asset` 到 `Assets/Resources/Unity-Theme-Database.json`。为此，请查看下面的示例。您可以看到新旧数据库的数据格式，以及如何复制 GUID。您现在可以忽略颜色值，稍后使用颜色选择器工具和旧调色板的屏幕截图更改颜色值更容易。

#### `Assets/Resources/Unity-Theme Database.asset` 示例 - 旧文件（源）

注意 `guid: 6b934efb-0b9b-42fd-82fd-7a0dbd1de53c`，该文件包含它 3 次，具体取决于您的设置。您需要将 guid 复制到 `Unity-Theme-Database.json` 文件。

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

### `Assets/Resources/Unity-Theme-Database.json` 示例 - 新文件（目标）

这是您在迁移过程结束时需要制作的文件。
完成后，您可能需要关闭并打开 Unity 项目以让 Unity-Theme 重新加载数据。

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

### 步骤 5 - 包更新

- 在 Unity 关闭时从项目中删除 Unity-Theme `2.x.x`

```bash
openupm remove extensions.unity.theme
```

- 安装最新的 Unity-Theme

```bash
openupm add extensions.unity.theme
```

### 步骤 6 - 清理

- 删除 `Assets/Resources/Unity-Theme Database.asset` 文件（原始文件和副本）。
- ✅ 迁移完成，干得好！
