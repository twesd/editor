#pragma once
#include "ExParticleAffectorBase.h"


class ExParticleAffectorAllDirection : public ExParticleAffectorBase
{
public:
	ExParticleAffectorAllDirection();
	virtual ~ExParticleAffectorAllDirection(void);	

	IParticleAffector* CreateAffector(IParticleSystemSceneNode* ps);

	vector3df Position;

	float Speed;
	
};
