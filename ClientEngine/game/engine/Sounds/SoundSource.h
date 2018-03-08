#pragma once
#include "../Core/Base.h"
#include "ISound.h"
#include "SoundResources.h"

class SoundSource : public ISound
{
public:
	SoundSource(SharedParams_t params, s32 bufferId, SoundResources* soundResources);
	virtual ~SoundSource();

	//Возращает тип элемента
	virtual SOUND_TYPE GetSoundType()
	{
		return SOUND_TYPE_SOURCE;
	}

	virtual void Play();

	virtual void Stop();

	virtual void Pause();

	virtual bool IsFinished();

	virtual bool GetLoop();

	virtual void SetLoop(bool loop);

	virtual void SetVolume(f32 vol);

	virtual f32 GetVolume();

private:
	SoundResources* _soundResources;

	s32 _sourceId;

	s32 _bufferId;

	bool _loop;
};
