using UnityEngine;

public class DWebViewOptions
{
    public Vector2Int Size = new Vector2Int(Screen.width, Screen.height);
    public Vector2Int Position = Vector2Int.zero;

    public Vector2Int CloseButtonSize = new Vector2Int(100, 100);

    public bool EnableJavascript;
    public bool OpenLinksInWebView;

    public static DWebViewOptions Default = new DWebViewOptions();
}
