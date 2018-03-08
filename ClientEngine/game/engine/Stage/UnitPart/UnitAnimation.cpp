#include "UnitAnimation.h"
#include "Animation.h"

UnitAnimation::UnitAnimation(SharedParams_t params) : Base(params),
	Name(), StartFrame(0), EndFrame(0), Speed(ANIMATION_SPEED), 
	Loop(false)
{
}

UnitAnimation::~UnitAnimation(void)
{
}

// Выполнить анимацию
void UnitAnimation::Execute( scene::ISceneNode* node )
{
	if(node == NULL) 
	{
		return;
	}

	if(node->getType() != ESNT_ANIMATED_MESH) 
	{
		return;
	}

	IAnimatedMeshSceneNode* animNode = (IAnimatedMeshSceneNode*)node;
	if (Loop)
	{
		s32 startFrame = animNode->getStartFrame();
		s32 endFrame = animNode->getEndFrame();
		if (startFrame == (StartFrame*ANIMATION_FRAME) && 
			endFrame == (EndFrame*ANIMATION_FRAME))
		{
			animNode->setLoopMode(Loop);
			return;
		}			
	}

	animNode->setLoopMode(Loop);
	animNode->setFrameLoop(StartFrame*ANIMATION_FRAME, EndFrame*ANIMATION_FRAME);
	animNode->setAnimationSpeed((f32)Speed);	
	
}


