#pragma once
#include "ExecuteBase.h"

class ExecuteChangeSceneNodeId : public ExecuteBase
{
public:
	ExecuteChangeSceneNodeId(SharedParams_t params);
	virtual ~ExecuteChangeSceneNodeId(void);

	// Выполнить действие
	virtual void Run(scene::ISceneNode* node, core::array<Event_t*>& events);

	// Новый индентификатор модели
	int NodeId;

};
