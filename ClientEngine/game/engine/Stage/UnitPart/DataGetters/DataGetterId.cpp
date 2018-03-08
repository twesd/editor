#include "DataGetterId.h"
#include "../../UnitInstance/UnitInstanceStandard.h"
#include "../../../Controls/ControlEvent.h"

DataGetterId::DataGetterId(SharedParams_t params) : DataGetterBase(params)
{
	FilterNodeId = 0;
}

DataGetterId::~DataGetterId()
{
	
}

// Получить данные
void* DataGetterId::GetDataItem(  core::array<Event_t*>& events )
{
	_DEBUG_BREAK_IF(Behavior == NULL)

	ISceneNode* mainNode = Behavior->GetUnitInstance()->SceneNode;
	
	ISceneNode* node = 0;
	ISceneNode* start =  SceneManager->getRootSceneNode();
	const core::list<ISceneNode*>& list = start->getChildren();
	core::list<ISceneNode*>::ConstIterator it = list.begin();
	for (; it!=list.end(); ++it)
	{
		node = *it;
		if(node->getType() != ESNT_ANIMATED_MESH && 
			node->getType() != ESNT_MESH &&
			node->getType() != ESNT_EMPTY) 
			continue;
		if(((node->getID() & FilterNodeId) == 0) || node == mainNode)
			continue;
		if(!node->isVisible())
			continue;
		return node;
	}

	return NULL;
}

