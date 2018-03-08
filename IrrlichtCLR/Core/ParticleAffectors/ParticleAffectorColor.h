#pragma once
#include "./../Base.h"

using namespace scene;

class ParticleAffectorColor : public IParticleAffector
{
public:
	ParticleAffectorColor(
		const core::array<video::SColor> &colors, 
		core::array<u32> timesForWay, 
		bool loop);
	virtual ~ParticleAffectorColor(void);
	
	virtual E_PARTICLE_AFFECTOR_TYPE getType() const { return EPAT_NONE; }

	//! Affects a particle.
	virtual void affect(u32 now, SParticle* particlearray, u32 count);

private:
	core::array<video::SColor> _colors;

	core::array<u32> _timesForWay;	

	bool _loop;

	bool _isInit;
};
