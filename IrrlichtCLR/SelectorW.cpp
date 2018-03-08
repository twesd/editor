#include "StdAfx.h"
#include "SelectorW.h"


SelectorW::SelectorW(SharedParams_t* sharedParams) : BaseW(sharedParams)
{
	_collisionManager = SceneManager->getSceneCollisionManager();
	_selectionResult = gcnew List<SceneNodeW^>();
}

SceneNodeW^ SelectorW::SelectNode(int x, int y)
{	
	SceneNodeW^ resNode = GetNodeByScreenCoords(x, y, -1);
	if(resNode == nullptr)
	{
		return nullptr;
	}
	resNode->Highlight();

	_selectionResult->Clear();
	_selectionResult->Add(resNode);

	return resNode;
}

// Выбрать модель
void SelectorW::SelectNode( SceneNodeW^ node )
{
	node->Highlight();
	_selectionResult->Clear();
	_selectionResult->Add(node);
}


SceneNodeW^ SelectorW::GetNodeByScreenCoords( int x, int y, int filter )
{
	position2d<s32> screenPos(x, y);
	core::line3d<f32> ray = _collisionManager->getRayFromScreenCoordinates(screenPos);
	ISceneNode* node = _collisionManager->getSceneNodeFromRayBB(ray, filter);
	if(node == NULL)
		return nullptr;
	bool isValidType = (node->getType() == ESNT_ANIMATED_MESH || 
		node->getType() == ESNT_CUBE ||
		node->getType() == ESNT_SPHERE);
	if(!isValidType)
		return nullptr;
	SceneNodeW^ resNode = gcnew SceneNodeW(node, SharedParams);
	return resNode;
}

// Снять со всех выделение
void SelectorW::ClearSelection()
{
	_selectionResult->Clear();

	ISceneNode* node = 0;
	ISceneNode* start =  SceneManager->getRootSceneNode();
	const core::list<ISceneNode*>& list = start->getChildren();
	core::list<ISceneNode*>::ConstIterator it = list.begin();
	for (; it!=list.end(); ++it)
	{
		node = *it;
		if(node->getType() != ESNT_ANIMATED_MESH && node->getType() != ESNT_MESH) 
			continue;
		SceneNodeW^ nodeW = gcnew SceneNodeW(node, SharedParams);
		nodeW->UnHighlight();
	}
}


