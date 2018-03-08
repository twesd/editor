#pragma once
#include "ExecuteBase.h"

class ExecuteRotation : public ExecuteBase
{
public:
	ExecuteRotation(SharedParams_t params);
	virtual ~ExecuteRotation(void);

	// Выполнить действие
	virtual void Run(scene::ISceneNode* node, core::array<Event_t*>& events);

	// Поворот
	vector3df Rotation;

	// Установить абсолютный поворот
	bool Absolute;

	// Использовать поворот из кругового контрола 
	bool AddAngleFromControlCircle;

	// Наименование кругового контрола (если установлен, то Rotation игнорируется)
	stringc ControlCircleName;

	// Скорость установки (угол/сек.). Ноль - установить сразу
	f32 Speed;

private:

	f32 CalcAngle(f32 angle, f32 nowAngle, f32 reqDiff);
};
