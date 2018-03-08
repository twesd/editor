// 3DAnimalFight.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "game/engine/Manager.h"
#include <iostream>
#include <irrKlang.h>
//#include <vld.h>

using namespace irrklang;
#pragma comment(lib, "irrKlang.lib") // link with irrKlang.dll

int _tmain(int argc, _TCHAR* argv[])
{
	Manager		*manager;
	IrrlichtDevice	*Device;
	ITimer*			Timer;
	unsigned int	time1 = 0, time2 = 0;

	//VLDEnable();

	ISoundEngine* soundEngine = createIrrKlangDevice();

	video::E_DRIVER_TYPE driverType = video::EDT_OPENGL;//EDT_DIRECT3D9//EDT_SOFTWARE//EDT_NULL

	float reciverScale = 1.0f; // Не используется в windows версии
	position2di controlsOffset;

	
	float controlsScale = 1.0f;
	core::dimension2d<s32> windowSize = core::dimension2d<s32>(960, 640);
	
	//float controlsScale = 0.5f;
	//core::dimension2d<s32> windowSize = core::dimension2d<s32>(480, 320);

	/*float controlsScale = 1.0f;
	core::dimension2d<s32> windowSize = core::dimension2d<s32>(1136, 640);
	controlsOffset.X = 88;*/

	if(argc > 2)
	{
		_TCHAR* argDisplayMode = argv[2];

		if(wcscmp(argDisplayMode, L"480_320") == 0)
		{
			windowSize = core::dimension2d<s32>(480, 320);
			reciverScale = 1.0f;
			controlsScale = 0.5f;
			controlsOffset = position2di(0, 0);
		}
		else if(wcscmp(argDisplayMode, L"960_640") == 0)
		{
			windowSize = (core::dimension2d<s32>(960, 640));
			reciverScale = 2.0f;
			controlsScale = 1.0f;
			controlsOffset = position2di(0, 0);
		}
		else if(wcscmp(argDisplayMode, L"1136_640") == 0)
		{
			windowSize = (core::dimension2d<s32>(1136, 640));
			reciverScale = 2.0f;
			controlsScale = 1.0f;
			controlsOffset = position2di(88, 0);
		}
	}

	Device = createDevice(driverType, windowSize,
		16, false, false, false);
	Timer = Device->getTimer();

	if (argc > 1)
	{
		_TCHAR* path = argv[1];
		char cPath[100];
		wcstombs(cPath, path, 100);
		Device->getFileSystem()->changeWorkingDirectoryTo(cPath);
	}
	else
	{
		//Device->getFileSystem()->changeWorkingDirectoryTo("C:/_Work_/Projects/Data/RoboKiller");
		//test only
		bool res = Device->getFileSystem()->addZipFileArchive("C:/_Work_/Projects/3DRoboKiller.zip", "assets", true, false);
	}

	if(soundEngine != NULL)
	{
		soundEngine->setSoundVolume(1);
	}

	//Set default user params
	UserSettings setting;

	setting.SetTextSetting("SoundEnable", "true");
	setting.SetTextSetting("MusicEnable", "true");
	setting.SetTextSetting("Stage", "1");
	setting.SetTextSetting("SuperPowerCount", "0");
	//setting.SetTextSetting("Language", "en");
	//setting.SetTextSetting("Language", "ru");

	manager = new Manager(
		Manager::CreateSharedParams(Device, &setting, windowSize),
		false,
		controlsScale,
		reciverScale,
		controlsOffset);
	manager->Init();

	while(Device->run())	
	{		
		if(!Device->getTimer()->isStopped())
		{
			time1 = Timer->getTime();
			if(time2>time1)
			{
				time2 = time1;
			}
			//17fps
			/*if(((time1-time2)<60)&&(time2 != 0))
			{
			continue;
			}*/
			
			//30fps
			if(((time1-time2)<30)&&(time2 != 0)) 
			{
				continue;
			}
		}

		manager->Update();
		time2 = Timer->getTime();		

	}

	delete manager;

	if(soundEngine)
		soundEngine->drop(); // delete engine

	//VLDReportLeaks();

	return 0;
}

