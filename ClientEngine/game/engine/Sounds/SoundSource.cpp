#include "SoundSource.h"

SoundSource::SoundSource(SharedParams_t params, s32 bufferId, SoundResources* soundResources) : ISound(params)
{
	_soundResources = soundResources;
	_sourceId = -1;
	_bufferId = bufferId;
	_DEBUG_BREAK_IF(bufferId == -1)
	_loop = false;
}

SoundSource::~SoundSource(void)
{
	if (_sourceId != -1)
		_soundResources->FreeSource(_sourceId);
}

void SoundSource::Play()
{
	if(_sourceId == -1)
	{
		_sourceId = _soundResources->GetSource();
		if (_sourceId == -1) 
			return;
	}

#ifdef WINDOWS_COMPILE

	alSourceStop(_sourceId);
	// Сбрасываем буффер если ему было ранее что то назначено
	alSourcei(_sourceId, AL_BUFFER, 0);
	// Назначаем новый буффер
	alSourcei(_sourceId, AL_BUFFER, _bufferId);

	// 
	alSourcef(_sourceId, AL_PITCH, 1.0f);
	alSourcef(_sourceId, AL_GAIN, 1.0f);

	if(_loop) 
		alSourcei(_sourceId, AL_LOOPING, AL_TRUE);
	else 
		alSourcei(_sourceId, AL_LOOPING, AL_FALSE);

	// Проверка на ошибки
	ALenum err = alGetError();
	if(err != 0) 
		return;

	// В
	alSourcePlay(_sourceId);	

#endif
}

void SoundSource::Stop()
{
	if(_sourceId == -1) return;
#ifdef WINDOWS_COMPILE
	alSourceStop(_sourceId);
	_sourceId = -1;
#endif
}

void SoundSource::Pause()
{
	_DEBUG_BREAK_IF(true)
}

bool SoundSource::IsFinished()
{
	if(_sourceId == -1) 
		return true;
#ifdef WINDOWS_COMPILE
	int sourceState;
	alGetSourcei(_sourceId, AL_SOURCE_STATE, &sourceState);
	return (sourceState != AL_PLAYING);
#endif
}

bool SoundSource::GetLoop()
{
	return _loop;
}

void SoundSource::SetLoop(bool loop)
{
	_loop = loop;
	if(!IsFinished()) 
		Stop();
}

void SoundSource::SetVolume( f32 vol )
{
	// Не реализовано
}

f32 SoundSource::GetVolume()
{
	// Не реализовано
	return 0;
}
