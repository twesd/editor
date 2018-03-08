#include "UnitSelectSceneNodeData.h"
#include "../../UnitInstance/UnitInstanceStandard.h"
#include "Core/NodeWorker.h"

UnitSelectSceneNodeData::UnitSelectSceneNodeData(SharedParams_t params) : UnitSelectSceneNodeBase(params),
	DataName()
{
}

UnitSelectSceneNodeData::~UnitSelectSceneNodeData()
{
}

// Выполнить выборку
core::array<scene::ISceneNode*> UnitSelectSceneNodeData::Select( core::array<Event_t*>& events )
{
	ISceneNode* mainNode = UnitInstance->SceneNode;
	vector3df mainNodePos = mainNode->getAbsolutePosition();

	core::array<scene::ISceneNode*> selectNodes;
	ISceneNode* node = UnitInstance->GetDataAsSceneNode(DataName);
	if(node == NULL) return selectNodes;

	selectNodes.push_back(node);
	return selectNodes;
}
