#include "InAppPurchases.h"

#if defined(WINDOWS_COMPILE) || defined(ANDROID_COMPILE)

InAppPurchases::InAppPurchases(SharedParams_t params) : Base(params)
{
	
}

InAppPurchases::~InAppPurchases(void)
{
	
}

void InAppPurchases::RequestFeature( stringc featureId )
{
	InAppFeature* feature = FindFeature(featureId);
	if(feature == NULL)
	{
		InAppFeature newFeature;
		//newFeature.Status = InAppStatusUnknown;
		newFeature.Status = InAppStatusAvail;
		newFeature.Id = featureId;
		_features.push_back(newFeature);
	}
	else 
	{
		//feature->Status = InAppStatusUnknown;
		feature->Status = InAppStatusAvail;
	}
}

InAppStatus InAppPurchases::GetFeatureStatus( stringc featureId )
{
	InAppFeature* feature = FindFeature(featureId);
	if(feature == NULL)
	{
		return InAppStatusUnknown;
	}
	return feature->Status;
}

InAppPurchases::InAppFeature* InAppPurchases::FindFeature( stringc featureId )
{
	for (u32 i = 0; i < _features.size() ; i++)
	{
		if(_features[i].Id == featureId)
			return &_features[i];
	}
	return NULL;
}

void InAppPurchases::ClearFeatureStatus( stringc featureId )
{
	for (u32 i = 0; i < _features.size() ; i++)
	{
		if(_features[i].Id == featureId)
		{
			_features.erase(i);
			return;
		}
	}
}





#endif
