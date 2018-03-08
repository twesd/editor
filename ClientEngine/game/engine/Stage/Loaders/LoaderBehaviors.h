#pragma once
#include "Core/CompareType.h"
#include "Core/Xml/XMLCache.h"
#include "Core/Parsers/ScriptCache.h"
#include "../UnitPart/UnitBehavior.h"
#include "../UnitPart/UnitExecute/ExecuteBase.h"
#include "../UnitPart/UnitSelectSceneNodes/UnitSelectSceneNodeBase.h"
#include "../UnitPart/UnitModels/UnitModelBase.h"
#include "../../ModuleManager.h"

class LoaderBehaviors
{
public:
	// Загрузка описания поведения из файла
	static UnitBehavior* LoadFromFile(
		SharedParams_t params, 
		XMLCache* xmlCache, 
		ScriptCache* scriptCache, 
		ModuleManager* moduleManager,
		SoundManager* soundManager,
		stringc path);
private:
	// Загрузка из <TreeView>
	static core::array<UnitAction*> LoadNodesActions(
		SharedParams_t params, 
		rapidxml::xml_node<>* sourceNode, 
		stringc& root, 
		UnitBehavior* behaviors, 
		SoundManager* soundManager,
		ScriptCache* scriptCache);
	
	// Загрузка доступных анимаций
	static core::array<UnitAnimation*> LoadAnimations(
		SharedParams_t params, 
		rapidxml::xml_node<>* sourceNode, 
		stringc& root, 
		UnitBehavior* behaviors, 
		SoundManager* soundManager,
		ScriptCache* scriptCache);

	// Загрузка <Tag xsi:type="UnitAction">
	static UnitAction* LoadAction(
		SharedParams_t params, 
		rapidxml::xml_node<>* sourceNode, 
		stringc& root, 
		UnitBehavior* behaviors, 
		SoundManager* soundManager,
		ScriptCache* scriptCache);

	// Загрузка блока действий <Tag xsi:type="UnitBlockAction">
	static UnitAction* LoadBlockAction( 
		SharedParams_t params, 
		rapidxml::xml_node<>* sourceNode, 
		stringc& root, 
		UnitBehavior* behavior, 
		SoundManager* soundManager,
		ScriptCache* scriptCache );

	// Загрузка <Clause>
	static UnitClause* LoadUnitClause(SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc& root, UnitBehavior* behaviors, ScriptCache* scriptCache);
	
	static void LoadUnitClauseEvents(
		UnitClause* clause, 
		SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc& root, UnitBehavior* behavior, ScriptCache* scriptCache);

	// Загрузка действия
	static ExecuteBase* LoadExecute( 
		SharedParams_t params, 
		rapidxml::xml_node<>* sourceNode, 
		stringc& root, 
		UnitBehavior* behaviors, 
		SoundManager* soundManager,
		UnitAction* behavior, 
		ScriptCache* scriptCache );
	
	static ExecuteBase* LoadExecuteParameter( SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc& root, UnitBehavior* behaviors, UnitAction* behavior );
	static ExecuteBase* LoadExecuteCreateUnit( SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc& root, UnitBehavior* behaviors, UnitAction* behavior );
	static ExecuteBase* LoadExecuteDeleteUnit( SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc& root, UnitBehavior* behaviors, UnitAction* behavior );
	static ExecuteBase* LoadExecuteDeleteUnitsAll( SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc& root, UnitBehavior* behaviors, UnitAction* behavior );
	static UnitActionBreak* LoadBehaviorBreak( SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc& root, UnitBehavior* behaviors, ScriptCache* scriptCache );
	static UnitAnimation* LoadAnimation( SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc root );
	static UnitExprParameter* LoadUnitExprParameter( SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc& root, UnitBehavior* behavior, ScriptCache* scriptCache );
	static void LoadUnitStartParameters( SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc& root, UnitBehavior* behavior );
	static UnitModelBase* LoadUnitModel( SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc& root, UnitBehavior* behavior );
	static ExecuteBase* LoadExecuteTransform( SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc& root, UnitBehavior* behaviors, UnitAction* behavior );
	static ExecuteBase* LoadExecuteTextures( SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc& root, UnitBehavior* behaviors, UnitAction* behavior );
	static ExecuteBase* LoadExecuteMaterial( SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc& root, UnitBehavior* behaviors, UnitAction* behavior );
	static ExecuteBase* LoadExecuteMoveToPoint( SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc& root, UnitBehavior* behaviors, UnitAction* behavior );
	static ExecuteBase* LoadExecuteRotation( SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc& root, UnitBehavior* behaviors, UnitAction* behavior );
	static ExecuteBase* LoadExecuteDeleteSelf( SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc& root, UnitBehavior* behavior, UnitAction* action );
	static ExecuteBase* LoadExecuteExtAction( SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc& root, UnitBehavior* behavior, UnitAction* action );
	static ExecuteBase* LoadExecuteAddNextAction( SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc& root, UnitBehavior* behavior, UnitAction* action );
	static ExecuteBase* LoadExecuteGroup( SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc& root, UnitBehavior* behavior, SoundManager* soundManager, UnitAction* action, ScriptCache* scriptCache );
	static ExecuteBase* LoadExecuteExtParameter( SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc& root, UnitBehavior* behavior, UnitAction* action );
	static ExecuteBase* LoadExecuteMappingTransform( SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc& root, UnitBehavior* behavior, UnitAction* action );
	static ExecuteBase* LoadExecuteChangeSceneNodeId( SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc& root, UnitBehavior* behavior, UnitAction* action );
	static ExecuteBase* LoadExecuteMoveToSceneNode( SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc& root, UnitBehavior* behavior, UnitAction* action );
	static ExecuteBase* LoadExecuteScript( SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc& root, UnitBehavior* behavior, UnitAction* action, ScriptCache* scriptCache );
	static ExecuteBase* LoadExecuteSetData( SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc& root, UnitBehavior* behavior, UnitAction* action );
	static ExecuteBase* LoadExecuteSound( SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc& root, UnitBehavior* behavior, SoundManager* soundManager, UnitAction* action );
	static ExecuteBase* LoadExecuteTimer( SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc& root, UnitBehavior* behavior, SoundManager* soundManager, UnitAction* action );
	static ExecuteBase* LoadExecuteParticleAffector( SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc& root, UnitBehavior* behavior, SoundManager* soundManager, UnitAction* action );
	static ExecuteBase* LoadExecuteParticleEmitter( SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc& root, UnitBehavior* behavior, SoundManager* soundManager, UnitAction* action );
	static ExecuteBase* LoadExecuteColor( SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc& root, UnitBehavior* behavior, UnitAction* action );
};
