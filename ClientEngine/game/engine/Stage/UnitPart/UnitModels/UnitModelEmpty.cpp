#include "UnitModelEmpty.h"

UnitModelEmpty::UnitModelEmpty( SharedParams_t params ) : UnitModelBase(params)
{
}

ISceneNode* UnitModelEmpty::LoadSceneNode()
{
	ISceneNode* node = SceneManager->addEmptySceneNode();
	return node;
}
