#pragma once
#include "Base.h"
#include "CompareType.h"

class NodeWorker
{
public:

	// Существует ли заданная модель
	static bool GetIsNodeExist(ISceneNode* searchNode, ISceneManager* sceneManager);

	// Получить дистанцию между моделями
	static f32 GetDistance(ISceneNode* node1, ISceneNode* node2, bool useNodeSize);

	// Получить дистанцию до модели
	static f32 GetDistanceToNode( vector3df position1, ISceneNode* node2, bool useNodeSize );
	
	// Получить абсолютный поворот
	static vector3df GetAbsoluteRotation(ISceneNode* node);

	// Получить поворот родителей
	static vector3df GetParentRotation( ISceneNode* node );

	// Установить абсолютный поворот
	static void SetAbsoluteRotation( ISceneNode* node, core::vector3df rot );
	
	// Произвести выборку
	static core::array<scene::ISceneNode*> SelectNodes( 
		ISceneNode* rootNode,
		s32 filterId, 
		CompareTypeEnum compareType, 
		f32 distance,
		bool useUnitSize, 
		ISceneNode* mainNode,
		bool selectChilds);

	static ISceneNode* GetClosedNode( 
		ISceneManager* sceneManager, 
		s32 filterId, 
		CompareTypeEnum compareType,
		f32 distance,
		bool useUnitSize, 
		ISceneNode* mainNode );

	static void ApplyMeshSetting(scene::IAnimatedMeshSceneNode* mesh, bool culling = false, bool lighting = false);

	static void ApplyMeshSetting(scene::ISceneNode* mesh, bool culling = false, bool lighting = false);

	static void ApplyMaterialSetting(video::SMaterial * material, bool lighting = false);

	static scene::IAnimatedMeshSceneNode* AddNode(
		ISceneManager* sceneManager,
		video::IVideoDriver* driver, 
		stringc path,bool culling = false, 
		bool lighting = false, bool use32bit = false, bool useHardwareBuffer = false);

	//remove node and all childs, from cache too.
	static void RemoveNode(scene::IAnimatedMeshSceneNode* node);

	static void RemoveNode(scene::ISceneNode* node);

private:
	static bool GetIsNodeExistInChilds( ISceneNode* &node, ISceneNode* searchNode );
};
