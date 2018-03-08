#include "CameraBehaviorFollowToNode.h"
#include "../../UnitManager.h"

CameraBehaviorFollowToNode::CameraBehaviorFollowToNode(
	SharedParams_t params, 
	ICameraSceneNode* camera,
	UnitManager* unitManager,
	stringc instName,
	bool rotateWithUnit,
	vector3df offset,
	vector3df targetOffset,
	u32 mapSize2d,
	f32 mapSize3d,
	int obstacleFilterId
	) : CameraBehaviorBase(params, camera),
		_pathFinder(params, mapSize2d, mapSize3d)
{
	_unitManager = unitManager;
	_unitInstanceName = instName;
	_rotateWithUnit = false;
	_offset = offset;
	_targetOffset = targetOffset;
	_obstacleFilterId = obstacleFilterId;

	_pathFinder.SetObstacleFilterId(_obstacleFilterId);
	_pathFinder.SetSourceNode(camera);

	_isInit = false;
	_lastTime = 0;
}

CameraBehaviorFollowToNode::~CameraBehaviorFollowToNode(void)
{
}

void CameraBehaviorFollowToNode::Update()
{
	UnitInstanceBase* fistIns = _unitManager->GetInstanceByName(_unitInstanceName);
	if (fistIns == NULL) return;
	vector3df unitPos = fistIns->SceneNode->getAbsolutePosition();

	if (!_isInit)
	{
		// Пересчитываем сдвиг
		//
		_targetOffset = _targetOffset - unitPos;
		_offset = _offset - unitPos;
		_isInit = true;
	}

	if(_lastTime == 0)
	{
		_lastTime = GetNowTime();
	}
	u32 timeDiff = GetNowTime() - _lastTime;
	_lastTime = GetNowTime();

	vector3df camPos = unitPos + _offset;

	// Если необходимо обрабатывать препятствия, то
	if (_obstacleFilterId != 0)
	{
		if (_pathFinder.CanMove(camPos))
		{
			SetCameraPositions(camPos,unitPos + _targetOffset, timeDiff);
		}
		else
		{
			vector3df outPos;
			if(_pathFinder.TryGetFreePosition(camPos, outPos))
			{
				// (outPos - _offset) - предполагаемая позиция юнита
				vector3df outTargetPos = (outPos - _offset) + _targetOffset;
				SetCameraPositions(outPos, outTargetPos, timeDiff);
			}
		}
	}
	else
	{
		SetCameraPositions(camPos,unitPos + _targetOffset, timeDiff);
	}
}

void CameraBehaviorFollowToNode::SetCameraPositions( vector3df pos, vector3df targetPos, u32 timeDiff )
{
	if (timeDiff == 0)
	{
		Camera->setPosition(pos);
		Camera->setTarget(targetPos);
		return;
	}

	// Коэфициент перемещения
	f32 tCoeff = (timeDiff / 300.0f);	
	if(tCoeff > 1) tCoeff = 1;

	vector3df oldPos = Camera->getPosition();
	vector3df oldTarget = Camera->getTarget();
	f32 dist = pos.getDistanceFromSQ(oldPos);
	if(dist < 0.1f)
	{
		Camera->setPosition(pos);
	}
	else 
	{
		vector3df posVector = pos - oldPos;
		oldPos += (posVector * tCoeff);
		Camera->setPosition(oldPos);
	}

	dist = targetPos.getDistanceFromSQ(oldTarget);
	if(dist < 0.2f)
	{
		Camera->setTarget(oldTarget);
	}
	else 
	{
		vector3df targetVector = targetPos - oldTarget;
		oldTarget += (targetVector * tCoeff);
		Camera->setTarget(oldTarget);
	}

}
