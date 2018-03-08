#include "ModuleFly.h"
#include "ManagerEvents.h"
#include "Stage/StageManagerEvents.h"

ModuleFly::ModuleFly(SharedParams_t params) : Base(params)
{
	ClearData();
}

ModuleFly::~ModuleFly(void)
{
}

irr::core::stringc ModuleFly::GetName()
{
	return "ModuleFly";
}

void ModuleFly::Init()
{
	if (_isInit) return;

	_timeText = (ControlText*)CntrlManager->GetControlByName(stringc("Timer"));
	_controlEnergy = CntrlManager->GetControlByName(stringc("Energy"));

	_endTime = GetNowTime() + 2000*60; // 2 минуты необходимо продержаться

	_isInit = true;
	_DEBUG_BREAK_IF(_controlEnergy == NULL)
	_DEBUG_BREAK_IF(_timeText == NULL)
}

void ModuleFly::Execute()
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

	_prevTime = GetNowTime();
}

void ModuleFly::ClearData()
{
	IModule::ClearData();
	_endTime = 0;
	_isInit = false;
	_timeText = NULL;
	_controlEnergy = NULL;
	_isInit = false;
	_targetAngle = 180;
	_prevTime = 0;
	_needShot = false;
}

