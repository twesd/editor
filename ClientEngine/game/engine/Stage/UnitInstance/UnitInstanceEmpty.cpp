#include "UnitInstanceEmpty.h"
#include "../Loaders/LoaderBehaviors.h"
#include "../../Controls/ControlEvent.h"
#include "../UnitManager.h"
#ifdef DEBUG_VISUAL_DATA
#include "../Debug/DebugNode.h"
#include "../Debug/DebugSettings.h"
#endif

UnitInstanceEmpty::UnitInstanceEmpty(
	SharedParams_t params, 
	UnitGenInstanceEmpty *genInstance, 
	UnitInstanceBase* parent, 
	UnitManager* unitManager,
	UnitInstanceBase* creator) :  
		UnitInstanceBase(params, genInstance, parent, creator)
{
	SceneNode = SceneManager->addEmptySceneNode();
	SceneNode->setPosition(genInstance->StartPosition);
	SceneNode->setRotation(genInstance->StartRotation);
	SceneNode->setID(genInstance->NodeId);
	SceneNode->setName(Name);

	aabbox3df box = SceneNode->getTransformedBoundingBox();
	SceneNode->setScale(genInstance->Scale);
	SceneNode->updateAbsolutePosition();
	aabbox3df box2 = SceneNode->getTransformedBoundingBox();
}

UnitInstanceEmpty::~UnitInstanceEmpty(void)
{
	
}

// Обновить данные
void UnitInstanceEmpty::Update()
{
	
}

// Обработка событий
void UnitInstanceEmpty::HandleEvent( core::array<Event_t*>& events )
{
	
}
