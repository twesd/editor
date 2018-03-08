//
//  irrlichtAppDelegate.h
//  irrlichtApp iPhone Framework
//


#import <UIKit/UIKit.h>
#import "iAdViewController.h"



@interface irrlichtAppDelegate : NSObject <UIApplicationDelegate> 
{
    UIWindow *window;
	
	int		 ExitNow;
	
	bool _isInit;
	
	int		 TreadWorking;	
}

@property (nonatomic, retain) IBOutlet UIWindow *window;

@property (nonatomic, retain) IBOutlet iAdViewController* adController;

- (void) applicationWillUpdate;

@end

