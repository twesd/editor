#include "ExParticleAffectorScale.h"
#include "../../../Core/ParticleAffectors/ParticleAffectorScale.h"

ExParticleAffectorScale::ExParticleAffectorScale()
{
	Loop = false;
}

ExParticleAffectorScale::~ExParticleAffectorScale()
{
	
}

IParticleAffector* ExParticleAffectorScale::CreateAffector( IParticleSystemSceneNode* ps )
{
	ParticleAffectorScale* affector = new ParticleAffectorScale(
		TargetScales, TimesForWay, Loop);
	return affector;
}
