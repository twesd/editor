#include "ModuleTapAndShot.h"
#include "Core/GeometryWorker.h"
#include "ManagerEvents.h"
#include "Stage/StageManagerEvents.h"

ModuleTapAndShot::ModuleTapAndShot(SharedParams_t params) : Base(params)
{
	ClearData();
}

ModuleTapAndShot::~ModuleTapAndShot(void)
{
}

irr::core::stringc ModuleTapAndShot::GetName()
{
	return "ModuleTapAndShot";
}

void ModuleTapAndShot::Init()
{
	if (_isInit) return;

	_timeText = (ControlText*)CntrlManager->GetControlByName(stringc("Timer"));
	_controlEnergy = CntrlManager->GetControlByName(stringc("Energy"));
	_tapScene = (ControlTapScene*)CntrlManager->GetControlByName(stringc("Tap"));

	_endTime = GetNowTime() + 2000*60; // 2 минуты необходимо продержаться
	_timeForHideMessage =  GetNowTime() + 5000;

	_isInit = true;
	_DEBUG_BREAK_IF(_controlEnergy == NULL)
	_DEBUG_BREAK_IF(_timeText == NULL)
	_DEBUG_BREAK_IF(_tapScene == NULL)
}

void ModuleTapAndShot::Execute()
{
	_DEBUG_BREAK_IF(UManager == NULL)	
	_DEBUG_BREAK_IF(CntrlManager == NULL)	

	if(!_isInit)
	{
		Init();
		return;
	}
	
	s32 timeMSec = _endTime - GetNowTime(); 
	if(timeMSec < 0) timeMSec = 0;
	u32 timeMin = timeMSec / 60000;
	u32 timeSec = (timeMSec / 1000) - (timeMin*60);
	stringc timeSecStr = stringc(timeSec);
	stringc timeMinStr = stringc(timeMin);
	if(timeSec < 10) timeSecStr = stringc("0") + timeSecStr;
	if(timeMin < 10) timeMinStr = stringc("0") + timeMinStr;
	_timeText->Text = timeMinStr + ":" + timeSecStr;

	if (GetNowTime() > _endTime)
	{
		GetEvent()->PostEvent(Event::ID_STAGE_MANAGER, STAGE_MANAGER_EVENT_STOP_UNIT_MANAGER);
		stringc cntrlName = stringc("WinMenu.controls");
		CntrlManager->SetControlPackage(cntrlName);
		return;
	}

	if (_timeForHideMessage != 0 && GetNowTime() > _timeForHideMessage)
	{
		CntrlManager->GetControlByName("BgMsg")->IsVisible = false;
		CntrlManager->GetControlByName("TextTapScreen")->IsVisible = false;
		_timeForHideMessage = 0;
	}

	UnitInstanceBase* instanceBase = UManager->GetInstanceByName(stringc("Player"));
	UnitInstanceStandard* player = dynamic_cast<UnitInstanceStandard*>(instanceBase);
	if (player == NULL)
	{
		GetEvent()->PostEvent(Event::ID_STAGE_MANAGER, STAGE_MANAGER_EVENT_STOP_UNIT_MANAGER);
		stringc cntrlName = stringc("LooseMenu.controls");
		CntrlManager->SetControlPackage(cntrlName);
		return;
	}

	const Parameter* uparam = player->GetBehavior()->GetParameter(stringc("Energy"));
	float energy = uparam->GetAsFloat();
	recti bounds = _controlEnergy->GetBounds();
	int width = (int)((f32)bounds.getWidth() * (energy / 100.0f));	
	recti clipArea = recti(bounds.UpperLeftCorner, 
		dimension2di(width, bounds.getHeight()));
	_controlEnergy->SetClip(clipArea);

	if (_tapScene->GetTapSceneDesc().State == BUTTON_STATE_DOWN)
	{
		Shot(player);
	}

	// Разварачиваем объект постепенно
	//
	f32 angle = _targetAngle;
	f32 nowAngle = player->SceneNode->getRotation().Y;
	f32 diffAngle = GeometryWorker::GetAngleDiffClosed(nowAngle, _targetAngle);
	if(diffAngle > 5)
	{
		f32 reqDiff = (f32)(GetNowTime() - _prevTime) * 0.2f;
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
	else 
	{
		if (_needShot)
		{
			player->GetBehavior()->SetParameter("NeedShot", "true");
			_needShot = false;
		}
	}
	player->SceneNode->setRotation(vector3df(0, angle, 0));

	_prevTime = GetNowTime();
}

void ModuleTapAndShot::ClearData()
{
	IModule::ClearData();
	_endTime = 0;
	_isInit = false;
	_timeText = NULL;
	_controlEnergy = NULL;
	_tapScene = NULL;
	_isInit = false;
	_targetAngle = 180;
	_prevTime = 0;
	_needShot = false;
}

void ModuleTapAndShot::Shot(UnitInstanceStandard* player)
{
	ControlTapSceneDesc_t desc = _tapScene->GetTapSceneDesc();
	plane3df plane(vector3df(0, 0, 0), vector3df(0,1,0));
	vector3df outIntersection;
	if(!plane.getIntersectionWithLine(
		desc.SceneRay.start, 
		desc.SceneRay.getVector(), 
		outIntersection))
	{
		return;
	}


	vector3df playerPos = player->SceneNode->getAbsolutePosition();

	playerPos.Y = 0;
	outIntersection.Y = 0;
	_targetAngle = (playerPos - outIntersection).getHorizontalAngle().Y;

	_needShot = true;
}

