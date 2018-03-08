#include "UnitCreationBBox.h"
#include "UnitGenInstanceBase.h"

UnitCreationBBox::UnitCreationBBox(SharedParams_t params) : UnitCreationBase(params)
{
	
}

UnitCreationBBox::~UnitCreationBBox(void)
{
}

// Необходимо ли создать юнит
bool UnitCreationBBox::NeedCreate()
{
	_DEBUG_BREAK_IF(GenInstance == NULL)

	vector3df mainNodePos = GenInstance->StartPosition;
	matrix4 m;
	m.setTranslation(mainNodePos);

	// Сдвигаем границу в заданную область
	//
	aabbox3df box(Boundbox);
	m.transformBox(box);

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
		if (box.isPointInside(node->getAbsolutePosition()))
		{
			count++;
		}
	}

	// Если CountNodes = -1, то не учитываем заданный параметр
	if(CountNodes == -1 && count > 0)
	{
		return true;
	}
	return (count == CountNodes);
}

// Можно ли удалить данное условие создания
bool UnitCreationBBox::CanDispose()
{
	return false;
}

void UnitCreationBBox::UnitCreated()
{
	
}
