#pragma once
#include "Core/Base.h"
#include "Core/Animators/IExtendAnimator.h"
#include "Core/Scene/PolylineSceneNode.h"
#include "AroundPointFinder.h"

using namespace scene;

//#define DEBUG_PATH_FINDER

class PathFinder : public Base
{
public:
	PathFinder(
		SharedParams_t params, 
		u32 mapSize2d,
		f32 mapSize3d);
	virtual ~PathFinder(void);

	/*
	Установить модель для которой ищется путь
		@node - модель
	*/
	void SetSourceNode(ISceneNode* node);

	// Установить фильтр для моделий препятствий
	void SetObstacleFilterId(int bitMask);

	// Получение следующего шага
	// @targetPoint - цель
	// @wayDist - растояние которое надо пройти
	// @targetDist - дистанция на которую надо подойти
	//
	vector3df GetNextPosition(vector3df targetPoint, f32 wayDist, f32 targetDist);

	/// <summary>Получение ближайщей свободной позиции к targetPosition</summary>
	/// <param name="targetPosition">Цель</param>
	/// <param name="outPosition">Результат</param>
	bool TryGetFreePosition(vector3df targetPosition, vector3df& outPosition);  

	/// <summary>Может ли объект передвинуться в заданную позицию.Определение идёт по boundbox</summary>
	/// <param name="pos">Позиция</param>
	bool CanMove( const vector3df& pos );

private:
	enum
	{
		// Препятствие
		MapWall = 1,
		// Прямой путь от старта к финишу
		MapLinePath = 2,
		// Точка входа в припятствие
		MapLineInWall = 4,
		// Точка выхода из припятствия
		MapLineOutWall = 8,
		// Путь обхода препятствия
		MapPath = 16

	}MapVals;

	/// <summary>Построение карты местности</summary>
	void BuildMap( ISceneNode* currentNode, s32 size, f32 size3d, vector3df nodeSize );

	// Карта местности
	char* _map;

	// Карта местности
	FinderGrid* _finderGrid;

	// Размер карты (дискретной)
	s32 _mapSize2d;

	// Размер карты
	f32 _mapSize3d;

	// Модель для которой ищется путь
	ISceneNode* _currentNode;

	// Фильтр для моделий препятствий
	int _obstacleFilterId;


#ifdef DEBUG_PATH_FINDER
	PolylineSceneNode* _pathSceneNode;
	core::array<PolylineSceneNode*> _searchLines;
	u32 _searchLinesCounter;
	PolylineSceneNode* _gridSceneNode;
#endif
};
