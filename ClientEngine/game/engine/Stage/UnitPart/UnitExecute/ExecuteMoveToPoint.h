#pragma once
#include "ExecuteBase.h"
#include "../../Paths/PathAnimator.h"

class ExecuteMoveToPoint : public ExecuteBase
{
public:
	ExecuteMoveToPoint(SharedParams_t params);
	virtual ~ExecuteMoveToPoint(void);

	// Заверение чтение настроек
	void CompleteLoading();

	// Выполнить действие
	virtual void Run(scene::ISceneNode* node, core::array<Event_t*>& events);

	// Получить позицию из события TapScene 
	bool GetPositionFromTapControl;

	// Наименование TapScene
	stringc TapSceneName;

	// Позиция цели
	vector3df TargetPosition;

	// Скорость перемещения (едениц в секунду)
	f32 Speed;

	// Минимальное расстояние до цели
	f32 TargetDist;

	// Фильтр для моделий препятствий
	int ObstacleFilterId;

private:
	// Поисковик пути
	PathAnimator* _pathAnimator;
};
