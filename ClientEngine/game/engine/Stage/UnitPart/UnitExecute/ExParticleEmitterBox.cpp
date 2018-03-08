#include "ExParticleEmitterBox.h"
#include "Core/ParticleEmmitors/ParticleBoxEmitter.h"

ExParticleEmitterBox::ExParticleEmitterBox()
{
	UseParentRotation = false;
}

ExParticleEmitterBox::~ExParticleEmitterBox()
{
	
}

IParticleEmitter* ExParticleEmitterBox::CreateEmitter(IParticleSystemSceneNode* ps)
{
	return new ParticleBoxEmitter(
		Box, // emitter size
		Direction,   // initial direction
		MinParticlesPerSecond,MaxParticlesPerSecond,// emit rate
		MinStartColor,       // darkest color
		MaxStartColor,       // brightest color
		LifeTimeMin,LifeTimeMax,MaxAngleDegrees, // min and max age, angle
		MinStartSize,
		MaxStartSize,
		ps,
		UseParentRotation); 
}
