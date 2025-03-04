using System.Linq;
using UnityEngine;

public class DWebViewManager
{
    public static void OpenWebView(string url, DWebViewOptions webViewOptions = null)
    {
        if (webViewOptions == null)
        {
            webViewOptions = DWebViewOptions.Default;
        }

#if UNITY_EDITOR
            Application.OpenURL("https://www.youtube.com/");
#elif UNITY_ANDROID
            CallAndroidJavaMethod("openWebView", url,
                webViewOptions.Size.x,
                webViewOptions.Size.y,
                webViewOptions.Position.x,
                webViewOptions.Position.y,
                webViewOptions.OpenLinksInWebView,
                webViewOptions.EnableJavascript,
                webViewOptions.CloseButtonSize.x,
                webViewOptions.CloseButtonSize.y
                );
#endif
    }

    public static void CloseWebView()
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        CallAndroidJavaMethod("closeWebView");
#endif
    }

    public static void SetCloseButtonVisual(string textureName)
    {
        Texture2D texture = Resources.Load<Texture2D>(textureName);

        if (texture == null)
        {
            return;
        }

        Texture2D readableTexture = new Texture2D(texture.width, texture.height, TextureFormat.RGBA32, false);
        readableTexture.SetPixels(texture.GetPixels());
        readableTexture.Apply();

        SetCloseButtonVisual(readableTexture.EncodeToPNG());
    }
    
    public static void SetCloseButtonVisual(byte[] imageBytes)
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        CallAndroidJavaMethod("setCloseButtonVisual", imageBytes);
#endif
    }

    private static void CallAndroidJavaMethod(string methodName, params object[] parameters)
    {
        using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaClass javaClass = new AndroidJavaClass("com.diogocake.dwebview.WebViewController");

            javaClass.CallStatic(methodName, (new object[] { activity }).Concat(parameters).ToArray());
        }
    }
}
