#pragma once
#include "./../Base.h"

using namespace scene;

class ParticleAffectorAllDirection : public IParticleAffector
{
public:
	ParticleAffectorAllDirection(const vector3df& position, f32 force);
	virtual ~ParticleAffectorAllDirection(void);
	
	virtual E_PARTICLE_AFFECTOR_TYPE getType() const { return EPAT_NONE; }

	//! Affects a particle.
	virtual void affect(u32 now, SParticle* particlearray, u32 count);

private:
	vector3df _position;
	f32 _force;
	u32 _lastTime;
};
