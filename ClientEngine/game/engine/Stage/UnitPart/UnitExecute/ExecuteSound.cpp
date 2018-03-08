#include "ExecuteSound.h"

ExecuteSound::ExecuteSound(SharedParams_t params, SoundManager* soundManager) : ExecuteBase(params)
{
	_DEBUG_BREAK_IF(soundManager == NULL)
	_soundManager = soundManager;
}

ExecuteSound::~ExecuteSound()
{
}

// Выполнить действие
void ExecuteSound::Run( scene::ISceneNode* node, core::array<Event_t*>& events )
{
	ExecuteBase::Run(node, events);

	if(Filename == "") return;

	if (Format == ExecuteSoundType_MP3)
	{
		_soundManager->PlayMusic(Filename, Loop, true);
	}
	else if (Format == ExecuteSoundType_MONO16_22050)
	{
		_soundManager->PlaySound(Filename, Loop, true);
	}
}
