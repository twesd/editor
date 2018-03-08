#pragma once
#include "./../Base.h"
#include "IExtendAnimator.h"

using namespace scene;

class LineAnimator : 
	public IExtendAnimator, 	
	public ISceneNodeAnimator,
	public Base
{
public:
	LineAnimator(SharedParams_t params);
	virtual ~LineAnimator(void);
	/*
	Иницилизация
		@points - точки определяющие смещения
		@timesForWay - время смещений в соответствии с каждой точкой
		@loop - зациклить процесс,
		@obstacleFilterId - маска для объектов препятствий
	*/
	void Init(
		const core::array<vector3df> &points, 
		core::array<u32> timesForWay, 
		bool loop, 
		int obstacleFilterId);

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
	
	bool CanMoveTo( core::vector3df& pos, ISceneNode* node );

	core::array<vector3df> _points;
	core::array<u32> _timesForWay;	
	// время смещений в соответствии с каждой точкой
	u32 _timeForWay;
	u32 _startTime;	
	u32 _endTime;
	bool _completeAnimate;
	u32 _currentNumber;
	core::vector3df _vector;
	bool _loop;
	void* _userParams;
	int _obstacleFilterId;
};
