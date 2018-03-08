#include "ExecuteMoveToSceneNode.h"
#include "../../UnitInstance/UnitInstanceStandard.h"

ExecuteMoveToSceneNode::ExecuteMoveToSceneNode(SharedParams_t params) : ExecuteBase(params),
	Speed(1), _selectSceneNode(NULL), TargetDist(1), ObstacleFilterId(-1)
{
	_pathAnimator = new PathSceneNodeAnimator(params);
}

ExecuteMoveToSceneNode::~ExecuteMoveToSceneNode()
{
	_selectSceneNode->drop();
	_pathAnimator->drop();
}

// Выполнить действие
void ExecuteMoveToSceneNode::Run( scene::ISceneNode* node, core::array<Event_t*>& events )
{
	ExecuteBase::Run(node, events);

	_selectSceneNode->SetUnitInstance(UnitInstance);
	core::array<ISceneNode*> nodes = _selectSceneNode->Select(events);
	if (nodes.size() == 0)
		return;

	_pathAnimator->Init(nodes[0], Speed, TargetDist, ObstacleFilterId);
	node->addAnimator(_pathAnimator);
}

// Установка алгоритм выборки объекта
// Объект захватывается grab
void ExecuteMoveToSceneNode::SetSelectSceneNode( UnitSelectSceneNodeBase* selectSceneNode )
{
	_selectSceneNode = selectSceneNode;
	_selectSceneNode->grab();
}
