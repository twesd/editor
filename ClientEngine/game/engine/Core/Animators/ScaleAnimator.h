#pragma once
#include "./../Base.h"
#include "IExtendAnimator.h"

using namespace scene;

class ScaleAnimator : 
	public IExtendAnimator, 	
	public ISceneNodeAnimator,
	public Base
{
public:
	ScaleAnimator(SharedParams_t params);
	virtual ~ScaleAnimator(void);
	/*
	Иницилизация
		@points - точки определяющие масштабирование
		@timesForWay - время смещений в соответствии с каждой точкой
		@loop - зациклить процесс
	*/
	void Init(const core::array<vector3df> &points, core::array<u32> timesForWay, bool loop);

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

	core::array<vector3df> _points;
	core::array<u32> _timesForWay;	
	u32 _timeForWay;
	u32 _startTime;	
	u32 _endTime;
	bool _completeAnimate;
	u32 _currentNumber;
	core::vector3df _vector;
	bool _loop;
	void* _userParams;
};
