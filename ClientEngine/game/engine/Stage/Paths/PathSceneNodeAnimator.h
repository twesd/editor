#pragma once
#include "../../Core/Base.h"
#include "../../Core/Animators/IExtendAnimator.h"
#include "PathFinder.h"

using namespace scene;

class PathSceneNodeAnimator : 
	public IExtendAnimator, 	
	public ISceneNodeAnimator,
	public Base
{
public:
	PathSceneNodeAnimator(SharedParams_t params);
	virtual ~PathSceneNodeAnimator(void);
	/*
	Иницилизация
		@targetSceneNode - цель
		@speed - скорость перемещения (единиц в секунду)
		@targetDist - дистанция на которую надо подойти
		@filterId - Фильтр для моделий препятствий
	*/
	void Init(ISceneNode* targetSceneNode, f32 speed, f32 targetDist, int filterId);

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

	// Конечная цель
	ISceneNode* _targetSceneNode;

	// Скорость перемещения
	f32 _speed;

	// Дистанция на которую надо подойти
	f32 _targetDist;

	// Завершён ли поиск
	bool _completeAnimate;
	
	// Предыдущие время выполнения
	u32 _prevTime;
	
	void* _userParams;

	PathFinder _pathFinder;
};
