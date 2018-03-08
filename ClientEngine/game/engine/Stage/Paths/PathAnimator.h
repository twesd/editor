#pragma once
#include "../../Core/Base.h"
#include "../../Core/Animators/IExtendAnimator.h"
#include "PathFinder.h"

using namespace scene;

class PathAnimator : 
	public IExtendAnimator, 	
	public ISceneNodeAnimator,
	public Base
{
public:
	PathAnimator(SharedParams_t params);
	virtual ~PathAnimator(void);
	/*
	Иницилизация
		@targetPoint - цель
		@speed - скорость перемещения (единиц в секунду)
		@targetDist - Дистанция на которую надо подойти
		@filterId - Фильтр для моделий препятствий
	*/
	void Init(vector3df targetPoint, f32 speed, f32 targetDist, int filterId);

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

	// Установить новую цель
	void SetTarget( vector3df targetPoint );
private:

	// Конечная точка
	vector3df _targetPos;

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

	int _stopCounter;
};
