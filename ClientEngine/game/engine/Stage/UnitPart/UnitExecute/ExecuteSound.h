#pragma once
#include "ExecuteBase.h"
#include "../../../Sounds/SoundManager.h"

enum ExecuteSoundFormat
{
	ExecuteSoundType_MONO16_22050,
	ExecuteSoundType_MP3
};

class ExecuteSound : public ExecuteBase
{
public:
	ExecuteSound(SharedParams_t params, SoundManager* soundManager);
	virtual ~ExecuteSound(void);

	// Выполнить действие
	virtual void Run(scene::ISceneNode* node, core::array<Event_t*>& events);

	// Путь до звукового файла
	stringc Filename;

	// Повторять звук
	bool Loop;

	// Формат
	ExecuteSoundFormat Format;
private:
	SoundManager* _soundManager;
};
