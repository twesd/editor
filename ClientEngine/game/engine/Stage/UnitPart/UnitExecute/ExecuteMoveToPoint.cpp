#include "ExecuteMoveToPoint.h"
#include "../../UnitInstance/UnitInstanceStandard.h"
#include "../UnitAction.h"
#include "../../../Controls/ControlEvent.h"

ExecuteMoveToPoint::ExecuteMoveToPoint(SharedParams_t params) : ExecuteBase(params),
	Speed(1),TapSceneName(),GetPositionFromTapControl(false), TargetPosition(),
	_pathAnimator(NULL), ObstacleFilterId(-1)
{
	TargetDist = 1;
}

ExecuteMoveToPoint::~ExecuteMoveToPoint()
{
	if (_pathAnimator != NULL)
	{
		_pathAnimator->drop();
	}
}

// Выполнить действие
void ExecuteMoveToPoint::Run( scene::ISceneNode* node, core::array<Event_t*>& events )
{
	ExecuteBase::Run(node, events);

	if(GetPositionFromTapControl)
	{
		for (u32 i = 0; i < events.size(); i++)
		{
			Event_t* eventInst = events[i];
			if(eventInst->EventId != EVENT_CONTROL_TAPSCENE) 
				continue;
			ControlTapSceneDesc_t* desc = (ControlTapSceneDesc_t*)eventInst->Params.CommonParam;
			if(desc->Name != TapSceneName) return;
			
			plane3df plane(vector3df(0, 0, 0), vector3df(0,1,0));
			vector3df outIntersection;
			if(!plane.getIntersectionWithLine(
				desc->SceneRay.start, 
				desc->SceneRay.getVector(), 
				outIntersection))
			{
				return;
			}
			_pathAnimator->SetTarget(outIntersection);			
			_pathAnimator->ResetState();
			node->addAnimator(_pathAnimator);
		}
	}
	else
	{

	}
}

// Завершение чтения настроек
void ExecuteMoveToPoint::CompleteLoading()
{
	_pathAnimator = new PathAnimator(SharedParams);
	_pathAnimator->Init(TargetPosition, Speed, TargetDist, ObstacleFilterId);
}
