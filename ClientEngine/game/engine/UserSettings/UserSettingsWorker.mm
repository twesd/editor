#include "UserSettingsWorker.h"

#ifdef IPHONE_COMPILE

//#define UserSettingsWorker_Debug

void UserSettingsWorker::Load( SharedParams_t sharedParams, UserSettings* settings )
{
	NSUserDefaults* usrDefs = [NSUserDefaults standardUserDefaults];
	NSArray* settingsArr = [usrDefs arrayForKey:@"UsrSettings"];
	if(settingsArr == nil)
	{
#ifdef UserSettingsWorker_Debug
		printf("[UserSettingsWorker] Load - no data \n");
#endif		
		return;		
	}
	for(NSString* strItem in settingsArr)
	{
		const char* chItem  = [strItem cStringUsingEncoding: NSUTF8StringEncoding ];
		if(chItem == NULL) 
			continue;
#ifdef UserSettingsWorker_Debug
		printf("[UserSettingsWorker] Load item : %s \n", chItem);
#endif
		stringc pairtem(chItem);
		s32 index = pairtem.find(":");
		if(index < 0)
			continue;
		stringc paramName = pairtem.subString(0, index);
		stringc paramVal = pairtem.subString(index + 1, pairtem.size());
		settings->SetTextSetting(paramName, paramVal);
	}	
}

void UserSettingsWorker::Save( SharedParams_t sharedParams, UserSettings* settings )
{
	NSUserDefaults* usrDefs = [NSUserDefaults standardUserDefaults];
	NSMutableArray* settingsArr = [[NSMutableArray alloc] init];
	core::array<Parameter> params = settings->GetParameters();
	
	for (u32 i = 0; i < params.size() ; i++)
	{
		stringw name = params[i].Name;
		stringw val = params[i].Value;
		stringc pairVal = stringc(name + ":" + val);
#ifdef UserSettingsWorker_Debug
		printf("[UserSettingsWorker] Save item : %s \n", pairVal.c_str());
#endif
		NSString* nsStr = [NSString stringWithUTF8String:pairVal.c_str()];
		[settingsArr addObject:nsStr];
	}
	[usrDefs setObject:settingsArr forKey:@"UsrSettings"];
	[usrDefs synchronize];
	[settingsArr release];
}



#endif
