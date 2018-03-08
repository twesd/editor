#include "NodeWorker.h"
#include "Core/FileUtility.h"


scene::IAnimatedMeshSceneNode* NodeWorker::AddNode(
	ISceneManager* sceneManager,
	video::IVideoDriver* driver, 
	stringc modelPath,bool culling, 
	bool lighting, 
	bool use32bit, 
	bool useHardwareBuffer)
{
	if(use32bit)
	{
		//for this texture use good quality
		driver->setTextureCreationFlag(video::ETCF_ALWAYS_32_BIT, true);
	}
	else
	{		
		driver->setTextureCreationFlag(video::ETCF_ALWAYS_16_BIT, true);
	}

	modelPath = FileUtility::GetUpdatedFileName(modelPath);
	scene::IAnimatedMeshSceneNode* node;
#ifdef DEBUG_MESSAGE_PRINT
	if(sceneManager->getMeshCache()->getMeshByFilename(modelPath.c_str()) == NULL)
	{
		printf("[WARNING] Node not in cache : %s \n", modelPath.c_str());
	}
#endif
	node = sceneManager->addAnimatedMeshSceneNode(sceneManager->getMesh(modelPath.c_str()));
	_DEBUG_BREAK_IF(node == NULL)
	ApplyMeshSetting(node, culling, lighting);

	if(useHardwareBuffer)
	{
		node->getMesh()->setHardwareMappingHint(scene::EHM_STREAM, scene::EBT_VERTEX);
	}

	//restore quality
	driver->setTextureCreationFlag(video::ETCF_ALWAYS_16_BIT, true);

	return node;
}


void NodeWorker::RemoveNode(scene::IAnimatedMeshSceneNode* node)
{
	if(node == NULL) return;
	if (node->getSceneManager() != NULL)
	{
		node->getSceneManager()->getMeshCache()->removeMesh(node->getMesh());
	}
	node->removeAll();
	node->remove();	
}

void NodeWorker::RemoveNode(scene::ISceneNode* node)
{	
	node->removeAll();
	node->remove();	
}


void NodeWorker::ApplyMeshSetting(scene::IAnimatedMeshSceneNode* mesh, bool culling, bool lighting)
{
	ApplyMeshSetting((scene::ISceneNode*)mesh, culling, lighting);
}

void NodeWorker::ApplyMeshSetting(scene::ISceneNode* mesh, bool culling, bool lighting)
{
	mesh->setMaterialFlag(video::EMF_LIGHTING, lighting);
	mesh->setDebugDataVisible(scene::EDS_OFF);
	mesh->setMaterialFlag(video::EMF_GOURAUD_SHADING, false);
	mesh->setMaterialFlag(video::EMF_BILINEAR_FILTER, true);
	mesh->setMaterialFlag(video::EMF_TRILINEAR_FILTER, false);
	mesh->setMaterialFlag(video::EMF_ANISOTROPIC_FILTER, false);
	mesh->setMaterialFlag(video::EMF_BACK_FACE_CULLING, false);
	mesh->setMaterialFlag(video::EMF_FRONT_FACE_CULLING, false);
	if(culling)
	{
		mesh->setAutomaticCulling(scene::EAC_BOX);
	}
	else
	{
		mesh->setAutomaticCulling(scene::EAC_OFF);
	}
}

void NodeWorker::ApplyMaterialSetting(video::SMaterial * material, bool lighting)
{
	material->setFlag(video::EMF_LIGHTING, lighting);	
	material->setFlag(video::EMF_GOURAUD_SHADING, false);
	material->setFlag(video::EMF_BILINEAR_FILTER, true);
	material->setFlag(video::EMF_TRILINEAR_FILTER, false);
	material->setFlag(video::EMF_ANISOTROPIC_FILTER, false);
	material->setFlag(video::EMF_BACK_FACE_CULLING, false);
	material->setFlag(video::EMF_FRONT_FACE_CULLING, false);
}

// Существует ли заданная модель
bool NodeWorker::GetIsNodeExist(ISceneNode* searchNode, ISceneManager* sceneManager )
{
	if(searchNode == NULL) 
		return false;
	ISceneNode* node = 0;
	ISceneNode* start =  sceneManager->getRootSceneNode();
	const core::list<ISceneNode*>& list = start->getChildren();
	core::list<ISceneNode*>::ConstIterator it = list.begin();
	for (; it!=list.end(); ++it)
	{
		node = *it;
		if (node == searchNode)
		{
			return true;
		}
		if(GetIsNodeExistInChilds(node, searchNode))
			return true;
	}

	return false;
}


bool NodeWorker::GetIsNodeExistInChilds( ISceneNode* &rootNode, ISceneNode* searchNode )
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


// Получить дистанцию между моделями
irr::f32 NodeWorker::GetDistance( ISceneNode* node1, ISceneNode* node2, bool useNodeSize )
{
	const core::aabbox3df & bbox1 = node1->getTransformedBoundingBox();
	const core::aabbox3df & bbox2 = node2->getTransformedBoundingBox();
	vector3df node1Center = bbox1.getCenter();
	vector3df node2Center = bbox2.getCenter();
	f32 dist = node1Center.getDistanceFrom(node2Center);
	// TODO : доделать корректную обработку UnitSize
	if(useNodeSize)
	{

		if(bbox1.intersectsWithBox(bbox2))
		{
			return 0;
		}

		// get world to object space transform
		core::matrix4 worldToObject;
		if (!node2->getAbsoluteTransformation().getInverse(worldToObject))
		{
			return dist;
		}

		const vector3df& rayVector = node2Center - node1Center;
		const vector3df& rayStart = node1Center;
		const aabbox3df& objectBox = node2->getBoundingBox();

		core::vector3df edges[8];
		bbox2.getEdges(edges);

		/* We need to check against each of 6 faces, composed of these corners:
				/3--------/7
				/  |      / |
			/   |     /  |
			1---------5  |
			|   2- - -| -6
			|  /      |  /
			|/        | /
			0---------4/

			Note that we define them as opposite pairs of faces.
		*/
		static const s32 faceEdges[6][3] =
		{
			{ 0, 1, 5 }, // Front
			{ 6, 7, 3 }, // Back
			{ 2, 3, 1 }, // Left
			{ 4, 5, 7 }, // Right
			{ 1, 3, 7 }, // Top
			{ 2, 0, 4 }  // Bottom
		};

		core::vector3df intersection;
		core::plane3df facePlane;
		f32 bestDistToBoxBorder = FLT_MAX;
		f32 bestToIntersectionSq = FLT_MAX;

		for(s32 face = 0; face < 6; ++face)
		{
			facePlane.setPlane(edges[faceEdges[face][0]],
				edges[faceEdges[face][1]],
				edges[faceEdges[face][2]]);

			// Only consider lines that might be entering through this face, since we
			// already know that the start point is outside the box.
			if(facePlane.classifyPointRelation(rayStart) != core::ISREL3D_FRONT)
			{
				continue;
			}


			// Don't bother using a limited ray, since we already know that it should be long
			// enough to intersect with the box.
			if(facePlane.getIntersectionWithLine(rayStart, rayVector, intersection))
			{
				const f32 toIntersectionSq = rayStart.getDistanceFromSQ(intersection);

				if (!bbox2.isPointInside(intersection))
				{
					continue;
				}
				
				// We have to check that the intersection with this plane is actually
				// on the box, so need to go back to object space again.
				worldToObject.transformVect(intersection);

				// find the closest point on the box borders. Have to do this as exact checks will fail due to floating point problems.
				f32 distToBorder = core::max_ ( core::min_ (core::abs_(objectBox.MinEdge.X-intersection.X), core::abs_(objectBox.MaxEdge.X-intersection.X)),
					core::min_ (core::abs_(objectBox.MinEdge.Y-intersection.Y), core::abs_(objectBox.MaxEdge.Y-intersection.Y)),
					core::min_ (core::abs_(objectBox.MinEdge.Z-intersection.Z), core::abs_(objectBox.MaxEdge.Z-intersection.Z)) );
				if ( distToBorder < bestDistToBoxBorder )
				{
					bestDistToBoxBorder = distToBorder;
					bestToIntersectionSq = toIntersectionSq;
				}
			}

			// If the ray could be entering through the first face of a pair, then it can't
			// also be entering through the opposite face, and so we can skip that face.
			if (!(face & 0x01))
				++face;
		}
		if (bestToIntersectionSq < FLT_MAX)
		{
			return sqrtf(bestToIntersectionSq);
		}
		else
		{
			return dist;
		}
	}
	return dist;
}

// Получить дистанцию до модели
irr::f32 NodeWorker::GetDistanceToNode( vector3df position1, ISceneNode* node2, bool useNodeSize )
{
	vector3df node1Center = position1;
	vector3df node2Center = node2->getTransformedBoundingBox().getCenter();
	f32 dist = node1Center.getDistanceFrom(node2Center);
	if(useNodeSize)
	{
		aabbox3df box = node2->getBoundingBox();
		dist -= (box.getExtent().getLength() / 2);
		if(dist < 0) dist = 0;
	}
	return dist;
}



irr::core::vector3df NodeWorker::GetAbsoluteRotation( ISceneNode* node )
{
	vector3df rot = node->getRotation();
	ISceneNode* parent = node->getParent();
	if(parent == NULL) return rot;
	while(parent != NULL)
	{
		rot += parent->getRotation();
		parent = parent->getParent();
	}	
	while(rot.Y>360) rot.Y-=360;
	while(rot.Y<0) rot.Y+=360;
	return rot;		
}

irr::core::vector3df NodeWorker::GetParentRotation( ISceneNode* node )
{
	vector3df rot;
	ISceneNode* parent = node->getParent();
	if(parent == NULL) return rot;
	while(parent != NULL)
	{
		rot += parent->getRotation();
		parent = parent->getParent();
	}	
	while(rot.Y>360) rot.Y-=360;
	while(rot.Y<0) rot.Y+=360;
	return rot;		
}

void NodeWorker::SetAbsoluteRotation( ISceneNode* node, core::vector3df rot )
{
	ISceneNode* parent = node->getParent();	
	if(parent == NULL)
	{
		node->setRotation(rot);
		return;
	}
	vector3df parentRot;
	while(parent != NULL)
	{
		parentRot += parent->getRotation();
		parent = parent->getParent();
	}
	rot -= parentRot;
	node->setRotation(rot);
}

// Произвести выборку
core::array<scene::ISceneNode*> NodeWorker::SelectNodes( 
	ISceneNode* rootNode, 
	s32 filterId, 
	CompareTypeEnum compareType, 
	f32 distance,
	bool useUnitSize, 
	ISceneNode* mainNode,
	bool selectChilds)
{
	core::array<scene::ISceneNode*> selectNodes;

	ISceneNode* node = 0;
	ISceneNode* start =  rootNode;
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
		if (selectChilds)
		{
			core::array<scene::ISceneNode*> chNodes = 
				NodeWorker::SelectNodes(node, filterId,
				compareType, distance, useUnitSize, mainNode, selectChilds);
			for (u32 iCh = 0; iCh < chNodes.size(); iCh++)
			{
				selectNodes.push_back(chNodes[iCh]);
			}
		}

		if(((node->getID() & filterId) == 0) || node == mainNode)
			continue;
		if(!node->isVisible())
			continue;
		f32 dist = NodeWorker::GetDistance(mainNode, node, useUnitSize);
		if(compareType == COMPARE_TYPE_EQUAL && (dist == distance))
			selectNodes.push_back(node);
		if(compareType == COMPARE_TYPE_LESS && (dist < distance))
			selectNodes.push_back(node);
		if(compareType == COMPARE_TYPE_MORE && (dist > distance))
			selectNodes.push_back(node);
	}

	return selectNodes;
}



// Найти ближайщую модель
ISceneNode* NodeWorker::GetClosedNode( 
	ISceneManager* sceneManager, 
	s32 filterId, 
	CompareTypeEnum compareType, 
	f32 distance,
	bool useUnitSize, 
	ISceneNode* mainNode )
{
	// 
	ISceneNode* destNode = NULL;
	f32 minDist = 0;

	ISceneNode* node = NULL;
	ISceneNode* start =  sceneManager->getRootSceneNode();
	const core::list<ISceneNode*>& list = start->getChildren();
	core::list<ISceneNode*>::ConstIterator it = list.begin();
	for (; it!=list.end(); ++it)
	{
		node = *it;
		if(node->getType() != ESNT_ANIMATED_MESH && 
			node->getType() != ESNT_MESH && 
			node->getType() != ESNT_EMPTY) 
			continue;
		if(((node->getID() & filterId) == 0) || node == mainNode)
			continue;
		if(!node->isVisible())
			continue;
		f32 dist = NodeWorker::GetDistance(mainNode, node, useUnitSize);
		if(compareType == COMPARE_TYPE_EQUAL && (dist == distance))
		{
			return node;
		}
		if(compareType == COMPARE_TYPE_LESS && (dist < distance))
		{
			if(destNode == NULL || dist < minDist)
			{
				minDist = dist;
				destNode = node;
			}
		}
		if(compareType == COMPARE_TYPE_MORE && (dist > distance))
		{
			if(destNode == NULL || dist < minDist)
			{
				minDist = dist;
				destNode = node;
			}
		}
	}

	return destNode;
}
