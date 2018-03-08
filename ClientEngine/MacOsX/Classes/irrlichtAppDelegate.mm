
#import "irrlichtAppDelegate.h"
#include "Application.h"
#import "../Flurry.h"


#define kFPS			30.0
//#define kFPS			20.0
//#define kFPS			25.0


static IrrlichtApplication *irrApp = new IrrlichtApplication();

@implementation irrlichtAppDelegate

@synthesize window;

@synthesize adController;

#define USE_TIMER_UPDATER

- (void)applicationDidFinishLaunching:(UIApplication *)application 
{
    //[Flurry startSession:@"MW2SZDY4D6J43SZMBCNF"];
    
	irrApp->_irrlichtMain();
    
#ifdef USE_TIMER_UPDATER	
	//Start the render schedule
	[NSTimer scheduledTimerWithTimeInterval:(1.0 / kFPS) target:self 
								   selector:@selector(applicationWillUpdate) userInfo:nil repeats:YES];
#else	
	printf("\n THREAD START \n \n \n ");

	dispatch_group_t group = dispatch_group_create();
	dispatch_group_async(group,dispatch_get_global_queue(DISPATCH_QUEUE_PRIORITY_DEFAULT, 0), ^ {
		NSLog(@"THREAD start");
		[self applicationWillUpdate];
		NSLog(@"THREAD end");
	});
#endif
	
	ExitNow	=	0;
	_isInit = false;
    
}

int ssCounter = 0;

- (void) applicationWillUpdate
{
	if(ExitNow)
	{
		return;
	}
    
	if(!_isInit)
	{
		// Fix me - esli perenesti v start launch - crashed
		// Ad not work if CreateManager not in main thread 
		irrApp->CreateManager();
		irrApp->InitManager();
		_isInit = true;
	}    
	
#ifdef USE_TIMER_UPDATER	
	irrApp->_applicationWillUpdate();
#else
	NSAutoreleasePool* pool = [[NSAutoreleasePool alloc] init];

	TreadWorking = 1;
	while(!ExitNow)
	{
		irrApp->_applicationWillUpdate();
	}
	TreadWorking = 0;
	
	[pool drain];
#endif
}

- (void)applicationWillResignActive:(UIApplication *)application 
{
	irrApp->_applicationWillResignActive();
}

- (void)applicationDidBecomeActive:(UIApplication *)application 
{
	irrApp->_applicationDidBecomeActive();
}

- (void)applicationDidReceiveMemoryWarning:(UIApplication *)application
{
	printf("\n \n \n applicationDidReceiveMemoryWarning ... !!!!!!!! \n \n \n");
//	[self applicationWillTerminate:application];
}



- (void)applicationWillTerminate:(UIApplication *)application 
{
	if(ExitNow)
		return;
	ExitNow	=	1;
#ifndef USE_TIMER_UPDATER	
	//wait end of thread
	while(TreadWorking) 
	{
		usleep(100);
	};
#endif
	irrApp->_applicationWillTerminate();
}

@end




