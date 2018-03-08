#include "UnitModelParticleSystem.h"
#include "Core/Scene/Billboard.h"
#include "Core/NodeWorker.h"


UnitModelParticleSystem::UnitModelParticleSystem( SharedParams_t params ) : UnitModelBase(params)
{
}

ISceneNode* UnitModelParticleSystem::LoadSceneNode()
{
	ISceneNode* node = SceneManager->addParticleSystemSceneNode();
	NodeWorker::ApplyMeshSetting(node);
	return node;
}
