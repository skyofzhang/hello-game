# Hello Game - Scene Setup Instructions for Cursor

## Unity Version: 2022.3.47f1c1
## Render Pipeline: Built-In Render Pipeline (3D)
## Orientation: Portrait (1080x1920)

## Scene Hierarchy (MainScene.unity)

```
MainScene
  Main Camera (default)
  Canvas (Screen Space - Overlay, Canvas Scaler: Scale With Screen Size, Reference: 1080x1920)
    BackgroundImage
        Component: Image
        RectTransform: Stretch all (left=0, right=0, top=0, bottom=0)
        Color: #1a1a2e (solid dark blue)
    TitleText
        Component: TextMeshPro - Text (UI)
        Text: "Hello Game"
        Font Size: 72
        Color: #FFFFFF (white)
        Alignment: Center + Middle
        RectTransform: anchorMin(0.1, 0.45) anchorMax(0.9, 0.6) pivot(0.5, 0.5)
    ColorButton
        Component: Button + Image
        RectTransform: anchorMin(0.3, 0.12) anchorMax(0.7, 0.18) pivot(0.5, 0.5)
        Image Color: #4A90D9
        Child: ButtonText (TextMeshPro)
            Text: "Change Color"
            Font Size: 28
            Color: #FFFFFF
            Alignment: Center + Middle
  GameManager (Empty GameObject)
      Component: GameManager.cs
      Drag References:
        titleText -> TitleText
        colorButton -> ColorButton
```

## Build Settings
- Platform: Android
- Architecture: ARM64
- Package Name: com.jiuqian.hellogame
- Minimum API Level: Android 7.0 (API 24)
- Target API Level: Automatic (highest installed)
- Scripting Backend: IL2CPP

## Required Packages
- TextMeshPro (com.unity.textmeshpro) - should be included by default

## Acceptance Criteria
- AC1: Launch within 3 seconds, no black screen
- AC2: Background fills entire screen, no black edges
- AC3: "Hello Game" text centered, white, clearly readable
- AC4: Button at bottom center, clickable
- AC5: Click button -> text changes color immediately (less than 50ms)
- AC6: 10 consecutive clicks without crash
- AC7: APK installs and runs normally
