#include "RotateAnimator.h"
#include "Core/GeometryWorker.h"

RotateAnimator::RotateAnimator(SharedParams_t params) : Base(params)
{
	_timeForWay = 0;
	_startTime = 0;
	_currentNumber = 0;
	_completeAnimate = true;
	_loop = false;
	_userParams = NULL;	
}

RotateAnimator::~RotateAnimator(void)
{
}

void RotateAnimator::Init(const core::array<vector3df> &points, core::array<u32> timesForWay, bool loop)
{
	if(points.size() == 0 || timesForWay.size() != points.size())
	{
		printf("ERROR: [RotateAnimator::init] invalid params");
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

void RotateAnimator::SetUserParams(void* params){
	_userParams = params;
}

bool RotateAnimator::AnimationEnd(){
	return _completeAnimate;
}

void RotateAnimator::CalulateValues(u32 number)
{
	if(_points.size() == 0) 
		return;
	_vector = _points[number];	
	_timeForWay = _timesForWay[number];
	_endTime = _startTime + _timeForWay;	
}

ISceneNodeAnimator* RotateAnimator::createClone(ISceneNode* node, ISceneManager* newManager){
	//TODO : create valid clone
	//RotateAnimator* newAnimator = new RotateAnimator(SharedParams);	
	//return newAnimator;	
	return NULL;
}

void RotateAnimator::animateNode(ISceneNode* node, u32 timeMs)
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
	{
		return;
	}
	f32 tCoeff = (_timeForWay != 0) ? (tDiff / _timeForWay) : 0;	
	core::vector3df rotation = node->getRotation();

	if( timeMs >= _endTime )
	{
		tDiff = (f32)(_endTime - _startTime);
		tCoeff = tDiff / _timeForWay;
		rotation += _vector * tCoeff;
		node->setRotation(rotation);
		
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

	rotation += _vector * tCoeff;
	GeometryWorker::NormalizeRotation(rotation);
	
	node->setRotation(rotation);	
	_startTime = timeMs;
}

// Сброс в начальные установки
void RotateAnimator::ResetState()
{
	_startTime = GetNowTime();
	_completeAnimate = false;
	_currentNumber = 0;
	CalulateValues(0);
}
