#include "AdManager.h"


#ifdef IPHONE_COMPILE

#import <UIKit/UIKit.h>
#import <iAd/iAd.h>
#import "irrlichtAppDelegate.h"
#import "iAdViewController.h"


AdManager::AdManager(SharedParams_t params) : Base(params)
{
	_adController = NULL;
//    [self performSelectorOnMainThread:@selector(AllocAdController) withObject:nil waitUntilDone:true];
	AllocAdController();
}

AdManager::~AdManager(void)
{
	
}

void AdManager::ShowAd(u32 waitTime)
{
	iAdViewController* adController = (iAdViewController*)_adController;
	[adController ShowBanner];
}

void AdManager::HideAd()
{
	iAdViewController* adController = (iAdViewController*)_adController;
	[adController HideBanner];
}

void AdManager::AllocAdController()
{
	if (_adController != NULL) 
	{
		return;
	}
	
	irrlichtAppDelegate* appDelegate = (irrlichtAppDelegate*)[[UIApplication sharedApplication] delegate];
		
	CGRect rect;
	rect.origin.x = 0;
	rect.origin.y = 0;
	rect.size.width = 0;
	rect.size.height = 0;
	
	UIView* mainView = [[UIView alloc]  initWithFrame:CGRectMake(0, 0, 0, 0)];
	mainView.backgroundColor = [UIColor blackColor];
	mainView.autoresizesSubviews = YES;
	
	iAdViewController* adController = [[iAdViewController alloc] init];
	adController.view = mainView;
	
	//[[appDelegate window] addSubview:adController.view];
	//[[appDelegate window] makeKeyAndVisible];
 	[[[[appDelegate window] rootViewController] view] addSubview:adController.view];
    
	//CGRect screenBounds = [UIScreen mainScreen].bounds;
	//adController.view.frame = CGRectMake(0, 0, 50, screenBounds.size.height);	
	//adController.view.frame = CGRectMake(-50, 0, 50, screenBounds.size.height);
	
	[adController CreateBanner];
	
	_adController = adController;
}

#endif