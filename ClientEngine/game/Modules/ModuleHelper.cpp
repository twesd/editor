#include "ModuleHelper.h"
#include "../engine/ManagerEvents.h"
#include "../engine/Stage/StageManagerEvents.h"
#include "../engine/UserSettings/UserSettingsWorker.h"
#include "../engine/Controls/ControlImage.h"
#include "Core/TextureWorker.h"

stringc ModuleHelper::GetLangTextPath( SharedParams_t* sharedParams )
{
	stringc lang = sharedParams->Settings->GetValue(stringc("Language"));
	stringc path;
	bool is2x = Is2x(sharedParams);
	
	if(ModuleHelper::Is568h(sharedParams))
	{
		if (lang == "ru")
		{
			path = "Menu568h/Text/ru/";
		}
		else 
		{
			path = "Menu568h/Text/en/";
		}
	}
	else if(is2x)
	{
		if (lang == "ru")
		{
			path = "Menu2x/Text/ru/";
		}
		else 
		{
			path = "Menu2x/Text/en/";
		}
	}
	else 
	{
		if (lang == "ru")
		{
			path = "Menu/Text/ru/";
		}
		else 
		{
			path = "Menu/Text/en/";
		}
	}	

	return path;
}


bool ModuleHelper::Is2x( SharedParams_t* sharedParams )
{
	bool is2x = false;

	int winWidth = sharedParams->WindowSize.Width;
	int winHeight = sharedParams->WindowSize.Height;
	
	if(winWidth == 1136 && winHeight == 640)
	{
		is2x = true;
	}
	else if(winWidth == 960 && winHeight == 640)
	{
		is2x = true;
	}
	else if(winWidth == 480 && winHeight == 320)
	{
		is2x = false;
	}
	return is2x;
}

bool ModuleHelper::Is568h( SharedParams_t* sharedParams )
{
	bool is2x = false;

	int winWidth = sharedParams->WindowSize.Width;
	int winHeight = sharedParams->WindowSize.Height;
	
	if(winWidth == 1136 && winHeight == 640)
	{
		is2x = true;
	}
	else if(winWidth == 960 && winHeight == 640)
	{
		is2x = false;
	}
	else if(winWidth == 480 && winHeight == 320)
	{
		is2x = false;
	}
	
	return is2x;
}

void ModuleHelper::DrawLoading(SharedParams_t* sharedParams )
{
	sharedParams->Driver->beginScene(true, true, video::SColor(255,0,0,0));
	if(ModuleHelper::Is568h(sharedParams))
	{
		TextureWorker::DrawTexture(sharedParams->Driver, 
			TextureWorker::GetTexture(
				sharedParams->Driver, 
				stringc("Menu/Loading/Loading586h") + stringc(".png")),0,0);
	}
	else if(ModuleHelper::Is2x(sharedParams))
	{
		TextureWorker::DrawTexture(sharedParams->Driver, 
			TextureWorker::GetTexture(
				sharedParams->Driver, 
				stringc("Menu/Loading/Loading2x") + stringc(".png")),0,0);
	}
	else
	{
		TextureWorker::DrawTexture(sharedParams->Driver, 
			TextureWorker::GetTexture(sharedParams->Driver,
			stringc("Menu/Loading/Loading")  + stringc(".png")),0,0);
	}
	sharedParams->Driver->endScene();
	sharedParams->Device->run();
}
