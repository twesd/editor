#include "StdAfx.h"
#include "SceneNodeW.h"
#include "Convertor.h"

SceneNodeW::SceneNodeW(ISceneNode* node, SharedParams_t* shareParams)
{
	ScNode = node;
	_animNode = NULL;
	if(ScNode->getType() == ESNT_ANIMATED_MESH)
	{
		_animNode = (IAnimatedMeshSceneNode*) ScNode;
	}
	SharedParams = shareParams;
}

// Установить свойства по умолчанию
void SceneNodeW::SetDefaultProperty()
{
	if(!GetIsExist())
	{
		return;
	}
	ScNode->setPosition(vector3df(0,0,0));

	ScNode->setMaterialFlag(video::EMF_LIGHTING, false);
	ScNode->setDebugDataVisible(scene::EDS_OFF);
	ScNode->setMaterialFlag(video::EMF_GOURAUD_SHADING, false);
	ScNode->setMaterialFlag(video::EMF_BILINEAR_FILTER, false);
	ScNode->setMaterialFlag(video::EMF_TRILINEAR_FILTER, false);
	ScNode->setMaterialFlag(video::EMF_ANISOTROPIC_FILTER, false);
	ScNode->setMaterialFlag(video::EMF_BACK_FACE_CULLING, false);
	ScNode->setAutomaticCulling(scene::EAC_BOX);

	if(_animNode != NULL)
	{
		_animNode = (IAnimatedMeshSceneNode*) ScNode;
		_animNode->setAnimationSpeed(3000);
		_animNode->setFrameLoop(0, 0);
	}

	ScNode->setID(1);
}

// Получить модель
ISceneNode* SceneNodeW::GetNode()
{
	if(!GetIsExist())
	{
		return NULL;
	}
	return ScNode;
}

// Установить скорость 
void SceneNodeW::SetAnimationSpeed(float speed)
{	 
	if(!GetIsExist())
	{
		return;
	}
	if(_animNode == NULL) return;
	_animNode->setAnimationSpeed(speed);
}

// Установить кадры
void SceneNodeW::SetFrameLoop(int startFrame, int endFrame)
{
	if(!GetIsExist())
	{
		return;
	}
	if(_animNode == NULL) return;
	_animNode->setFrameLoop(startFrame, endFrame);
}

// Установить повтор
void SceneNodeW::SetLoopMode(bool loop)
{
	if(!GetIsExist())
	{
		return;
	}
	if(_animNode == NULL) return;
	_animNode->setLoopMode(loop);
}

// Получить позицию
Vertex3dW^ SceneNodeW::GetPosition()
{
	if(!GetIsExist())
	{
		return nullptr;
	}
	return gcnew Vertex3dW(ScNode->getPosition());
}

// Установить позицию
void SceneNodeW::SetPosition(Vertex3dW^ position)
{
	if(!GetIsExist())
	{
		return;
	}
	ScNode->setPosition(position->GetVector());
}

// Получить уголы поворота
Vertex3dW^ SceneNodeW::GetRotation()
{
	if(!GetIsExist())
	{
		return nullptr;
	}
	return gcnew Vertex3dW(ScNode->getRotation());
}

// Установить уголы поворота
void SceneNodeW::SetRotation(Vertex3dW^ rotation)
{
	if(!GetIsExist())
	{
		return;
	}
	ScNode->setRotation(rotation->GetVector());
}

// Получить масштаб
Vertex3dW^ SceneNodeW::GetScale()
{
	if(!GetIsExist())
	{
		return nullptr;
	}
	return gcnew Vertex3dW(ScNode->getScale());
}

// Установить масштаб
void SceneNodeW::SetScale(Vertex3dW^ scale)
{
	if(!GetIsExist())
	{
		return;
	}
	ScNode->setScale(scale->GetVector());
}


// Добавить трансформацию
void SceneNodeW::AddTransform(int type, List<Vertex3dW^>^ pointsW, List<u32>^ timesW)
{
	if(!GetIsExist())
	{
		return;
	}
	core::array<vector3df> points;
	for(int i = 0; i < pointsW->Count; i++)
	{
		points.push_back(pointsW[i]->GetVector());
	}

	core::array<u32> times;
	for(int i = 0; i < timesW->Count; i++)
	{
		times.push_back(timesW[i]);
	}

	vector3df rotation = ScNode->getRotation();

	if(type == 0)// линейное перемещение
	{
		LineAnimator* animator = new LineAnimator(*SharedParams);
		animator->Init(GetTransformPoints(points, rotation), times, false, false);
		ScNode->addAnimator(animator);
		animator->drop();
	}
}
	
// Удалить все трансформации
void SceneNodeW::ClearTransforms()
{
	if(!GetIsExist())
	{
		return;
	}
	ScNode->removeAnimators();
}

core::array<vector3df> SceneNodeW::GetTransformPoints(core::array<vector3df> points, vector3df rotation)
{
	core::array<vector3df> outPoints;
	for(u32 i = 0; i < points.size(); i++)
	{
		vector3df point = points[i];
		point.rotateXZBy(-rotation.Y);
		outPoints.push_back(point);
	}
	return outPoints;
}

// Подсветить объект
void SceneNodeW::Highlight()
{
	if(!GetIsExist())
	{
		return;
	}
	// Если у объекта не задана текстура, то делаем его прозрачным
	// Это связано с тем, что у объекта без текстуры не видно границ
	bool hasTexture = false;
	if (ScNode->getMaterialCount() != 0)
	{
		hasTexture = (ScNode->getMaterial(0).getTexture(0) != NULL);
	}

	if(!hasTexture)
	{
		ScNode->setDebugDataVisible(ScNode->isDebugDataVisible() | EDS_HALF_TRANSPARENCY);
	}
	ScNode->setDebugDataVisible(ScNode->isDebugDataVisible() | EDS_MESH_WIRE_OVERLAY);
}

// Снять подсветку
void SceneNodeW::UnHighlight()
{
	if(!GetIsExist())
	{
		return;
	}
	bool hasTexture = false;
	if (ScNode->getMaterialCount() != 0)
	{
		hasTexture = (ScNode->getMaterial(0).getTexture(0) != NULL);
	}

	if(!hasTexture && (ScNode->isDebugDataVisible() & EDS_HALF_TRANSPARENCY))
	{
		ScNode->setDebugDataVisible(ScNode->isDebugDataVisible() ^ EDS_HALF_TRANSPARENCY);
	}

	if(ScNode->isDebugDataVisible() & EDS_MESH_WIRE_OVERLAY)
	{
		ScNode->setDebugDataVisible(ScNode->isDebugDataVisible() ^ EDS_MESH_WIRE_OVERLAY);
	}
}

// Установить тип материи
void SceneNodeW::SetMaterialType( int matType )
{
	if(!GetIsExist())
	{
		return;
	}
	ScNode->setMaterialType((video::E_MATERIAL_TYPE)matType);
}

// Установить текстуру
void SceneNodeW::SetTexture( int index, String^ path )
{
	if(!GetIsExist())
	{
		return;
	}
	c8* chars = Convertor::ConvertToU8(path);
	ScNode->setMaterialTexture(index, SharedParams->Driver->getTexture(chars));
}

// Установить полупрозрачность
void SceneNodeW::EnableHalfTransparent( bool enable )
{
	if(!GetIsExist())
	{
		return;
	}
	if(enable)
	{
		ScNode->setDebugDataVisible(ScNode->isDebugDataVisible() | EDS_HALF_TRANSPARENCY);
	}
	else if (ScNode->isDebugDataVisible() & EDS_HALF_TRANSPARENCY)
	{
		ScNode->setDebugDataVisible(ScNode->isDebugDataVisible() ^ EDS_HALF_TRANSPARENCY);
	}
}

BoundboxW^ SceneNodeW::GetBoundBox()
{
	if(!GetIsExist())
	{
		return gcnew BoundboxW(aabbox3df());
	}
	core::aabbox3d<f32> box = ScNode->getBoundingBox();
	return gcnew BoundboxW(box);
}

bool SceneNodeW::GetIsExist()
{
	if(ScNode == NULL) 
		return false;
	ISceneNode* node = 0;
	ISceneNode* start =  SharedParams->SceneManager->getRootSceneNode();
	const core::list<ISceneNode*>& list = start->getChildren();
	core::list<ISceneNode*>::ConstIterator it = list.begin();
	for (; it!=list.end(); ++it)
	{
		node = *it;
		if (node == ScNode)
		{
			return true;
		}
		if(GetIsNodeExistInChilds(node, ScNode))
			return true;
	}

	return false;
}


bool SceneNodeW::GetIsNodeExistInChilds( ISceneNode* &rootNode, ISceneNode* searchNode )
{
	const core::list<ISceneNode*>& childs = rootNode->getChildren();
	core::list<ISceneNode*>::ConstIterator itChilds = childs.begin();
	ISceneNode* childNode;
	for (; itChilds!=childs.end(); ++itChilds)
	{
		childNode = *itChilds;
		if (childNode == searchNode)
		{
			return true;
		}
		if(GetIsNodeExistInChilds(childNode, searchNode))
			return true;
	}
	return false;
}
