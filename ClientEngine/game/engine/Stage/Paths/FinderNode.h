#pragma once

#include "../../Core/Base.h"

class FinderNode
{
public:
	FinderNode(int x, int y, bool walkable)
	{
		Reset();
		Position.X = x;
		Position.Y = y;
		IsWalkable = walkable;
	};


	bool Equal(const FinderNode* other) const	
	{
		return (Position == other->Position);
	}

	void Reset()
	{
		IsWalkable = true;
		IsInWall = false;
		IsOutWall = false;
		IsClosed = false;
		IsWay = false;
		Parent = NULL;
	}

	// Если препятствие
	bool IsWalkable;

	// Были ли мы здесь
	bool IsClosed;

	// Узел является частью пути
	bool IsWay;

	// Является ли узел входом в препятствие
	bool IsInWall;

	// Является ли узел выходом из препятствия
	bool IsOutWall;

	vector2di Position;

	FinderNode* Parent;

};

