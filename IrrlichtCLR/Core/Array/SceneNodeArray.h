#pragma once
#include "./../Base.h"

using namespace scene;

class SceneNodeArray
{
public:
	// Отсортировить по дистанции от базовой точки
	static void SortByDistance(core::array<ISceneNode*>& arr, const vector3df& basePoint);
	// Слить два массива в один
	static void Merge(core::array<ISceneNode*>& arr1, core::array<ISceneNode*>& arr2);
private:
	static void HeapsinkByDist(ISceneNode** arr, s32 element, s32 max, const vector3df& basePoint);
};
