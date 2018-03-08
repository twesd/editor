#include "UnitInstanceBillboard.h"
#include "Core/NodeWorker.h"
#include "Core/Scene/Billboard.h"
#include "Core/TextureWorker.h"

UnitInstanceBillboard::UnitInstanceBillboard(
	SharedParams_t params, 
	UnitGenInstanceBillboard* genInstance, 
	UnitInstanceBase* parent,
	UnitInstanceBase* creator) : 
		UnitInstanceBase(params, genInstance, parent, creator)
{
	Name = genInstance->UnitName;

	Billboard* billboard = new Billboard(SharedParams); 
	billboard->drop();//RefCounter to 1;
	billboard->SetDimension(core::dimension2df(genInstance->Width, genInstance->Height));
	NodeWorker::ApplyMeshSetting(billboard, false, false);
	if(genInstance->TexturePath != "")
	{
		video::ITexture* texture = TextureWorker::GetTexture(
			Driver, genInstance->TexturePath, genInstance->Use32Bit);
		billboard->setMaterialTexture(0, texture);
	}	
	billboard->setMaterialType(genInstance->MaterialType);
	billboard->SetUseUpVector(genInstance->UseUpVector);
	billboard->SetUpVector(genInstance->UpVector);
	billboard->SetUseViewVector(genInstance->UseViewVector);
	billboard->SetViewVector(genInstance->ViewVector);

	SceneNode = billboard;
	SceneNode->setID(genInstance->NodeId);
	genInstance->ApplyStartParameters(SceneNode);
}

UnitInstanceBillboard::~UnitInstanceBillboard(void)
{
}

// Обновить данные
void UnitInstanceBillboard::Update()
{
	
}

// Обработка событий
void UnitInstanceBillboard::HandleEvent(core::array<Event_t*>& events)
{

}
