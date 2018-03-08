#include "ExecuteParticleAffector.h"
#include "../../UnitInstance/UnitInstanceStandard.h"

ExecuteParticleAffector::ExecuteParticleAffector(SharedParams_t params) : ExecuteBase(params)
{
	_affector = NULL;
}

ExecuteParticleAffector::~ExecuteParticleAffector()
{
	if(_affector != NULL)
		delete _affector;
}

// Выполнить действие
void ExecuteParticleAffector::Run( scene::ISceneNode* node, core::array<Event_t*>& events )
{
	ExecuteBase::Run(node, events);
	_DEBUG_BREAK_IF(_affector == NULL)

	if(node->getType() !=  ESNT_PARTICLE_SYSTEM)
	{
		_DEBUG_BREAK_IF(true)
		return;
	}
	IParticleSystemSceneNode* pSystem = static_cast<IParticleSystemSceneNode*>(node);
	IParticleAffector* affector = _affector->CreateAffector(pSystem);
	_DEBUG_BREAK_IF(affector == NULL)
	pSystem->addAffector(affector);
	affector->drop();
}

void ExecuteParticleAffector::SetAffector( ExParticleAffectorBase* affector )
{
	_DEBUG_BREAK_IF(affector == NULL)
	_affector = affector;
}
