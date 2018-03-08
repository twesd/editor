#pragma once

#include "../Core/Base.h"
#include "ISound.h"
#include "SoundResources.h"


class SoundManager : public Base
{
public:
	SoundManager(SharedParams_t params);
	virtual ~SoundManager(void);

	void Update();

	ISound* PlayMusic(stringc filename, bool loopMode, bool autoDelete);

	ISound* PlaySound(stringc filename, bool loopMode, bool autoDelete);
	
	void StopAll();

	void AddToCache(void* sender, stringc filename);

	void SetEnableSound(bool enable);

	void SetEnableMusic(bool enable);

private:	
	
	SoundResources* _soundResources;

	core::array<ISound*> _autoDeleteItems;

	bool _soundEnabled;

	bool _musikEnabled;

#ifdef WINDOWS_COMPILE
	irrklang::ISoundEngine* _soundEngine;	
#endif

#ifdef IPHONE_COMPILE
	void* GetNSUrl(stringc filepath);
	void CreateSoundBuffer(stringc filename);
	void initOpenAL();
	u32 GetFreeSource();

	void* mMusicPlayer;
#endif

};
