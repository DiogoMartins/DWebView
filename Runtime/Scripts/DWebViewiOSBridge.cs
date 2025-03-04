using System.Runtime.InteropServices;

public class DWebViewiOSBridge
{
#if UNITY_IOS
    [DllImport("__Internal")]
    private static extern void OpenWebView(string url, float x, float y, float width, float height);

    [DllImport("__Internal")]
    private static extern void SetCloseButtonImage(byte[] imageData, int length, float width, float height);

    [DllImport("__Internal")]
    private static extern void CloseWebView();
#endif

    public static void Open(string url, float x, float y, float width, float height)
    {
#if UNITY_IOS
        OpenWebView(url, x, y, width, height);
#endif
    }

    public static void Close()
    {
#if UNITY_IOS
        CloseWebView();
#endif
    }

    public static void SetCloseButtonVisual(byte[] imageBytes, float width, float height)
    {
#if UNITY_IOS
        SetCloseButtonImage(imageBytes, imageBytes.Length, width, height);
#endif
    }
}
