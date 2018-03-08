#pragma once
#include "ExecuteBase.h"
#include "../../../Core/Animators/IExtendAnimator.h"
#include "../../../Core/Animators/LineAnimator.h"
#include "../../../Core/Animators/RotateAnimator.h"
#include "../../../Core/Animators/ScaleAnimator.h"

class ExecuteTransform : public ExecuteBase
{
public:
	ExecuteTransform(SharedParams_t params);
	virtual ~ExecuteTransform(void);

	// Установить аниматор
	void SetAnimator(
		stringc transformType, 
		const core::array<vector3df> &points, 
		core::array<u32> timesForWay, 
		bool loop,
		int obstacleFilterId);

	// Выполнить действие
	virtual void Run(scene::ISceneNode* node, core::array<Event_t*>& events);

private:
	// Получить точки в соответствии с поворотом основного объекта
	core::array<vector3df> GetTransformLinePoints(core::array<vector3df>& inPoints, const matrix4& mat);

	// Получить точки в соответствии с поворотом основного объекта
	core::array<vector3df> GetTransformRotatePoints(core::array<vector3df>& inPoints, const matrix4& mat);

	// Точки изменения
	core::array<vector3df> _points;
	
	// Точки изменений
	core::array<u32> _timesForWay;

	// Повторять изменения
	bool _loop;

	int _obstacleFilterId;

	// Аниматор перемещения по линии
	LineAnimator* _lineAnimator;

	// Аниматор вращения
	RotateAnimator* _rotateAnimator;

	// Аниматор масштабирования
	ScaleAnimator* _scaleAnimator;
};
