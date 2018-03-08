#pragma once
#include "ExParticleEmitterBase.h"

class ExParticleEmitterBox : public ExParticleEmitterBase
{
public:
	ExParticleEmitterBox();
	virtual ~ExParticleEmitterBox(void);	

	IParticleEmitter* CreateEmitter(IParticleSystemSceneNode* ps);

	aabbox3df Box;

	vector3df Direction;

	u32 MinParticlesPerSecond;

	u32 MaxParticlesPerSecond;

	video::SColor MinStartColor;

	video::SColor MaxStartColor;

	u32 LifeTimeMin;

	u32 LifeTimeMax;

	s32 MaxAngleDegrees;

	dimension2df MinStartSize;

	dimension2df MaxStartSize;

	bool UseParentRotation;
	
};
