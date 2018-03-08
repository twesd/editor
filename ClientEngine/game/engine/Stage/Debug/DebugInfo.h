#pragma once
#include "Core/Base.h"

class UnitManager;
class UnitInstanceStandard;


class DebugInfo : public Base
{
public:
	static DebugInfo* GetInstance(SharedParams_t params)
	{
		static DebugInfo instance(params);
		return &instance;
	}

	void Update(reciverInfo_t *reciverInfo, UnitManager* unitManager);

	void PrintInfo( UnitInstanceStandard* standard );

private:
	DebugInfo(SharedParams_t params);

};
