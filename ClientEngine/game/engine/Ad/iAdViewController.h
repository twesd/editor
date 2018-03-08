//
//  irrlichtAppDelegate.h
//  irrlichtApp iPhone Framework
//


#import <UIKit/UIKit.h>
#import <iAd/iAd.h>


@interface iAdViewController : UIViewController <ADBannerViewDelegate>
{
	ADBannerView* _adBannerView;
	BOOL _isReady;
}

- (void) CreateBanner;

- (void) CreateBannerInner;

- (void) ShowBanner;

-(void) ShowBannerInner;

- (void) adjustBannerView;

- (BOOL) IsReady;

- (void) HideBanner;

- (void) HideBannerInner;

@end

