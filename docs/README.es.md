# [Unity-Theme (Paleta de Colores)](https://github.com/IvanMurzak/Unity-Theme)

[![openupm](https://img.shields.io/npm/v/extensions.unity.theme?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/extensions.unity.theme/)
[![r](https://github.com/IvanMurzak/Unity-Theme/workflows/release/badge.svg 'Pruebas Superadas')](https://github.com/IvanMurzak/Unity-Theme/actions/workflows/release.yml)
[![Unity Editor](https://img.shields.io/badge/Editor-X?style=flat&logo=unity&labelColor=333A41&color=49BC5C 'Unity Editor compatible')](https://unity.com/releases/editor/archive)
[![Unity Runtime](https://img.shields.io/badge/Runtime-X?style=flat&logo=unity&labelColor=333A41&color=49BC5C 'Unity Runtime compatible')](https://unity.com/releases/editor/archive)

[![Stars](https://img.shields.io/github/stars/IvanMurzak/Unity-Theme 'Stars')](https://github.com/IvanMurzak/Unity-Theme/stargazers)
[![License](https://img.shields.io/github/license/IvanMurzak/Unity-Theme)](https://github.com/IvanMurzak/Unity-Theme/blob/main/LICENSE)
[![Stand With Ukraine](https://raw.githubusercontent.com/vshymanskyy/StandWithUkraine/main/badges/StandWithUkraine.svg)](https://stand-with-ukraine.pp.ua)

Crea paletas de colores y componentes para cambiar colores específicos en elementos visuales específicos. Muy útil para UI.

![Unity-Theme-1](https://github.com/IvanMurzak/Unity-Theme/assets/9135028/1c545d11-aea4-4cd2-8aaa-75539bbb6699)

<div align="center" width="100%">

<b>[English](https://github.com/IvanMurzak/Unity-Theme/blob/main/README.md) | [中文](https://github.com/IvanMurzak/Unity-Theme/blob/main/docs/README.zh-CN.md) | [日本語](https://github.com/IvanMurzak/Unity-Theme/blob/main/docs/README.ja.md)</b>

</div>

## Características

- ✔️ Nombrar colores
- ✔️ Crear temas personalizados con nombres
- ✔️ Agregar tantos colores como necesites
- ✔️ Nombrar temas como quieras
- ✔️ Cambiar tema en cualquier momento por nombre
- ✔️ Vincular color a: `Image`, `SpriteRenderer`, `TextMeshPro`, etc
- ✔️ Forma fácil de agregar vinculador de color personalizado con una nueva clase C# con solo unas pocas líneas de código
- ✔️ Renombrar color incluso después de vincular a un componente, sin enlaces rotos

## Estado de estabilidad

| Versión Unity | Prueba Editor | Prueba Player | Prueba Build |
|---------------|-------------|-------------|------------|
| 2022.3.57f1   | ![2022.3.57f1](https://github.com/IvanMurzak/Unity-Theme/workflows/release/badge.svg?job=test-unity-2022-3-57f1-editmode) | ![2022.3.57f1](https://github.com/IvanMurzak/Unity-Theme/workflows/release/badge.svg?job=test-unity-2022-3-57f1-playmode) | ![2022.3.57f1](https://github.com/IvanMurzak/Unity-Theme/workflows/release/badge.svg?job=test-unity-2022-3-57f1-standalone) |
| 2023.1.20f1   | ![2023.1.20f1](https://github.com/IvanMurzak/Unity-Theme/workflows/release/badge.svg?job=test-unity-2023-1-20f1-editmode) | ![2023.1.20f1](https://github.com/IvanMurzak/Unity-Theme/workflows/release/badge.svg?job=test-unity-2023-1-20f1-playmode) | ![2023.1.20f1](https://github.com/IvanMurzak/Unity-Theme/workflows/release/badge.svg?job=test-unity-2023-1-20f1-standalone) |
| 2023.2.20f1   | ![2023.2.20f1](https://github.com/IvanMurzak/Unity-Theme/workflows/release/badge.svg?job=test-unity-2023-2-20f1-editmode) | ![2023.2.20f1](https://github.com/IvanMurzak/Unity-Theme/workflows/release/badge.svg?job=test-unity-2023-2-20f1-playmode) | ![2023.2.20f1](https://github.com/IvanMurzak/Unity-Theme/workflows/release/badge.svg?job=test-unity-2023-2-20f1-standalone) |
| 6000.0.37f1   | ![6000.0.37f1](https://github.com/IvanMurzak/Unity-Theme/workflows/release/badge.svg?job=test-unity-6000-0-37f1-editmode) | ![6000.0.37f1](https://github.com/IvanMurzak/Unity-Theme/workflows/release/badge.svg?job=test-unity-6000-0-37f1-playmode) | ![6000.0.37f1](https://github.com/IvanMurzak/Unity-Theme/workflows/release/badge.svg?job=test-unity-6000-0-37f1-standalone) |

# Instalación

### Opción 1 - Instalador

- **[⬇️ Descargar Instalador](https://github.com/IvanMurzak/Unity-Theme/releases/download/4.2.0/Unity-Theme-Installer.unitypackage)**
- **📂 Importar instalador al proyecto Unity**
  > - Puede hacer doble clic en el archivo - Unity lo abrirá
  > - O: Puede abrir Unity Editor primero, luego hacer clic en `Assets/Import Package/Custom Package`, luego elegir el archivo

### Opción 2 - OpenUPM-CLI

- [⬇️ Instalar OpenUPM-CLI](https://github.com/openupm/openupm-cli#installation)
- 📟 Abrir línea de comandos en la carpeta del proyecto Unity

```bash
openupm add extensions.unity.theme
```

### Opción 3 - Asset Store

- **[▶️ Abrir Asset Store](https://u3d.as/3DQp)**

---

# Uso

- Ir a `Window/Unity-Theme`
- Personalizar paletas de colores como desees
- Agregar componentes `ColorBinder` al GameObject de destino

### Vinculador de Color

Un componente que vincula un color a un objetivo específico, como `Image`, `SpriteRenderer`, `TextMeshPro`, o cualquier otra cosa.

Hay una lista de vinculadores de color incorporados:

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

### Paletas de colores

Modifica paletas, respuesta instantánea con todos los GameObjects conectados.

![Unity-Theme-Palettes](https://github.com/IvanMurzak/Unity-Theme/assets/9135028/179215af-23f1-4a8e-bb29-a7169f3433a5)

## API de C#

### Colores

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

### Temas

```csharp
using Unity.Theme;

Theme.Instance.AddTheme("Light");
Theme.Instance.SetOrAddTheme("Light");

Theme.Instance.CurrentThemeName = "Light";
Theme.Instance.CurrentThemeIndex = 0;

Theme.Instance.RemoveTheme("Light");
Theme.Instance.RemoveAllThemes();
```

### Crear un `ColorBinder` personalizado

Si necesita vincular color a otra cosa, puede extender desde `BaseColorBinder` como se indica a continuación.

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

## Otros

- **[Constructor de paleta de colores](https://m3.material.io/theme-builder#/custom)** de Material Design V3 de Google

---

## Migración desde la versión `2.x.x`

La versión `3.x.x` o más reciente tiene una estructura de base de datos diferente, por lo que necesita migrar sus datos manualmente si desea mantener los vinculadores existentes conectados al color correcto. Los vinculadores utilizan GUIDs para conectarse a los colores.

> ❗❗❗**Por favor, siga el orden exacto de los pasos**

### Paso 1

Cerrar el proyecto Unity para evitar cualquier pérdida de datos.

### Paso 2 - copia de seguridad

- Hacer una copia de seguridad de `Assets/Resources/Unity-Theme Database.asset`, colocarla fuera del proyecto.
- Hacer una captura de pantalla de los colores en cada tema para reproducirlos más tarde con el selector de color.

### Paso 3 - plantilla de base de datos

- Descargar [Unity-Theme-Database.json](https://raw.githubusercontent.com/IvanMurzak/Unity-Theme/refs/heads/main/Assets/Resources/Unity-Theme-Database.json).
- Guardarlo en `Assets/Resources/Unity-Theme-Database.json`.
- Usarlo como plantilla. Siéntase libre de eliminar todos los colores existentes si lo desea.

### Paso 4 - migración de datos

**Necesita copiar los GUIDs** desde `Assets/Resources/Unity-Theme Database.asset` a `Assets/Resources/Unity-Theme-Database.json`. Para hacerlo, consulte los ejemplos a continuación. Puede ver cuál es el formato de datos de las bases de datos antiguas y nuevas, y cómo copiar los GUIDs. Puede ignorar el color por ahora, es más fácil cambiar los valores de color más tarde usando la herramienta de selector de color y la captura de pantalla de la paleta de colores antigua.

#### Ejemplo de `Assets/Resources/Unity-Theme Database.asset` - archivo antiguo (origen)

Observe el `guid: 6b934efb-0b9b-42fd-82fd-7a0dbd1de53c`, el archivo lo contiene 3 veces, dependiendo de su configuración. Necesitará copiar el guid al archivo `Unity-Theme-Database.json`.

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

### Ejemplo de `Assets/Resources/Unity-Theme-Database.json` - archivo nuevo (destino)

Este es el archivo que necesita crear al final del proceso de migración.
Cuando termine, es posible que deba cerrar y abrir el proyecto Unity para permitir que Unity-Theme recargue los datos.

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

### Paso 5 - actualización del paquete

- Eliminar Unity-Theme `2.x.x` del proyecto mientras Unity está cerrado

```bash
openupm remove extensions.unity.theme
```

- Instalar Unity-Theme más reciente

```bash
openupm add extensions.unity.theme
```

### Paso 6 - limpieza

- Eliminar el archivo `Assets/Resources/Unity-Theme Database.asset` (original y copia).
- ✅ migración completada, ¡bien hecho!
