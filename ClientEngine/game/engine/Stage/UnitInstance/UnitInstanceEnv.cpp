#include "UnitInstanceEnv.h"
#include "../UnitGenInstance/UnitGenInstanceEnv.h"
#include "Core/NodeWorker.h"

UnitInstanceEnv::UnitInstanceEnv(
	SharedParams_t params, 
	UnitGenInstanceEnv* genInstance, 
	UnitInstanceBase* parent,
	UnitInstanceBase* creator) : 
		UnitInstanceBase(params, genInstance, parent, creator)
{
	Name = genInstance->UnitName;

	SceneNode = NodeWorker::AddNode(SceneManager, Driver, genInstance->ModelPath, false, false, false);
	genInstance->ApplyStartParameters(SceneNode);
#ifdef DEBUG_VISUAL_DATA
	SceneNode->setName(genInstance->ModelPath);
#endif
	SceneNode->setID(genInstance->NodeId);
	SceneNode->updateAbsolutePosition();
}

UnitInstanceEnv::~UnitInstanceEnv(void)
{
}

// Обновить данные
void UnitInstanceEnv::Update()
{
	
}

// Обработка событий
void UnitInstanceEnv::HandleEvent(core::array<Event_t*>& events)
{

}
