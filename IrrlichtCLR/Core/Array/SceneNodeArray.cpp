#include "SceneNodeArray.h"

// Отсортировить по дистанции от базовой точки
void SceneNodeArray::SortByDistance( core::array<ISceneNode*>& arr, const vector3df& basePoint )
{
	// for heapsink we pretent this is not c++, where
	// arrays start with index 0. So we decrease the array pointer,
	// the maximum always +2 and the element always +1

	u32 size = arr.size();
	ISceneNode** array_ = arr.pointer();

	if(size < 2) return;

	ISceneNode** virtualArray = array_ - 1;
	s32 virtualSize =  + 2;
	s32 i;

	// build heap

	for (i=((size-1)/2); i>=0; --i)
		HeapsinkByDist(virtualArray, i+1, virtualSize-1, basePoint);

	// sort array

	for (i=size-1; i>=0; --i)
	{
		ISceneNode* t = array_[0];
		array_[0] = array_[i];
		array_[i] = t;
		HeapsinkByDist(virtualArray, 1, i + 1, basePoint);
	}
}

void SceneNodeArray::HeapsinkByDist(ISceneNode** arr, s32 element, s32 max, const vector3df& basePoint)
{
	while ((element<<1) < max) // there is a left child
	{
		s32 j = (element<<1);

		f32 jDist = arr[j]->getPosition().getDistanceFromSQ(basePoint);
		f32 j1Dist = arr[j+1]->getPosition().getDistanceFromSQ(basePoint);

		if (j+1 < max && jDist < j1Dist)
			j = j+1; // take right child

		if (arr[element] < arr[j])
		{
			ISceneNode* t = arr[j]; // swap elements
			arr[j] = arr[element];
			arr[element] = t;
			element = j;
		}
		else
			return;
	}
}

void SceneNodeArray::Merge( core::array<ISceneNode*>& arr1, core::array<ISceneNode*>& arr2 )
{
	u32 newSize = arr1.size() + arr2.size();
	u32 startIndex = arr1.size();
	arr1.reallocate(newSize);
	arr1.set_used(newSize);
	for (u32 i = startIndex; i < newSize ; i++)
	{
		arr1[i] = arr2[i];
	}
}
