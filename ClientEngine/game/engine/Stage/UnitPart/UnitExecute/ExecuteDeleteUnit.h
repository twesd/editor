#pragma once
#include "ExecuteBase.h"

class ExecuteDeleteUnit : public ExecuteBase
{
public:
	ExecuteDeleteUnit(SharedParams_t params);
	virtual ~ExecuteDeleteUnit(void);

	// Выполнить действие
	virtual void Run(scene::ISceneNode* node, core::array<Event_t*>& events);

	// Установить основную директорию
	void SetRoot(stringc root);

	// Путь до поведений
	stringc BehaviorsPath;

private:
	// Основная директория
	stringc _root;
};
