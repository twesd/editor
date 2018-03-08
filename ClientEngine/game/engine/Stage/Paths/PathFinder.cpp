#include "PathFinder.h"
#include "Core/Array/SceneNodeArray.h"
#include "Core/GeometryWorker.h"

#ifdef DEBUG_PATH_FINDER

//#define DEBUG_PATH_FINDER_GRID
#define DEBUG_PATH_FINDER_SEARCH_LINES

#endif

PathFinder::PathFinder(
	SharedParams_t params, 
	u32 mapSize2d,
	f32 mapSize3d) : Base(params)
{
	_currentNode = NULL;
	_obstacleFilterId = -1;
#ifdef DEBUG_PATH_FINDER
	_pathSceneNode = new PolylineSceneNode(params);

	_gridSceneNode = new PolylineSceneNode(params);
	_gridSceneNode->SetColor(video::SColor(0, 255, 255, 255));	
	_gridSceneNode->SetVertexColor(video::SColor(255, 0, 0, 255));	
	_gridSceneNode->setMaterialType(video::EMT_TRANSPARENT_ALPHA_CHANNEL);
	_gridSceneNode->SetWidth(0.3f);

	for (u32 iSL = 0; iSL < 10; iSL++)
	{
		PolylineSceneNode* sLine = new PolylineSceneNode(params);
		sLine->SetColor(video::SColor(128, 128, 128, 255));	
		sLine->SetVertexColor(video::SColor(255, 255, 255, 255));	
		sLine->SetWidth(0.4f);
		_searchLines.push_back(sLine);
	}
	_searchLinesCounter = 0;
#endif
	_mapSize2d = mapSize2d;
	_mapSize3d = mapSize3d;
	_map = new char[_mapSize2d*_mapSize2d];

	_finderGrid = new FinderGrid(_mapSize2d, _mapSize2d);
}

PathFinder::~PathFinder(void)
{
#ifdef DEBUG_PATH_FINDER
	_pathSceneNode->remove();
	_gridSceneNode->remove();
	for (u32 iSL = 0; iSL < _searchLines.size(); iSL++)
	{
		_searchLines[iSL]->remove();
	}
	_searchLines.clear();
#endif
	delete _map;
	delete _finderGrid;
}


/*
Установить модель для которой ищется путь
@node - модель
*/
void PathFinder::SetSourceNode( ISceneNode* node )
{
	_currentNode = node;
}

// Установить фильтр для моделий препятствий
void PathFinder::SetObstacleFilterId( int bitMask )
{
	_obstacleFilterId = bitMask;
}

/*
Получение следующего шага
@targetPos - цель
@wayDist - растояние которое надо пройти
@minTargetDist - дистанция на которую надо подойти
*/
vector3df PathFinder::GetNextPosition( vector3df targetPos, f32 wayDist, f32 minTargetDist )
{
	//wayDist = 0.002f; // DEBUG

	_DEBUG_BREAK_IF(_currentNode == NULL)
	_DEBUG_BREAK_IF(minTargetDist == 0)
		
	vector3df nodePos = _currentNode->getAbsolutePosition();
	aabbox3df curBox = _currentNode->getTransformedBoundingBox();

	targetPos.Y = nodePos.Y; // TODO : реализовать учёт высоты

	GeometryWorker::RoundVector(nodePos);
	GeometryWorker::RoundVector(targetPos);

	s32 halfSize = _mapSize2d / 2;
	f32 halfSize3d = _mapSize3d / 2;

	f32 coeff3dToMap = ((f32)halfSize / halfSize3d);
	f32 coeffMapTo3d = ((f32)halfSize3d / halfSize);

	vector3df nodeExtent = _currentNode->getBoundingBox().getExtent();
	nodeExtent /= 2;

	// Построение карты местности	
	BuildMap(_currentNode, _mapSize2d, _mapSize3d, nodeExtent);

#ifdef DEBUG_PATH_FINDER
#ifdef DEBUG_PATH_FINDER_GRID
	_gridSceneNode->Clear();

	for (s32 z = 0; z < _mapSize2d ; z++)
	{
		for (s32 x = 0; x < _mapSize2d ; x++)
		{
			if (_map[x + z*_mapSize2d] & MapWall)
			{	
				vector3df pos;
				pos.X = ((f32)(x - halfSize)) * coeffMapTo3d;
				pos.Z = ((f32)(z - halfSize)) * coeffMapTo3d;
				pos.Y = 1;				
				_gridSceneNode->AddPoint(pos);
			}
		}	
	}
#endif
#endif

#ifdef DEBUG_PATH_FINDER
	_pathSceneNode->Clear();
	_pathSceneNode->AddPoint(nodePos);
#endif

#ifdef DEBUG_PATH_FINDER_SEARCH_LINES
	for (u32 iSL = 0; iSL < _searchLines.size(); iSL++)
	{
		_searchLines[iSL]->Clear();
	}
	_searchLinesCounter = 0;
#endif

	// Текущая позиция в 2d карте
	s32 startX = (s32)floor(nodePos.X * coeff3dToMap + 0.5f) + halfSize;
	s32 startZ = (s32)floor(nodePos.Z * coeff3dToMap + 0.5f) + halfSize;

	if(_map[startX + startZ*_mapSize2d] & MapWall)
	{
		// Объект внутри препятствия
		return nodePos;
	}

	// Позиция цели в 2d карте
	s32 endX = (s32)floor(targetPos.X * coeff3dToMap + 0.5f) + halfSize;
	s32 endZ = (s32)floor(targetPos.Z * coeff3dToMap + 0.5f) + halfSize;

	// Обновляем данные в FinderGrid
	for (int y = 0; y < _mapSize2d; ++y) 
	{
		s32 ofssetY = y * _mapSize2d;
		for (int x = 0; x < _mapSize2d; ++x) 
		{
			FinderNode* node = _finderGrid->getNodeAt(x + ofssetY);
			node->Reset();
			node->IsWalkable = !(_map[x + ofssetY] & MapWall);
		}
	}

	AroundPointFinder finder;
	core::array<FinderNode*> pathNodes = finder.FindPath(vector2di(startX, startZ), vector2di(endX, endZ), *_finderGrid);
	if(pathNodes.size() < 2)
	{
		return nodePos;
	}


#ifdef DEBUG_PATH_FINDER
	for(s32 iDbg = 0; iDbg < (s32)pathNodes.size(); iDbg++)
	{
		FinderNode* fNode = pathNodes[iDbg];
		vector3df pathPos;
		pathPos.X = (f32)(fNode->Position.X - halfSize) * coeffMapTo3d;
		pathPos.Z = (f32)(fNode->Position.Y - halfSize) * coeffMapTo3d;
		_pathSceneNode->AddPoint(pathPos);
	}
#endif

	// Получаем позицию в соответствии с wayDist
	//
	vector3df prevPos = nodePos;
	for(s32 iPos = 1; iPos < (s32)pathNodes.size(); iPos++)
	{
		FinderNode* fNode = pathNodes[iPos];
		vector3df pathPos;
		pathPos.X = (f32)(fNode->Position.X - halfSize) * coeffMapTo3d;
		pathPos.Z = (f32)(fNode->Position.Y - halfSize) * coeffMapTo3d;
		f32 tDist = pathPos.getDistanceFrom(prevPos);
		if(tDist >= wayDist)
		{
			vector3df outP = prevPos + (pathPos - prevPos).normalize() * wayDist;
			s32 outX = (s32)floor((outP.X * coeff3dToMap) + 0.5f) + halfSize;
			s32 outZ = (s32)floor((outP.Z * coeff3dToMap) + 0.5f) + halfSize;
			if(_map[outX + outZ*_mapSize2d] & MapWall)
			{
				// Пытаемся перескачить препятствие увеличивая длину шага
				//
				f32 distStep = coeffMapTo3d / 2.0f;
				f32 wayDistLarge = wayDist;
				for(int i = 0; i < 3; i++)
				{
					wayDistLarge += distStep;
					if(wayDistLarge > tDist)
					{
						break;
					}
					outP = prevPos + (pathPos - prevPos).normalize() * wayDistLarge;
					outX = (s32)floor((outP.X * coeff3dToMap) + 0.5f) + halfSize;
					outZ = (s32)floor((outP.Z * coeff3dToMap) + 0.5f) + halfSize;
					if(!(_map[outX + outZ*_mapSize2d] & MapWall))
					{
						return outP;
					}
				}
				// В данном случаи не получилось перейти в новое положение
				return prevPos;
			}
			return outP;
		}
		else
		{
			wayDist -= tDist;
		}
		prevPos = pathPos;
	}

	_DEBUG_BREAK_IF(true);
	return nodePos;
}


/// <summary>Построение карты местности.</summary>
/// <param name="currentNode">[in,out] If non-null, the current node.</param>
/// <param name="size">		  The size.</param>
/// <param name="size3d">	  The size 3D.</param>
/// <param name="nodeSize">   Size of the node.</param>
void PathFinder::BuildMap( ISceneNode* currentNode, s32 size, f32 size3d, vector3df nodeSize)
{
	memset(_map, 0, size * size);

	s32 halfSize = size / 2;
	f32 halfSize3d = size3d / 2;

	f32 coeff3dToMap = ((f32)halfSize / halfSize3d);
	f32 coeffMapTo3d = ((f32)halfSize3d / halfSize);

	aabbox3df curNodeBox = currentNode->getTransformedBoundingBox();

	const core::list<ISceneNode*>& children = SceneManager->getRootSceneNode()->getChildren();
	core::list<ISceneNode*>::ConstIterator it = children.begin();
	for (; it != children.end(); ++it)
	{
		ISceneNode* current = *it;
		if(current == currentNode)
		{
			continue;
		}

		if(current->getType() != ESNT_ANIMATED_MESH && 
			current->getType() != ESNT_MESH &&
			current->getType() != ESNT_EMPTY)
		{
			continue;
		}

		if (current->isVisible())
		{
			if(current->getID() & _obstacleFilterId)
			{
				aabbox3df box = current->getTransformedBoundingBox();
				if (box.MaxEdge.Y < curNodeBox.MinEdge.Y ||
					box.MinEdge.Y > curNodeBox.MaxEdge.Y)
				{
					// Объекты находятся на разных высотах
					continue;
				}

				box.MinEdge.Y = 0;
				box.MaxEdge.Y = 0;

				const vector3df middle = box.getCenter();
				vector3df diag = middle - box.MinEdge;
				diag.X += (nodeSize.X / 2);
				diag.Z += (nodeSize.Z / 2);
				const vector3df p0(middle.X - diag.X, 0, middle.Z - diag.Z);
				const vector3df p1(middle.X + diag.X, 0, middle.Z - diag.Z);
				const vector3df p2(middle.X + diag.X, 0, middle.Z + diag.Z);
				const vector3df p3(middle.X - diag.X, 0, middle.Z + diag.Z);

				// (3) ----(2)
				// |        |
				// |        |
				// (0) --- (1)

				s32 startX = (s32)(p0.X * coeff3dToMap) + halfSize;
				s32 endX = (s32)(p1.X * coeff3dToMap) + halfSize;

				s32 startZ = (s32)(p0.Z * coeff3dToMap) + halfSize;
				s32 endZ = (s32)(p3.Z * coeff3dToMap) + halfSize;

				for (s32 iPosZ = startZ; iPosZ <= endZ; iPosZ++)
				{
					if(iPosZ < 0 || iPosZ >= size)
					{
						continue;
					}
					s32 offsetZ = (iPosZ * size);

					for (s32 iPosX = startX; iPosX <= endX; iPosX++)
					{
						if(iPosX < 0 || iPosX >= size)
						{
							continue;
						}

						_map[iPosX + offsetZ] |= MapWall;
					}
				}

			}
		}
	}
}

// Получение ближайщей свободной позиции к targetPosition
//	@targetPosition - цель
//	@outPosition - результат
//
bool PathFinder::TryGetFreePosition(vector3df targetPosition, vector3df& outPosition )
{
	BuildMap(_currentNode, _mapSize2d, _mapSize3d, vector3df(0, 0, 0));

	GeometryWorker::RoundVector(targetPosition);

	s32 halfSize = _mapSize2d / 2;
	f32 halfSize3d = _mapSize3d / 2;

	f32 coeff3dToMap = ((f32)halfSize / halfSize3d);
	f32 coeffMapTo3d = ((f32)halfSize3d / halfSize);

	s32 x2d = (s32)floor(targetPosition.X * coeff3dToMap + 0.5f) + halfSize;
	s32 z2d = (s32)floor(targetPosition.Z * coeff3dToMap + 0.5f) + halfSize;

	s32 outX;
	s32 outY;

	f32 minDist = 0;
	u32 mapOffset = 0;
	bool found = false;
	vector3df compareVec;
	compareVec.Y = targetPosition.Y;
	// Ищем ближайщею проходимю ячейку в 2d близкую к targetPosition
	//
	for (s32 iY = 0; iY < _mapSize2d; iY++)
	{
		for (s32 iX = 0; iX < _mapSize2d; iX++)
		{
			if((_map[iX + mapOffset] & MapWall) == 0)
			{
				compareVec.X = (f32)(iX - halfSize) * coeffMapTo3d;
				compareVec.Z = (f32)(iY - halfSize) * coeffMapTo3d;
				f32 dist = compareVec.getDistanceFrom(targetPosition);
				if((!found) || (minDist > dist))
				{
					minDist = dist;
					outPosition = compareVec;
					outX = iX;
					outY = iY;
				}
				found = true;
			}
		}
		mapOffset += _mapSize2d;
	}

	// Если targetPosition попадает в прохадимую ячейку, то
	if(found && outX == x2d && outY == z2d)
	{
		// Возращаем targetPosition
		outPosition = targetPosition;
	}

	return found;
}


// Может ли объект передвинуться в заданную позицию
// Определение идёт по boundbox
bool PathFinder::CanMove( const vector3df& pos )
{
	const core::list<ISceneNode*>& children = SceneManager->getRootSceneNode()->getChildren();
	core::list<ISceneNode*>::ConstIterator it = children.begin();
	for (; it != children.end(); ++it)
	{
		ISceneNode* current = *it;

		if(current == _currentNode) 
			continue;

		if(current->getType() != ESNT_ANIMATED_MESH && 
			current->getType() != ESNT_MESH &&
			current->getType() != ESNT_EMPTY)
			continue;

		if (current->isVisible())
		{
			if(current->getID() & _obstacleFilterId)
			{
				aabbox3df currentBox = current->getTransformedBoundingBox();
				if(currentBox.isPointInside(pos))
					return false;
			}
		}
	}
	return true;
}
