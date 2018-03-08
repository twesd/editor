#include "TextureAnimator.h"

TextureAnimator::TextureAnimator(SharedParams_t params) : Base(params)
{
	_loopMode = false;
	_timePerFrame = 0;
	_currentIndex = 0;
	_completeAnimate = false;
	_isFirstTextureSet = false;
}

TextureAnimator::~TextureAnimator(void)
{
	_textures.clear();
}

void TextureAnimator::Init(core::array<video::ITexture*> textures,u32 timePerFrame,bool loop){
	_textures.clear();
	for(u32 i=0;i<textures.size();i++)
	{
		_textures.push_back(textures[i]);
	}
	if(_textures.size() == 0)
	{
		_DEBUG_BREAK_IF(true)
		return;
	}
	_timePerFrame = timePerFrame;
	_loopMode = loop;
	_currentIndex = 0;
	_completeAnimate = false;
	_isFirstTextureSet = false;
	_fireTime = GetNowTime() + _timePerFrame;
}

void TextureAnimator::ResetState()
{
	_currentIndex = 0;
	_completeAnimate = false;
	_fireTime = GetNowTime() + _timePerFrame;
}

bool TextureAnimator::AnimationEnd()
{
	return _completeAnimate;
}

ISceneNodeAnimator* TextureAnimator::createClone(ISceneNode* node, ISceneManager* newManager)
{
	return NULL;
}

void TextureAnimator::animateNode(ISceneNode* node, u32 timeMs)
{
	if(_completeAnimate) return;
	if(_textures.size() == 0) return;

	if(!_isFirstTextureSet)
	{
		node->setMaterialTexture(0, _textures[0]);
		_isFirstTextureSet = true;
	}

	if(timeMs >= _fireTime)
	{
		if(_currentIndex == _textures.size())
		{
			if(_loopMode)
			{
				ResetState();
				node->setMaterialTexture(0, _textures[_currentIndex]);
				_currentIndex++;
			}
			else 
			{
				_completeAnimate = true;
			}
			return;
		}
		node->setMaterialTexture(0, _textures[_currentIndex]);
		_fireTime = GetNowTime() + _timePerFrame;
		_currentIndex++;
	}
}