#pragma once
#include "ExecuteBase.h"

class ExecuteDeleteUnitsAll : public ExecuteBase
{
public:
	ExecuteDeleteUnitsAll(SharedParams_t params);
	virtual ~ExecuteDeleteUnitsAll(void);

	// Выполнить действие
	virtual void Run(scene::ISceneNode* node, core::array<Event_t*>& events);

	// Путь до поведений
	stringc JointName;

};
