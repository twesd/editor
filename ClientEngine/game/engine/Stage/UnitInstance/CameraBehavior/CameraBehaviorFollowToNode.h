#pragma once
#include "CameraBehaviorBase.h"
#include "../../Paths/PathFinder.h"

class UnitManager;

class CameraBehaviorFollowToNode : public CameraBehaviorBase
{
public:
	CameraBehaviorFollowToNode(
		SharedParams_t params, 
		ICameraSceneNode* camera,
		UnitManager* unitManager,
		stringc instName,
		bool rotateWithUnit,
		vector3df offset,
		vector3df targetOffset,
		u32 mapSize2d,
		f32 mapSize3d,
		int obstacleFilterId);
	virtual ~CameraBehaviorFollowToNode(void);

	CameraBehaviorType GetType()
	{
		return CameraBehaviorBase::FollowToNode;
	}

	void Update();

private:
	void SetCameraPositions( vector3df  pos, vector3df targetPos, u32 timeDiff );

	UnitManager* _unitManager;

	stringc _unitInstanceName;

	bool _rotateWithUnit;

	bool _isInit;

	// Фильтр препятствий
	int _obstacleFilterId;

	// Сдвиг в позиции камеры
	vector3df _offset;

	// Сдвиг в цели камеры
	vector3df _targetOffset;

	PathFinder _pathFinder;

	u32 _lastTime;
};
