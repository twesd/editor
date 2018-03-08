#ifndef __3D_H_GLOBAL_INCLUDED__
#define __3D_H_GLOBAL_INCLUDED__

#include "irrlicht.h"
#include <iostream>
#include "configureCompile.h"
#ifdef IPHONE_COMPILE
	#include <pthread.h>
#else
	//include here some windows headers
#endif
//Include here owner game defines
#include "defs.h"

#include "Core/Parameter.h"
#include "Core/UserSettings.h"

using namespace irr;
using namespace core;
using namespace scene;


////////////////////////////////////////// STRUCTs ////////////////////////////////////////////////////


typedef struct
{
	IrrlichtDevice*			Device;
	video::IVideoDriver*	Driver;
	scene::ISceneManager*	SceneManager;
	UserSettings*			Settings;
	ITimer*					Timer;
	io::IFileSystem*        FileSystem;
	void*					Event;	
	u32*					TimeDiff;
	// Глобальные параметры - действуют в пределах стадии
	core::array<Parameter>* GlobalParams;
	// Параметры игры - действуют в пределах всего запуска игры
	core::array<Parameter>* GameParams;

	core::dimension2d<s32> WindowSize;
} SharedParams_t;


typedef struct
{	
	void *CommonParam;
	int IntVar;
	u32 U32Var;
	stringc StrVar;
}EventParameters_t;

typedef struct
{
	int HandlerId;
	int EventId;
	EventParameters_t Params;
	bool IsUsed;
} Event_t;


typedef struct
{
	int XTouch[2];
	int YTouch[2];
	int StateTouch[2];
	f32 Xaccel;
	f32 Yaccel;
	f32 Zaccel;
#ifndef IPHONE_COMPILE
	bool MousePressed;
#endif
}reciverInfo_t;


#endif