#pragma once
#include "../Core/Base.h"
#include "ISound.h"


#ifdef WINDOWS_COMPILE
#include <al.h>
#include <alc.h>
#endif

class SoundResources : public Base
{
public:
	SoundResources(SharedParams_t params);
	virtual ~SoundResources();

	// Создать аудио буффер
	s32 CreateSoundBuffer(stringc filename);

	// Получить свободный ресурс
	s32 GetSource();

	// Освободить ресурс
	void FreeSource(s32 sourceId);

	void Update();

private:
	u32 MaxSourcesSounds;

	typedef struct{
		stringc Filename;
		s32 BufferId;
	}BufferItem_t;

	typedef struct{
		s32 SourceId;
		bool IsBusy;
	}SoundSource_t;

#ifdef WINDOWS_COMPILE
	// OpenAL context for playing sounds
	ALCcontext* _alcContext;

	// The device we are going to use to play sounds
	ALCdevice* _alcDevice;
#endif

	// Индентификаторы ресурсов
	core::array<SoundSource_t> _soundSourceIds;

	// Ресурсы звуков
	core::array<BufferItem_t> _bufferIds;
};
