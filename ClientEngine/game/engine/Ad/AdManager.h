#pragma once

#include "../Core/Base.h"


class AdManager : public Base
{
public:
	AdManager(SharedParams_t params);
	virtual ~AdManager(void);

	void ShowAd(u32 waitTime);

	void HideAd();
	
private:
	void AllocAdController();
	
	void* _adController;
};
