#pragma once
#include "ExecuteBase.h"
#include "ExParticleAffectorBase.h"

class ExecuteParticleAffector : public ExecuteBase
{
public:
	ExecuteParticleAffector(SharedParams_t params);
	virtual ~ExecuteParticleAffector(void);

	// Выполнить действие
	virtual void Run(scene::ISceneNode* node, core::array<Event_t*>& events);

	void SetAffector(ExParticleAffectorBase* affector);
private:
	ExParticleAffectorBase* _affector;
};
