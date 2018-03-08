#pragma once
#include "../../../Core/Base.h"


class ExParticleAffectorBase
{
public:
	ExParticleAffectorBase();
	virtual ~ExParticleAffectorBase(void);	

	virtual IParticleAffector* CreateAffector(IParticleSystemSceneNode* ps) = 0;
};
