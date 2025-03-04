package com.diogocake.dwebview;

import android.annotation.SuppressLint;
import android.app.Activity;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.drawable.BitmapDrawable;
import android.graphics.drawable.Drawable;
import android.view.View;
import android.view.ViewGroup;
import android.webkit.WebSettings;
import android.webkit.WebView;
import android.webkit.WebViewClient;
import android.widget.Button;
import android.widget.FrameLayout;

public class WebViewController
{
    private static WebView webView;
    private static Button closeButton;
    private static Drawable closeButtonVisual;

    public static void openWebView(final Activity unityActivity, final String url, final int width, final int height, final int posX, final int posY, final boolean openLinksInWebView, final boolean enableJavascript, final int closeButtonSizeX, final int closeButtonSizeY) {
        unityActivity.runOnUiThread(new Runnable() {
            @SuppressLint("SetJavaScriptEnabled")
            @Override
            public void run() {
                if (webView == null) {
                    webView = new WebView(unityActivity);

                    FrameLayout.LayoutParams layoutParams = new FrameLayout.LayoutParams(width, height);
                    layoutParams.leftMargin = posX;
                    layoutParams.topMargin = posY;

                    webView.setLayoutParams(layoutParams);

                    if (enableJavascript) {
                        WebSettings webSettings = webView.getSettings();
                        webSettings.setJavaScriptEnabled(true);
                    }

                    if (openLinksInWebView) {
                        // Ensure links open inside the WebView
                        webView.setWebViewClient(new WebViewClient());
                    }

                    unityActivity.addContentView(webView, webView.getLayoutParams());
                }

                if (closeButton == null) {
                    closeButton = new Button(unityActivity);

                    FrameLayout.LayoutParams buttonParams = new FrameLayout.LayoutParams(closeButtonSizeX, closeButtonSizeY);

                    buttonParams.leftMargin = posX + 15;
                    buttonParams.topMargin = posY + 15;

                    closeButton.setLayoutParams(buttonParams);

                    if (closeButtonVisual != null) {
                        closeButton.setBackground(closeButtonVisual);
                    }
                    else {
                        closeButton.setText("Close");
                    }

                    closeButton.setOnClickListener(new View.OnClickListener() {
                        @Override
                        public void onClick(View v) {
                            closeWebView(unityActivity);
                        }
                    });

                    // Add Close Button to the layout
                    unityActivity.addContentView(closeButton, buttonParams);
                }

                // Load the URL
                webView.loadUrl(url);
            }
        });
    }

    public static void closeWebView(final Activity unityActivity) {
        unityActivity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                if (webView != null) {
                    ((ViewGroup) webView.getParent()).removeView(webView);
                    webView.destroy();
                    webView = null;
                }

                if (closeButton != null) {
                    ((ViewGroup) closeButton.getParent()).removeView(closeButton);
                    closeButton = null;
                }
            }
        });
    }

    public static void setCloseButtonVisual(final Activity unityActivity, final byte[] imageData) {
        if (imageData == null || imageData.length == 0) {
            return;
        }

        Bitmap bitmap = BitmapFactory.decodeByteArray(imageData, 0, imageData.length);
        closeButtonVisual = new BitmapDrawable(unityActivity.getResources(), bitmap);
    }
}
