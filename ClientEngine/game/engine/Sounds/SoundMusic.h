#pragma once
#include "../Core/Base.h"
#include "ISound.h"

class SoundMusic : public ISound
{
public:
	SoundMusic(SharedParams_t params);
	virtual ~SoundMusic();

	//Возращает тип элемента
	virtual SOUND_TYPE GetSoundType()
	{
		return SOUND_TYPE_MUSIK;
	}


	virtual void Play();

	virtual void Stop();

	virtual void Pause();

	virtual bool IsFinished();

	virtual bool GetLoop();

	virtual void SetLoop(bool loop);

	virtual void SetVolume(f32 vol);

	virtual f32 GetVolume();

	void SetPlayer(void* player);

private:
	
#ifdef IPHONE_COMPILE
	void* _player;
#endif
	
#ifdef WINDOWS_COMPILE
	irrklang::ISound* _klangPlayer;
#endif
};
