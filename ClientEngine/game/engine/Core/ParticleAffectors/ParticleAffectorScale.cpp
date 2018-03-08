#include "ParticleAffectorScale.h"

ParticleAffectorScale::ParticleAffectorScale(
	const core::array<dimension2df> &dimensions, 
	core::array<u32> timesForWay, 
	bool loop) : 
	IParticleAffector()
{
	_dimensions = dimensions;
	_timesForWay = timesForWay;
	_loop = loop;
	_isInit = false;
}

ParticleAffectorScale::~ParticleAffectorScale(void)
{
}

void ParticleAffectorScale::affect( u32 nowTime, SParticle* particlearray, u32 count )
{
	if(_dimensions.size() == 0) 
	{
		return;
	}

	for (u32 i=0; i< count; i++)
	{
		SParticle* particle = &particlearray[i];
		// Время, которое прошло с момента создания
		int timeDiff = nowTime - particle->startTime; 


		// Вычисляем нужный размер
		//
		u32 itemIndex = 0;
		dimension2df startSize = particle->startSize;
		while(true)
		{
			s32 timeForWay = (s32)_timesForWay[itemIndex];
			if(timeDiff <= timeForWay)
			{
				f32 tCoeff = (timeForWay != 0) ? ((f32)timeDiff / timeForWay) : 0;	
				if(tCoeff == 0)
				{
					particle->size = startSize;
				}
				else 
				{
					particle->size = _dimensions[itemIndex].getInterpolated(startSize, tCoeff);
				}
				break;
			}
			else
			{
				// Запоминаем начальный размер
				startSize = _dimensions[itemIndex];

				itemIndex++;
				if(itemIndex >= _timesForWay.size())
				{
					if (_loop)
					{
						itemIndex = 0;
					}
					else
					{
						particle->size = _dimensions.getLast();
						break;
					}
				}
				timeDiff -= timeForWay;
			}
		}
	}	
}
