#include "ParserExtensionCommon.h"
#include "Core/NodeWorker.h"
#include "Core/GeometryWorker.h"
#include "Core/TextureWorker.h"
#include "../Stage/StageManagerEvents.h"
#include "../ManagerEvents.h"
#include "../UserSettings/UserSettingsWorker.h"

ParserUserData* ParserExtensionCommon_GetUserData()
{
	asIScriptContext* ctx = asGetActiveContext();
	asIScriptEngine* engine = ctx->GetEngine();
	ParserUserData* parserUserData = (ParserUserData*)engine->GetUserData();
	_DEBUG_BREAK_IF(parserUserData == NULL)
	_DEBUG_BREAK_IF(parserUserData->BaseData == NULL)
	return parserUserData;
}

// Текущие время int GetNowTime()
u32 ParserExtensionCommon_GetNowTime()
{
	ParserUserData* parserUserData = ParserExtensionCommon_GetUserData();
	Base* baseData = parserUserData->BaseData;
	return baseData->GetNowTime();
}

void ParserExtensionCommon_SetGlobalParameter( std::string& name, std::string& val )
{
	ParserUserData* parserUserData = ParserExtensionCommon_GetUserData();
	Base* baseData = parserUserData->BaseData;
	baseData->SetGlobalParameter(name.c_str(), val.c_str());
}

// string GetGlobalParameter(name)
std::string ParserExtensionCommon_GetGlobalParameter( std::string& name )
{
	ParserUserData* parserUserData = ParserExtensionCommon_GetUserData();
	Base* baseData =  (Base*)parserUserData->BaseData;
	stringc paramVal = baseData->GetGlobalParameter(name.c_str());
	return std::string(paramVal.c_str());
}

// string GetUserSetting(name)
std::string ParserExtensionCommon_GetUserSetting( std::string& name )
{
	ParserUserData* parserUserData = ParserExtensionCommon_GetUserData();
	Base* baseData = parserUserData->BaseData;
	stringc paramName(name.c_str());
	stringc paramVal = baseData->GetUserSettings()->GetValue(paramName);
	return std::string(paramVal.c_str());
}

void ParserExtensionCommon_SetUserSetting( std::string& name, std::string& val )
{
	ParserUserData* parserUserData = ParserExtensionCommon_GetUserData();
	Base* baseData = parserUserData->BaseData;
	baseData->GetUserSettings()->SetTextSetting(name.c_str(), val.c_str());
}

// int GetSceneNodeId(node)
int ParserExtensionCommon_GetNodeId( int nodePointer )
{
	ParserUserData* parserUserData = ParserExtensionCommon_GetUserData();
	Base* baseData = parserUserData->BaseData;

	ISceneNode* node = (ISceneNode*)(nodePointer);
	scene::ISceneManager* sceneManager = baseData->GetSharedParams().SceneManager;
	if(NodeWorker::GetIsNodeExist(node, sceneManager))
		return node->getID();
	else
		return 0;
}

bool ParserExtensionCommon_GetNodeVisible( int nodePointer )
{
	ParserUserData* parserUserData = ParserExtensionCommon_GetUserData();
	Base* baseData = parserUserData->BaseData;

	ISceneNode* node = (ISceneNode*)(nodePointer);
	scene::ISceneManager* sceneManager = baseData->GetSharedParams().SceneManager;
	if(NodeWorker::GetIsNodeExist(node, sceneManager))
		return node->isVisible();
	else
		return 0;
}

// int GetIsNodeExist(node)
bool ParserExtensionCommon_GetIsNodeExist( int nodePointer )
{
	ParserUserData* parserUserData = ParserExtensionCommon_GetUserData();
	Base* baseData = parserUserData->BaseData;

	scene::ISceneManager* sceneManager = baseData->GetSharedParams().SceneManager;
	ISceneNode* node = (ISceneNode*)(nodePointer);
	return NodeWorker::GetIsNodeExist(node, sceneManager);
}

vector3df ParserExtensionCommon_GetDirectRotation( int nodePointer1, int nodePointer2 )
{
	ParserUserData* parserUserData = ParserExtensionCommon_GetUserData();
	Base* baseData = parserUserData->BaseData;

	scene::ISceneManager* sceneManager = baseData->GetSharedParams().SceneManager;
	ISceneNode* node1 = (ISceneNode*)(nodePointer1);
	ISceneNode* node2 = (ISceneNode*)(nodePointer2);
	if(NodeWorker::GetIsNodeExist(node1, sceneManager) && NodeWorker::GetIsNodeExist(node2, sceneManager))
	{
		vector3df curPos = node1->getAbsolutePosition();
		vector3df targetPos = node2->getAbsolutePosition();
		f32 angle = GeometryWorker::GetAngle(curPos, targetPos);
		//if(node1->getParent() != NULL)
		//	angle -= node1->getParent()->getRotation().Y;
		return vector3df(0,angle,0);
	}
	else
	{
		return vector3df();	
	}
}

// GetNodeRotation(node)
vector3df ParserExtensionCommon_GetNodeRotation( int nodePointer )
{
	ParserUserData* parserUserData = ParserExtensionCommon_GetUserData();
	Base* baseData = parserUserData->BaseData;

	scene::ISceneManager* sceneManager = baseData->GetSharedParams().SceneManager;
	ISceneNode* node = (ISceneNode*)(nodePointer);
	if(NodeWorker::GetIsNodeExist(node, sceneManager))
	{
		vector3df rot = node->getRotation();		
		while(rot.Y>360) rot.Y-=360;
		while(rot.Y<0) rot.Y+=360;
		return rot;
	}
	else
	{
		return vector3df();	
	}
}

// GetNodeAbsolutRotation(node)
vector3df ParserExtensionCommon_GetNodeAbsolutRotation( int nodePointer )
{
	ParserUserData* parserUserData = ParserExtensionCommon_GetUserData();
	Base* baseData = parserUserData->BaseData;

	scene::ISceneManager* sceneManager = baseData->GetSharedParams().SceneManager;
	ISceneNode* node = (ISceneNode*)(nodePointer);
	if(NodeWorker::GetIsNodeExist(node, sceneManager))
	{
		return NodeWorker::GetAbsoluteRotation(node);
	}
	else
	{
		return vector3df();	
	}
}

// GetNodePosition(node)
vector3df ParserExtensionCommon_GetNodePosition( int nodePointer )
{
	ParserUserData* parserUserData = ParserExtensionCommon_GetUserData();
	Base* baseData = parserUserData->BaseData;

	scene::ISceneManager* sceneManager = baseData->GetSharedParams().SceneManager;
	ISceneNode* node = (ISceneNode*)(nodePointer);
	if(NodeWorker::GetIsNodeExist(node, sceneManager))
	{
		vector3df curPos = node->getPosition();
		return curPos;
	}
	else
	{
		return vector3df();
	}
}

// void SetNodeRotation(node, rotation)
void ParserExtensionCommon_SetNodeRotation( int nodePointer, irr::core::vector3df& rotation )
{
	ParserUserData* parserUserData = ParserExtensionCommon_GetUserData();
	Base* baseData = parserUserData->BaseData;

	scene::ISceneManager* sceneManager = baseData->GetSharedParams().SceneManager;
	ISceneNode* node = (ISceneNode*)(nodePointer);
	if(!NodeWorker::GetIsNodeExist(node, sceneManager)) return;
	
	node->setRotation(rotation);
}

void ParserExtensionCommon_SetNodeAbsoluteRotation( int nodePointer, irr::core::vector3df& rotation )
{
	ParserUserData* parserUserData = ParserExtensionCommon_GetUserData();
	Base* baseData = parserUserData->BaseData;

	scene::ISceneManager* sceneManager = baseData->GetSharedParams().SceneManager;
	ISceneNode* node = (ISceneNode*)(nodePointer);
	if(!NodeWorker::GetIsNodeExist(node, sceneManager)) return;

	NodeWorker::SetAbsoluteRotation(node, rotation);
}

//void SetNodePosition(node,position)
void ParserExtensionCommon_SetNodePosition( int nodePointer, vector3df& position )
{
	ParserUserData* parserUserData = ParserExtensionCommon_GetUserData();
	Base* baseData = parserUserData->BaseData;

	scene::ISceneManager* sceneManager = baseData->GetSharedParams().SceneManager;
	ISceneNode* node = (ISceneNode*)(nodePointer);
	if(!NodeWorker::GetIsNodeExist(node,sceneManager)) return;

	node->setPosition(position);
	node->updateAbsolutePosition();
}

void ParserExtensionCommon_SetNodeVisible( int nodePointer, bool visible )
{
	ParserUserData* parserUserData = ParserExtensionCommon_GetUserData();
	Base* baseData = parserUserData->BaseData;

	scene::ISceneManager* sceneManager = baseData->GetSharedParams().SceneManager;
	ISceneNode* node = (ISceneNode*)(nodePointer);
	if(!NodeWorker::GetIsNodeExist(node,sceneManager)) return;

	node->setVisible(visible);
}

// CompleteStage
void ParserExtensionCommon_CompleteStage( )
{
	ParserUserData* parserUserData = ParserExtensionCommon_GetUserData();
	Base* baseData = parserUserData->BaseData;

	baseData->GetEvent()->PostEvent(Event::ID_MANAGER, MANAGER_EVENT_STAGE_COMPLETE);
}

// void RestartStage()
void ParserExtensionCommon_RestartStage( )
{
	ParserUserData* parserUserData = ParserExtensionCommon_GetUserData();
	Base* baseData = parserUserData->BaseData;

	baseData->GetEvent()->PostEvent(Event::ID_MANAGER, MANAGER_EVENT_STAGE_RESTART);
}

//LoadStage(name)
void ParserExtensionCommon_LoadStage( std::string& name )
{
	ParserUserData* parserUserData = ParserExtensionCommon_GetUserData();
	Base* baseData = parserUserData->BaseData;
	EventParameters_t params;
	params.StrVar = stringc(name.c_str());
	baseData->GetEvent()->PostEvent(Event::ID_MANAGER, MANAGER_EVENT_STAGE_LOAD, params);
}

// SaveUserSettings()
void ParserExtensionCommon_SaveUserSettings( )
{
	ParserUserData* parserUserData = ParserExtensionCommon_GetUserData();
	Base* baseData = parserUserData->BaseData;
	UserSettingsWorker::Save(baseData->GetSharedParams(), baseData->GetSharedParams().Settings);
}

void ParserExtensionCommon_StopUnitManager()
{
	ParserUserData* parserUserData = ParserExtensionCommon_GetUserData();
	Base* baseData = parserUserData->BaseData;
	baseData->GetEvent()->PostEvent(Event::ID_STAGE_MANAGER, STAGE_MANAGER_EVENT_STOP_UNIT_MANAGER);
}

void ParserExtensionCommon_StartUnitManager()
{
	ParserUserData* parserUserData = ParserExtensionCommon_GetUserData();
	Base* baseData = parserUserData->BaseData;
	baseData->GetEvent()->PostEvent(Event::ID_STAGE_MANAGER, STAGE_MANAGER_EVENT_START_UNIT_MANAGER);
}

void ParserExtensionCommon_ShowAd()
{
	ParserUserData* parserUserData = ParserExtensionCommon_GetUserData();
	Base* baseData = parserUserData->BaseData;
	baseData->GetEvent()->PostEvent(Event::ID_MANAGER, MANAGER_EVENT_AD_SHOW);
}

void ParserExtensionCommon_HideAd()
{
	ParserUserData* parserUserData = ParserExtensionCommon_GetUserData();
	Base* baseData = parserUserData->BaseData;
	baseData->GetEvent()->PostEvent(Event::ID_MANAGER, MANAGER_EVENT_AD_HIDE);
}


void ParserExtensionCommon_StopTimer()
{
	ParserUserData* parserUserData = ParserExtensionCommon_GetUserData();
	Base* baseData = parserUserData->BaseData;
	ITimer* timer = baseData->GetSharedParams().Timer;
	if(!timer->isStopped()) 
		timer->stop();
}

void ParserExtensionCommon_StartTimer()
{
	ParserUserData* parserUserData = ParserExtensionCommon_GetUserData();
	Base* baseData = parserUserData->BaseData;
	ITimer* timer = baseData->GetSharedParams().Timer;
	if(timer->isStopped()) 
	{
		timer->start();
	}
}

// DebugBreak()
void ParserExtensionCommon_DebugBreak( )
{
	_DEBUG_BREAK_IF(1);
}

void ParserExtensionCommon_Print( std::string& msg)
{
	msg += "\n";
	printf("%s", msg.c_str());
}

int ParserExtensionCommon_ExtractInt( std::string& strVal)
{
	stringc nrStr;
	for (u32 iCh = 0; iCh < strVal.size() ; iCh++)
	{	
		char c = strVal[iCh];		
		if(strchr("0123456789", c) != 0)// Это число
		{
			nrStr += c;
		}
	}
	
	return (s32)core::fast_atof(nrStr.c_str());
}


void ParserExtensionCommon_DrawImage( std::string& imgPath, bool use32bit, bool refreshScene)
{
	ParserUserData* parserUserData = ParserExtensionCommon_GetUserData();
	Base* baseData = parserUserData->BaseData;
	video::IVideoDriver* drv = baseData->GetSharedParams().Driver;
	if(refreshScene)
		drv->beginScene(true, true, video::SColor(255,0,0,0));
	baseData->GetSharedParams().Device->run();

	video::ITexture* texture = TextureWorker::GetTexture(drv, imgPath.c_str(), use32bit);
	if(texture != NULL)
	{
		TextureWorker::DrawTexture(drv, texture, 0, 0);
	}
	if(refreshScene)
		drv->endScene();
	baseData->GetSharedParams().Device->run();
}

void ParserExtensionCommon_RateApp(std::string& appId)
{
	ParserUserData* parserUserData = ParserExtensionCommon_GetUserData();
	Base* baseData = parserUserData->BaseData;
	EventParameters_t params;
	params.StrVar = stringc(appId.c_str());
	baseData->GetEvent()->PostEvent(Event::ID_MANAGER, MANAGER_EVENT_RATE_APP, params);
}

// Регистрация функций
void ParserExtensionCommon_RegisterFunctions( asIScriptEngine *engine )
{
	int r;
		
	r = engine->RegisterGlobalFunction("void Print(string &in)", asFUNCTION(ParserExtensionCommon_Print), asCALL_CDECL); _DEBUG_BREAK_IF( r < 0 );
	r = engine->RegisterGlobalFunction("int ExtractInt(string &in)", asFUNCTION(ParserExtensionCommon_ExtractInt), asCALL_CDECL); _DEBUG_BREAK_IF( r < 0 );

	r = engine->RegisterGlobalFunction("void DebugBreak()", asFUNCTION(ParserExtensionCommon_DebugBreak), asCALL_CDECL); _DEBUG_BREAK_IF( r < 0 );
	r = engine->RegisterGlobalFunction("uint GetNowTime()", asFUNCTION(ParserExtensionCommon_GetNowTime), asCALL_CDECL); _DEBUG_BREAK_IF( r < 0 );
	r = engine->RegisterGlobalFunction("string GetGlobalParameter(string &in)", asFUNCTION(ParserExtensionCommon_GetGlobalParameter), asCALL_CDECL); _DEBUG_BREAK_IF( r < 0 );
	r = engine->RegisterGlobalFunction("string GetUserSetting(string &in)", asFUNCTION(ParserExtensionCommon_GetUserSetting), asCALL_CDECL); _DEBUG_BREAK_IF( r < 0 );
	r = engine->RegisterGlobalFunction("int GetNodeId(int)", asFUNCTION(ParserExtensionCommon_GetNodeId), asCALL_CDECL); _DEBUG_BREAK_IF( r < 0 );
	r = engine->RegisterGlobalFunction("bool GetIsNodeExist(int)", asFUNCTION(ParserExtensionCommon_GetIsNodeExist), asCALL_CDECL); _DEBUG_BREAK_IF( r < 0 );
	r = engine->RegisterGlobalFunction("vector3df GetNodeRotation(int)", asFUNCTION(ParserExtensionCommon_GetNodeRotation), asCALL_CDECL); _DEBUG_BREAK_IF( r < 0 );
	r = engine->RegisterGlobalFunction("vector3df GetNodeAbsoluteRotation(int)", asFUNCTION(ParserExtensionCommon_GetNodeAbsolutRotation), asCALL_CDECL); _DEBUG_BREAK_IF( r < 0 );	
	r = engine->RegisterGlobalFunction("vector3df GetNodePosition(int)", asFUNCTION(ParserExtensionCommon_GetNodePosition), asCALL_CDECL); _DEBUG_BREAK_IF( r < 0 );
	r = engine->RegisterGlobalFunction("vector3df GetDirectRotation(int,int)", asFUNCTION(ParserExtensionCommon_GetDirectRotation), asCALL_CDECL); _DEBUG_BREAK_IF( r < 0 );
	r = engine->RegisterGlobalFunction("bool GetNodeVisible(int)", asFUNCTION(ParserExtensionCommon_GetNodeVisible), asCALL_CDECL); _DEBUG_BREAK_IF( r < 0 );

	r = engine->RegisterGlobalFunction("void SetGlobalParameter(string &in,string &in)", asFUNCTION(ParserExtensionCommon_SetGlobalParameter), asCALL_CDECL); _DEBUG_BREAK_IF( r < 0 );
	r = engine->RegisterGlobalFunction("void SetNodeRotation(int, vector3df &in)", asFUNCTION(ParserExtensionCommon_SetNodeRotation), asCALL_CDECL); _DEBUG_BREAK_IF( r < 0 );
	r = engine->RegisterGlobalFunction("void SetNodePosition(int, vector3df &in)", asFUNCTION(ParserExtensionCommon_SetNodePosition), asCALL_CDECL); _DEBUG_BREAK_IF( r < 0 );
	r = engine->RegisterGlobalFunction("void SetNodeAbsoluteRotation(int, vector3df &in)", asFUNCTION(ParserExtensionCommon_SetNodeAbsoluteRotation), asCALL_CDECL); _DEBUG_BREAK_IF( r < 0 );	
	r = engine->RegisterGlobalFunction("void SetNodeVisible(int, bool)", asFUNCTION(ParserExtensionCommon_SetNodeVisible), asCALL_CDECL); _DEBUG_BREAK_IF( r < 0 );
	r = engine->RegisterGlobalFunction("void SetUserSetting(string &in,string &in)", asFUNCTION(ParserExtensionCommon_SetUserSetting), asCALL_CDECL); _DEBUG_BREAK_IF( r < 0 );
	r = engine->RegisterGlobalFunction("void CompleteStage()", asFUNCTION(ParserExtensionCommon_CompleteStage), asCALL_CDECL); _DEBUG_BREAK_IF( r < 0 );
	r = engine->RegisterGlobalFunction("void StopUnitManager()", asFUNCTION(ParserExtensionCommon_StopUnitManager), asCALL_CDECL); _DEBUG_BREAK_IF( r < 0 );
	r = engine->RegisterGlobalFunction("void StartUnitManager()", asFUNCTION(ParserExtensionCommon_StartUnitManager), asCALL_CDECL); _DEBUG_BREAK_IF( r < 0 );
	r = engine->RegisterGlobalFunction("void RestartStage()", asFUNCTION(ParserExtensionCommon_RestartStage), asCALL_CDECL); _DEBUG_BREAK_IF( r < 0 );
	r = engine->RegisterGlobalFunction("void LoadStage(string &in)", asFUNCTION(ParserExtensionCommon_LoadStage), asCALL_CDECL); _DEBUG_BREAK_IF( r < 0 );
	r = engine->RegisterGlobalFunction("void SaveUserSettings()", asFUNCTION(ParserExtensionCommon_SaveUserSettings), asCALL_CDECL); _DEBUG_BREAK_IF( r < 0 );
	r = engine->RegisterGlobalFunction("void StopTimer()", asFUNCTION(ParserExtensionCommon_StopTimer), asCALL_CDECL); _DEBUG_BREAK_IF( r < 0 );
	r = engine->RegisterGlobalFunction("void StartTimer()", asFUNCTION(ParserExtensionCommon_StartTimer), asCALL_CDECL); _DEBUG_BREAK_IF( r < 0 );
	r = engine->RegisterGlobalFunction("void ShowAd()", asFUNCTION(ParserExtensionCommon_ShowAd), asCALL_CDECL); _DEBUG_BREAK_IF( r < 0 );
	r = engine->RegisterGlobalFunction("void HideAd()", asFUNCTION(ParserExtensionCommon_HideAd), asCALL_CDECL); _DEBUG_BREAK_IF( r < 0 );

	r = engine->RegisterGlobalFunction("void DrawImage(string &in, bool, bool)", asFUNCTION(ParserExtensionCommon_DrawImage), asCALL_CDECL); _DEBUG_BREAK_IF( r < 0 );

	r = engine->RegisterGlobalFunction("void RateApp(string &in)", asFUNCTION(ParserExtensionCommon_RateApp), asCALL_CDECL); _DEBUG_BREAK_IF( r < 0 );


}

