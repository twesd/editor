#include "UnitEventAnimation.h"
#include "../UnitBehavior.h"
#include "Animation.h"

UnitEventAnimation::UnitEventAnimation(SharedParams_t params) : UnitEventBase(params)
{
	FrameNr = 0;
	OnEnd = true;
	_isStarted = true;
}

UnitEventAnimation::~UnitEventAnimation(void)
{
}

// Выполняется ли условие
bool UnitEventAnimation::IsApprove( core::array<Event_t*>& events )
{
	ISceneNode* node = Behavior->GetSceneNode();
	if(node->getType() != ESNT_ANIMATED_MESH) return false;
	IAnimatedMeshSceneNode* animNode = (IAnimatedMeshSceneNode*)node;
	s32 curFrm = (s32)animNode->getFrameNr();
	if(OnEnd)
	{
		return (animNode->getEndFrame() == curFrm);
	}
	else 
	{
		s32 startFrm = animNode->getStartFrame();
		s32 offset = ((curFrm - startFrm) / ANIMATION_FRAME);
		return (offset >= FrameNr);
	}
}

// Начало выполнения
void UnitEventAnimation::Begin()
{
	_isStarted = true;
}


