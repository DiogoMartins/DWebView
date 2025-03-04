// Created with ChatGPT

#import <UIKit/UIKit.h>
#import <WebKit/WebKit.h>

// WebViewController to manage the WebView
@interface WebViewController : UIViewController
@property (nonatomic, strong) WKWebView *webView;
@property (nonatomic, strong) UIButton *closeButton;
@end

@implementation WebViewController

- (void)viewDidLoad {
    [super viewDidLoad];

    // Create WebView
    self.webView = [[WKWebView alloc] initWithFrame:self.view.bounds];
    self.webView.autoresizingMask = UIViewAutoresizingFlexibleWidth | UIViewAutoresizingFlexibleHeight;
    [self.view addSubview:self.webView];

    // Create Close Button
    self.closeButton = [UIButton buttonWithType:UIButtonTypeCustom];
    self.closeButton.frame = CGRectMake(20, 50, 50, 50); // Default position and size
    [self.closeButton addTarget:self action:@selector(closeWebView) forControlEvents:UIControlEventTouchUpInside];

    // Add close button to the view
    [self.view addSubview:self.closeButton];
}

// Close WebView
- (void)closeWebView {
    [self dismissViewControllerAnimated:YES completion:nil];
}

// Load URL into WebView
- (void)loadURL:(NSString *)urlString {
    NSURL *url = [NSURL URLWithString:urlString];
    NSURLRequest *request = [NSURLRequest requestWithURL:url];
    [self.webView loadRequest:request];
}

// Set WebView size and position
- (void)setWebViewFrame:(CGFloat)x y:(CGFloat)y width:(CGFloat)width height:(CGFloat)height {
    self.webView.frame = CGRectMake(x, y, width, height);
}

// Set Close Button Image
- (void)setCloseButtonImage:(UIImage *)image width:(CGFloat)width height:(CGFloat)height {
    [self.closeButton setImage:image forState:UIControlStateNormal];
    self.closeButton.frame = CGRectMake(self.view.bounds.size.width - width - 20, 50, width, height);
}

@end

// Global reference to WebViewController
WebViewController *webViewController = nil;

// C++ Functions Exposed to Unity
extern "C" {

// Open WebView from Unity
void OpenWebView(const char* url, float x, float y, float width, float height) {
    dispatch_async(dispatch_get_main_queue(), ^{
        if (webViewController == nil) {
            webViewController = [[WebViewController alloc] init];
        }

        UIViewController *rootViewController = [UIApplication sharedApplication].keyWindow.rootViewController;
        [rootViewController presentViewController:webViewController animated:YES completion:^{
            [webViewController loadURL:[NSString stringWithUTF8String:url]];
            [webViewController setWebViewFrame:x y:y width:width height:height];
        }];
    });
}

// Set close button image from Unity
void SetCloseButtonImage(const void* imageData, int length, float width, float height) {
    NSData *data = [NSData dataWithBytes:imageData length:length];
    UIImage *image = [UIImage imageWithData:data];
    
    if (webViewController != nil) {
        dispatch_async(dispatch_get_main_queue(), ^{
            [webViewController setCloseButtonImage:image width:width height:height];
        });
    }
}

// Close WebView
void CloseWebView() {
    if (webViewController != nil) {
        dispatch_async(dispatch_get_main_queue(), ^{
            [webViewController closeWebView];
            webViewController = nil;
        });
    }
}

}
