#pragma once
#include "Core/Base.h"

using namespace scene;

class ParticleBoxEmitter : public IParticleBoxEmitter
{
public:
	ParticleBoxEmitter(
		const core::aabbox3df& box,
		const core::vector3df& direction = core::vector3df(0.0f,0.03f,0.0f),
		u32 minParticlesPerSecond = 20,
		u32 maxParticlesPerSecond = 40,
		video::SColor minStartColor = video::SColor(255,0,0,0),
		video::SColor maxStartColor = video::SColor(255,255,255,255),
		u32 lifeTimeMin=2000,
		u32 lifeTimeMax=4000,
		s32 maxAngleDegrees=0,
		const core::dimension2df& minStartSize = core::dimension2df(5.0f,5.0f),
		const core::dimension2df& maxStartSize = core::dimension2df(5.0f,5.0f),
		IParticleSystemSceneNode* parent = NULL,
		bool useParentRotation = false
		);

	virtual s32 emitt(u32 now, u32 timeSinceLastCall, SParticle*& outArray);

	virtual void setDirection( const core::vector3df& newDirection ) { Direction = newDirection; }

	virtual void setMinParticlesPerSecond( u32 minPPS ) { MinParticlesPerSecond = minPPS; }

	virtual void setMaxParticlesPerSecond( u32 maxPPS ) { MaxParticlesPerSecond = maxPPS; }

	virtual void setMinStartColor( const video::SColor& color ) { MinStartColor = color; }

	virtual void setMaxStartColor( const video::SColor& color ) { MaxStartColor = color; }

	virtual void setMaxStartSize( const core::dimension2df& size ) { MaxStartSize = size; };

	virtual void setMinStartSize( const core::dimension2df& size ) { MinStartSize = size; };

	virtual void setBox( const core::aabbox3df& box ) { Box = box; }

	virtual const core::vector3df& getDirection() const { return Direction; }

	virtual u32 getMinParticlesPerSecond() const { return MinParticlesPerSecond; }

	virtual u32 getMaxParticlesPerSecond() const { return MaxParticlesPerSecond; }

	virtual const video::SColor& getMinStartColor() const { return MinStartColor; }

	virtual const video::SColor& getMaxStartColor() const { return MaxStartColor; }

	virtual const core::dimension2df& getMaxStartSize() const { return MaxStartSize; };

	virtual const core::dimension2df& getMinStartSize() const { return MinStartSize; };

	virtual const core::aabbox3df& getBox() const { return Box; }

private:
	core::array<SParticle> Particles;
	
	core::aabbox3df Box;
	
	core::vector3df Direction;
	
	core::dimension2df MaxStartSize, MinStartSize;
	
	u32 MinParticlesPerSecond, MaxParticlesPerSecond;
	
	video::SColor MinStartColor, MaxStartColor;
	
	u32 MinLifeTime, MaxLifeTime;

	u32 Time;

	u32 Emitted;

	s32 MaxAngleDegrees;

	IParticleSystemSceneNode* _parentNode;

	// Использовать поворот родителя
	bool _useParentRotation;
};
