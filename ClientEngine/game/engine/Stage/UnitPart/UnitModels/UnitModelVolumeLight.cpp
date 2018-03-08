#include "UnitModelVolumeLight.h"

UnitModelVolumeLight::UnitModelVolumeLight( SharedParams_t params ) : UnitModelBase(params)
{
	SubdivU = 32;
	SubdivV = 32;
}

ISceneNode* UnitModelVolumeLight::LoadSceneNode()
{
	ISceneNode* node = SceneManager->addVolumeLightSceneNode(
		NULL, 0, SubdivU, SubdivV, Foot, Tail);
	return node;
}
