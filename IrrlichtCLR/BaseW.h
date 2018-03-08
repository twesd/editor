#pragma once
#include "core/Base.h"
#include "Vertex3dW.h"
#include "BoundboxW.h"

public ref class BaseW
{
public:
	BaseW(SharedParams_t* sharedParams);

protected:
		///////////////////////////// Свойства ///////////////////////////////////
	SharedParams_t*			SharedParams;

	//irrlicht members
	IrrlichtDevice*			Device;
	video::IVideoDriver*	Driver;
	scene::ISceneManager*	SceneManager;
	io::IFileSystem*		FileSystem;
	ITimer*					Timer;

	Event*					EventBase;
	//seed for random 
	int						SeedRandomize;	

};

