<div align="center" width="100%">
  <h1>Unity-Theme — <i>カラーパレット</i></h1>

[![OpenUPM](https://img.shields.io/npm/v/extensions.unity.theme?label=OpenUPM&registry_uri=https://package.openupm.com&labelColor=333A41 'OpenUPM package')](https://openupm.com/packages/extensions.unity.theme/)
[![Unity Editor](https://img.shields.io/badge/Editor-X?style=flat&logo=unity&labelColor=333A41&color=2A2A2A 'Unity エディタ対応')](https://unity.com/releases/editor/archive)
[![Unity Runtime](https://img.shields.io/badge/Runtime-X?style=flat&logo=unity&labelColor=333A41&color=2A2A2A 'Unity ランタイム対応')](https://unity.com/releases/editor/archive)
[![r](https://github.com/IvanMurzak/Unity-Theme/workflows/release/badge.svg 'テスト合格')](https://github.com/IvanMurzak/Unity-Theme/actions/workflows/release.yml)</br>
[![Stars](https://img.shields.io/github/stars/IvanMurzak/Unity-Theme 'Stars')](https://github.com/IvanMurzak/Unity-Theme/stargazers)
[![License](https://img.shields.io/github/license/IvanMurzak/Unity-Theme?label=License&labelColor=333A41)](https://github.com/IvanMurzak/Unity-Theme/blob/main/LICENSE)
[![Stand With Ukraine](https://raw.githubusercontent.com/vshymanskyy/StandWithUkraine/main/badges/StandWithUkraine.svg)](https://stand-with-ukraine.pp.ua)

  <img src="https://github.com/IvanMurzak/Unity-Theme/assets/9135028/1c545d11-aea4-4cd2-8aaa-75539bbb6699" alt="Unity-Theme" width="100%">

  <b>[English](https://github.com/IvanMurzak/Unity-Theme/blob/main/README.md) | [中文](https://github.com/IvanMurzak/Unity-Theme/blob/main/docs/README.zh-CN.md) | [Español](https://github.com/IvanMurzak/Unity-Theme/blob/main/docs/README.es.md)</b>

</div>

カラーパレットとコンポーネントを作成して、特定のビジュアル要素の特定の色を変更します。UI に非常に便利です。

## 機能

- ✔️ 色に名前を付ける
- ✔️ 名前付きのカスタムテーマを作成
- ✔️ 必要な数だけ色を追加
- ✔️ テーマに自由に名前を付ける
- ✔️ 名前でいつでもテーマを変更
- ✔️ `Image`、`SpriteRenderer`、`TextMeshPro` などに色をバインド
- ✔️ 数行のコードで新しい C# クラスを作成し、カスタムカラーバインダーを簡単に追加
- ✔️ コンポーネントへのバインド後でも色の名前を変更でき、リンクが壊れない

## 安定性ステータス

| Unity バージョン | エディタテスト | プレイヤーテスト | ビルドテスト |
|---------------|-------------|-------------|------------|
| 2022.3.62f3   | ![2022.3.62f3](https://github.com/IvanMurzak/Unity-Theme/workflows/release/badge.svg?job=test-unity-2022-3-62f3-editmode) | ![2022.3.62f3](https://github.com/IvanMurzak/Unity-Theme/workflows/release/badge.svg?job=test-unity-2022-3-62f3-playmode) | ![2022.3.62f3](https://github.com/IvanMurzak/Unity-Theme/workflows/release/badge.svg?job=test-unity-2022-3-62f3-standalone) |
| 6000.3.9f1    | ![6000.3.9f1](https://github.com/IvanMurzak/Unity-Theme/workflows/release/badge.svg?job=test-unity-6000-3-9f1-editmode) | ![6000.3.9f1](https://github.com/IvanMurzak/Unity-Theme/workflows/release/badge.svg?job=test-unity-6000-3-9f1-playmode) | ![6000.3.9f1](https://github.com/IvanMurzak/Unity-Theme/workflows/release/badge.svg?job=test-unity-6000-3-9f1-standalone) |

# インストール

### オプション 1 - インストーラー

- **[⬇️ インストーラーをダウンロード](https://github.com/IvanMurzak/Unity-Theme/releases/download/4.3.0/Unity-Theme-Installer.unitypackage)**
- **📂 インストーラーを Unity プロジェクトにインポート**
  > - ファイルをダブルクリック - Unity が開きます
  > - または：Unity エディタを先に開き、`Assets/Import Package/Custom Package` をクリックしてファイルを選択

### オプション 2 - OpenUPM-CLI

- [⬇️ OpenUPM-CLI をインストール](https://github.com/openupm/openupm-cli#installation)
- 📟 Unity プロジェクトフォルダでコマンドラインを開く

```bash
openupm add extensions.unity.theme
```

---

# 使用方法

- `Window/Unity-Theme` に移動
- 必要に応じてカラーパレットをカスタマイズ
- ターゲット GameObject に `ColorBinder` コンポーネントを追加

### カラーバインダー

`Image`、`SpriteRenderer`、`TextMeshPro`、またはその他のものなど、特定のターゲットに色をバインドするコンポーネントです。

組み込みカラーバインダーのリスト：

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

### カラーパレット

パレットを変更すると、接続されているすべての GameObject が即座に反応します。

![Unity-Theme-Palettes](https://github.com/IvanMurzak/Unity-Theme/assets/9135028/179215af-23f1-4a8e-bb29-a7169f3433a5)

## C# API

### カラー

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

### テーマ

```csharp
using Unity.Theme;

Theme.Instance.AddTheme("Light");
Theme.Instance.SetOrAddTheme("Light");

Theme.Instance.CurrentThemeName = "Light";
Theme.Instance.CurrentThemeIndex = 0;

Theme.Instance.RemoveTheme("Light");
Theme.Instance.RemoveAllThemes();
```

### カスタム `ColorBinder` の作成

他のものに色をバインドする必要がある場合は、以下のように `BaseColorBinder` から拡張できます。

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

## その他

- **[カラーパレットビルダー](https://m3.material.io/theme-builder#/custom)**（Google の Material Design V3 提供）

---

## `2.x.x` バージョンからの移行

バージョン `3.x.x` 以降は異なるデータベース構造を持っているため、既存のバインダーを正しい色に接続したままにする場合は、データを手動で移行する必要があります。バインダーは GUID を使用して色に接続します。

> ❗❗❗**以下の手順を正確な順序で実行してください**

### ステップ 1

データの損失を避けるため、Unity プロジェクトを閉じます。

### ステップ 2 - バックアップ

- `Assets/Resources/Unity-Theme Database.asset` のバックアップコピーを作成し、プロジェクトの外に配置します。
- 後でカラーピッカーで再現できるように、各テーマの色のスクリーンショットを撮ります。

### ステップ 3 - データベーステンプレート

- [Unity-Theme-Database.json](https://raw.githubusercontent.com/IvanMurzak/Unity-Theme/refs/heads/main/Assets/Resources/Unity-Theme-Database.json) をダウンロードします。
- `Assets/Resources/Unity-Theme-Database.json` に保存します。
- これをテンプレートとして使用します。必要に応じて、既存の色をすべて削除できます。

### ステップ 4 - データ移行

**GUID をコピーする必要があります**（`Assets/Resources/Unity-Theme Database.asset` から `Assets/Resources/Unity-Theme-Database.json` へ）。そのためには、以下のサンプルを参照してください。新旧のデータベースのデータ形式と GUID のコピー方法がわかります。今は色の値を無視しても構いません。後でカラーピッカーツールと古いカラーパレットのスクリーンショットを使用して色の値を変更する方が簡単です。

#### `Assets/Resources/Unity-Theme Database.asset` のサンプル - 旧ファイル（ソース）

`guid: 6b934efb-0b9b-42fd-82fd-7a0dbd1de53c` に注目してください。ファイルには、設定によって 3 回含まれています。guid を `Unity-Theme-Database.json` ファイルにコピーする必要があります。

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

### `Assets/Resources/Unity-Theme-Database.json` のサンプル - 新ファイル（宛先）

これは、移行プロセスの最後に作成する必要があるファイルです。
完了したら、Unity プロジェクトを閉じて開き、Unity-Theme にデータを再ロードさせる必要がある場合があります。

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

### ステップ 5 - パッケージの更新

- Unity が閉じている間に、プロジェクトから Unity-Theme `2.x.x` を削除します

```bash
openupm remove extensions.unity.theme
```

- 最新の Unity-Theme をインストールします

```bash
openupm add extensions.unity.theme
```

### ステップ 6 - クリーンアップ

- `Assets/Resources/Unity-Theme Database.asset` ファイル（オリジナルとコピー）を削除します。
- ✅ 移行完了、お疲れ様でした！
