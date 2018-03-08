#pragma once
#include "ExecuteBase.h"

class ExecuteMappingTransform : public ExecuteBase
{
public:
	ExecuteMappingTransform(SharedParams_t params);
	virtual ~ExecuteMappingTransform(void);

	// Выполнить действие
	virtual void Run(scene::ISceneNode* node, core::array<Event_t*>& events);

	// Установить основную директорию
	void SetRoot(stringc root);

	bool UseThisBehavior;

	// Путь до поведения
	stringc BehaviorChildPath;

	stringc ScaleX;

	stringc ScaleY;

	stringc ScaleZ;

	stringc PositionX;

	stringc PositionY;

	stringc PositionZ;

	stringc RotationX;

	stringc RotationY;

	stringc RotationZ;

private:
	// Основная директория
	stringc _root;
};
