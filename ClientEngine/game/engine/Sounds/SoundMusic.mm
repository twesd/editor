#include "SoundMusic.h"
#include "SoundSource.h"


#ifdef IPHONE_COMPILE

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>
#import <AudioToolbox/AudioToolbox.h>
#import <AVFoundation/AVAudioPlayer.h>

SoundMusic::SoundMusic(SharedParams_t params) : ISound(params)
{
	_player = NULL;
}

SoundMusic::~SoundMusic(void)
{
	if(_player != NULL)
	{
		AVAudioPlayer* aPlayer = static_cast<AVAudioPlayer*> (_player);
		[aPlayer stop];
		[aPlayer release];
		_player = NULL;
	}
}

void SoundMusic::SetPlayer(void* player)
{
	_DEBUG_BREAK_IF(player == NULL)
	_player = static_cast<AVAudioPlayer*> (player);
}

void SoundMusic::Play()
{
	_DEBUG_BREAK_IF(_player == NULL)
	//_player->setIsPaused(false);
}

void SoundMusic::Stop()
{
	_DEBUG_BREAK_IF(_player == NULL)
	//_player->stop();
	AVAudioPlayer* aPlayer = static_cast<AVAudioPlayer*> (_player);
	[aPlayer stop];
	[aPlayer release];
	_player = NULL;
}

void SoundMusic::Pause()
{
	_DEBUG_BREAK_IF(_player == NULL)
	//_player->setIsPaused(true);
}

bool SoundMusic::IsFinished()
{
	_DEBUG_BREAK_IF(_player == NULL)
	//AVAudioPlayer* aPlayer = static_cast<AVAudioPlayer*> (_player);
	//return _player->isFinished();
	return false;
}

bool SoundMusic::GetLoop()
{
	_DEBUG_BREAK_IF(_player == NULL)
	//return _player->isLooped();
	//AVAudioPlayer* aPlayer = static_cast<AVAudioPlayer*> (_player);
	return true;
}

void SoundMusic::SetLoop(bool loop)
{
	_DEBUG_BREAK_IF(_player == NULL)
//	_player->setIsLooped(loop);
}

void SoundMusic::SetVolume( f32 vol )
{
	_DEBUG_BREAK_IF(_player == NULL)
	//_player->setVolume(vol);
}

irr::f32 SoundMusic::GetVolume()
{
	_DEBUG_BREAK_IF(_player == NULL)
//	return _player->getVolume();
	return 1.0f;	
}

#endif