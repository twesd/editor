#pragma once
#include "ExParticleAffectorBase.h"


class ExParticleAffectorEmmiterParam : public ExParticleAffectorBase
{
public:
	ExParticleAffectorEmmiterParam();
	virtual ~ExParticleAffectorEmmiterParam(void);	

	IParticleAffector* CreateAffector(IParticleSystemSceneNode* ps);

	// Изменение параметров MinParticlesPerSecond и MaxParticlesPerSecond
	int ParticlesPerSecond;

	// Время за которое должно измениться на велечину ParticlesPerSecond
	u32 TimePerSecond;
};
