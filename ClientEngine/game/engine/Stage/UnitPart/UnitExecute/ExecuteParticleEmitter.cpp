#include "ExecuteParticleEmitter.h"
#include "../../UnitInstance/UnitInstanceStandard.h"

ExecuteParticleEmitter::ExecuteParticleEmitter(SharedParams_t params) : ExecuteBase(params)
{
	_particleEmitter = NULL;
}

ExecuteParticleEmitter::~ExecuteParticleEmitter()
{
	if(_particleEmitter != NULL) 
		delete _particleEmitter;
}

// Выполнить действие
void ExecuteParticleEmitter::Run( scene::ISceneNode* node, core::array<Event_t*>& events )
{
	_DEBUG_BREAK_IF(_particleEmitter == NULL)
	ExecuteBase::Run(node, events);

	if(node->getType() !=  ESNT_PARTICLE_SYSTEM)
	{
		_DEBUG_BREAK_IF(true)
		return;
	}
	IParticleSystemSceneNode* pSystem = static_cast<IParticleSystemSceneNode*>(node);
	IParticleEmitter* emmiter = _particleEmitter->CreateEmitter(pSystem);
	_DEBUG_BREAK_IF(emmiter == NULL)
	pSystem->setEmitter(emmiter);
	emmiter->drop();
}

void ExecuteParticleEmitter::SetParticaleEmitter( ExParticleEmitterBase* emmiter )
{
	_DEBUG_BREAK_IF(emmiter == NULL)
	_particleEmitter = emmiter;
}
