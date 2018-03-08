#include "ExecuteCreateUnit.h"
#include "../../UnitInstance/UnitInstanceStandard.h"
#include "../../../Controls/ControlEvent.h"
#include "Core/NodeWorker.h"
#include "Core/GeometryWorker.h"

ExecuteCreateUnit::ExecuteCreateUnit(SharedParams_t params) : ExecuteBase(params),
	_startPosition(), 
	_startRotation(), 
	BehaviorsPath(),
	AllowSeveralInstances(false), 
	_root(),
	CreationType(CreationType_Child),
	GetPositionFromTapScene(false),
	TapSceneName(),
	StartScriptFileName()
{
}

ExecuteCreateUnit::~ExecuteCreateUnit()
{

}

// Установить начальную позицию
void ExecuteCreateUnit::SetStartPosition( vector3df pos )
{
	_startPosition = pos;
}

// Установить начальное вращение
void ExecuteCreateUnit::SetStartRotation( vector3df rotation )
{
	_startRotation = rotation;
}

// Выполнить действие
void ExecuteCreateUnit::Run(scene::ISceneNode* node, core::array<Event_t*>& events)
{
	ExecuteBase::Run(node, events);

	_DEBUG_BREAK_IF(UnitInstance == NULL)
	UnitGenInstanceStandard genInstance(SharedParams);
	genInstance.PathBehavior = _root + BehaviorsPath;
	if(StartScriptFileName != "")
		genInstance.StartScriptFileName = _root + StartScriptFileName;
	else
		genInstance.StartScriptFileName = "";
	if (GetPositionFromTapScene)
	{
		bool found = false;
		vector3df position;

		for (u32 i = 0; i < events.size(); i++)
		{
			Event_t* eventInst = events[i];
			if(eventInst->EventId != EVENT_CONTROL_TAPSCENE) 
				continue;
			ControlTapSceneDesc_t* desc = (ControlTapSceneDesc_t*)eventInst->Params.CommonParam;

			if(desc->Name == TapSceneName)
			{			
				plane3df plane(vector3df(0, 0, 0), vector3df(0,1,0));
				vector3df outIntersection;
				if(!plane.getIntersectionWithLine(
					desc->SceneRay.start, 
					desc->SceneRay.getVector(), 
					position))
				{
					_DEBUG_BREAK_IF(true)
				}
				found = true;
				break;
			}
		}
		_DEBUG_BREAK_IF(!found)
		GeometryWorker::RoundVector(position);
		genInstance.StartPosition = position;
		// Так как позиция и угол высчитывается относительно центра юнита,
		// выставляем позицию и угол, так, чтобы конеченая позиция была из TapControl
		node->updateAbsolutePosition();
		vector3df absPos = node->getAbsolutePosition();
		vector3df rotation = NodeWorker::GetAbsoluteRotation(node);
		genInstance.StartPosition -= absPos;
		genInstance.StartRotation = _startRotation - rotation;
	}
	else
	{
		genInstance.StartPosition = _startPosition;
		genInstance.StartRotation = _startRotation;
	}
	

	genInstance.JointName = JointName;	

	if (CreationType == CreationType_Child)
	{
		UnitInstance->CreateChild(genInstance, AllowSeveralInstances);
	}	
	else if (CreationType == CreationType_Externals)
	{
		UnitInstance->CreateExternalUnit(genInstance, AllowSeveralInstances);
	}
	else
	{
		_DEBUG_BREAK_IF(true)
	}
}

// Установить основную директорию
void ExecuteCreateUnit::SetRoot( stringc root )
{
	_root = root;
}
