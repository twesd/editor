#include "AroundPointFinder.h"


AroundPointFinder::AroundPointFinder(void)
{
}


AroundPointFinder::~AroundPointFinder(void)
{
}

core::array<FinderNode*> AroundPointFinder::FindPath(vector2di startPos, vector2di endPos, FinderGrid& grid)
{
	core::array<FinderNode*> pathList;
	core::array<FinderNode*> empty;
		
	FinderNode* startNode = grid.getNodeAt(startPos);
	FinderNode* endNode = grid.getNodeAt(endPos);

	if (!startNode->IsWalkable)
	{
		return empty;
	}

	// Запоминаем начальную точку
	pathList.push_back(startNode);


	// Помечаем прямой путь от старта до финиша
	// Провести условный отрезок из старта в финиш. Этот отрезок может быть самым коротким путем
	// Необходимо запомнить все точки входов в препятствия и выходов из них. 
	// Если конечная точка в препятствие, то возращаем ближайщею точку, не в препятствие
	bool hasObstacle = false;
	endNode = TraceToEnd(startNode, endNode, grid, hasObstacle);
	if (!hasObstacle)
	{
		// На пути нет препятствий
		pathList.push_back(endNode);
		return pathList;
	}

	if (startNode->Equal(endNode))
	{
		return empty;
	}
	
	FinderNode* curNode = startNode;
	FinderNode* lastWalkableNode = startNode;
	while(true)
	{
		// Помечаем, что узел был посещён
		curNode->IsClosed = true;

		if (curNode->Equal(endNode))
		{
			pathList.push_back(endNode);
			return pathList;
		}

		vector2di direction = (endNode->Position - curNode->Position);
		direction.X = (direction.X > 0) ? 1 : (direction.X < 0) ? -1 : 0;
		direction.Y = (direction.Y > 0) ? 1 : (direction.Y < 0) ? -1 : 0;
		vector2di nodeIdx = curNode->Position + direction;
		curNode = grid.getNodeAt(nodeIdx);
		if (curNode == NULL)
		{
			break;
		}

		if(!curNode->IsWalkable)
		{
			curNode = FindAround(lastWalkableNode, curNode, grid, pathList);
			if (curNode == NULL)
			{
				break;
			}
		}	
		else
		{
			lastWalkableNode = curNode;
		}

		if (pathList.size() > 500)
		{
			return pathList;
		}
	}

	return empty;
}

// Помечаем прямой путь от старта до финиша
// Провести условный отрезок из старта в финиш. Этот отрезок может быть самым коротким путем
// Необходимо запомнить все точки входов в препятствия и выходов из них. 
// Если конечная точка в препятствие, то возращаем ближайщею точку, не в препятствие
FinderNode* AroundPointFinder::TraceToEnd( FinderNode* startNode, FinderNode* endNode, FinderGrid &grid, bool& hasObstacle )
{
	// Есть ли препятсвия на пути
	hasObstacle = false;
	// Флаг трасировки внутри препятствия
	bool inObstacle = false; 

	FinderNode* curNode = startNode;
	FinderNode* lastWalkableNode = startNode;
	while(true)
	{
		curNode->IsWay = true;

		if (curNode->Equal(endNode))
		{
			break;
		}

		vector2di direction = (endNode->Position - curNode->Position);
		direction.X = (direction.X > 0) ? 1 : (direction.X < 0) ? -1 : 0;
		direction.Y = (direction.Y > 0) ? 1 : (direction.Y < 0) ? -1 : 0;
		vector2di nodeIdx = curNode->Position + direction;
		curNode = grid.getNodeAt(nodeIdx);
		if (curNode == NULL)
		{
			break;
		}

		if(!curNode->IsWalkable)
		{
			hasObstacle = true;
			if(!inObstacle)
			{
				// Запоминаем точку входа в препятствие
				//
				curNode->IsInWall = true;
				inObstacle = true;
			}
		}
		else
		{
			if(inObstacle)
			{
				// Запоминаем выхода из препятствия
				//
				curNode->IsOutWall = true;
				inObstacle = false;
			}
			lastWalkableNode = curNode;
		}
	}

	return lastWalkableNode;
}

// Найти обход препятствия
FinderNode* AroundPointFinder::FindAround(FinderNode* curNode, FinderNode* wallNode, FinderGrid &grid, core::array<FinderNode*>& pathNodes)
{

	// Выбираем стартовые точки пути и стены так, чтобы они не лежали на диагонали
	//
	int wallX = wallNode->Position.X;
	int wallY = wallNode->Position.Y;
	if(!grid.isWalkableAt(vector2di(curNode->Position.X, wallNode->Position.Y)))
	{	
		wallX = curNode->Position.X;
	}
	else if(!grid.isWalkableAt(vector2di(wallNode->Position.X, curNode->Position.Y)))
	{
		wallY = curNode->Position.Y;
	}
	vector2di nodeIdx = vector2di(wallX, wallY);
	wallNode = grid.getNodeAt(nodeIdx);
	if (wallNode == NULL)
	{
		return NULL;
	}

	AroundItem_t leftItem;
	leftItem.CurNode = curNode;
	leftItem.WallNode = wallNode;
	leftItem.IsDone = false;
	leftItem.IsValid = true;
	leftItem.IsLeft = true;

	AroundItem_t rightItem;
	rightItem.CurNode = curNode;
	rightItem.WallNode = wallNode;
	rightItem.IsDone = false;
	rightItem.IsValid = true;
	rightItem.IsLeft = false;

	int stepCounter = 0;
	while(true)
	{
		stepCounter++;
		if(stepCounter > 256)
		{
			// Путь не найден
			return NULL;
		}

		DoAroundStep(leftItem, grid);
		if (leftItem.IsDone)
		{
			for (u32 i = 0; i < leftItem.Path.size(); i++)
			{
				pathNodes.push_back(leftItem.Path[i]);
			}
			return leftItem.CurNode;
		}

		DoAroundStep(rightItem, grid);	
		if (rightItem.IsDone)
		{
			for (u32 i = 0; i < rightItem.Path.size(); i++)
			{
				pathNodes.push_back(rightItem.Path[i]);
			}
			return rightItem.CurNode;
		}

		if (!leftItem.IsValid && !rightItem.IsValid)
		{
			return NULL;
		}
	}

	return NULL;
}

void AroundPointFinder::DoAroundStep( AroundItem_t& item, FinderGrid& grid )
{
	if (item.IsDone || !item.IsValid)
	{
		return;
	}

	// цифрой «1» обозначены клетки, стоящие справа от клетки стены, 
	// а цифрой «2» – клетки, стоящие справа от клетки пути.
	vector2di v1, v2;

	if (item.IsLeft)
	{
		// обход слева

		v1.X = item.CurNode->Position.X - (item.WallNode->Position.Y - item.CurNode->Position.Y);
		v1.Y = item.CurNode->Position.Y - (item.CurNode->Position.X - item.WallNode->Position.X);

		v2.X = item.WallNode->Position.X - (item.WallNode->Position.Y - item.CurNode->Position.Y);
		v2.Y = item.WallNode->Position.Y - (item.CurNode->Position.X - item.WallNode->Position.X);
	}	
	else
	{
		v1.X = item.CurNode->Position.X + item.WallNode->Position.Y - item.CurNode->Position.Y;
		v1.Y = item.CurNode->Position.Y + item.CurNode->Position.X - item.WallNode->Position.X;

		v2.X = item.WallNode->Position.X + item.WallNode->Position.Y - item.CurNode->Position.Y;
		v2.Y = item.WallNode->Position.Y + item.CurNode->Position.X - item.WallNode->Position.X;
	}

	FinderNode* node1 = grid.getNodeAt(v1);
	if (node1 == NULL)
	{
		item.IsValid = false;
		return;
	}
		
	if (!node1->IsWalkable)
	{
		item.WallNode = node1;
	}
	else
	{
		item.CurNode = node1;
		item.Path.push_back(item.CurNode);

		// Данный узел уже посящался
		if (item.CurNode->IsClosed)
		{
			item.IsValid = false;
			return;
		}

		// То, что текущая точка уже является путем и она не является стартовой
		// означает, что мы обошли препятствие
		if (item.CurNode->IsWay)
		{
			item.IsDone = true;
			return;
		}

		FinderNode* node2 = grid.getNodeAt(v2);
		if (node2 == NULL)
		{
			item.IsValid = false;
			return;
		}
			
		if (!node2->IsWalkable)
		{
			item.WallNode = node2;
		}
		else
		{
			item.CurNode = node2;
			item.Path.push_back(item.CurNode);

			// Данный узел уже посящался
			if (item.CurNode->IsClosed)
			{
				item.IsValid = false;
				return;
			}

			// То, что текущая точка уже является путем и она не является стартовой
			// означает, что мы обошли препятствие
			if (item.CurNode->IsWay)
			{
				item.IsDone = true;
				return;
			}
		}
	}

}
