#include "StdAfx.h"
#include "BaseW.h"


BaseW::BaseW(SharedParams_t* params)
{
	Device =	params->Device;
	Driver	=	params->Driver;
	SceneManager = params->SceneManager;
	Timer	=	params->Timer;
	EventBase	=	(Event *)params->Event;
	FileSystem = params->FileSystem;	
	SeedRandomize = Timer->getRealTime();
	SharedParams = params;
}
