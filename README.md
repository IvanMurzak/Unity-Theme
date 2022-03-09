# Unity-Theme
![npm](https://img.shields.io/npm/v/extensions.unity.theme) ![License](https://img.shields.io/github/license/IvanMurzak/Unity-Theme)

Create palettes of colors and components for change specific color on a specific visual element. Very useful to UI.

![Unity Theme Settings](https://imgur.com/FKVF2H9.gif)

# How to install
- Install [ODIN Inspector](https://odininspector.com/)
- Add this code to <code>/Packages/manifest.json</code>
```json
{
  "dependencies": {
    "extensions.unity.theme": "0.0.2",
  },
  "scopedRegistries": [
    {
      "name": "Unity Extensions",
      "url": "https://registry.npmjs.org",
      "scopes": [
        "extensions.unity"
      ]
    }
  ]
}
```

# Color Binder
Each colored component should be binded to specific color.

- ImageColorBinder
- SpriteRendereColorBinder
- TextMeshProColorBinder

![Color Binders](https://imgur.com/AeNC3tF.gif)
