#include "ExecuteChangeSceneNodeId.h"
#include "../../UnitInstance/UnitInstanceStandard.h"

ExecuteChangeSceneNodeId::ExecuteChangeSceneNodeId(SharedParams_t params) : ExecuteBase(params),
	NodeId(-1)
{
}

ExecuteChangeSceneNodeId::~ExecuteChangeSceneNodeId()
{
}

// Выполнить действие
void ExecuteChangeSceneNodeId::Run( scene::ISceneNode* node, core::array<Event_t*>& events )
{
	ExecuteBase::Run(node, events);

	node->setID(NodeId);
}
