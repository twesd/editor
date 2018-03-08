#include "ModuleEndGame.h"
#include "../engine/ManagerEvents.h"
#include "../engine/Stage/StageManagerEvents.h"
#include "../engine/UserSettings/UserSettingsWorker.h"
#include "../engine/Controls/ControlImage.h"

ModuleEndGame::ModuleEndGame(SharedParams_t params) : Base(params)
{
	ClearData();
}

ModuleEndGame::~ModuleEndGame(void)
{
}

irr::core::stringc ModuleEndGame::GetName()
{
	return "ModuleEndGame";
}

void ModuleEndGame::Init()
{
	if (_isInit) return;

	UserSettingsWorker::Load(SharedParams, SharedParams.Settings);
	stringc sndEnabled = SharedParams.Settings->GetValue(stringc("SoundEnable"));
	stringc musikEnabled = SharedParams.Settings->GetValue(stringc("MusicEnable"));
	SndManager->SetEnableSound(sndEnabled == "true");
	SndManager->SetEnableMusic(musikEnabled == "true");

	ISound* snd = SndManager->PlayMusic("Music/EndMusic.mp3", true, true);
	if(snd != NULL)
		snd->SetVolume(0.3f);

	_isInit = true;
}


void ModuleEndGame::ClearData()
{
	IModule::ClearData();
	_isInit = false;
}


void ModuleEndGame::Execute()
{
	_DEBUG_BREAK_IF(UManager == NULL)	
	_DEBUG_BREAK_IF(CntrlManager == NULL)	


	if(!_isInit)
	{
		Init();
		return;
	}
}

