#pragma once

#include "./../Base.h"
#include "IExtendAnimator.h"

using namespace scene;

class TextureAnimator :
	public IExtendAnimator,
	public ISceneNodeAnimator,
	public Base	
{
public:
	TextureAnimator(SharedParams_t params);
	virtual ~TextureAnimator(void);
	/*
	Иницилизация
		@textures - текстуры для анимации
		@timePerFrame - время показа одной текстуры
		@loop - зациклить процесс
	*/
	void Init(core::array<video::ITexture*> textures,u32 timePerFrame,bool loop);

	//*** IExtendAnimator **//
	//Обновить все данные в начальное состояние
	void ResetState();
	//Закончена ли анимация
	bool AnimationEnd();
	//Устанавливает дополнительные параметры
	void SetUserParams(void* params) {};

	//*** ISceneNodeAnimator **//
	ISceneNodeAnimator* createClone(ISceneNode* node, ISceneManager* newManager);
	void animateNode(ISceneNode* node, u32 timeMs);
private:
	core::array<video::ITexture*> _textures;	
	bool _loopMode;	
	u32 _timePerFrame;
	u32 _fireTime;
	u32 _currentIndex;
	bool _completeAnimate;
	// Установлена ли первая текстура
	bool _isFirstTextureSet;
};
