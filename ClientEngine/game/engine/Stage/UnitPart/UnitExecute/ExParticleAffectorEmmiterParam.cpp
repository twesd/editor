#include "ExParticleAffectorEmmiterParam.h"
#include "../../../Core/ParticleAffectors/ParticleAffectorEmmiterParam.h"

ExParticleAffectorEmmiterParam::ExParticleAffectorEmmiterParam()
{
}

ExParticleAffectorEmmiterParam::~ExParticleAffectorEmmiterParam()
{
	
}

IParticleAffector* ExParticleAffectorEmmiterParam::CreateAffector( IParticleSystemSceneNode* ps )
{
	ParticleAffectorEmmiterParam* affector = new ParticleAffectorEmmiterParam(
		ps, ParticlesPerSecond, TimePerSecond);
	return affector;
}
