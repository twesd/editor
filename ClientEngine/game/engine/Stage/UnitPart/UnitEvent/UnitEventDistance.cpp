#include "UnitEventDistance.h"
#include "../UnitBehavior.h"
#include "../../UnitInstance/UnitInstanceStandard.h"

UnitEventDistance::UnitEventDistance(SharedParams_t params) : UnitEventBase(params),
	DataName()
{
	CompareType = COMPARE_TYPE_EQUAL;
	FilterNodeId = -1;
	Distance = 0;
	UseUnitSize = true;
}

UnitEventDistance::~UnitEventDistance(void)
{
}

// Выполняется ли условие
bool UnitEventDistance::IsApprove( core::array<Event_t*>& events )
{
	ISceneNode* mainNode = Behavior->GetSceneNode();
	vector3df mainNodePos = mainNode->getTransformedBoundingBox().getCenter();
	
	if(DataName != "")
	{
		ISceneNode* dataNode = Behavior->GetUnitInstance()->GetDataAsSceneNode(DataName);
		if(dataNode == NULL)
		{
			return false;
		}
		if(((dataNode->getID() & FilterNodeId) == 0) || dataNode == mainNode)
		{
			return false;
		}
		if(!dataNode->isVisible())
		{
			return false;
		}
		f32 dist = NodeWorker::GetDistance(mainNode, dataNode, UseUnitSize);
		if(CompareType == COMPARE_TYPE_EQUAL && (dist == Distance))
		{
			return true;
		}
		if(CompareType == COMPARE_TYPE_LESS && (dist < Distance))
		{
			return true;
		}
		if(CompareType == COMPARE_TYPE_MORE && (dist > Distance))
		{
			return true;
		}
	}
	else
	{
		ISceneNode* node = NodeWorker::GetClosedNode(
			SceneManager,
			FilterNodeId,
			CompareType,
			Distance,
			UseUnitSize,
			mainNode);
		return (node != NULL);
	}

	return false;
}

// Начало выполнения
void UnitEventDistance::Begin()
{
	
}

