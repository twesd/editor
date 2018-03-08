#include "RateAppManager.h"


#ifdef IPHONE_COMPILE

RateAppManager::RateAppManager()
{
	
}

void RateAppManager::RateApp( stringc appId )
{
	#if TARGET_IPHONE_SIMULATOR
	NSLog(@"APPIRATER NOTE: iTunes App Store is not supported on the iOS simulator. Unable to open App Store page.");
	#else
	NSString* templateReviewURL = @"itms-apps://ax.itunes.apple.com/WebObjects/MZStore.woa/wa/viewContentsUserReviews?type=Purple+Software&id=APP_ID";
	NSString* nsAppId = [NSString stringWithUTF8String:appId.c_str()];
	NSString *reviewURL = [templateReviewURL stringByReplacingOccurrencesOfString:@"APP_ID" withString:[NSString stringWithFormat:@"%@", nsAppId]];
	[[UIApplication sharedApplication] openURL:[NSURL URLWithString:reviewURL]];
	#endif
}


#endif