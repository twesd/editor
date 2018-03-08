#include "UnitSelectSceneNodeDistance.h"
#include "../../UnitInstance/UnitInstanceStandard.h"

UnitSelectSceneNodeDistance::UnitSelectSceneNodeDistance(SharedParams_t params) : UnitSelectSceneNodeBase(params)
{
	CompareType = COMPARE_TYPE_LESS;
	Distance = 1;
	UseUnitSize = false;
}

UnitSelectSceneNodeDistance::~UnitSelectSceneNodeDistance()
{
}

// Выполнить выборку
core::array<scene::ISceneNode*> UnitSelectSceneNodeDistance::Select( core::array<Event_t*>& events )
{
	ISceneNode* mainNode = UnitInstance->SceneNode;
	core::array<scene::ISceneNode*> selectNodes = NodeWorker::SelectNodes(
		SceneManager->getRootSceneNode(),
		FilterNodeId,
		CompareType,
		Distance,
		UseUnitSize,
		mainNode,
		SelectChilds);
	return selectNodes;
}
