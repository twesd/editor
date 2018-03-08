
#import "iAdViewController.h"

@implementation iAdViewController

- (void) CreateBanner
{
	[self performSelectorOnMainThread:@selector(CreateBannerInner) withObject:nil waitUntilDone:true];
}

- (void) CreateBannerInner
{
	_isReady = false;
	if(_adBannerView != NULL)
	{
		_isReady = true;
		return;
	}
	_adBannerView = [[ADBannerView alloc] initWithFrame:CGRectZero];
    
	_adBannerView.requiredContentSizeIdentifiers = [NSSet setWithObjects: ADBannerContentSizeIdentifierLandscape, nil];
	
	_adBannerView.currentContentSizeIdentifier = ADBannerContentSizeIdentifierLandscape;
	_adBannerView.delegate=self;
}

-(void) ShowBanner
{
	[self performSelectorOnMainThread:@selector(ShowBannerInner) withObject:nil waitUntilDone:true];
}

-(void) ShowBannerInner
{
	printf("ShowBannerInner\n");
	if ([[self.view subviews] count] == 0)
	{
		[self.view setHidden:NO];
		CGRect screenBounds = [UIScreen mainScreen].bounds;
		self.view.frame = CGRectMake(-50, 0, 50, screenBounds.size.height);
		[self.view addSubview:_adBannerView];
	}
	
    
	if ([_adBannerView isBannerLoaded])
	{
		[self adjustBannerView];
	}	
}

-(void) HideBanner
{
	[self performSelectorOnMainThread:@selector(HideBannerInner) withObject:nil waitUntilDone:true];
}

- (void) HideBannerInner
{
	printf("HideBannerInner \n");
	
	if ([[self.view subviews] count] > 0) 
	{
		[_adBannerView removeFromSuperview];
	}	
	CGRect screenBounds = [UIScreen mainScreen].bounds;
	self.view.frame = CGRectMake(-50, 0, 50, screenBounds.size.height);	
	[self.view setHidden:YES];
}

- (void)bannerViewDidLoadAd:(ADBannerView *)banner
{
	printf("bannerViewDidLoadAd\n");
	_isReady = true;
    [self adjustBannerView];
}

- (void)bannerView:(ADBannerView *)banner didFailToReceiveAdWithError:(NSError *)error
{
	NSLog(@"%@", error);
	_isReady = true;
    [self adjustBannerView];
}

- (BOOL)bannerViewActionShouldBegin:(ADBannerView *)banner willLeaveApplication:(BOOL)willLeave
{
    return YES;
}

- (void)bannerViewActionDidFinish:(ADBannerView *)banner
{
	
}


- (void) adjustBannerView
{
	if([[self.view subviews] count] == 0)
	{
		return;
	}
	
    CGRect adBannerFrame = _adBannerView.frame;
	CGRect bannerBounds = _adBannerView.bounds;
	
    if([_adBannerView isBannerLoaded])
    {
		printf("isBannerLoaded - true\n");
		[self.view setHidden:NO];
		CGRect screenBounds = [UIScreen mainScreen].bounds;
		self.view.frame = CGRectMake(0, 
									 0, 
									 bannerBounds.size.width,
									 bannerBounds.size.height);	
    }
    else
    {
		printf("isBannerLoaded - false\n");
        [self HideBanner];
		return;
    }
    [UIView animateWithDuration:0.5 animations:^{
		[self.view layoutIfNeeded];
		_adBannerView.frame = self.view.bounds;
    }];
	// Do events
	[[NSRunLoop currentRunLoop] runUntilDate:[NSDate date]];
}

- (BOOL) IsReady
{
	return _isReady;
}

- (BOOL)shouldAutorotateToInterfaceOrientation:(UIInterfaceOrientation)interfaceOrientation 
{
    if(UIInterfaceOrientationIsLandscape(interfaceOrientation))
		return YES;
	return NO;
}

- (BOOL)shouldAutorotate
{
    return [self shouldAutorotateToInterfaceOrientation:self.interfaceOrientation];
}

- (NSUInteger)supportedInterfaceOrientations
{
    return UIInterfaceOrientationMaskLandscapeRight;
}

- (UIInterfaceOrientation)preferredInterfaceOrientationForPresentation
{
    return UIInterfaceOrientationMaskLandscapeRight;
}



@end