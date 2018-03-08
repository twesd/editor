#include "UnitCreationDistance.h"
#include "UnitGenInstanceBase.h"

UnitCreationDistance::UnitCreationDistance(SharedParams_t params) : UnitCreationBase(params)
{
	
}

UnitCreationDistance::~UnitCreationDistance(void)
{
}

// Необходимо ли создать юнит
bool UnitCreationDistance::NeedCreate()
{
	_DEBUG_BREAK_IF(GenInstance == NULL)

	vector3df mainNodePos = GenInstance->StartPosition;

	core::array<scene::ISceneNode*> selectNodes;

	ISceneNode* node = 0;
	ISceneNode* start =  SceneManager->getRootSceneNode();
	const core::list<ISceneNode*>& list = start->getChildren();
	core::list<ISceneNode*>::ConstIterator it = list.begin();
	int count = 0;
	for (; it!=list.end(); ++it)
	{
		node = *it;
		if(node->getType() != ESNT_ANIMATED_MESH && 
			node->getType() != ESNT_MESH &&
			node->getType() != ESNT_EMPTY) 
			continue;
		if(((node->getID() & FilterNodeId) == 0))
			continue;
		if(!node->isVisible())
			continue;
		f32 dist = NodeWorker::GetDistanceToNode(mainNodePos, node, UseUnitSize);
		if(CompareType == COMPARE_TYPE_EQUAL && (dist == Distance))
			count++;
		if(CompareType == COMPARE_TYPE_LESS && (dist < Distance))
			count++;
		if(CompareType == COMPARE_TYPE_MORE && (dist > Distance))
			count++;
	}

	// Если CountNodes = -1, то не учитываем заданный параметр
	if(CountNodes == -1 && count > 0)
	{
		return true;
	}
	return (count == CountNodes);
}

// Можно ли удалить данное условие создания
bool UnitCreationDistance::CanDispose()
{
	return false;
}

void UnitCreationDistance::UnitCreated()
{
	
}
