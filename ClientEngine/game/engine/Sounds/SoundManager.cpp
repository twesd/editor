#include "SoundManager.h"
#include "SoundSource.h"
#include "SoundMusic.h"

#ifdef WINDOWS_COMPILE

SoundManager::SoundManager(SharedParams_t params) : Base(params)
{
	_soundEngine = irrklang::createIrrKlangDevice();
	_soundResources = new SoundResources(params);
	_soundEnabled = true;
	_musikEnabled = true;
}

SoundManager::~SoundManager(void)
{
	StopAll();
	_soundEngine->drop();
	_soundResources->drop();
}

void SoundManager::StopAll()
{
	core::array<ISound*> newSounds;
	for(u32 i = 0; i < _autoDeleteItems.size(); i++)
	{
		_autoDeleteItems[i]->Stop();
		_autoDeleteItems[i]->drop();
	}
	_autoDeleteItems.clear();
}

void SoundManager::Update()
{
	_soundResources->Update();

	core::array<ISound*> updatedSounds;
	for(u32 i = 0; i < _autoDeleteItems.size(); i++)
	{
		if(_autoDeleteItems[i]->IsFinished())
		{
			_autoDeleteItems[i]->drop();
		} 
		else 
		{
			updatedSounds.push_back(_autoDeleteItems[i]);
		}	
	}
	_autoDeleteItems = updatedSounds;
}

ISound* SoundManager::PlayMusic(stringc filename, bool loopMode, bool autoDelete)
{
	if(!_musikEnabled)
		return NULL;
	if(filename == "") 
		return NULL;
	filename = FileSystem->getAbsolutePath(filename);
	irrklang::ISound* player = _soundEngine->play2D((filename).c_str(), loopMode, false, true);	
	if(player == NULL) 
		return NULL;
	SoundMusic* musik = new SoundMusic(SharedParams);
	musik->SetPlayer(player);
	musik->Play();
	if(autoDelete)
		_autoDeleteItems.push_back(musik);
	return musik;
}

ISound* SoundManager::PlaySound(stringc filename, bool loopMode, bool autoDelete)
{
	if(!_soundEnabled)
		return NULL;

	if(filename == "") return NULL;
	s32 bufferId = _soundResources->CreateSoundBuffer(filename);
	if(bufferId == -1) return NULL;
	SoundSource* source = new SoundSource(SharedParams, bufferId, _soundResources);
	source->Play();
	if(autoDelete)
		_autoDeleteItems.push_back(source);
	return source;
}


void SoundManager::AddToCache(void* owner, stringc filename)
{
	// Кэширование не реализовано
}

void SoundManager::SetEnableSound( bool enable )
{
	_soundEnabled = enable;
}

void SoundManager::SetEnableMusic( bool enable )
{
	_musikEnabled = enable;
}



#endif