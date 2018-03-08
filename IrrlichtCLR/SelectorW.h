#pragma once
#include "Vertex3dW.h"
#include "SceneNodeW.h"
#include "BaseW.h"

using namespace System::Collections::Generic;

public ref class SelectorW : BaseW
{
public:
	SelectorW(SharedParams_t* sharedParams);

	SceneNodeW^ SelectNode(int x, int y);

	void SelectNode(SceneNodeW^ node);

	void ClearSelection();

	SceneNodeW^ GetNodeByScreenCoords(int x, int y, int filter);

	property List<SceneNodeW^>^ SelectionResult
	{
		List<SceneNodeW^>^ get()
		{
			return _selectionResult;
		}
	}
private:
	List<SceneNodeW^>^ _selectionResult;

	ISceneCollisionManager* _collisionManager;
};

