# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

Unity-Theme is a Unity Package Manager (UPM) plugin (`extensions.unity.theme`, v4.2.0) for runtime color theming. It provides color palettes, GUID-based color binding to UI/renderer components, and theme switching. Minimum Unity version: 2022.3.

## Repository Structure

- **Unity-Theme/** — Main plugin project (UPM package at `Assets/root/`)
  - `Scripts/Config/` — Core `Theme` class (partial, split across 6 files: Theme.cs, Theme.Singleton.cs, Theme.Colors.cs, Theme.Theme.cs, Theme.Settings.cs, Theme.Editor.cs)
  - `Scripts/Binders/` — 19 color binder implementations with abstract base classes in `Base/`
  - `Scripts/Extensions/` — Color utility extensions (hex conversion, component setters)
  - `Scripts/Utils/` — Logging, safe event invocation, weak references
  - `Editor/` — Custom inspectors, property drawers, editor window
  - `Tests/` — NUnit tests split into Base (shared utilities), Editor, and Runtime
- **Installer/** — Separate Unity project that builds `Unity-Theme-Installer.unitypackage`
- **docs/** — README translations (Chinese, Japanese, Spanish)

## Architecture

**Singleton + Event-Driven:** `Theme` is a partial class singleton managing color palettes and notifying binders on changes via `onThemeChanged` and `onThemeColorChanged` events.

**Data Model:** `ThemeData` (named theme with GUID → list of `ColorData`) → `ColorData` (GUID + hex color) → `ColorDataRef` (color reference by name). `ColorBinderData` stores binding metadata (color GUID, alpha override).

**Binder Hierarchy:**
```
BaseColorBinder (abstract, [ExecuteAlways])
├── GenericColorBinder<T>        — single-target binders (Image, Button, TextMeshPro, Light, etc.)
├── GenericRendererColorBinder<T> — renderer/material binders (Mesh, Skinned, SpriteShape)
└── BaseMultiColorBinder → GenericMultiColorBinder<T> — multiple color bindings per component
```
Binders subscribe to Theme events, call abstract `SetColorInternal(Color)` on invalidation, and support alpha overrides.

**Namespaces:** `Unity.Theme`, `Unity.Theme.Binders`, `Unity.Theme.Utils`, `Unity.Theme.Editor`

**Assembly Definitions:** `Unity.Theme` (runtime), `Unity.Theme.Editor` (editor-only), `Unity.Theme.Tests.Base`, `Unity.Theme.Tests.Editor`, `Unity.Theme.Tests.Runtime`

## Testing

Tests use Unity Test Framework (NUnit). Shared test utilities in `Tests/Base/` (`TestBase`, `TestUtils`).

**Run tests via CI:** Tests run automatically on PRs (requires `ci-ok` label). The CI matrix tests:
- 3 Unity versions: 2022.3.62f3, 2023.2.22f1, 6000.3.9f1
- 3 modes: editmode, playmode, standalone
- 2 platforms: base, windows-mono

**Run tests locally:** Open `Unity-Theme/` project in Unity Editor → Window → General → Test Runner → Run All.

## CI/CD

- **test_pull_request.yml** — Runs on PRs to main/dev (gated by `ci-ok` label)
- **release.yml** — On main push: validates version tag, runs full test matrix, builds installer .unitypackage, creates GitHub release with auto-changelog
- **test_unity_plugin.yml** — Reusable workflow for the test matrix

## Key Conventions

- Colors are stored as hex strings and referenced by GUID (survives renames)
- Theme data persists as JSON in `Resources/Unity-Theme-Database.json`
- Package version in `Unity-Theme/Assets/root/package.json` drives release tags
- Binders use `[ExecuteAlways]`/`[ExecuteInEditMode]` for live editor preview
- Logging uses `LogableMonoBehaviour` base class with configurable `DebugLevel`
