/*
 *  Application.mm
 *  irrlichtApp iPhone Framework
 *
 */

//#import <AVFoundation/AVFoundation.h>
#import "irrlichtAppDelegate.h"
#import "iAdViewController.h"

#import <OpenGLES/EAGL.h>
#import <OpenGLES/ES1/gl.h>


#import <iostream>
#import "Application.h"

//_irrlichtMain is the equivalent of the main in a normal main.cpp,
//do your normal irrlicht stuff below (rather use InitApplication)

int IrrlichtApplication::_irrlichtMain()
{
	printf("************************************\n");
	printf("************************************\n");
	printf("** Game Create by Andrey Nenashev **\n");
	printf("************************************\n");
	printf("************************************\n");
	
	
	if(!InitApplication())
	{
		return 1;
	}
		
	return 0;
}

void IrrlichtApplication::_applicationWillUpdate()
{
	UpdateScene();
}

void IrrlichtApplication::_applicationWillTerminate()
{
	//This notification comes from the system when the home/back button is pressed, 
	//So we want to shut down immediately because we will never get back into the main loop
	QuitApplication();
}

void IrrlichtApplication::_applicationDidBecomeActive()
{
	//Respond to the application returning from a system event,
	//Such as unlocking the _device after it has "fallen asleep"
	//This is also called when the application first becomes active,
	//so becareful that you dont run something thats uninitialised

}

void IrrlichtApplication::_applicationWillResignActive()
{
	//This is called when the application will enter a suspended
	//state, for example when the "lock" button is pressed.
}

bool IrrlichtApplication::InitApplication()
{	
	[[UIApplication sharedApplication] setStatusBarOrientation:UIInterfaceOrientationLandscapeRight animated:NO];	
	
    
	//Create a copy of our application for use.
	irrlichtAppDelegate *appDelegate = (irrlichtAppDelegate*)[[UIApplication sharedApplication] delegate];
	
	_winSize = core::dimension2d<s32>(960,640);
	_controlsOffset = position2di(0, 0);
    _reciveScale = 2.0f;
    _controlsScale = 1.0f;
	
	//Also note that, this macros won't work if the application is not optimised 
	//for the iPhone 5 screen (missing the Default-568h@2x.png image), 
	//as the screen size will still be 320x480 in such a case.

	
    bool isWidescreen = (fabs( ( double )[ [ UIScreen mainScreen ] bounds ].size.height - ( double )568 ) < DBL_EPSILON );
    
	bool isRetina;
	if ([[UIScreen mainScreen] respondsToSelector:@selector(displayLinkWithTarget:selector:)] &&
		([UIScreen mainScreen].scale == 2.0)) 
	{
		// Retina display
		isRetina = true;
	}
	else 
	{
		// non-Retina display
		isRetina = false;
	}
	printf("ScreenInfo isRetina (%d) \n", isRetina);
		
	if (isRetina)
	{
        if(isWidescreen)
        {
            _winSize = core::dimension2d<s32>(1136, 640);
            _reciveScale = 2.0f;
            _controlsScale = 1.0f;
            _controlsOffset = position2di(88, 0);
        }
        else
        {
            _winSize = core::dimension2d<s32>(960, 640);
            _reciveScale = 2.0f;
            _controlsScale = 1.0f;
            _controlsOffset = position2di(0, 0);
        }	
	}
    else
    {
        _winSize = core::dimension2d<s32>(480, 320);
        _reciveScale = 1.0f;
        _controlsScale = 0.5f;
        _controlsOffset = position2di(0, 0);
    }
	
	//Set up some creation parameters , normally what is below is fine
	SIrrlichtCreationParameters params;
	
	params.DriverType = video::EDT_OGLES1;
	params.WindowSize = _winSize;
	params.WindowId   = appDelegate.window;
	params.Bits		  = 32;
	params.Stencilbuffer	=	false;

	_device = createDeviceEx((const SIrrlichtCreationParameters)params);
	
	if(!_device)
		return false;

	//The iPhone OS/Irrlicht Filesystem is out of sync, lets quickly fix that so we can load
	//our application resources as normal, as if it were a normal irrlicht filesystem
//	printf(getBundlePath().c_str());
	stringc workDir = getBundlePath() + stringc("/data");	
	_device->getFileSystem()->changeWorkingDirectoryTo(workDir.c_str());

	return true;
}


void IrrlichtApplication::CreateManager()
{	
	_userSetting.SetTextSetting("Stage", "1");
	_userSetting.SetTextSetting("MusicEnable", "true");
	_userSetting.SetTextSetting("SoundEnable", "true");
	
	_manager = new Manager(Manager::CreateSharedParams(_device, &_userSetting,
													   _winSize),
                           false,
                           _controlsScale,
                           _reciveScale,
                           _controlsOffset
						   );
	if(!_manager) 
		printf("[ERROR] <Game:initGame>[_manager](null pointer)\n");
}

void IrrlichtApplication::InitManager()
{
	_manager->Init();
}

bool IrrlichtApplication::QuitApplication()
{
	delete _manager;
	return true;
}



bool IrrlichtApplication::UpdateScene()
{
	_manager->Update();
	return true;
}

core::stringc IrrlichtApplication::getBundlePath()
{
	CFBundleRef mainBundle  = CFBundleGetMainBundle();
	CFURLRef	mainURL		= CFBundleCopyBundleURL( mainBundle );	
	CFStringRef mainPath	= CFURLCopyFileSystemPath( mainURL, kCFURLPOSIXPathStyle );
	
	long		pathLength  = CFStringGetLength( mainPath ) + 1;
	char*		cStrPath	= new char[ pathLength ];
	CFStringGetCString( mainPath, cStrPath, pathLength, kCFStringEncodingMacRoman );
		
	core::stringc path( cStrPath );
	delete[ ] cStrPath;
	
	// CLEAN UP MEMORY
	CFRelease( mainURL );
	CFRelease( mainPath );
	
	return path;
}





