#pragma once
#include "ExecuteBase.h"

class ExecuteTimer : public ExecuteBase
{
public:
	ExecuteTimer(SharedParams_t params);
	virtual ~ExecuteTimer(void);

	// Выполнить действие
	virtual void Run(scene::ISceneNode* node, core::array<Event_t*>& events);

	stringc TimerName;

	// Использовать таймер доступный во всей стадии
	bool UseStageTimer;

	bool StartTimer;

	bool StopTimer;

	bool SetTime;

	u32 Time;

};
