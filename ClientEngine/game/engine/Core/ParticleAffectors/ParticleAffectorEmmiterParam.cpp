#include "ParticleAffectorEmmiterParam.h"

ParticleAffectorEmmiterParam::ParticleAffectorEmmiterParam(
	IParticleSystemSceneNode* ps,
	int particlesPerSecond, 
	u32 timePerSecond) : IParticleAffector()
{
	_ps = ps;
	_particlesPerSecond = particlesPerSecond;
	_timePerSecond = timePerSecond;
	_lastTime = 0;
}

ParticleAffectorEmmiterParam::~ParticleAffectorEmmiterParam(void)
{
}

void ParticleAffectorEmmiterParam::affect( u32 now, SParticle* particlearray, u32 count )
{
	if (!Enabled)
		return;
	if(_particlesPerSecond == 0)
		return;

	if(_lastTime == 0 || _lastTime > now)
	{
		_lastTime = now;
		return;
	}

	f32 timeDelta = (f32)( now - _lastTime ) / _timePerSecond;
	int diff = (int)(timeDelta * _particlesPerSecond);
	if(diff == 0)
	{
		// Не сохраняем последние время, чтоб увеличить промежуток
		return;
	}

	_lastTime = now;

	 IParticleEmitter* emmiter = _ps->getEmitter();
	 if(emmiter == NULL) return;
	 s32 maxPerSec = (s32)emmiter->getMaxParticlesPerSecond();
	 s32 minPerSec = (s32)emmiter->getMinParticlesPerSecond();
	 maxPerSec += diff;
	 minPerSec += diff;
	 if(maxPerSec < 0) maxPerSec = 0;
	 if(minPerSec < 0) minPerSec = 0;

	 emmiter->setMaxParticlesPerSecond(maxPerSec);
	 emmiter->setMinParticlesPerSecond(minPerSec);
}
