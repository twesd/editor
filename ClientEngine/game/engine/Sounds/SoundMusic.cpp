#include "SoundMusic.h"

#ifdef WINDOWS_COMPILE

SoundMusic::SoundMusic(SharedParams_t params) : ISound(params)
{
	_klangPlayer = NULL;
}

SoundMusic::~SoundMusic(void)
{
	_klangPlayer->stop();
	_klangPlayer->drop();
}

void SoundMusic::SetPlayer(void* player)
{
	_DEBUG_BREAK_IF(player == NULL)
	_klangPlayer = static_cast<irrklang::ISound*> (player);
}

void SoundMusic::Play()
{
	_DEBUG_BREAK_IF(_klangPlayer == NULL)
	_klangPlayer->setIsPaused(false);
}

void SoundMusic::Stop()
{
	_DEBUG_BREAK_IF(_klangPlayer == NULL)
	_klangPlayer->stop();
}

void SoundMusic::Pause()
{
	_DEBUG_BREAK_IF(_klangPlayer == NULL)
	_klangPlayer->setIsPaused(true);
}

bool SoundMusic::IsFinished()
{
	_DEBUG_BREAK_IF(_klangPlayer == NULL)
	return _klangPlayer->isFinished();
}

bool SoundMusic::GetLoop()
{
	_DEBUG_BREAK_IF(_klangPlayer == NULL)
	return _klangPlayer->isLooped();
}

void SoundMusic::SetLoop(bool loop)
{
	_DEBUG_BREAK_IF(_klangPlayer == NULL)
	_klangPlayer->setIsLooped(loop);
}

void SoundMusic::SetVolume( f32 vol )
{
	_DEBUG_BREAK_IF(_klangPlayer == NULL)
	_klangPlayer->setVolume(vol);
}

irr::f32 SoundMusic::GetVolume()
{
	_DEBUG_BREAK_IF(_klangPlayer == NULL)
	return _klangPlayer->getVolume();
}

#endif