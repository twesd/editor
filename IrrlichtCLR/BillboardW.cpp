#include "StdAfx.h"
#include "BillboardW.h"
#include "Convertor.h"

BillboardW::BillboardW(ISceneNode* node, SharedParams_t* shareParams):
SceneNodeW(node, shareParams)
{
	_billboard = dynamic_cast<Billboard*>(node);
}

void BillboardW::SetDimension(f32 width, f32 height)
{
	_billboard->SetDimension(dimension2df(width, height));
}

void BillboardW::SetUseUpVector(bool useIt)
{
	_billboard->SetUseUpVector(useIt);
}

void BillboardW::SetUpVector(Vertex3dW^ up)
{
	vector3df v = up->GetVector();
	_billboard->SetUpVector(v);
}

void BillboardW::SetUseViewVector(bool useIt)
{
	_billboard->SetUseViewVector(useIt);
}

void BillboardW::SetViewVector(Vertex3dW^ vec)
{
	vector3df v = vec->GetVector();
	_billboard->SetViewVector(v);
}
