#include "ExecuteRotation.h"
#include "Core/NodeWorker.h"
#include "Core/GeometryWorker.h"
#include "../../UnitInstance/UnitInstanceStandard.h"
#include "../../../Controls/ControlEvent.h"

ExecuteRotation::ExecuteRotation(SharedParams_t params) : ExecuteBase(params), 
	Rotation(), ControlCircleName()
{
	Absolute = false;
	AddAngleFromControlCircle = false;
	Speed = 0;
}

ExecuteRotation::~ExecuteRotation()
{
}

// Выполнить действие
void ExecuteRotation::Run( scene::ISceneNode* node, core::array<Event_t*>& events )
{
	ExecuteBase::Run(node, events);

	vector3df startRotation = node->getRotation();
	vector3df reqRotation = Rotation;

	if(AddAngleFromControlCircle)
	{
		for (u32 i = 0; i < events.size(); i++)
		{
			Event_t* eventInst = events[i];
			if(eventInst->EventId != EVENT_CONTROL_CIRCLE) 
				continue;
			ControlCircleDesc_t* desc = (ControlCircleDesc_t*)eventInst->Params.CommonParam;
			if(desc->Name != ControlCircleName) continue;
			
			reqRotation += vector3df(0, desc->Angle, 0);
		}
	}

	if(Absolute)
	{
		vector3df parentRotation = NodeWorker::GetParentRotation(node);
		reqRotation -= parentRotation;
	}
	
	if (Speed > 0)
	{
		f32 reqDiff = GetChangeValue(Speed, 1000);
		vector3df resRot;
		resRot.X = CalcAngle(reqRotation.X, startRotation.X, reqDiff);
		resRot.Y = CalcAngle(reqRotation.Y, startRotation.Y, reqDiff);
		resRot.Z = CalcAngle(reqRotation.Z, startRotation.Z, reqDiff);
		GeometryWorker::NormalizeRotation(resRot);
		node->setRotation(resRot);
	}
	else
	{
		node->setRotation(reqRotation);
	}
}

f32 ExecuteRotation::CalcAngle( f32 reqAngle, f32 nowAngle, f32 reqDiff )
{
	f32 curDiff = GeometryWorker::GetAngleDiffClosed(reqAngle, nowAngle);
	if(curDiff > 3)
	{
		if (reqDiff > curDiff)
		{
			reqDiff = curDiff;
		}

		if(GeometryWorker::IsClockwiseDirectional(nowAngle, reqAngle))
		{
			reqAngle = nowAngle - reqDiff;
		}
		else
		{
			reqAngle = nowAngle + reqDiff;
		}
	}
	return reqAngle;
}
