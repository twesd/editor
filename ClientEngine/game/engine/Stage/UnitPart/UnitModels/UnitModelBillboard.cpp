#include "UnitModelBillboard.h"
#include "Core/Scene/Billboard.h"
#include "Core/NodeWorker.h"
#include "Core/TextureWorker.h"

UnitModelBillboard::UnitModelBillboard( SharedParams_t params ) : UnitModelBase(params),
	TexturePath()
{
	Use32Bit = false;
	Width = 0;
	Height = 0;
	MaterialType = video::EMT_SOLID;
	UpVector = vector3df(0, 1, 0);
	UseUpVector = true;
	UseViewVector = false;
}

ISceneNode* UnitModelBillboard::LoadSceneNode()
{
	//ISceneNode* billboard = SceneManager->addBillboardSceneNode(0, dimension2df(Width, Height));
	Billboard* billboard = new Billboard(SharedParams); 
	billboard->drop();//RefCounter to 1;
	billboard->SetDimension(core::dimension2df(Width, Height));
	NodeWorker::ApplyMeshSetting(billboard, false, false);
	if(TexturePath != "")
	{
		video::ITexture* texture = TextureWorker::GetTexture(Driver, TexturePath, Use32Bit);
		billboard->setMaterialTexture(0, texture);
	}	
	billboard->setMaterialType(MaterialType);
	billboard->SetUseUpVector(UseUpVector);
	billboard->SetUpVector(UpVector);
	billboard->SetUseViewVector(UseViewVector);
	billboard->SetViewVector(ViewVector);
	return billboard;
}
