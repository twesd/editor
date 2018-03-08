#include "PathAnimator.h"
#include "Core/GeometryWorker.h"

PathAnimator::PathAnimator(SharedParams_t params) : Base(params),
	_targetPos(), _pathFinder(params, 256, 250.0f)
{
	_speed = 0;	
	_prevTime = 0;
	_targetDist = 1;
	_completeAnimate = true;
	_userParams = NULL;
	_stopCounter = 0;
}

PathAnimator::~PathAnimator(void)
{
}

void PathAnimator::Init(vector3df targetPoint, f32 speed, f32 targetDist, int filterId)
{
	if(speed <= 0)
	{
		printf("ERROR: [PathAnimator::init] invalid params");
		_completeAnimate = true;
		return;
	}
	_targetPos = targetPoint;
	_speed = speed;
	_prevTime = GetNowTime();
	_completeAnimate = false;
	_targetDist = targetDist;
	_stopCounter = 0;

	_pathFinder.SetObstacleFilterId(filterId);
}

void PathAnimator::SetUserParams(void* params)
{
	_userParams = params;
}

bool PathAnimator::AnimationEnd()
{
	return _completeAnimate;
}

ISceneNodeAnimator* PathAnimator::createClone(ISceneNode* node, ISceneManager* newManager)
{
	//TODO : create valid clone
	//PathAnimator* newAnimator = new PathAnimator(SharedParams);	
	//return newAnimator;	
	return NULL;
}

void PathAnimator::animateNode(ISceneNode* node, u32 timeMs)
{
	if(_completeAnimate) 
	{
		return;
	}

	f32 tDiff = (f32)(timeMs - _prevTime) / 1000;
	if(tDiff == 0)
		return;	
	core::vector3df curPos = node->getPosition();
	
	f32 wayDist = _speed * tDiff;
	f32 dist = _targetPos.getDistanceFrom(curPos);
	if (wayDist > dist || dist < _targetDist)
	{
		_completeAnimate = true;
		return;
	}


	// Получаем новую позицию
	_pathFinder.SetSourceNode(node);
	core::vector3df newPos = _pathFinder.GetNextPosition(_targetPos, wayDist, _targetDist);
	if(newPos.getDistanceFromSQ(node->getPosition()) < 0.001f)
	{
		_stopCounter++;
		if(_stopCounter > 20)
		{
			_stopCounter = 0;
			_completeAnimate = true;
			return;
		}
	}
	else
	{
		_stopCounter = 0;
	}
	node->setPosition(newPos);
	
	// Устанавливаем поворот
	f32 angle;
	if(curPos == newPos)
		angle = GeometryWorker::GetAngle(curPos, _targetPos);
	else 
		angle = GeometryWorker::GetAngle(curPos, newPos);

	// Разварачиваем объект постепенно
	//
	f32 nowAngle = node->getRotation().Y;
	f32 diffAngle = GeometryWorker::GetAngleDiffClosed(nowAngle, angle);
	if(diffAngle > 10)
	{
		f32 reqDiff = (f32)(timeMs - _prevTime) * 0.5f;
		if(reqDiff < diffAngle)
		{
			if(GeometryWorker::IsClockwiseDirectional(nowAngle, angle))
				angle = nowAngle - reqDiff;
			else
				angle = nowAngle + reqDiff;
		}		
	}
	node->setRotation(vector3df(0, angle, 0));
	
	_prevTime = timeMs;
}

// Сброс в начальные установки
void PathAnimator::ResetState()
{
	_prevTime = GetNowTime();
	_completeAnimate = false;
}

void PathAnimator::SetTarget( vector3df targetPoint )
{
	_targetPos = targetPoint;
}
