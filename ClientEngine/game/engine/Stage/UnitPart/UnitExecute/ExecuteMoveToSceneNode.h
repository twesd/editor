#pragma once
#include "ExecuteBase.h"
#include "../UnitSelectSceneNodes/UnitSelectSceneNodeBase.h"
#include "../../Paths/PathSceneNodeAnimator.h"

class ExecuteMoveToSceneNode : public ExecuteBase
{
public:
	ExecuteMoveToSceneNode(SharedParams_t params);
	virtual ~ExecuteMoveToSceneNode(void);

	// Выполнить действие
	virtual void Run(scene::ISceneNode* node, core::array<Event_t*>& events);

	// Установка алгоритм выборки объекта
	// Объект захватывается grab
	void SetSelectSceneNode(UnitSelectSceneNodeBase* selectSceneNode);

	// Скорость перемещения (едениц в секунду)
	f32 Speed;

	// Дистанция на которую надо подойти
	f32 TargetDist;

	// Фильтр для моделий препятствий
	int ObstacleFilterId;

private:
	// Алгоритм выборки объекта
	UnitSelectSceneNodeBase* _selectSceneNode;

	// Поисковик пути
	PathSceneNodeAnimator* _pathAnimator;
};
