#pragma once
#include "ExecuteBase.h"
#include "ExParticleEmitterBase.h"

class ExecuteParticleEmitter : public ExecuteBase
{
public:
	ExecuteParticleEmitter(SharedParams_t params);
	virtual ~ExecuteParticleEmitter(void);

	// Выполнить действие
	virtual void Run(scene::ISceneNode* node, core::array<Event_t*>& events);
		
	void SetParticaleEmitter(ExParticleEmitterBase* emmiter);
private:
	ExParticleEmitterBase* _particleEmitter;
};
