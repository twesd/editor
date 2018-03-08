#include "ExParticleAffectorFadeOut.h"
#include "../../../Core/ParticleAffectors/ParticleAffectorColor.h"

ExParticleAffectorFadeOut::ExParticleAffectorFadeOut()
{
	Loop = false;
}

ExParticleAffectorFadeOut::~ExParticleAffectorFadeOut()
{
	
}

IParticleAffector* ExParticleAffectorFadeOut::CreateAffector( IParticleSystemSceneNode* ps )
{
	ParticleAffectorColor* affector = new ParticleAffectorColor(
		Colors, TimesForWay, Loop);
	return affector;
}
