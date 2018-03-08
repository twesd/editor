#pragma once

#include "FinderNode.h"

class FinderGrid
{
public:
	FinderGrid(int width, int height);
	~FinderGrid(void);

	FinderNode* getNodeAt(vector2di& pos);

	FinderNode* getNodeAt(int absoluteOffset);

	bool isWalkableAt(const vector2di& pos) const;

	bool isInside(const vector2di& pos) const;

private:
	typedef struct
	{
		core::array<FinderNode*> Nodes;
	}CacheNodes_t;
	
	core::array<FinderNode*> _nodes;

	int _width;

	int _height;
};

