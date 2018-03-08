#pragma once
#include "ExecuteBase.h"

class ExecuteAddNextAction : public ExecuteBase
{
public:
	ExecuteAddNextAction(SharedParams_t params);
	virtual ~ExecuteAddNextAction(void);

	// Выполнить действие
	virtual void Run(scene::ISceneNode* node, core::array<Event_t*>& events);

	// Наименование след. действия
	stringc ActionName;
};
