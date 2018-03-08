#include "PathSceneNodeAnimator.h"
#include "Core/NodeWorker.h"
#include "Core/GeometryWorker.h"

PathSceneNodeAnimator::PathSceneNodeAnimator(SharedParams_t params) : Base(params),
	_targetSceneNode(NULL), _pathFinder(params, 128, 500.0f)
{
	_speed = 0;	
	_prevTime = 0;
	_targetDist = 0;
	_completeAnimate = true;
	_userParams = NULL;
}

PathSceneNodeAnimator::~PathSceneNodeAnimator(void)
{
}

void PathSceneNodeAnimator::Init(ISceneNode* targetSceneNode, f32 speed, f32 targetDist, int filterId)
{
	if(speed <= 0)
	{
		printf("ERROR: [PathSceneNodeAnimator::init] invalid params");
		_completeAnimate = true;
		return;
	}
	_targetSceneNode = targetSceneNode;
	_speed = speed;
	_targetDist = targetDist;
	_prevTime = GetNowTime();
	_completeAnimate = false;

	_pathFinder.SetObstacleFilterId(filterId);
}

void PathSceneNodeAnimator::SetUserParams(void* params)
{
	_userParams = params;
}

bool PathSceneNodeAnimator::AnimationEnd()
{
	return _completeAnimate;
}

ISceneNodeAnimator* PathSceneNodeAnimator::createClone(ISceneNode* node, ISceneManager* newManager)
{
	//TODO : 	
	return NULL;
}

void PathSceneNodeAnimator::animateNode(ISceneNode* node, u32 timeMs)
{
	if(_completeAnimate) return;

	f32 tDiff = (f32)(timeMs - _prevTime) / 1000;
	if(tDiff == 0)
	{
		return;
	}

	// Проверка на завершение
	//
	if(!NodeWorker::GetIsNodeExist(_targetSceneNode, SceneManager)) 
	{
		_completeAnimate = true;
		return;
	}

	vector3df curPos = node->getAbsolutePosition();
	vector3df targetPos = _targetSceneNode->getAbsolutePosition();
	
	f32 distance = targetPos.getDistanceFrom(curPos);
	if (distance <= _targetDist)
	{
		_completeAnimate = true;
		return;
	}

	targetPos.Y = curPos.Y;// TODO : реализовать учёт высоты

	// Получаем новую позицию
	//
	f32 wayDist = _speed * tDiff;
	wayDist = 0.20999999f;
	_pathFinder.SetSourceNode(node);
	core::vector3df newPos = _pathFinder.GetNextPosition(targetPos, wayDist, _targetDist);
	node->setPosition(newPos);

	// Устанавливаем поворот
	//
	f32 angle;
	if(curPos == newPos)
	{
		angle = GeometryWorker::GetAngle(curPos, targetPos);
	}
	else 
	{
		angle = GeometryWorker::GetAngle(curPos, newPos);	
	}

	// Разварачиваем объект постепенно
	//
	f32 nowAngle = node->getRotation().Y;
	f32 diffAngle = GeometryWorker::GetAngleDiffClosed(nowAngle, angle);
	if(diffAngle > 5)
	{
		f32 reqDiff = (f32)(timeMs - _prevTime) * 0.2f;
		if(reqDiff < diffAngle)
		{
			if(GeometryWorker::IsClockwiseDirectional(nowAngle, angle))
			{
				angle = nowAngle - reqDiff;
			}
			else
			{
				angle = nowAngle + reqDiff;
			}
		}		
	}

	node->setRotation(vector3df(0, angle, 0));

	_prevTime = timeMs;
}

// Сброс в начальные установки
void PathSceneNodeAnimator::ResetState()
{
	_prevTime = GetNowTime();
	_completeAnimate = false;
}

