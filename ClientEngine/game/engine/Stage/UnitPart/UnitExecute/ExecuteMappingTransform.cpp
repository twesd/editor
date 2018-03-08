#include "ExecuteMappingTransform.h"
#include "../../UnitInstance/UnitInstanceStandard.h"
#include "../UnitMapping/UnitMappingTransform.h"


ExecuteMappingTransform::ExecuteMappingTransform(SharedParams_t params) : 
	ExecuteBase(params), BehaviorChildPath(),
	_root(), UseThisBehavior(true),
	ScaleX(), ScaleY(), ScaleZ(),
	PositionX(), PositionY(), PositionZ(),
	RotationX(), RotationY(), RotationZ()
{
}

ExecuteMappingTransform::~ExecuteMappingTransform()
{

}

// Выполнить действие
void ExecuteMappingTransform::Run(scene::ISceneNode* node, core::array<Event_t*>& events)
{
	ExecuteBase::Run(node, events);

	_DEBUG_BREAK_IF(UnitInstance == NULL)
	
	UnitInstanceStandard* unitInstance;
	if (UseThisBehavior)
	{
		unitInstance = UnitInstance;
	}
	else
	{
		unitInstance = UnitInstance->GetChildByPath(_root + BehaviorChildPath);
	}
	if (unitInstance == NULL) return;

	UnitMappingTransform* mapping = new UnitMappingTransform(SharedParams);
	mapping->SetUnitInstance(unitInstance);
	if (!UseThisBehavior)
	{
		mapping->SetExternalParameters(UnitInstance->GetBehavior()->GetParameters());
	}

	mapping->ScaleX = ScaleX;
	mapping->ScaleY = ScaleY;
	mapping->ScaleZ = ScaleZ;

	mapping->PositionX = PositionX;
	mapping->PositionY = PositionY;
	mapping->PositionZ = PositionZ;

	mapping->RotationX = RotationX;
	mapping->RotationY = RotationY;
	mapping->RotationZ = RotationZ;
		
	unitInstance->AddMapping(mapping);
	mapping->drop();
}

// Установить основную директорию
void ExecuteMappingTransform::SetRoot( stringc root )
{
	_root = root;
}
