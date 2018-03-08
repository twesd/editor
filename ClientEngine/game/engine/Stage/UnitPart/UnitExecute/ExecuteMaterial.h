#pragma once
#include "ExecuteBase.h"

class ExecuteMaterial : public ExecuteBase
{
public:
	ExecuteMaterial(SharedParams_t params);
	virtual ~ExecuteMaterial(void);

	// Выполнить действие
	virtual void Run(scene::ISceneNode* node, core::array<Event_t*>& events);

	video::E_MATERIAL_TYPE MaterialType;
private:

};
