#include "DataGetterDistance.h"
#include "../../UnitInstance/UnitInstanceStandard.h"
#include "../../../Controls/ControlEvent.h"

DataGetterDistance::DataGetterDistance(SharedParams_t params) : DataGetterBase(params)
{
	FilterNodeId = 0;
	CompareType = COMPARE_TYPE_LESS;
	Distance = 1;
	UseUnitSize = false;
}

DataGetterDistance::~DataGetterDistance()
{
	
}

// Получить данные
void* DataGetterDistance::GetDataItem(  core::array<Event_t*>& events )
{
	_DEBUG_BREAK_IF(Behavior == NULL)

	core::array<scene::ISceneNode*> selectNodes = NodeWorker::SelectNodes(
		SceneManager->getRootSceneNode(),
		FilterNodeId,
		CompareType,
		Distance,
		UseUnitSize,
		Behavior->GetSceneNode(),
		false);
	if(selectNodes.size() > 0)
		return selectNodes[0];
	return NULL;
}

