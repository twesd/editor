#pragma once

#include "../Core/Base.h"
#include "InAppStatus.h"


class InAppPurchases : public Base
{
public:
	InAppPurchases(SharedParams_t params);
	virtual ~InAppPurchases(void);

	void RequestFeature(stringc featureId);

	void ClearFeatureStatus(stringc featureId);

	InAppStatus GetFeatureStatus(stringc featureId);

private:
	typedef struct
	{
		stringc Id;
		InAppStatus Status;
	}InAppFeature;

	InAppFeature* FindFeature(stringc featureId);

	core::array<InAppFeature> _features;
};
