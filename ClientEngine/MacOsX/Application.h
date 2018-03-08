/*
 *  Application.h
 *  irrlichtApp iPhone Framework
 *
 */

#ifndef APPLCATION_H_
#define APPLCATION_H_

#import <AVFoundation/AVFoundation.h>
#import "irrlicht.h"
#include "game/engine/Manager.h"


using namespace std;
using namespace irr;
using namespace core;
using namespace scene;
using namespace video;
using namespace io;
using namespace gui;


class IrrlichtApplication
{
public:
	
	void _applicationWillResignActive();
	void _applicationDidBecomeActive();
	void _applicationWillUpdate();
	void _applicationWillTerminate();
	
	int  _irrlichtMain();
	void InitManager();
	void CreateManager();
	
	bool InitApplication();
	bool QuitApplication();
	bool UpdateScene();
		
private:
	IrrlichtDevice*	_device;
	
    Manager*	_manager;
	
    UserSettings _userSetting;
	
    core::dimension2d<s32> _winSize;
	
    float _controlsScale;
    
    float _reciveScale;
    
    position2di _controlsOffset;

	core::stringc getBundlePath();
};



#endif //APPLCATION_H_

