#pragma once
#include "./../Base.h"
#include "IExtendAnimator.h"

using namespace scene;

class ColorAnimator : 
	public IExtendAnimator, 	
	public ISceneNodeAnimator,
	public Base
{
public:
	ColorAnimator(SharedParams_t params);
	virtual ~ColorAnimator(void);
	
	void Init(
		const core::array<video::SColor> &colors, 
		core::array<u32> timesForWay, 
		bool loop);

	//*** IExtendAnimator **//
	// Сброс в начальные установки
	void ResetState();
	// Закончена ли анимация
	bool AnimationEnd();
	// Устанавливает дополнительные параметры
	void SetUserParams(void* params);

	//*** ISceneNodeAnimator **//
	ISceneNodeAnimator* createClone(ISceneNode* node, ISceneManager* newManager);
	void animateNode(ISceneNode* node, u32 timeMs);
private:
	void CalulateValues(u32 number);

	core::array<video::SColor> _colors;
	
	core::array<u32> _timesForWay;	
	
	u32 _timeForWay;

	u32 _lastTime;	

	u32 _startTime;

	u32 _endTime;

	bool _completeAnimate;

	u32 _currentNumber;

	video::SColor _curTargetColor;

	bool _loop;

	bool _isInit;

	scene::IMeshManipulator *_manipulator;
};
