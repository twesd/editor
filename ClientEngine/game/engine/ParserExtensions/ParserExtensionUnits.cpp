#include "ParserExtensionUnits.h"
#include "Core/NodeWorker.h"
#include "../Stage/UnitManager.h"
#include "../ManagerEvents.h"


ParserUserData* ParserExtensionUnits_GetUserData()
{
	asIScriptContext* ctx = asGetActiveContext();
	asIScriptEngine* engine = ctx->GetEngine();
	ParserUserData* parserUserData = (ParserUserData*)engine->GetUserData();
	_DEBUG_BREAK_IF(parserUserData == NULL)
	_DEBUG_BREAK_IF(parserUserData->UnitInstanceData == NULL)
	return parserUserData;
}

// Данные юнита int GetData(string& name)
int ParserExtensionUnits_GetData(int instPointer, std::string& name )
{
	ParserUserData* parserUserData = ParserExtensionUnits_GetUserData();
	UnitInstanceStandard* instanceBase = (UnitInstanceStandard*)parserUserData->UnitInstanceData;

	UnitInstanceStandard* instance = (UnitInstanceStandard*)instPointer;
	_DEBUG_BREAK_IF(instance == NULL)
	bool exist = instanceBase->GetUnitManager()->GetIsUnitInstanceExist(instance);
	if (!exist) 
	{
		_DEBUG_BREAK_IF(!exist)
		return 0;
	}
	
	stringc namec = stringc(name.c_str());
	return (int)instance->GetData(namec);
}

// (int)void* GetThisNode()
int ParserExtensionUnits_GetThisNode(  )
{
	ParserUserData* parserUserData = ParserExtensionUnits_GetUserData();
	UnitInstanceStandard* instance = (UnitInstanceStandard*)parserUserData->UnitInstanceData;
	return (int)instance->SceneNode;
}

// (int)void* GetThisUnitInstance()
int ParserExtensionUnits_GetThisUnitInstance( )
{
	ParserUserData* parserUserData = ParserExtensionUnits_GetUserData();
	UnitInstanceStandard* instance = (UnitInstanceStandard*)parserUserData->UnitInstanceData;
	return (int)instance;
}

//string GetUnitInstanceParameter(instance,attrName)
std::string ParserExtensionUnits_GetUnitInstanceParameter( int instInt, std::string& name )
{
	ParserUserData* parserUserData = ParserExtensionUnits_GetUserData();
	UnitInstanceStandard* instanceBase = (UnitInstanceStandard*)parserUserData->UnitInstanceData;
	
	void* instPointer = (void*)instInt;	 
	UnitInstanceStandard* instance = (UnitInstanceStandard*)instPointer;
	_DEBUG_BREAK_IF(instance == NULL)
	bool exist = instanceBase->GetUnitManager()->GetIsUnitInstanceExist(instance);
	if(!exist)
	{
		_DEBUG_BREAK_IF(!exist)
		return std::string();
	}
	
	stringc paramVal = instance->GetBehavior()->GetParameters()->Get(name.c_str());
	return std::string(paramVal.c_str());
}

//void SetUnitInstanceParameter(instance,attrName,attrVal)
void ParserExtensionUnits_SetUnitInstanceParameter(int instInt, std::string& name, std::string& val)
{
	ParserUserData* parserUserData = ParserExtensionUnits_GetUserData();
	UnitInstanceStandard* instanceBase = (UnitInstanceStandard*)parserUserData->UnitInstanceData;

	void* instPointer = (void*)instInt;	 
	UnitInstanceStandard* instance = (UnitInstanceStandard*)instPointer;
	_DEBUG_BREAK_IF(instance == NULL)
	bool exist = instanceBase->GetUnitManager()->GetIsUnitInstanceExist(instance);
	if (!exist) 
	{
		_DEBUG_BREAK_IF(!exist)
		return;
	}
	stringc paramName = stringc(name.c_str());
	stringc paramVal = stringc(val.c_str());
	instance->GetBehavior()->GetParameters()->Set(paramName, paramVal);	
	//instance->GetBehavior()->AddCandidateNextAction("Select");
}

//void SetUnitInstanceParameter(instance,actionName)
void ParserExtensionUnits_SetUnitInstanceCandidateNextAction(int instInt, std::string& actionName)
{
	ParserUserData* parserUserData = ParserExtensionUnits_GetUserData();
	UnitInstanceStandard* instanceBase = (UnitInstanceStandard*)parserUserData->UnitInstanceData;

	void* instPointer = (void*)instInt;	 
	UnitInstanceStandard* instance = (UnitInstanceStandard*)instPointer;
	_DEBUG_BREAK_IF(instance == NULL)
	bool exist = instanceBase->GetUnitManager()->GetIsUnitInstanceExist(instance);
	if (!exist) 
	{
		_DEBUG_BREAK_IF(!exist)
		return;
	}
	stringc irrActionName = stringc(actionName.c_str());		
	instance->GetBehavior()->AddCandidateNextAction(irrActionName);
}

// SetData(name,val)
void ParserExtensionUnits_SetData(int instPointer, std::string& name, int val )
{
	ParserUserData* parserUserData = ParserExtensionUnits_GetUserData();
	UnitInstanceStandard* instanceBase = (UnitInstanceStandard*)parserUserData->UnitInstanceData;

	UnitInstanceStandard* instance = (UnitInstanceStandard*)instPointer;
	_DEBUG_BREAK_IF(instance == NULL)
	bool exist = instanceBase->GetUnitManager()->GetIsUnitInstanceExist(instance);
	if (!exist) 
	{
		_DEBUG_BREAK_IF(!exist)
		return;
	}

	stringc paramName = stringc(name.c_str());
	instance->SetData(paramName, (void*)val);
}

int ParserExtensionUnits_GetUnitInstanceByNode(int nodePointer)
{
	ParserUserData* parserUserData = ParserExtensionUnits_GetUserData();
	UnitInstanceStandard* instanceBase = (UnitInstanceStandard*)parserUserData->UnitInstanceData;

	 scene::ISceneManager* sceneManager = instanceBase->GetSharedParams().SceneManager;
	ISceneNode* node = (ISceneNode*)(nodePointer);
	if(!NodeWorker::GetIsNodeExist(node, sceneManager)) return 0;

	UnitInstanceBase* instance = instanceBase->GetUnitManager()->GetInstanceBySceneNode(node);
	return (int)instance;
}

// Получить создателя юнита
int ParserExtensionUnits_GetUnitInstanceCreator(int instPointer)
{
	ParserUserData* parserUserData = ParserExtensionUnits_GetUserData();
	UnitInstanceStandard* instanceBase = (UnitInstanceStandard*)parserUserData->UnitInstanceData;

	UnitInstanceStandard* instance = (UnitInstanceStandard*)instPointer;
	_DEBUG_BREAK_IF(instance == NULL)
	bool exist = instanceBase->GetUnitManager()->GetIsUnitInstanceExist(instance);
	if (!exist) 
	{
		_DEBUG_BREAK_IF(!exist)
		return 0;
	}

	return (int)instance->GetCreator();
}

int ParserExtensionUnits_GetThisUnitInstanceNode(int instPointer)
{
	ParserUserData* parserUserData = ParserExtensionUnits_GetUserData();
	UnitInstanceStandard* instanceBase = (UnitInstanceStandard*)parserUserData->UnitInstanceData;

	UnitInstanceStandard* instance = (UnitInstanceStandard*)instPointer;
	_DEBUG_BREAK_IF(instance == NULL)
	bool exist = instanceBase->GetUnitManager()->GetIsUnitInstanceExist(instance);
	if (!exist) 
	{
		_DEBUG_BREAK_IF(!exist)
		return 0;
	}

	return (int)instance->SceneNode;
}


// Регистрация функций
void ParserExtensionUnits_RegisterFunctions( asIScriptEngine *engine )
{
	int r;
	
	r = engine->RegisterGlobalFunction("int GetData(int, string &in)", asFUNCTION(ParserExtensionUnits_GetData), asCALL_CDECL); _DEBUG_BREAK_IF( r < 0 );
	r = engine->RegisterGlobalFunction("int GetThisNode()", asFUNCTION(ParserExtensionUnits_GetThisNode), asCALL_CDECL); _DEBUG_BREAK_IF( r < 0 );
	r = engine->RegisterGlobalFunction("int GetThisUnitInstance()", asFUNCTION(ParserExtensionUnits_GetThisUnitInstance), asCALL_CDECL); _DEBUG_BREAK_IF( r < 0 );
	r = engine->RegisterGlobalFunction("string GetUnitInstanceParameter(int, string &in)", asFUNCTION(ParserExtensionUnits_GetUnitInstanceParameter), asCALL_CDECL); _DEBUG_BREAK_IF( r < 0 );
	r = engine->RegisterGlobalFunction("int GetUnitInstanceByNode(int)", asFUNCTION(ParserExtensionUnits_GetUnitInstanceByNode), asCALL_CDECL); _DEBUG_BREAK_IF( r < 0 );
	r = engine->RegisterGlobalFunction("int GetUnitInstanceCreator(int)", asFUNCTION(ParserExtensionUnits_GetUnitInstanceCreator), asCALL_CDECL); _DEBUG_BREAK_IF( r < 0 );
	r = engine->RegisterGlobalFunction("int GetUnitInstanceNode(int)", asFUNCTION(ParserExtensionUnits_GetThisUnitInstanceNode), asCALL_CDECL); _DEBUG_BREAK_IF( r < 0 );	

	r = engine->RegisterGlobalFunction("void SetUnitInstanceParameter(int, string &in,string &in)", asFUNCTION(ParserExtensionUnits_SetUnitInstanceParameter), asCALL_CDECL); _DEBUG_BREAK_IF( r < 0 );
	r = engine->RegisterGlobalFunction("void SetUnitInstanceCandidateNextAction(int, string &in)", asFUNCTION(ParserExtensionUnits_SetUnitInstanceCandidateNextAction), asCALL_CDECL); _DEBUG_BREAK_IF( r < 0 );
	
	r = engine->RegisterGlobalFunction("void SetData(int,string &in,int)", asFUNCTION(ParserExtensionUnits_SetData), asCALL_CDECL); _DEBUG_BREAK_IF( r < 0 );

}

