#include "ParticleAffectorColor.h"

ParticleAffectorColor::ParticleAffectorColor(
	const core::array<video::SColor> &colors, 
	core::array<u32> timesForWay, 
	bool loop) : 
	IParticleAffector()
{
	_isInit = false;
	_colors = colors;
	_timesForWay = timesForWay;		
	_loop = loop;
}

ParticleAffectorColor::~ParticleAffectorColor(void)
{
}

void ParticleAffectorColor::affect( u32 nowTime, SParticle* particlearray, u32 count )
{
	if(_colors.size() == 0) 
	{
		return;
	}

	for (u32 i=0; i< count; i++)
	{
		SParticle* particle = &particlearray[i];
		// Время, которое прошло с момента создания
		int timeDiff = nowTime - particle->startTime; 


		// Вычисляем нужный цвет
		//
		u32 itemIndex = 0;
		video::SColor startColor = particle->startColor;
		while(true)
		{
			s32 timeForWay = (s32)_timesForWay[itemIndex];
			if(timeDiff <= timeForWay)
			{
				f32 tCoeff = (timeForWay != 0) ? ((f32)timeDiff / timeForWay) : 0;	
				if(tCoeff == 0)
				{
					particle->color = startColor;
				}
				else 
				{
					particle->color = _colors[itemIndex].getInterpolated(startColor, tCoeff);
				}
				break;
			}
			else
			{
				// Запоминаем начальный цвет
				startColor = _colors[itemIndex];

				itemIndex++;
				if(itemIndex >= _timesForWay.size())
				{
					if (_loop)
					{
						itemIndex = 0;
					}
					else
					{
						particle->color = _colors.getLast();
						break;
					}
				}
				timeDiff -= timeForWay;
			}
		}
	}	
}
