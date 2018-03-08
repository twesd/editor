#include "LineAnimator.h"

LineAnimator::LineAnimator(SharedParams_t params) : Base(params)
{
	_timeForWay = 0;
	_startTime = 0;
	_currentNumber = 0;
	_completeAnimate = true;
	_loop = false;
	_userParams = NULL;
	_obstacleFilterId = 0;
}

LineAnimator::~LineAnimator(void)
{
}

void LineAnimator::Init(
	const core::array<vector3df> &points, 
	core::array<u32> timesForWay, 
	bool loop, 
	int obstacleFilterId)
{
	if(points.size() == 0 || timesForWay.size() != points.size())
	{
		printf("ERROR: [LineAnimator::init] invalid params");
		_completeAnimate = true;
		return;
	}
	_points = points;
	_startTime = GetNowTime();
	_timesForWay = timesForWay;		
	_completeAnimate = false;
	_currentNumber = 0;
	_loop = loop;
	_obstacleFilterId = obstacleFilterId;
	CalulateValues(0);
}


void LineAnimator::SetUserParams(void* params)
{
	_userParams = params;
}

bool LineAnimator::AnimationEnd()
{
	return _completeAnimate;
}

void LineAnimator::CalulateValues(u32 number)
{
	if(_points.size() == 0) return;
	_vector = _points[number];	
	_timeForWay = _timesForWay[number];
	_endTime = _startTime + _timeForWay;	
}

ISceneNodeAnimator* LineAnimator::createClone(ISceneNode* node, ISceneManager* newManager){
	//TODO : create valid clone
	//LineAnimator* newAnimator = new LineAnimator(SharedParams);	
	//return newAnimator;	
	return NULL;
}

void LineAnimator::animateNode(ISceneNode* node, u32 timeMs)
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
	core::vector3df pos = node->getPosition();

	if( timeMs >= _endTime )
	{
		tDiff = (f32)(_endTime - _startTime);
		tCoeff = tDiff / _timeForWay;
		pos += _vector * tCoeff;
		if (_obstacleFilterId != 0)
		{
			vector3df nextPos = pos + (_vector * 1);
			if(CanMoveTo(pos, node) && CanMoveTo(nextPos, node))
				node->setPosition(pos);
		}
		else 
		{
			node->setPosition(pos);
		}

		_currentNumber++;
		if(_currentNumber >= _points.size()){
			if(!_loop){
				_completeAnimate = true;
			} else {
				_currentNumber = 0;
				_startTime = _endTime;
				CalulateValues(_currentNumber);
				animateNode(node, timeMs);
			}			
		} else {
			_startTime = _endTime;
			CalulateValues(_currentNumber);
			animateNode(node, timeMs);
		}
		return;
	}

	pos += _vector * tCoeff;

	_startTime = timeMs;

	if (_obstacleFilterId != 0)
	{
		vector3df nextPos = pos + (_vector * 1);
		if(CanMoveTo(pos, node) && CanMoveTo(nextPos, node))
			node->setPosition(pos);
	}
	else 
	{
		node->setPosition(pos);
	}
}

// Сброс в начальные установки
void LineAnimator::ResetState()
{
	_startTime = GetNowTime();
	_completeAnimate = false;
	_currentNumber = 0;
	CalulateValues(0);
}

bool LineAnimator::CanMoveTo( core::vector3df& pos, ISceneNode* node )
{
	core::matrix4 mat = node->getAbsoluteTransformation();
	mat.setTranslation(pos);// Переопределяем позицию
	aabbox3df mainNodeBox = node->getBoundingBox();
	mat.transformBox(mainNodeBox);

	const core::list<ISceneNode*>& children = SceneManager->getRootSceneNode()->getChildren();
	core::list<ISceneNode*>::ConstIterator it = children.begin();
	for (; it != children.end(); ++it)
	{
		ISceneNode* current = *it;
		if(current == node) continue;

		if(current->getType() != ESNT_ANIMATED_MESH && 
			current->getType() != ESNT_MESH &&
			current->getType() != ESNT_EMPTY)
			continue;

		if (current->isVisible())
		{
			if(current->getID() & _obstacleFilterId)
			{
				aabbox3df currentBox = current->getTransformedBoundingBox();
				if(currentBox.intersectsWithBox(mainNodeBox))
					return false;
			}
		}
	}
	return true;
}
