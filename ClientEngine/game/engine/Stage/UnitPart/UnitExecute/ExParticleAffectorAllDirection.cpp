#include "ExParticleAffectorAllDirection.h"
#include "../../../Core/ParticleAffectors/ParticleAffectorAllDirection.h"

ExParticleAffectorAllDirection::ExParticleAffectorAllDirection()
{
}

ExParticleAffectorAllDirection::~ExParticleAffectorAllDirection()
{
	
}

IParticleAffector* ExParticleAffectorAllDirection::CreateAffector( IParticleSystemSceneNode* ps )
{
	vector3df pos = ps->getAbsolutePosition() + Position;
	ParticleAffectorAllDirection* affector = new ParticleAffectorAllDirection(
		pos, Speed);
	return affector;
}
