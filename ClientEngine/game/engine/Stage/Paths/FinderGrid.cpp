#include "FinderGrid.h"


FinderGrid::FinderGrid(int width, int height)
{
	static core::array<CacheNodes_t> _cache;

	_width = width;
	_height = height;
	
	int size = (width*height);

	for (u32 i = 0; i < _cache.size(); i++)
	{
		if(_cache[i].Nodes.size() == size)
		{
			_nodes = _cache[i].Nodes;
			return;
		}
	}

	_nodes.reallocate(size);
	for(int y = 0; y < height; y++)
	{
		for(int x = 0; x < width; x++)
		{
			_nodes.push_back(new FinderNode(x, y, true));
		}
	}
	CacheNodes_t cacheItem;
	cacheItem.Nodes = _nodes;
	_cache.push_back(cacheItem);
}


FinderGrid::~FinderGrid(void)
{
	/*for (u32 i = 0; i < _nodes.size(); i++)
	{
		delete _nodes[i];
	}
	_nodes.clear();*/
}

FinderNode* FinderGrid::getNodeAt(int absoluteOffset)
{
	if(absoluteOffset >= (s32)_nodes.size())
	{
		return NULL;
	}
	return _nodes[absoluteOffset];
}

FinderNode* FinderGrid::getNodeAt(vector2di& pos)
{
	if(!isInside(pos))
	{
		return NULL;
	}
	return _nodes[(pos.Y*_width) + pos.X];
}

bool FinderGrid::isWalkableAt(const vector2di& pos) const
{
	if(!isInside(pos))
	{
		return false;
	}

	return _nodes[(pos.Y*_width) + pos.X]->IsWalkable;
}

bool FinderGrid::isInside(const vector2di& pos) const
{
    return (pos.X >= 0 && pos.X < _width) && (pos.Y >= 0 && pos.Y < _height);
}
