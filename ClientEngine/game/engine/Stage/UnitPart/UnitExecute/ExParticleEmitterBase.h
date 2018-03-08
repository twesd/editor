#pragma once
#include "../../../Core/Base.h"


class ExParticleEmitterBase
{
public:
	ExParticleEmitterBase();
	virtual ~ExParticleEmitterBase(void);	

	virtual IParticleEmitter* CreateEmitter(IParticleSystemSceneNode* ps) = 0;
};
