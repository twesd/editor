#include "InAppPurchases.h"


#ifdef IPHONE_COMPILE

#include "MKStoreManager.h"

InAppPurchases::InAppPurchases(SharedParams_t params) : Base(params)
{
	[MKStoreManager sharedManager];
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
		NSString* nsStr = [NSString stringWithUTF8String:featureId.c_str()];
		[[MKStoreManager sharedManager] buyFeature:nsStr];
		newFeature.Status = InAppStatusUnknown;
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
	NSString* nsStr = [NSString stringWithUTF8String:featureId.c_str()];
	BOOL inProgress = [[MKStoreManager sharedManager] featureInProgress : nsStr];
	if(inProgress)
	{
		feature->Status = InAppStatusUnknown;
	}
	else 
	{
		BOOL isAvail = [[MKStoreManager sharedManager] featurePurchased: nsStr];
		if(isAvail)
		{
			feature->Status = InAppStatusAvail;
		}
		else 
		{
			feature->Status = InAppStatusFail;
		}
	}
	
	return feature->Status;
}

InAppPurchases::InAppFeature* InAppPurchases::FindFeature( stringc featureId )
{
	for (int i = 0; i < _features.size() ; i++)
	{
		if(_features[i].Id == featureId)
			return &_features[i];
	}
	return NULL;
}

void InAppPurchases::ClearFeatureStatus( stringc featureId )
{
	for (int i = 0; i < _features.size() ; i++)
	{
		if(_features[i].Id == featureId)
		{
			_features.erase(i);
			return;
		}
	}
}




#endif