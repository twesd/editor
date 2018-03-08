#include "ExecuteTransform.h"
#include "Core/GeometryWorker.h"
#include "../../UnitInstance/UnitInstanceStandard.h"


ExecuteTransform::ExecuteTransform(SharedParams_t params) : ExecuteBase(params),
	_lineAnimator(NULL), _rotateAnimator(NULL), _scaleAnimator(NULL),
	_points(), _timesForWay()
{
	_loop = false;
	_obstacleFilterId = 0;
}

ExecuteTransform::~ExecuteTransform()
{
	if(_lineAnimator != NULL) 
		_lineAnimator->drop();
	if(_rotateAnimator != NULL) 
		_rotateAnimator->drop();
	if(_scaleAnimator != NULL) 
		_scaleAnimator->drop();
}

// Установить аниматор
void ExecuteTransform::SetAnimator( 
	stringc transformType, 
	const core::array<vector3df> &points, 
	core::array<u32> timesForWay, 
	bool loop,
	int obstacleFilterId)
{
	_points = points;
	_timesForWay = timesForWay;
	_loop = loop;
	_obstacleFilterId = obstacleFilterId;

	if(transformType == "LINE")
	{
		_lineAnimator = new LineAnimator(SharedParams);
	}
	else if(transformType == "ROTATE")
	{
		_rotateAnimator = new RotateAnimator(SharedParams);
	}
	else if(transformType == "SCALE")
	{
		_scaleAnimator = new ScaleAnimator(SharedParams);
	}
	else
	{
#ifdef DEBUG_MESSAGE_PRINT
		printf("[ERROR] <ExecuteTransform::SetAnimator>(unknown type %s)\n", transformType.c_str());
#endif
	}
}

// Выполнить действие
void ExecuteTransform::Run( scene::ISceneNode* node, core::array<Event_t*>& events )
{
	ExecuteBase::Run(node, events);

	vector3df rotation = node->getRotation();
	if (_lineAnimator != NULL)
	{
		matrix4 mat;
		mat.setRotationDegrees(rotation);

		_lineAnimator->ResetState();
		_lineAnimator->Init(GetTransformLinePoints(_points, mat), _timesForWay, _loop, _obstacleFilterId);
		node->addAnimator(_lineAnimator);
	}
	else if (_rotateAnimator != NULL)
	{
		matrix4 mat;
		mat.setRotationDegrees(rotation);

		_rotateAnimator->ResetState();
		_rotateAnimator->Init(GetTransformRotatePoints(_points, mat), _timesForWay, _loop);
		node->addAnimator(_rotateAnimator);
	}
	else if (_scaleAnimator != NULL)
	{
		_scaleAnimator->ResetState();		
		_scaleAnimator->Init(_points, _timesForWay, _loop);
		node->addAnimator(_scaleAnimator);
	}
}

core::array<vector3df> ExecuteTransform::GetTransformLinePoints(core::array<vector3df>& inPoints, const matrix4& mat)
{
	core::array<vector3df> outPoints;
	for(u32 i = 0; i < inPoints.size(); i++)
	{
		vector3df point = inPoints[i];
		mat.transformVect(point);
		outPoints.push_back(point);
	}
	return outPoints;
}

core::array<vector3df> ExecuteTransform::GetTransformRotatePoints(core::array<vector3df>& inPoints, const matrix4& mat)
{
	core::array<vector3df> outPoints;
	for(u32 i = 0; i < inPoints.size(); i++)
	{
		vector3df point = inPoints[i];
		mat.rotateVect(point);
		GeometryWorker::RoundVector(point);
		outPoints.push_back(point);
	}
	return outPoints;
}
