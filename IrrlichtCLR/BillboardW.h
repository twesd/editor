#pragma once

#include "BaseW.h"
#include "SceneNodeW.h"
#include "Core/Scene/Billboard.h"

using namespace System::Collections::Generic;

public ref class BillboardW : SceneNodeW
{
public:
	BillboardW(ISceneNode* node, SharedParams_t* shareParams);	

	void SetDimension(f32 width, f32 height);

	void SetUseUpVector(bool useIt);

	void SetUpVector(Vertex3dW^ up);

	void SetUseViewVector(bool useIt);

	void SetViewVector(Vertex3dW^ vec);

private:
	Billboard* _billboard;
};
