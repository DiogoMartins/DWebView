# DWebView - Native WebView for Unity (Android & iOS)

DWebView is a Unity package that allows you to open native WebViews on Android and iOS devices. This package provides a simple API to display webpages within your Unity application, offering options for size, position, JavaScript support, and custom close button visuals.

## Features
- Open native WebViews on Android and iOS
- Set custom size and position for the WebView
- Enable or disable JavaScript support
- Control whether links open inside the WebView or an external browser
- Customize the close button with an image
- Simple API for opening and closing the WebView

## Installation
1. Clone or download this repository.
2. Import the package into your Unity project.
3. Add the necessary platform-specific dependencies if required (Android/iOS).

## API Reference

### OpenWebView
Opens a native WebView with the specified URL and options.

```csharp
DWebViewManager.OpenWebView("https://example.com", DWebViewOptions.Default);
```

or

```csharp
DWebViewManager.OpenWebView("https://example.com", new DWebViewOptions
{
    Size = new Vector2Int(800, 600),
    Position = new Vector2Int(100, 100),
    EnableJavascript = true,
    OpenLinksInWebView = false,
    CloseButtonSize = new Vector2Int(50, 50)
});
```

Note: when creating a `DWebViewOptions`, some default values are set, like `Size` (defaults to fullscreen).

#### Parameters
- `url` (`string`): The URL to open.
- `webViewOptions` (`DWebViewOptions`, optional): Customization options for the WebView.

### CloseWebView
Closes the currently open WebView.
```csharp
DWebViewManager.CloseWebView();
```

### SetCloseButtonVisual
Sets a custom image for the WebView close button.
```csharp
DWebViewManager.SetCloseButtonVisual("Textures/CloseButton");
```

#### Parameters
- `textureName` (string): The name of the texture in the Unity `Resources` folder.

Alternatively, set the close button using a byte array:
```csharp
byte[] imageBytes = File.ReadAllBytes("path/to/image.png");
DWebViewManager.SetCloseButtonVisual(imageBytes);
```

## Editor Behavior
In the Unity Editor, WebView calls will open an external browser instead, as native WebViews are not supported in the editor.

## License
This package is licensed under the Apache-2.0 license.

## Contributing
Feel free to submit issues and pull requests to improve the package!

## Author
Developed by Diogo Martins.

