#pragma once
#include "ExecuteBase.h"

class ExecuteDeleteSelf : public ExecuteBase
{
public:
	ExecuteDeleteSelf(SharedParams_t params);
	virtual ~ExecuteDeleteSelf(void);

	// Выполнить действие
	virtual void Run(scene::ISceneNode* node, core::array<Event_t*>& events);
private:

};
