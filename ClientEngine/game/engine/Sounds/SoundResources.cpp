#include "SoundResources.h"
#include "WavFileReader.h"
#include "../Core/FileUtility.h"



SoundResources::SoundResources(SharedParams_t params) : Base(params)
{
	MaxSourcesSounds = 30;
#ifdef WINDOWS_COMPILE
	// Get the device we are going to use for sound.  Using NULL gets the default device
	_alcDevice = alcOpenDevice(NULL);
	
	// If a device has been found we then need to create a context, make it current and then
	// preload the OpenAL Sources
	if(_alcDevice) 
	{
		// Use the device we have now got to create a context "air"
		_alcContext = alcCreateContext(_alcDevice, NULL);
		// Make the context we have just created into the active context
		alcMakeContextCurrent(_alcContext);
		// Pre-create 32 sound sources which can be dynamically allocated to buffers (sounds)
		u32 sourceId;
		for(u32 index = 0; index < MaxSourcesSounds; index++) 
		{
			// Generate an OpenAL source
			alGenSources(1, &sourceId);
			SoundSource_t sourceItem;
			sourceItem.SourceId = sourceId;
			sourceItem.IsBusy = false;
			_soundSourceIds.push_back(sourceItem);
		}		
	}	
#endif
}

SoundResources::~SoundResources(void)
{
#ifdef WINDOWS_COMPILE
	for(u32 i = 0; i < _soundSourceIds.size(); i++)
	{
		alDeleteSources(1, (u32*)&_soundSourceIds[i]);
	}
	_soundSourceIds.clear();

	for(u32 i = 0; i < _bufferIds.size(); i++)
	{
		alDeleteBuffers(1, (u32*)&_bufferIds[i].BufferId);
	}
	_bufferIds.clear();	

	if(_alcContext != NULL)
	{
		alcMakeContextCurrent(NULL);
		alcDestroyContext(_alcContext);
	}

	if(_alcDevice != NULL)
		alcCloseDevice(_alcDevice);		
#endif
}


s32  SoundResources::CreateSoundBuffer(stringc filename)
{
	filename = FileUtility::GetUpdatedFileName(filename);
	for(u32 i = 0; i < _bufferIds.size(); i++)
	{
		if(_bufferIds[i].Filename == filename) 
			return _bufferIds[i].BufferId;
	}	

	u32 fileSize;
	u8* buffer;
	WavFileReader fileReader;
	if(!fileReader.Read(filename.c_str()))
		return -1;
	if(!fileReader.GetActualData(&buffer, &fileSize))
		return -1;
	if (buffer == NULL) return -1;	

	// Generate a buffer within OpenAL for this sound
	s32 bufferId = -1;

#ifdef WINDOWS_COMPILE
	alGenBuffers(1, (u32*)&bufferId);
		
	// Place the audio data into the new buffer
	alBufferData(bufferId, AL_FORMAT_MONO16, buffer, fileSize, 22050/**/);

	BufferItem_t item;
	item.Filename = filename;
	item.BufferId = bufferId;
	_bufferIds.push_back(item);
#endif

	return bufferId;
}

// Получить свободный ресурс
s32 SoundResources::GetSource()
{
	for(u32 i = 0; i < _soundSourceIds.size(); i++)
	{
		if(!_soundSourceIds[i].IsBusy)
		{
			_soundSourceIds[i].IsBusy = true;
			return _soundSourceIds[i].SourceId;
		}
	}
	return -1;	
}

void SoundResources::Update()
{
	
}

// Освободить ресурс
void SoundResources::FreeSource( s32 sourceId )
{
	for(u32 i = 0; i < _soundSourceIds.size(); i++)
	{
		if(_soundSourceIds[i].SourceId == sourceId)
		{
			_soundSourceIds[i].IsBusy = false;
			return;
		}
	}
}
