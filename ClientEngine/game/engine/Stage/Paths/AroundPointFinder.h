#pragma once

#include "FinderGrid.h"

class AroundPointFinder
{
public:
	AroundPointFinder(void);
	~AroundPointFinder(void);

	core::array<FinderNode*> FindPath(vector2di startPos, vector2di endPos, FinderGrid& grid);

private:
	typedef struct
	{
		FinderNode* CurNode;
		FinderNode* WallNode;
		// Обход препятствия с левой стороны
		bool IsLeft;
		/// <summary>Поиск закончен неудачно</summary>
		bool IsValid;
		/// <summary>Поиск закончен успешно</summary>
		bool IsDone;
		// Путь обхода
		core::array<FinderNode*> Path;
	}AroundItem_t;

	FinderNode* TraceToEnd( FinderNode* startNode, FinderNode* endNode, FinderGrid &grid, bool& hasObstacle );

	FinderNode* FindAround(FinderNode* curNode, FinderNode* wallNode, FinderGrid &grid, core::array<FinderNode*>& pathNodes);

	void DoAroundStep(AroundItem_t& item, FinderGrid& grid);
};

