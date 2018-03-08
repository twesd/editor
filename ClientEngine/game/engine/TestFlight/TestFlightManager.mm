#include "TestFlightManager.h"
#include "TestFlight.h"


#ifdef IPHONE_COMPILE

void TestFlightManager::Log( stringc text )
{
	if(text == "") return;
	//NSString* nsStr = [NSString stringWithUTF8String:text.c_str()];
	//TFLog(nsStr);
}
void TestFlightManager::Init(stringc testId)
{
	//[TestFlight takeOff:@"bf4e811e57fb2eaf913a225354242_OTA5OTkyMDEzLTAxLTI4IDA0OjQ3OjI5LjgzMjgwNQ"];	
    //[TestFlight setDeviceIdentifier:[[UIDevice currentDevice] uniqueIdentifier]];	
}

void TestFlightManager::Log( stringc text )
{
	// Ничего
}


#endif