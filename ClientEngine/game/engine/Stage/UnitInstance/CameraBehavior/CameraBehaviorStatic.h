#pragma once
#include "CameraBehaviorBase.h"

class CameraBehaviorStatic : public CameraBehaviorBase
{
public:
	CameraBehaviorStatic(SharedParams_t params, ICameraSceneNode* camera);
	virtual ~CameraBehaviorStatic(void);

	CameraBehaviorType GetType()
	{
		return CameraBehaviorBase::Static;
	}

	void Update();
};
