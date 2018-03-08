#include "ScaleAnimator.h"

ScaleAnimator::ScaleAnimator(SharedParams_t params) : Base(params)
{
	_timeForWay = 0;
	_startTime = 0;
	_currentNumber = 0;
	_completeAnimate = true;
	_loop = false;
	_userParams = NULL;	
}

ScaleAnimator::~ScaleAnimator(void)
{
}

void ScaleAnimator::Init(const core::array<vector3df> &points, core::array<u32> timesForWay, bool loop)
{
	if(points.size() == 0 || timesForWay.size() != points.size())
	{
		_DEBUG_BREAK_IF(true)
		_completeAnimate = true;
		return;
	}
	_points = points;
	_startTime = GetNowTime();
	_timesForWay = timesForWay;		
	_completeAnimate = false;
	_currentNumber = 0;
	_loop = loop;

	CalulateValues(0);
}

void ScaleAnimator::SetUserParams(void* params)
{
	_userParams = params;
}

bool ScaleAnimator::AnimationEnd()
{
	return _completeAnimate;
}

void ScaleAnimator::CalulateValues(u32 number)
{
	if(_points.size() == 0) return;
	_vector = _points[number];	
	_timeForWay = _timesForWay[number];
	_endTime = _startTime + _timeForWay;	
}

ISceneNodeAnimator* ScaleAnimator::createClone(ISceneNode* node, ISceneManager* newManager){
	//TODO : create valid clone
	//ScaleAnimator* newAnimator = new ScaleAnimator(SharedParams);	
	//return newAnimator;	
	return NULL;
}

void ScaleAnimator::animateNode(ISceneNode* node, u32 timeMs)
{
	if(_completeAnimate) 
	{
		return;
	}

	if(_startTime > timeMs)
	{
		// Случай когда таймер был сброшен в ноль
		//
		if(_endTime < _startTime)
		{
			_endTime = timeMs;
			_startTime = timeMs;
		}
		else 
		{
			s32 diff = (_endTime - _startTime);
			_startTime = timeMs;
			_endTime = timeMs + diff;
		}
		return;
	}

	f32 tDiff = (f32)(timeMs - _startTime);
	if(tDiff == 0)
		return;
	f32 tCoeff = (_timeForWay != 0) ? (tDiff / _timeForWay) : 0;	
	core::vector3df scale = node->getScale();

	if( timeMs >= _endTime )
	{
		tDiff = (f32)(_endTime - _startTime);
		tCoeff = tDiff / _timeForWay;
		scale += _vector * tCoeff;
		if(scale.X < 0) scale.X = 0.001f;
		if(scale.Y < 0) scale.Y = 0.001f;
		if(scale.Z < 0) scale.Z = 0.001f;
		node->setScale(scale);	

		_currentNumber++;
		if(_currentNumber >= _points.size())
		{
			if(!_loop)
			{
				_completeAnimate = true;
			}
			else 
			{
				_currentNumber = 0;
				_startTime = _endTime;
				CalulateValues(_currentNumber);
				animateNode(node, timeMs);
			}			
		}
		else
		{
			_startTime = _endTime;
			CalulateValues(_currentNumber);
			animateNode(node, timeMs);
		}
		return;
	}

	scale += _vector * tCoeff;
	if(scale.X < 0) scale.X = 0.001f;
	if(scale.Y < 0) scale.Y = 0.001f;
	if(scale.Z < 0) scale.Z = 0.001f;
	node->setScale(scale);	
	_startTime = timeMs;
}

// Сброс в начальные установки
void ScaleAnimator::ResetState()
{
	_startTime = GetNowTime();
	_completeAnimate = false;
	_currentNumber = 0;
	CalulateValues(0);
}
