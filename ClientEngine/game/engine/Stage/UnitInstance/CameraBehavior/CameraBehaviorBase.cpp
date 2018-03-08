#include "CameraBehaviorBase.h"

CameraBehaviorBase::CameraBehaviorBase(SharedParams_t params, ICameraSceneNode* camera) : Base(params)
{
	Camera = camera;
}

CameraBehaviorBase::~CameraBehaviorBase(void)
{
}
