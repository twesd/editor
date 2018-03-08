#include "ParticleAffectorAllDirection.h"

ParticleAffectorAllDirection::ParticleAffectorAllDirection(const vector3df& position, f32 force) : 
	IParticleAffector()
{
	_position = position;
	_force = force;
	_lastTime = 0;
}

ParticleAffectorAllDirection::~ParticleAffectorAllDirection(void)
{
}

void ParticleAffectorAllDirection::affect( u32 now, SParticle* particlearray, u32 count )
{
	if (!Enabled)
		return;

	if(_lastTime == 0 || _lastTime > now)
	{
		_lastTime = now;
		return;
	}

	f32 timeDelta = ( now - _lastTime ) / 1000.0f;
	_lastTime = now;

	for (u32 i=0; i<count; i++)
	{
		SParticle* particle = &particlearray[i];
		vector3df pos =  (particle->pos - _position).normalize();
		f32 len = _position.getDistanceFrom(particle->pos);
		//printf("%f\n", len);
		if(len <= 0.1) continue;
		pos = (pos * (_force * timeDelta));
		particle->pos += pos;
	}
}
