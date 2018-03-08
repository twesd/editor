#include "UnitModelSphere.h"
#include "Core/Scene/Billboard.h"
#include "Core/NodeWorker.h"


UnitModelSphere::UnitModelSphere( SharedParams_t params ) : UnitModelBase(params)
{
	PolyCount = 8;
	Radius = 1;
}

ISceneNode* UnitModelSphere::LoadSceneNode()
{
	ISceneNode* node = SceneManager->addSphereSceneNode(Radius, PolyCount);
	NodeWorker::ApplyMeshSetting(node, false, false);
	return node;
}
