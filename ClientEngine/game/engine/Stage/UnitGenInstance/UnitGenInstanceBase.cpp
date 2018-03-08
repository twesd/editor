#include "UnitGenInstanceBase.h"
#include "Animation.h"

UnitGenInstanceBase::UnitGenInstanceBase(SharedParams_t params) : Base(params),
	UnitName(), StartPosition(), StartRotation(),
	Creations()
{
	
}

UnitGenInstanceBase::~UnitGenInstanceBase(void)
{
	for (u32 i = 0; i < Creations.size(); i++)
	{
		Creations[i]->drop();
	}
}

// Применить начальные параметры к объекту сцены
void UnitGenInstanceBase::ApplyStartParameters( ISceneNode* node )
{
	node->setPosition(StartPosition);
	node->setRotation(StartRotation);
	if(node->getType() == ESNT_ANIMATED_MESH)
	{
		IAnimatedMeshSceneNode* animNode = (IAnimatedMeshSceneNode*)node;
		animNode->setAnimationSpeed(ANIMATION_SPEED);
		animNode->setFrameLoop(0, ANIMATION_FRAME);
		animNode->setLoopMode(false);

		// Обновляем BoundBox
		animNode->setCurrentFrame(1);
		animNode->animateJoints(true);
		IAnimatedMesh* animMesh = animNode->getMesh();
		// Получаем mesh для updateBoundingBox
		animMesh->getMesh(0);

		animNode->setCurrentFrame(0);
		animNode->updateAbsolutePosition();
	}
}

bool UnitGenInstanceBase::NeedCreate()
{
	if(Creations.size() == 0) 
		return false;	
	for (u32 i = 0; i < Creations.size(); i++)
	{
		UnitCreationBase* creation = Creations[i];
		if(!creation->NeedCreate()) 
		{
			return false;
		}
	}

	// Оповещаем об создании нового юнита
	for (u32 i = 0; i < Creations.size(); i++)
	{
		UnitCreationBase* creation = Creations[i];
		creation->UnitCreated();
	}

	return true;
}

