#include "UnitSelectSceneNodeId.h"
#include "../../UnitInstance/UnitInstanceStandard.h"

UnitSelectSceneNodeId::UnitSelectSceneNodeId(SharedParams_t params) : UnitSelectSceneNodeBase(params)
{

}

UnitSelectSceneNodeId::~UnitSelectSceneNodeId()
{
}

// Выполнить выборку
core::array<scene::ISceneNode*> UnitSelectSceneNodeId::Select( core::array<Event_t*>& events )
{
	ISceneNode* mainNode = UnitInstance->SceneNode;

	core::array<scene::ISceneNode*> selectNodes;

	ISceneNode* start =  SceneManager->getRootSceneNode();

	SelectNodes(start, mainNode, selectNodes);

	return selectNodes;
}

void UnitSelectSceneNodeId::SelectNodes( ISceneNode* start, ISceneNode* mainNode, core::array<scene::ISceneNode*> &selectNodes )
{
	ISceneNode* node = 0;
	const core::list<ISceneNode*>& list = start->getChildren();
	core::list<ISceneNode*>::ConstIterator it = list.begin();
	for (; it!=list.end(); ++it)
	{
		node = *it;
		if(node->getType() != ESNT_ANIMATED_MESH && 
			node->getType() != ESNT_MESH &&
			node->getType() != ESNT_EMPTY) 
		{
			continue;
		}

		if (SelectChilds)
		{
			SelectNodes(node, mainNode, selectNodes);
		}

		if(((node->getID() & FilterNodeId) == 0) || node == mainNode)
			continue;
		if(!node->isVisible())
			continue;
		selectNodes.push_back(node);
	}
}
