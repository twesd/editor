#pragma once
#include "ExParticleAffectorBase.h"


class ExParticleAffectorFadeOut : public ExParticleAffectorBase
{
public:
	ExParticleAffectorFadeOut();
	virtual ~ExParticleAffectorFadeOut(void);	

	IParticleAffector* CreateAffector(IParticleSystemSceneNode* ps);

	core::array<video::SColor> Colors;

	core::array<u32> TimesForWay;

	bool Loop;
};
