#pragma once
#include "ExParticleAffectorBase.h"


class ExParticleAffectorRotation : public ExParticleAffectorBase
{
public:
	ExParticleAffectorRotation();
	virtual ~ExParticleAffectorRotation(void);	

	IParticleAffector* CreateAffector(IParticleSystemSceneNode* ps);

	vector3df Position;

	vector3df Speed;
	
};
