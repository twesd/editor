#include "SoundManager.h"
#include "SoundSource.h"
#include "SoundMusic.h"


#ifdef IPHONE_COMPILE

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>
#import <OpenAL/al.h>
#import <OpenAL/alc.h>
#import <AudioToolbox/AudioToolbox.h>
#import <AVFoundation/AVAudioPlayer.h>

SoundManager::SoundManager(SharedParams_t params) : Base(params)
{
	_soundResources = new SoundResources(params);
	_soundEnabled = true;
	_musikEnabled = true;
}

SoundManager::~SoundManager(void)
{
	StopAll();
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


AudioFileID openAudioFile(NSURL* afUrl) 
{	
	AudioFileID outAFID;
	
	// Open the audio file provided
	OSStatus result = AudioFileOpenURL((CFURLRef)afUrl, kAudioFileReadPermission, 0, &outAFID);
	
	// If we get a result that is not 0 then something has gone wrong.  We report it and 
	// return the out audio file id
	if(result != 0)	{
		NSLog(@"ERROR SoundEngine: Cannot open file: %@", afUrl);
		return nil;
	}
	
	return outAFID;
}

UInt32 audioFileSize(AudioFileID fileDescriptor)
{
	UInt64 outDataSize = 0;
	UInt32 thePropSize = sizeof(UInt64);
	OSStatus result = AudioFileGetProperty(
										   fileDescriptor, kAudioFilePropertyAudioDataByteCount, &thePropSize, &outDataSize);
	if(result != 0)	
		NSLog(@"ERROR: cannot file file size");	
	return (UInt32)outDataSize;
}


void* SoundManager::GetNSUrl(stringc filepath)
{
	NSURL *playUrl = NULL;
	
	if(filepath == "")
		return NULL;
	
	int lastPos = filepath.findLast('/');
	if(lastPos < 0)
		return NULL;
	stringc fName = filepath.subString(lastPos+1, filepath.size() - lastPos - 1);
	stringc dir = filepath.subString(0, lastPos + 1);
	dir = stringc("./data/") + dir;
	
	lastPos = fName.findLast('.');
	if(lastPos < 0)
		return NULL;
	
	stringc fExt = fName.subString(lastPos+1, fName.size() - lastPos - 1);	
	fName = fName.subString(0, lastPos);
	NSString* strName = [[NSString alloc] initWithCString:fName.c_str()];
	NSString* strEx = [[NSString alloc] initWithCString:fExt.c_str()];
	NSString* strDir = [[NSString alloc] initWithCString:dir.c_str()];	
	
//	printf("fExt = %s \n",fExt.c_str());
//	printf("fName2 = %s \n",fName.c_str());
//	printf("dir = %s \n",dir.c_str());
	
	playUrl = [[NSURL alloc] initFileURLWithPath: [[NSBundle mainBundle] 
												   pathForResource:strName ofType:strEx 
												   inDirectory:strDir ]];
	
	[strName release];
	[strEx release];
	[strDir release];
	
	return playUrl;
}



ISound* SoundManager::PlayMusic(stringc filename, bool loopMode, bool autoDelete)
{
	if(!_musikEnabled)
		return NULL;
	if(filename == "") 
		return NULL;
	
	AVAudioPlayer* player = NULL;
	NSURL* playUrl = (NSURL*)GetNSUrl(filename);	
	if(playUrl != NULL) 
	{
		player = [[AVAudioPlayer alloc] initWithContentsOfURL:playUrl error: nil];		
		[playUrl release];
		
		player.numberOfLoops = 5000;
		[player play];		
		
		player.volume = 0.5f;
	}	
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