#pragma once

#include "../Core/Base.h"

#ifdef WINDOWS_COMPILE
#include <iostream>
#include <irrKlang.h>
#endif
#ifdef IPHONE_COMPILE
#include <OpenAL/al.h>
#include <OpenAL/alc.h>
#endif

// Тип элемента звука
enum SOUND_TYPE
{
	// Звук (wav формат)
	SOUND_TYPE_SOURCE,
	// Музыка (mp3 формат)
	SOUND_TYPE_MUSIK
};

class SoundManager;

class ISound : public Base
{
public:
	ISound(SharedParams_t params) : Base(params)
	{
		SndManager = NULL;
	};
	virtual ~ISound() { };

	//Возращает тип элемента 
	virtual SOUND_TYPE GetSoundType() = 0;

	virtual void SetSoundManager(SoundManager* manager)
	{
		SndManager = manager;
	}

	virtual void Play() = 0;

	virtual void Stop() = 0;

	virtual void Pause() = 0;

	virtual bool IsFinished() = 0;

	virtual bool GetLoop() = 0;

	virtual void SetLoop(bool loop) = 0;

	virtual void SetVolume(f32 vol) = 0;

	virtual f32 GetVolume() = 0;

protected:
	SoundManager* SndManager;
};
