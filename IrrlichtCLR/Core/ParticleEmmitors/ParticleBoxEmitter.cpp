#include "ParticleBoxEmitter.h"
#include "Core/Randomizer.h"
#include "Core/NodeWorker.h"

//! constructor
ParticleBoxEmitter::ParticleBoxEmitter(
	const core::aabbox3df& box, const core::vector3df& direction,
	u32 minParticlesPerSecond, u32 maxParticlesPerSecond,
	video::SColor minStartColor, video::SColor maxStartColor,
	u32 lifeTimeMin, u32 lifeTimeMax, s32 maxAngleDegrees,
	const core::dimension2df& minStartSize, const core::dimension2df& maxStartSize,
	IParticleSystemSceneNode* parent, bool useParentRotation)
	: Box(box), Direction(direction),
	MaxStartSize(maxStartSize), MinStartSize(minStartSize),
	MinParticlesPerSecond(minParticlesPerSecond),
	MaxParticlesPerSecond(maxParticlesPerSecond),
	MinStartColor(minStartColor), MaxStartColor(maxStartColor),
	MinLifeTime(lifeTimeMin), MaxLifeTime(lifeTimeMax),
	Time(0), Emitted(0), MaxAngleDegrees(maxAngleDegrees),
	_parentNode(parent), _useParentRotation(useParentRotation)
{
}


//! Prepares an array with new particles to emitt into the system
//! and returns how much new particles there are.
s32 ParticleBoxEmitter::emitt(u32 now, u32 timeSinceLastCall, SParticle*& outArray)
{
	Time += timeSinceLastCall;

	const u32 pps = (MaxParticlesPerSecond - MinParticlesPerSecond);
	const f32 perSecond = pps ? (f32)MinParticlesPerSecond + (Randomizer::Rand() % pps) : MinParticlesPerSecond;
	const f32 everyWhatMillisecond = 1000.0f / perSecond;

	if (Time > everyWhatMillisecond)
	{
		Particles.set_used(0);
		u32 amount = (u32)((Time / everyWhatMillisecond) + 0.5f);
		Time = 0;
		SParticle p;
		const core::vector3df& extent = Box.getExtent();

		if (amount > MaxParticlesPerSecond*2)
			amount = MaxParticlesPerSecond * 2;

		vector3df direction = Direction;
		if (_useParentRotation && _parentNode != NULL)
		{
			vector3df parentRot = NodeWorker::GetAbsoluteRotation(_parentNode);
			matrix4 m;
			m.setRotationDegrees(parentRot);
			m.transformVect(direction);
		}

		for (u32 i=0; i<amount; ++i)
		{
			// MY change
			p.pos.X = Box.MinEdge.X + (fmodf((f32)Randomizer::Rand(), extent.X * 1000.0f) * 0.001f);
			p.pos.Y = Box.MinEdge.Y + (fmodf((f32)Randomizer::Rand(), extent.Y * 1000.0f) * 0.001f);
			p.pos.Z = Box.MinEdge.Z + (fmodf((f32)Randomizer::Rand(), extent.Z * 1000.0f) * 0.001f);

			p.startTime = now;
			p.vector = direction;

			if (MaxAngleDegrees)
			{
				core::vector3df tgt = direction;
				tgt.rotateXYBy((Randomizer::Rand()%(MaxAngleDegrees*2)) - MaxAngleDegrees);
				tgt.rotateYZBy((Randomizer::Rand()%(MaxAngleDegrees*2)) - MaxAngleDegrees);
				tgt.rotateXZBy((Randomizer::Rand()%(MaxAngleDegrees*2)) - MaxAngleDegrees);
				p.vector = tgt;
			}

			if (MaxLifeTime - MinLifeTime == 0)
				p.endTime = now + MinLifeTime;
			else
				p.endTime = now + MinLifeTime + (Randomizer::Rand() % (MaxLifeTime - MinLifeTime));

			p.color = MinStartColor.getInterpolated(
				MaxStartColor, (Randomizer::Rand() % 100) / 100.0f);

			p.startColor = p.color;
			p.startVector = p.vector;

			if (MinStartSize==MaxStartSize)
				p.startSize = MinStartSize;
			else
				p.startSize = MinStartSize.getInterpolated(
				MaxStartSize, (Randomizer::Rand() % 100) / 100.0f);
			p.size = p.startSize;

			Particles.push_back(p);
		}

		outArray = Particles.pointer();

		return Particles.size();
	}

	return 0;
}
