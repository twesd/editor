#pragma once
#include "./../Base.h"

using namespace scene;

class ParticleAffectorScale : public IParticleAffector
{
public:
	ParticleAffectorScale(
		const core::array<dimension2df> &dimensions, 
		core::array<u32> timesForWay, 
		bool loop);
	virtual ~ParticleAffectorScale(void);
	
	virtual E_PARTICLE_AFFECTOR_TYPE getType() const { return EPAT_NONE; }

	//! Affects a particle.
	virtual void affect(u32 now, SParticle* particlearray, u32 count);

private:
	void CalulateValues(u32 number, u32 nowTime);

	core::array<dimension2df> _dimensions;

	core::array<u32> _timesForWay;	

	bool _loop;

	bool _isInit;

};
