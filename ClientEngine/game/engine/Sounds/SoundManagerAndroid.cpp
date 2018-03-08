#include "SoundManager.h"
#include "SoundSource.h"
#include "SoundMusic.h"

#ifdef ANDROID_COMPILE

SoundManager::SoundManager(SharedParams_t params) : Base(params)
{
}

SoundManager::~SoundManager(void)
{
}

void SoundManager::StopAll()
{

}

void SoundManager::Update()
{

}

ISound* SoundManager::PlayMusic(stringc filename, bool loopMode, bool autoDelete)
{
	return NULL;
}

ISound* SoundManager::PlaySound(stringc filename, bool loopMode, bool autoDelete)
{
	return NULL;
}


void SoundManager::AddToCache(void* owner, stringc filename)
{
	// Кэширование не реализовано
}

void SoundManager::SetEnableSound( bool enable )
{

}

void SoundManager::SetEnableMusic( bool enable )
{

}



#endif
