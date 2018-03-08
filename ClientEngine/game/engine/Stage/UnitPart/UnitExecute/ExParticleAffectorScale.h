#pragma once
#include "ExParticleAffectorBase.h"


class ExParticleAffectorScale : public ExParticleAffectorBase
{
public:
	ExParticleAffectorScale();
	virtual ~ExParticleAffectorScale(void);	

	IParticleAffector* CreateAffector(IParticleSystemSceneNode* ps);

	core::array<dimension2df> TargetScales;
	
	core::array<u32> TimesForWay;

	bool Loop;
};
