#include "CameraGenFollowToNode.h"


CameraGenFollowToNode::CameraGenFollowToNode() : CameraGenBase(),
	UnitInstanceName()
{
	RotateWithUnit = false;
	MapSize2d = 0;
	MapSize3d = 0;
	ObstacleFilterId = 0;
}

CameraGenFollowToNode::~CameraGenFollowToNode(void)
{
}
