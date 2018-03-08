#include "ExParticleAffectorRotation.h"
#include "../../../Core/ParticleAffectors/ParticleAffectorAllDirection.h"

ExParticleAffectorRotation::ExParticleAffectorRotation()
{
}

ExParticleAffectorRotation::~ExParticleAffectorRotation()
{
	
}

IParticleAffector* ExParticleAffectorRotation::CreateAffector( IParticleSystemSceneNode* ps )
{
	vector3df pos = ps->getAbsolutePosition() + Position;
	return ps->createRotationAffector(Speed, pos);
}
