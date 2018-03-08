#pragma once
#include "./../Base.h"

using namespace scene;

class ParticleAffectorEmmiterParam : public IParticleAffector
{
public:
	ParticleAffectorEmmiterParam(
		IParticleSystemSceneNode* ps,
		int particlesPerSecond, 
		u32 timePerSecond);
	virtual ~ParticleAffectorEmmiterParam(void);
	
	virtual E_PARTICLE_AFFECTOR_TYPE getType() const { return EPAT_NONE; }

	//! Affects a particle.
	virtual void affect(u32 now, SParticle* particlearray, u32 count);

private:
	IParticleSystemSceneNode* _ps;
	
	int _particlesPerSecond;

	u32 _timePerSecond;
	
	u32 _lastTime;
};
