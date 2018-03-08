#include "ExecuteMaterial.h"
#include "../../UnitInstance/UnitInstanceStandard.h"

ExecuteMaterial::ExecuteMaterial(SharedParams_t params) : ExecuteBase(params),
	MaterialType(video::EMT_SOLID)
{
}

ExecuteMaterial::~ExecuteMaterial()
{
}

// Выполнить действие
void ExecuteMaterial::Run( scene::ISceneNode* node, core::array<Event_t*>& events )
{
	ExecuteBase::Run(node, events);

	node->setMaterialType(MaterialType);
}
