#include "LoaderBehaviors.h"
#include "../../Controls/ControlEvent.h"
#include "Core/Animators/TextureAnimator.h"
#include "Core/TextureWorker.h"

#include "../UnitPart/UnitBlockAction.h"

#include "../UnitPart/UnitModels/UnitModelAnim.h"
#include "../UnitPart/UnitModels/UnitModelBillboard.h"
#include "../UnitPart/UnitModels/UnitModelSphere.h"
#include "../UnitPart/UnitModels/UnitModelEmpty.h"
#include "../UnitPart/UnitModels/UnitModelParticleSystem.h"
#include "../UnitPart/UnitModels/UnitModelVolumeLight.h"


#include "../UnitPart/UnitEvent/UnitEventControlButton.h"
#include "../UnitPart/UnitEvent/UnitEventControlTapScene.h"
#include "../UnitPart/UnitEvent/UnitEventControlCircle.h"
#include "../UnitPart/UnitEvent/UnitEventTimer.h"
#include "../UnitPart/UnitEvent/UnitEventDistance.h"
#include "../UnitPart/UnitEvent/UnitEventActionEnd.h"
#include "../UnitPart/UnitEvent/UnitEventAnimation.h"
#include "../UnitPart/UnitEvent/UnitEventSelection.h"
#include "../UnitPart/UnitEvent/UnitEventOperator.h"
#include "../UnitPart/UnitEvent/UnitEventScript.h"
#include "../UnitPart/UnitEvent/UnitEventChildUnit.h"

#include "../UnitPart/UnitExecute/ExecuteCreateUnit.h"
#include "../UnitPart/UnitExecute/ExecuteDeleteUnit.h"
#include "../UnitPart/UnitExecute/ExecuteDeleteUnitsAll.h"
#include "../UnitPart/UnitExecute/ExecuteTransform.h"
#include "../UnitPart/UnitExecute/ExecuteColor.h"
#include "../UnitPart/UnitExecute/ExecuteParameter.h"
#include "../UnitPart/UnitExecute/ExecuteTextures.h"
#include "../UnitPart/UnitExecute/ExecuteMaterial.h"
#include "../UnitPart/UnitExecute/ExecuteMoveToPoint.h"
#include "../UnitPart/UnitExecute/ExecuteDeleteSelf.h"
#include "../UnitPart/UnitExecute/ExecuteExtAction.h"
#include "../UnitPart/UnitExecute/ExecuteAddNextAction.h"
#include "../UnitPart/UnitExecute/ExecuteGroup.h"
#include "../UnitPart/UnitExecute/ExecuteExtParameter.h"
#include "../UnitPart/UnitExecute/ExecuteMappingTransform.h"
#include "../UnitPart/UnitExecute/ExecuteChangeSceneNodeId.h"
#include "../UnitPart/UnitExecute/ExecuteMoveToSceneNode.h"
#include "../UnitPart/UnitExecute/ExecuteScript.h"
#include "../UnitPart/UnitExecute/ExecuteSetData.h"
#include "../UnitPart/UnitExecute/ExecuteRotation.h"
#include "../UnitPart/UnitExecute/ExecuteSound.h"
#include "../UnitPart/UnitExecute/ExecuteTimer.h"

#include "../UnitPart/UnitExecute/ExecuteParticleEmitter.h"
#include "../UnitPart/UnitExecute/ExecuteParticleAffector.h"
#include "../UnitPart/UnitExecute/ExParticleEmitterBox.h"
#include "../UnitPart/UnitExecute/ExParticleAffectorFadeOut.h"
#include "../UnitPart/UnitExecute/ExParticleAffectorAllDirection.h"
#include "../UnitPart/UnitExecute/ExParticleAffectorEmmiterParam.h"
#include "../UnitPart/UnitExecute/ExParticleAffectorRotation.h"
#include "../UnitPart/UnitExecute/ExParticleAffectorScale.h"

#include "LoaderCommon.h"
#include "LoaderUnitSelectSceneNode.h"
#include "LoaderDataGetters.h"

using namespace rapidxml;

// Загрузка описания юнита <TreeView>
UnitBehavior* LoaderBehaviors::LoadFromFile(
		SharedParams_t params, 
		XMLCache* xmlCache, 
		ScriptCache* scriptCache, 
		ModuleManager* moduleManager,
		SoundManager* soundManager,
		stringc path)
{
	UnitBehavior* behavior = new UnitBehavior(params);
	behavior->OwnerPath = path;
	behavior->SetModuleManager(moduleManager);

	XMLFileDocument* xmlFileDoc = xmlCache->GetItem(path.c_str(), true);
#ifdef DEBUG_MESSAGE_PRINT
	if(!xmlFileDoc) 
	{
		printf("[ERROR] <LoaderBehaviors::LoadBehavior>[xmlFile](null pointer)\n");
		_DEBUG_BREAK_IF(true)
	}	
#endif
	stringc root = params.FileSystem->getFileDir(path) + "/";
	
	xml_document<>* doc = xmlFileDoc->GetDocument();
	xml_node<>* rootNode = doc->first_node();

	for (xml_node<>* chNode = rootNode->first_node(); chNode; chNode = chNode->next_sibling())
	{
		if(strcmp(chNode->name(), "UnitModel") == 0)		
		{
			behavior->UnitModel = LoadUnitModel(params, chNode, root, behavior);
		}
		else if(strcmp(chNode->name(), "NodeId") == 0)		
		{
			behavior->NodeId = XMLHelper::GetTextAsInt(chNode);
		}
		else if(strcmp(chNode->name(), "StartParameters") == 0)		
		{
			LoadUnitStartParameters(params, chNode, root, behavior);
		}
		else if(strcmp(chNode->name(), "ExprParameterExt") == 0)
		{
			for (xml_node<>* paramNode = chNode->first_node(); paramNode; paramNode = paramNode->next_sibling())
			{				
				if(strcmp(paramNode->name(), "UnitExprParameter") == 0)		
				{
					UnitExprParameter* exprParam = LoadUnitExprParameter(
						params, paramNode, root, behavior, scriptCache);
					behavior->AddExprParameterExt(exprParam);
				}
				else
				{
					_DEBUG_BREAK_IF(true)						
				}
			}
		}
		else if(strcmp(chNode->name(), "TreeView") == 0)
		{
			behavior->SetActions(
				LoadNodesActions(params, chNode, root, behavior, soundManager, scriptCache));
		}
		else if(strcmp(chNode->name(), "Animations") == 0)
		{
			behavior->SetAnimations(
				LoadAnimations(params, chNode, root, behavior, soundManager, scriptCache));
		}
		else if(strcmp(chNode->name(), "ScriptFileName") == 0)
		{
			stringc scriptPath(chNode->value());
			scriptPath.trim();
			if(scriptPath.size() > 0)
			{
				behavior->SetParser(scriptCache->GetItem(root + scriptPath));
			}
		}
		else if(strcmp(chNode->name(), "ModuleName") == 0)
		{
			behavior->ModuleName = 	stringc(chNode->value()).trim();
		}
		else if(strcmp(chNode->name(), "MainModel") == 0 ||
			strcmp(chNode->name(), "UnitBehavior") == 0 ||
			strcmp(chNode->name(), "EnviromentModels") == 0 ||
			strcmp(chNode->name(), "Camera") == 0 ||
			strcmp(chNode->name(), "ChildsBehaviorsPaths") == 0 ||
			strcmp(chNode->name(), "XRefPaths") == 0)
		{
			// Пропускаем таги редактора
		}
		else
		{
			_DEBUG_BREAK_IF(true)
		}
	}

	
	return behavior;
}


// Загрузка <UnitModel xsi:type="UnitModelAnim">
UnitModelBase* LoaderBehaviors::LoadUnitModel( SharedParams_t params, xml_node<>* sourceNode, stringc& root, UnitBehavior* behavior )
{	
	stringc xsi_type(sourceNode->first_attribute("xsi:type")->value());
	if(xsi_type == "UnitModelAnim")
	{
		UnitModelAnim* unitmodel = new UnitModelAnim(params);
		for (xml_node<>* chNode = sourceNode->first_node(); chNode; chNode = chNode->next_sibling())
		{
			if(strcmp(chNode->name(), "ModelPath") == 0)
			{
				unitmodel->ModelPath = root + stringc(chNode->value());
			}			
			else if(strcmp(chNode->name(), "Use32Bit") == 0)
			{
				unitmodel->Use32Bit = XMLHelper::GetTextAsBool(chNode);
			}			
			else if(strcmp(chNode->name(), "Culling") == 0)
			{
				unitmodel->Culling = XMLHelper::GetTextAsBool(chNode);
			}
			else
			{
				_DEBUG_BREAK_IF(true)
			}
		}
		return unitmodel;
	}
	else if(xsi_type == "UnitModelBillboard")
	{
		UnitModelBillboard* unitmodel = new UnitModelBillboard(params);
		for (xml_node<>* chNode = sourceNode->first_node(); chNode; chNode = chNode->next_sibling())
		{			
			if(strcmp(chNode->name(), "Width") == 0)
			{
				unitmodel->Width = XMLHelper::GetTextAsFloat(chNode);
			}
			else if(strcmp(chNode->name(), "Height") == 0)
			{
				unitmodel->Height = XMLHelper::GetTextAsFloat(chNode);
			}
			else if(strcmp(chNode->name(), "Texture") == 0)
			{
				stringc texturePath = stringc(chNode->value()).trim();
				if(texturePath != "")
					unitmodel->TexturePath = root + texturePath;
			}
			else if(strcmp(chNode->name(), "Use32Bit") == 0)
			{
				unitmodel->Use32Bit = XMLHelper::GetTextAsBool(chNode);
			}
			else if(strcmp(chNode->name(), "MaterialType") == 0)
			{
				unitmodel->MaterialType = LoaderCommon::GetMaterialType(stringc(chNode->value()));
			}
			else if(strcmp(chNode->name(), "UseUpVector") == 0)
			{
				unitmodel->UseUpVector = XMLHelper::GetTextAsBool(chNode);
			}
			else if(strcmp(chNode->name(), "UpVector") == 0)
			{
				unitmodel->UpVector = XMLHelper::GetVector3df(chNode);
			}
			else if(strcmp(chNode->name(), "UseViewVector") == 0)
			{
				unitmodel->UseViewVector = XMLHelper::GetTextAsBool(chNode);
			}
			else if(strcmp(chNode->name(), "ViewVector") == 0)
			{
				unitmodel->ViewVector = XMLHelper::GetVector3df(chNode);
			}
			else
			{
				_DEBUG_BREAK_IF(true)
			}
		}
		return unitmodel;
	}
	else if(xsi_type == "UnitModelSphere")
	{
		UnitModelSphere* unitmodel = new UnitModelSphere(params);
		for (xml_node<>* chNode = sourceNode->first_node(); chNode; chNode = chNode->next_sibling())
		{
			if(strcmp(chNode->name(), "Radius") == 0)
			{
				unitmodel->Radius = XMLHelper::GetTextAsFloat(chNode);
			}
			else if(strcmp(chNode->name(), "PolyCount") == 0)
			{
				unitmodel->PolyCount = XMLHelper::GetTextAsInt(chNode);
			}
			else
			{
				_DEBUG_BREAK_IF(true)
			}
		}
		return unitmodel;
	}
	else if(xsi_type == "UnitModelEmpty")
	{
		UnitModelEmpty* unitmodel = new UnitModelEmpty(params);
		return unitmodel;
	}
	else if(xsi_type == "UnitModelParticleSystem")
	{
		UnitModelParticleSystem* unitmodel = new UnitModelParticleSystem(params);
		return unitmodel;
	}
	else if(xsi_type == "UnitModelVolumeLight")
	{
		UnitModelVolumeLight* unitmodel = new UnitModelVolumeLight(params);
		for (xml_node<>* chNode = sourceNode->first_node(); chNode; chNode = chNode->next_sibling())
		{
			if(strcmp(chNode->name(), "SubdivU") == 0)
			{
				unitmodel->SubdivU = XMLHelper::GetTextAsUInt(chNode);
			}
			else if(strcmp(chNode->name(), "SubdivV") == 0)
			{
				unitmodel->SubdivV = XMLHelper::GetTextAsUInt(chNode);
			}
			else if(strcmp(chNode->name(), "Foot") == 0)
			{
				unitmodel->Foot = XMLHelper::GetTextAsSColor(chNode);
			}
			else if(strcmp(chNode->name(), "Tail") == 0)
			{
				unitmodel->Tail = XMLHelper::GetTextAsSColor(chNode);
			}
			else
			{
				_DEBUG_BREAK_IF(true)
			}
		}
		return unitmodel;
	}	
	else
	{
		_DEBUG_BREAK_IF(true)
	}
	_DEBUG_BREAK_IF(true);
	return NULL;
}


// Загрузка доступных анимаций
core::array<UnitAnimation*> LoaderBehaviors::LoadAnimations(
	SharedParams_t params, 
	xml_node<>* sourceNode, 
	stringc& root, 
	UnitBehavior* behavior, 
	SoundManager* soundManager,
	ScriptCache* scriptCache)
{
	core::array<UnitAnimation*> animations;	
	core::array<rapidxml::xml_node<>*> selectNodes = 
		XMLHelper::Select(sourceNode, "UnitAnimation");
	for(u32 iNode = 0; iNode < selectNodes.size(); iNode++)
	{
		rapidxml::xml_node<>* selectNode = selectNodes[iNode];

		UnitAnimation* anim = LoadAnimation(params, selectNode, root);
		animations.push_back(anim);
	}

	return animations;
}

// Загрузка из <TreeView>
core::array<UnitAction*> LoaderBehaviors::LoadNodesActions(
	SharedParams_t params, 
	xml_node<>* sourceNode, 
	stringc& root, 
	UnitBehavior* behavior, 
	SoundManager* soundManager,
	ScriptCache* scriptCache)
{
	core::array<UnitAction*> actionItems;	
	core::array<rapidxml::xml_node<>*> selectNodes = 
		XMLHelper::Select(sourceNode, "Nodes/SerializableTreeNode");
	for(u32 iNode = 0; iNode < selectNodes.size(); iNode++)
	{
		rapidxml::xml_node<>* selectNode = selectNodes[iNode];
				
		rapidxml::xml_node<>* tagNode = selectNode->first_node("Tag");
		if(tagNode == NULL)
		{
			rapidxml::xml_node<>* childNodes = selectNode->first_node("Nodes");
			rapidxml::xml_node<>* chNode = childNodes->first_node();
			if(chNode != NULL)
			{
				core::array<UnitAction*> childActions = LoadNodesActions(params,
					selectNode, root, behavior, soundManager, scriptCache);
				for (u32 iAct = 0; iAct < childActions.size() ; iAct++)
					actionItems.push_back(childActions[iAct]);
			}
		}

		if(tagNode == NULL) continue;
		rapidxml::xml_attribute<>* attr = tagNode->first_attribute("xsi:type");
		char* xsi_type = attr->value();
		if(strcmp(xsi_type, "UnitAction") == 0)
		{
			UnitAction* action = LoadAction(
				params, tagNode, root, behavior, soundManager, scriptCache);
			actionItems.push_back(action);
		}
		else if(strcmp(xsi_type, "UnitBlockAction") == 0)
		{
			UnitAction* action = LoadBlockAction(
				params, tagNode, root, behavior, soundManager, scriptCache);
			actionItems.push_back(action);
		}
		else if(strcmp(xsi_type, "XRefData") == 0 ||
			strcmp(xsi_type, "GroupData") == 0)
		{
			core::array<UnitAction*> xrefActions = LoadNodesActions(params,
				selectNode, root, behavior, soundManager, scriptCache);
			for (u32 iAct = 0; iAct < xrefActions.size() ; iAct++)
				actionItems.push_back(xrefActions[iAct]);
		}
		else
		{
			_DEBUG_BREAK_IF(true)
		}
	}

	return actionItems;
}

// Загрузка <Tag xsi:type="UnitAction">
UnitAction* LoaderBehaviors::LoadAction(
	SharedParams_t params, 
	xml_node<>* sourceNode, 
	stringc& root, 
	UnitBehavior* behavior, 
	SoundManager* soundManager,
	ScriptCache* scriptCache)
{
	UnitAction* action = new UnitAction(params);
	for (xml_node<>* chNode = sourceNode->first_node(); chNode; chNode = chNode->next_sibling())		
	{
		if(strcmp(chNode->name(), "Id") == 0)
		{
			action->Id = stringc(chNode->value());
		}
		else if(strcmp(chNode->name(), "Name") == 0)
		{
			action->Name = stringc(chNode->value());
		}
		else if(strcmp(chNode->name(), "AnimationId") == 0)
		{
            stringc animId = stringc(chNode->value());
			UnitAnimation* animation = behavior->GetAnimationById(animId);
			action->SetAnimation(animation);
		} 
		else if(strcmp(chNode->name(), "Priority") == 0)
		{
			action->Priority = XMLHelper::GetTextAsInt(chNode);
		}
		else if(strcmp(chNode->name(), "NoChangeCurrentAction") == 0)
		{
			action->NoChangeCurrentAction = XMLHelper::GetTextAsBool(chNode);
		}				
		else if(strcmp(chNode->name(), "FilterEvent") == 0)
		{
			action->FilterEvent = stringc(chNode->value());
		}
		else if(strcmp(chNode->name(), "Clause") == 0)
		{
			UnitClause* clause = LoadUnitClause(params, chNode, root, behavior, scriptCache);
			action->SetClause(clause);
			clause->drop();
		}
		else if(strcmp(chNode->name(), "Break") == 0)
		{
			UnitActionBreak* breakClause = LoadBehaviorBreak(params, chNode, root, behavior, scriptCache);
			action->SetBreak(breakClause, scriptCache);
			breakClause->drop();
		}
		else if(strcmp(chNode->name(), "Executes") == 0)
		{
			for (xml_node<>* exNode = chNode->first_node(); exNode; exNode = exNode->next_sibling())		
			{
				_DEBUG_BREAK_IF(strcmp(exNode->name(), "ExecuteBase") != 0)
				ExecuteBase* execute = LoadExecute(
					params, exNode, root, behavior, soundManager, action, scriptCache);
				action->AddExecute(execute);
				execute->drop();
			}
		}
		else if(strcmp(chNode->name(), "LinkPath") == 0)
		{
			// Пропускаем эти таги, они используются только в редакторе	
		}
		else
		{
			_DEBUG_BREAK_IF(true)
		}
	}
	return action;
}

// Загрузка блока действий <Tag xsi:type="UnitBlockAction">
UnitAction* LoaderBehaviors::LoadBlockAction( 
	SharedParams_t params, 
	xml_node<>* sourceNode, 
	stringc& root, 
	UnitBehavior* behavior, 
	SoundManager* soundManager,
	ScriptCache* scriptCache)
{
	UnitBlockAction* blockAction = new UnitBlockAction(params);
	for (xml_node<>* chNode = sourceNode->first_node(); chNode; chNode = chNode->next_sibling())		
	{
		if(strcmp(chNode->name(), "Id") == 0)
		{
			blockAction->Id = stringc(chNode->value());
		}
		else if(strcmp(chNode->name(), "Name") == 0)
		{
			blockAction->Name = stringc(chNode->value());
		}
		else if(strcmp(chNode->name(), "AnimationId") == 0)
		{
            stringc animId = stringc(chNode->value());
			UnitAnimation* animation = behavior->GetAnimationById(animId);
			blockAction->SetAnimation(animation);
		} 
		else if(strcmp(chNode->name(), "Priority") == 0)
		{
			blockAction->Priority = XMLHelper::GetTextAsInt(chNode);
		}
		else if(strcmp(chNode->name(), "NoChangeCurrentAction") == 0)
		{
			blockAction->NoChangeCurrentAction = XMLHelper::GetTextAsBool(chNode);
		}		
		else if(strcmp(chNode->name(), "FilterEvent") == 0)
		{
			blockAction->FilterEvent = stringc(chNode->value());
		}
		else if(strcmp(chNode->name(), "Clause") == 0)
		{
			UnitClause* clause = LoadUnitClause(params, chNode, root, behavior, scriptCache);
			blockAction->SetClause(clause);
			clause->drop();
		}
		else if(strcmp(chNode->name(), "Break") == 0)
		{
			UnitActionBreak* breakClause = LoadBehaviorBreak(params, chNode, root, behavior, scriptCache);
			blockAction->SetBreak(breakClause, scriptCache);
			breakClause->drop();
		}
		else if(strcmp(chNode->name(), "Executes") == 0)
		{
			for (xml_node<>* exNode = chNode->first_node(); exNode; exNode = exNode->next_sibling())		
			{
				_DEBUG_BREAK_IF(strcmp(exNode->name(), "ExecuteBase") != 0)
				ExecuteBase* execute = LoadExecute(
					params, exNode, root, behavior, soundManager, blockAction, scriptCache);
				blockAction->AddExecute(execute);
				execute->drop();
			}			
		}
		else if(strcmp(chNode->name(), "LinkPath") == 0)
		{
			// Пропускаем эти таги, они используются только в редакторе	
		}
		else
		{
			_DEBUG_BREAK_IF(true)
		}
	}

	// Переходим к дочерним объектам	
	core::array<UnitAction*> xrefActions = LoadNodesActions(params,
		sourceNode->parent(), root, behavior, soundManager, scriptCache);
	_DEBUG_BREAK_IF(xrefActions.size() == 0)
	for (u32 iAct = 0; iAct < xrefActions.size() ; iAct++)
		blockAction->AddAction(xrefActions[iAct]);


	return blockAction;
}


// Загрузка <Clause>
UnitClause* LoaderBehaviors::LoadUnitClause(
	SharedParams_t params, xml_node<>* sourceNode, stringc& root, UnitBehavior* behavior, ScriptCache* scriptCache)
{
	UnitClause* clause = new UnitClause(params);
	clause->SetBehavior(behavior);
		
	for (xml_node<>* chNode = sourceNode->first_node(); chNode; chNode = chNode->next_sibling())		
	{
		if(strcmp(chNode->name(), "Parameters") == 0)
		{
			for (xml_node<>* paramNode = chNode->first_node(); paramNode; paramNode = paramNode->next_sibling())		
			{
				if(strcmp(paramNode->name(), "Parameter") == 0)
				{
					stringc name = stringc(paramNode->first_node("Name")->value());
					stringc value = stringc(paramNode->first_node("Value")->value());
					clause->AddParameter(false, name, value);
				}
				else
				{
					_DEBUG_BREAK_IF(true)
				}
			}
		}
		else if(strcmp(chNode->name(), "GlobalParameters") == 0)
		{
			for (xml_node<>* paramNode = chNode->first_node(); paramNode; paramNode = paramNode->next_sibling())		
			{
				if(strcmp(paramNode->name(), "Parameter") == 0)
				{
					stringc name = stringc(paramNode->first_node("Name")->value());
					stringc value = stringc(paramNode->first_node("Value")->value());
					clause->AddParameter(true, name, value);
				}
				else
				{
					_DEBUG_BREAK_IF(true)
				}
			}
		}
		else if(strcmp(chNode->name(), "Events") == 0)
		{
			LoadUnitClauseEvents(clause, params, chNode, root, behavior, scriptCache);			
		} 
		else
		{
			_DEBUG_BREAK_IF(true)
		}
	}	
	return clause;
}

void LoaderBehaviors::LoadUnitClauseEvents(
	UnitClause* clause, SharedParams_t params, 	xml_node<>* sourceNode, 
	stringc& root, UnitBehavior* behavior, ScriptCache* scriptCache)
{
	for (xml_node<>* chNode = sourceNode->first_node(); chNode; chNode = chNode->next_sibling())		
	{
		char* evtType = chNode->first_attribute("xsi:type")->value();
		if(strcmp(evtType, "UnitEventControlButton") == 0)
		{
			UnitEventControlButton* unitEvent = new UnitEventControlButton(params);

			unitEvent->ButtonName = stringc(chNode->first_node("ButtonName")->value());
			stringc stateStr = stringc(chNode->first_node("State")->value());
			unitEvent->ButtonState = BUTTON_STATE_DOWN;
			if(stateStr == "Down")
				unitEvent->ButtonState = BUTTON_STATE_DOWN;
			else if(stateStr == "Up")
				unitEvent->ButtonState = BUTTON_STATE_UP;
			else if(stateStr == "Pressed")
				unitEvent->ButtonState = BUTTON_STATE_PRESSED;
			else if(stateStr == "Hold")
				unitEvent->ButtonState = BUTTON_STATE_HOLD;
			else
			{
				_DEBUG_BREAK_IF(true)
			}

			clause->AddUnitEvent(unitEvent);
			unitEvent->drop();
		}
		else if(strcmp(evtType, "UnitEventControlTapScene") == 0)
		{
			UnitEventControlTapScene* unitEvent = new UnitEventControlTapScene(params);

			for (xml_node<>* unitEvNode = chNode->first_node(); unitEvNode; unitEvNode = unitEvNode->next_sibling())		
			{
				if(strcmp(unitEvNode->name(), "Name") == 0)
				{
					// ничего	
				}
				else if(strcmp(unitEvNode->name(), "TapSceneName") == 0)
				{
					unitEvent->TapSceneName = stringc(unitEvNode->value());
				}
				else if(strcmp(unitEvNode->name(), "IgnoreNode") == 0)
				{
					unitEvent->IgnoreNode = XMLHelper::GetTextAsBool(unitEvNode);
				}
				else if(strcmp(unitEvNode->name(), "IdentNode") == 0)
				{
					unitEvent->IdentNode = XMLHelper::GetTextAsBool(unitEvNode);
				}
				else if(strcmp(unitEvNode->name(), "FilterId") == 0)
				{
					unitEvent->FilterId = XMLHelper::GetTextAsInt(unitEvNode);
				}
				else if(strcmp(unitEvNode->name(), "DataName") == 0)
				{
					unitEvent->DataName = stringc(unitEvNode->value()).trim();
				}
				else if(strcmp(unitEvNode->name(), "State") == 0)
				{
					stringc stateStr(unitEvNode->value()); 
					unitEvent->TapSceneState = BUTTON_STATE_DOWN;
					if(stateStr == "Down")
						unitEvent->TapSceneState = BUTTON_STATE_DOWN;
					else if(stateStr == "Up")
						unitEvent->TapSceneState = BUTTON_STATE_UP;
					else if(stateStr == "Pressed")
						unitEvent->TapSceneState = BUTTON_STATE_PRESSED;
					else if(stateStr == "Hold")
						unitEvent->TapSceneState = BUTTON_STATE_HOLD;
					else
					{
						_DEBUG_BREAK_IF(true)
					}
				}
				else
				{
					_DEBUG_BREAK_IF(true)
				}
			}

			clause->AddUnitEvent(unitEvent);
			unitEvent->drop();
		}
		else if(strcmp(evtType, "UnitEventControlCircle") == 0)
		{
			UnitEventControlCircle* unitEvent = new UnitEventControlCircle(params);

			for (xml_node<>* unitEvNode = chNode->first_node(); 
				unitEvNode; unitEvNode = unitEvNode->next_sibling())		
			{
				if(strcmp(unitEvNode->name(), "Name") == 0)
				{
					// ничего	
				}
				else if(strcmp(unitEvNode->name(), "ControlName") == 0)
				{
					unitEvent->ControlName = stringc(unitEvNode->value());
				}
				else if(strcmp(unitEvNode->name(), "States") == 0)
				{
					for (xml_node<>* evtStateNode = unitEvNode->first_node(); 
						evtStateNode; evtStateNode = evtStateNode->next_sibling())		
					{
						CONTROL_CIRCLE_STATE state;
						stringc stateStr = stringc(evtStateNode->value()); 										
						if(stateStr == "Down")
							state = CONTROL_CIRCLE_STATE_DOWN;
						else if(stateStr == "Up")
							state = CONTROL_CIRCLE_STATE_UP;
						else if(stateStr == "Pressed")
							state = CONTROL_CIRCLE_STATE_PRESSED;
						else 
						{
							_DEBUG_BREAK_IF(true);
						}

						unitEvent->States.push_back(state);
					}								
				}
				else
				{
					_DEBUG_BREAK_IF(true)
				}
			}


			clause->AddUnitEvent(unitEvent);
			unitEvent->drop();
		}
		else if(strcmp(evtType, "UnitEventTimer") == 0)
		{
			UnitEventTimer* unitEvent = new UnitEventTimer(params);				
			unitEvent->Interval = XMLHelper::GetTextAsUInt(chNode->first_node("Interval"));
			unitEvent->Loop = XMLHelper::GetTextAsBool(chNode->first_node("Loop"));
			xml_node<>* tNode = chNode->first_node("TimerName");
			if (tNode != NULL)
			{
				unitEvent->TimerName = stringc(tNode->value()).trim();
				unitEvent->UseStageTimer = XMLHelper::GetTextAsBool(chNode->first_node("UseStageTimer"));
			}

			clause->AddUnitEvent(unitEvent);
			unitEvent->drop();
		}
		else if(strcmp(evtType, "UnitEventDistance") == 0)
		{
			UnitEventDistance* unitEvent = new UnitEventDistance(params);

			for (xml_node<>* unitEvNode = chNode->first_node(); 
				unitEvNode; unitEvNode = unitEvNode->next_sibling())		
			{
				if(strcmp(unitEvNode->name(), "Distance") == 0)
				{
					unitEvent->Distance = XMLHelper::GetTextAsFloat(unitEvNode);
				}
				else if(strcmp(unitEvNode->name(), "CompareType") == 0)
				{
					stringc compareTypeStr = stringc(unitEvNode->value());
					unitEvent->CompareType = LoaderCommon::GetCompareType(compareTypeStr);
				}
				else if(strcmp(unitEvNode->name(), "FilterNodeId") == 0)
				{
					unitEvent->FilterNodeId = XMLHelper::GetTextAsInt(unitEvNode);
				}
				else if(strcmp(unitEvNode->name(), "UseUnitSize") == 0)
				{
					unitEvent->UseUnitSize = XMLHelper::GetTextAsBool(unitEvNode);
				}
				else if(strcmp(unitEvNode->name(), "DataName") == 0)
				{
					unitEvent->DataName = stringc(unitEvNode->value()).trim();
				}
				else if(strcmp(unitEvNode->name(), "Name") == 0)
				{
					// Пропускаем
				}
				else
				{
					_DEBUG_BREAK_IF(true)
				}
			}


			clause->AddUnitEvent(unitEvent);
			unitEvent->drop();
		}
		else if(strcmp(evtType, "UnitEventActionEnd") == 0)
		{
			UnitEventActionEnd* unitEvent = new UnitEventActionEnd(params);
			clause->AddUnitEvent(unitEvent);
			unitEvent->drop();
		}
		else if(strcmp(evtType, "UnitEventAnimation") == 0)
		{
			UnitEventAnimation* unitEvent = new UnitEventAnimation(params);
			for (xml_node<>* unitEvNode = chNode->first_node(); 
				unitEvNode; unitEvNode = unitEvNode->next_sibling())		
			{
				if(strcmp(unitEvNode->name(), "OnEnd") == 0)
				{
					unitEvent->OnEnd = XMLHelper::GetTextAsBool(unitEvNode);
				}
				else if(strcmp(unitEvNode->name(), "FrameNr") == 0)
				{
					unitEvent->FrameNr = XMLHelper::GetTextAsInt(unitEvNode);
				}
				else if(strcmp(unitEvNode->name(), "Name") == 0)
				{
					// Пропускаем
				}
				else
				{
					_DEBUG_BREAK_IF(true)
				}
			}
			clause->AddUnitEvent(unitEvent);
			unitEvent->drop();
		}	
		else if(strcmp(evtType, "UnitEventSelection") == 0)
		{
			UnitEventSelection* unitEvent = new UnitEventSelection(params);

			unitEvent->Count = XMLHelper::GetTextAsInt(chNode->first_node("Count"));
			for (xml_node<>* unitEvNode = chNode->first_node("SelectSceneNodes")->first_node(); 
				unitEvNode; unitEvNode = unitEvNode->next_sibling())		
			{
				if(strcmp(unitEvNode->name(), "UnitSelectSceneNodeBase") == 0)
				{
					UnitSelectSceneNodeBase* selector = 
						LoaderUnitSelectSceneNode::LoadUnitSelectSceneNodeBase(
						params, unitEvNode, root, behavior, NULL);
					unitEvent->AddSelector(selector);
					selector->drop();
				}
				else 
				{
					_DEBUG_BREAK_IF(true)
				}
			}

			clause->AddUnitEvent(unitEvent);
			unitEvent->drop();
		}
		else if(strcmp(evtType, "UnitEventOperator") == 0)
		{
			UnitEventOperator* unitEvent = new UnitEventOperator(params);

			unitEvent->Operator = LoaderCommon::GetOperatorType(
				stringc(chNode->first_node("Operator")->value()));

			clause->AddUnitEvent(unitEvent);
			unitEvent->drop();
		}
		else if(strcmp(evtType, "UnitEventScript") == 0)
		{
			UnitEventScript* unitEvent = new UnitEventScript(params);				
			stringc scriptFilePath = root + 
				stringc(chNode->first_node("ScriptFileName")->value());
			unitEvent->SetParser(scriptCache->GetItem(scriptFilePath));
			clause->AddUnitEvent(unitEvent);
			unitEvent->drop();
		}
		else if(strcmp(evtType, "UnitEventChildUnit") == 0)
		{
			UnitEventChildUnit* unitEvent = new UnitEventChildUnit(params);

			for (xml_node<>* unitEvNode = chNode->first_node(); 
				unitEvNode; unitEvNode = unitEvNode->next_sibling())		
			{
				if(strcmp(unitEvNode->name(), "ChildPath") == 0)
				{
					unitEvent->ChildPath = stringc(unitEvNode->value());
				}
				else if(strcmp(unitEvNode->name(), "Parameters") == 0)
				{
					for (xml_node<>* paramNode = unitEvNode->first_node(); 
						paramNode; paramNode = paramNode->next_sibling())	
					{
						if(strcmp(paramNode->name(), "Parameter") == 0)
						{
							stringc name = stringc(paramNode->first_node("Name")->value());
							stringc valueStr = stringc(paramNode->first_node("Value")->value());
							unitEvent->Parameters->Set(name, valueStr, true);
						}
						else
						{
							_DEBUG_BREAK_IF(true)
						}
					}
				}
				else if(strcmp(unitEvNode->name(), "Name") == 0)
				{
					// Пропускаем
				}
				else
				{
					_DEBUG_BREAK_IF(true)
				}
			}
			unitEvent->SetRoot(root);
			clause->AddUnitEvent(unitEvent);
			unitEvent->drop();
		}
		else
		{
	#ifdef DEBUG_MESSAGE_PRINT
			printf("[ERROR] <LoaderBehaviors::LoadBehaviorClause>[unknown type](%s)\n", evtType);
	#endif
			_DEBUG_BREAK_IF(true)
		}
	}
}


// Загрузка действия <ExecuteBase>
ExecuteBase* LoaderBehaviors::LoadExecute( 
	SharedParams_t params, 
	xml_node<>* sourceNode, 
	stringc& root, 
	UnitBehavior* behavior, 
	SoundManager* soundManager,
	UnitAction* action, 
	ScriptCache* scriptCache )
{
	char* exType = sourceNode->first_attribute("xsi:type")->value();

	ExecuteBase* executeBase;

	// Загружаем базовые параметры
	//
	UnitClause* clause = NULL;
	xml_node<>* clauseNode = sourceNode->first_node("Clause");
	if (clauseNode != NULL)
	{
		clause = LoadUnitClause(params, clauseNode, root, behavior, scriptCache);
	}
	else
	{
		_DEBUG_BREAK_IF(true)
	}

	if(strcmp(exType, "ExecuteParameter") == 0)
	{
		executeBase = LoadExecuteParameter(params, sourceNode, root, behavior, action);
	}
	else if(strcmp(exType, "ExecuteCreateUnit") == 0)
	{
		executeBase = LoadExecuteCreateUnit(params, sourceNode, root, behavior, action);
	}
	else if(strcmp(exType, "ExecuteDeleteUnit") == 0)
	{
		executeBase = LoadExecuteDeleteUnit(params, sourceNode, root, behavior, action);
	}
	else if(strcmp(exType, "ExecuteDeleteUnitsAll") == 0)
	{
		executeBase = LoadExecuteDeleteUnitsAll(params, sourceNode, root, behavior, action);
	}
	else if(strcmp(exType, "ExecuteTransform") == 0)
	{
		executeBase = LoadExecuteTransform(params, sourceNode, root, behavior, action);
	}
	else if(strcmp(exType, "ExecuteColor") == 0)
	{
		executeBase = LoadExecuteColor(params, sourceNode, root, behavior, action);
	}	
	else if(strcmp(exType, "ExecuteTextures") == 0)
	{
		executeBase = LoadExecuteTextures(params, sourceNode, root, behavior, action);
	}
	else if(strcmp(exType, "ExecuteMaterial") == 0)
	{
		executeBase = LoadExecuteMaterial(params, sourceNode, root, behavior, action);
	}
	else if(strcmp(exType, "ExecuteMoveToPoint") == 0)
	{
		executeBase = LoadExecuteMoveToPoint(params, sourceNode, root, behavior, action);
	}
	else if(strcmp(exType, "ExecuteDeleteSelf") == 0)
	{
		executeBase = LoadExecuteDeleteSelf(params, sourceNode, root, behavior, action);
	}
	else if(strcmp(exType, "ExecuteExtAction") == 0)
	{
		executeBase = LoadExecuteExtAction(params, sourceNode, root, behavior, action);
	}
	else if(strcmp(exType, "ExecuteExtParameter") == 0)
	{
		executeBase = LoadExecuteExtParameter(params, sourceNode, root, behavior, action);
	}
	else if(strcmp(exType, "ExecuteAddNextAction") == 0)
	{
		executeBase = LoadExecuteAddNextAction(params, sourceNode, root, behavior, action);
	}
	else if(strcmp(exType, "ExecuteGroup") == 0)
	{
		executeBase = LoadExecuteGroup(params, sourceNode, root, behavior, soundManager, action, scriptCache);
	}
	else if(strcmp(exType, "ExecuteMappingTransform") == 0)
	{
		executeBase = LoadExecuteMappingTransform(params, sourceNode, root, behavior, action);
	}
	else if(strcmp(exType, "ExecuteChangeSceneNodeId") == 0)
	{
		executeBase = LoadExecuteChangeSceneNodeId(params, sourceNode, root, behavior, action);
	}
	else if(strcmp(exType, "ExecuteMoveToSceneNode") == 0)
	{
		executeBase = LoadExecuteMoveToSceneNode(params, sourceNode, root, behavior, action);
	}
	else if(strcmp(exType, "ExecuteScript") == 0)
	{
		executeBase = LoadExecuteScript(params, sourceNode, root, behavior, action, scriptCache);
	}	
	else if(strcmp(exType, "ExecuteSetData") == 0)
	{
		executeBase = LoadExecuteSetData(params, sourceNode, root, behavior, action);
	}
	else if(strcmp(exType, "ExecuteRotation") == 0)
	{
		executeBase = LoadExecuteRotation(params, sourceNode, root, behavior, action);
	}	
	else if(strcmp(exType, "ExecuteSound") == 0)
	{
		executeBase = LoadExecuteSound(params, sourceNode, root, behavior, soundManager, action);
	}
	else if(strcmp(exType, "ExecuteTimer") == 0)
	{
		executeBase = LoadExecuteTimer(params, sourceNode, root, behavior, soundManager, action);
	}
	else if(strcmp(exType, "ExecuteParticleAffector") == 0)
	{
		executeBase = LoadExecuteParticleAffector(params, sourceNode, root, behavior, soundManager, action);
	}
	else if(strcmp(exType, "ExecuteParticleEmitter") == 0)
	{
		executeBase = LoadExecuteParticleEmitter(params, sourceNode, root, behavior, soundManager, action);
	}
	else
	{
		_DEBUG_BREAK_IF(true)
	}

	// Устанавливаем условия
	if (clause != NULL)
		executeBase->SetClause(clause);

	return executeBase;	
}

// Загрузка <ExecuteBase xsi:type="ExecuteParameter">
ExecuteBase* LoaderBehaviors::LoadExecuteParameter( SharedParams_t params, xml_node<>* sourceNode, stringc& root, UnitBehavior* behavior, UnitAction* action )
{
	ExecuteParameter* execute = new ExecuteParameter(params);
	execute->SetBehavior(behavior);
	for (xml_node<>* chNode = sourceNode->first_node(); chNode; chNode = chNode->next_sibling())		
	{
		if (strcmp(chNode->name(), "IsGlobal") == 0)
		{
			execute->IsGlobal = XMLHelper::GetTextAsBool(chNode);
		}
		else if (strcmp(chNode->name(), "Parameters") == 0)
		{
			for (xml_node<>* paramNode = chNode->first_node(); paramNode; paramNode = paramNode->next_sibling())		
			{
				stringc name = stringc(paramNode->first_node("Name")->value());
				stringc val = stringc(paramNode->first_node("Value")->value());
				execute->AddParameter(name, val);
			}
		}
		else if(strcmp(chNode->name(), "Clause") == 0)
		{
			// ничего
		}
		else
		{
			_DEBUG_BREAK_IF(true)
		}
	}	
	return execute;	
}

// Загрузка <ExecuteBase xsi:type="ExecuteCreateUnit">
ExecuteBase* LoaderBehaviors::LoadExecuteCreateUnit( SharedParams_t params, xml_node<>* sourceNode, stringc& root, UnitBehavior* behaviors, UnitAction* behavior )
{
	ExecuteCreateUnit* execute = new ExecuteCreateUnit(params);
	execute->SetRoot(root);

	for (xml_node<>* chNode = sourceNode->first_node(); chNode; chNode = chNode->next_sibling())		
	{
		if(strcmp(chNode->name(), "BehaviorsPath") == 0)
		{
			execute->BehaviorsPath = stringc(chNode->value());					
		}
		else if(strcmp(chNode->name(), "Position") == 0)
		{					
			execute->SetStartPosition(XMLHelper::GetVector3df(chNode));
		}
		else if(strcmp(chNode->name(), "Rotation") == 0)
		{					
			execute->SetStartRotation(XMLHelper::GetVector3df(chNode));
		}
		else if(strcmp(chNode->name(), "AllowSeveralInstances") == 0)
		{					
			execute->AllowSeveralInstances = XMLHelper::GetTextAsBool(chNode);
		}
		else if(strcmp(chNode->name(), "CreationType") == 0)
		{					
			stringc creationType = stringc(chNode->value());
			if (creationType == "Child")
			{
				execute->CreationType = CreationType_Child;
			}
			else if (creationType == "External")
			{
				execute->CreationType = CreationType_Externals;
			}
			else
			{
				_DEBUG_BREAK_IF(true)
			}
		}
		else if(strcmp(chNode->name(), "GetPositionFromTapScene") == 0)
		{
			execute->GetPositionFromTapScene = XMLHelper::GetTextAsBool(chNode);
		}
		else if(strcmp(chNode->name(), "TapSceneName") == 0)
		{
			execute->TapSceneName = stringc(chNode->value());
		}
		else if(strcmp(chNode->name(), "JointName") == 0)
		{
			execute->JointName = stringc(chNode->value()).trim();
		}
		else if(strcmp(chNode->name(), "StartScriptFileName") == 0)
		{
			execute->StartScriptFileName = stringc(chNode->value()).trim();
		}
		else if(strcmp(chNode->name(), "Clause") == 0)
		{
			// ничего
		}
		else
		{
			_DEBUG_BREAK_IF(true)
		}
	}	
	return execute;	
}

// Загрузка <ExecuteBase xsi:type="ExecuteDeleteUnit">
ExecuteBase* LoaderBehaviors::LoadExecuteDeleteUnit( SharedParams_t params, xml_node<>* sourceNode, stringc& root, UnitBehavior* behaviors, UnitAction* behavior )
{
	ExecuteDeleteUnit* execute = new ExecuteDeleteUnit(params);
	execute->SetRoot(root);

	for (xml_node<>* chNode = sourceNode->first_node(); chNode; chNode = chNode->next_sibling())		
	{
		if(strcmp(chNode->name(), "BehaviorsPath") == 0)
		{
			execute->BehaviorsPath = stringc(chNode->value());					
		}
		else if(strcmp(chNode->name(), "Clause") == 0)
		{
			// ничего
		}
		else
		{
			_DEBUG_BREAK_IF(true)
		}
	}	
	return execute;	
}

ExecuteBase* LoaderBehaviors::LoadExecuteDeleteUnitsAll( SharedParams_t params, xml_node<>* sourceNode, stringc& root, UnitBehavior* behaviors, UnitAction* behavior )
{
	ExecuteDeleteUnitsAll* execute = new ExecuteDeleteUnitsAll(params);

	for (xml_node<>* chNode = sourceNode->first_node(); chNode; chNode = chNode->next_sibling())		
	{
		if(strcmp(chNode->name(), "JointName") == 0)
		{
			execute->JointName = stringc(chNode->value());					
		}
		else if(strcmp(chNode->name(), "Clause") == 0)
		{
			// ничего
		}
		else
		{
			_DEBUG_BREAK_IF(true)
		}
	}	
	return execute;	
}


// Загрузка <Break>
UnitActionBreak* LoaderBehaviors::LoadBehaviorBreak( SharedParams_t params, xml_node<>* sourceNode, stringc& root, UnitBehavior* behaviors, ScriptCache* scriptCache )
{
	UnitActionBreak* breakClause = new UnitActionBreak(params);
	for (xml_node<>* chNode = sourceNode->first_node(); chNode; chNode = chNode->next_sibling())		
	{
		if(strcmp(chNode->name(), "StartClauseNotApproved") == 0)
		{
			breakClause->StartClauseNotApproved = XMLHelper::GetTextAsBool(chNode);
		}
		else if(strcmp(chNode->name(), "StartClauseApproved") == 0)
		{
			breakClause->StartClauseApproved = XMLHelper::GetTextAsBool(chNode);
		}
		else if(strcmp(chNode->name(), "AnimationEnd") == 0)
		{
			breakClause->AnimationEnd = XMLHelper::GetTextAsBool(chNode);
		}
		else if(strcmp(chNode->name(), "IsExecuteOnly") == 0)
		{
			breakClause->IsExecuteOnly = XMLHelper::GetTextAsBool(chNode);
		}
		else if(strcmp(chNode->name(), "AnimatorEnd") == 0)
		{
			stringc animatorEnd = stringc(chNode->value());
			breakClause->AnimatorEnd = UnitActionBreak::AnimatorNone;
			if (animatorEnd == "Any")					
				breakClause->AnimatorEnd = UnitActionBreak::AnimatorAny;
			else if (animatorEnd == "MoveToPoint")
				breakClause->AnimatorEnd = UnitActionBreak::AnimatorMoveToPoint;
			else if (animatorEnd == "MoveToSceneNode")
				breakClause->AnimatorEnd = UnitActionBreak::AnimatorMoveToSceneNode;
			else if (animatorEnd != "None")
			{
				_DEBUG_BREAK_IF(true)
			}
		}
		else if(strcmp(chNode->name(), "ScriptFileName") == 0)
		{
			stringc path = stringc(chNode->value()).trim();
			if(path != "")
				breakClause->ScriptFileName = root + path;
		}
		else if(strcmp(chNode->name(), "Clause") == 0)
		{
			// ничего
		}
		else
		{
			_DEBUG_BREAK_IF(true)
		}
	}	
	return breakClause;	
}

// Загрузка <Animation>
UnitAnimation* LoaderBehaviors::LoadAnimation( SharedParams_t params, xml_node<>* sourceNode, stringc root )
{
	UnitAnimation* animation = new UnitAnimation(params);
	for (xml_node<>* chNode = sourceNode->first_node(); chNode; chNode = chNode->next_sibling())		
	{
		if(strcmp(chNode->name(), "Id") == 0)
		{
			animation->Id = stringc(chNode->value());					
		}
		else if(strcmp(chNode->name(), "Name") == 0)
		{
			animation->Name = stringc(chNode->value());					
		}
		else if(strcmp(chNode->name(), "StartFrame") == 0)
		{
			animation->StartFrame = XMLHelper::GetTextAsInt(chNode);					
		}
		else if(strcmp(chNode->name(), "EndFrame") == 0)
		{
			animation->EndFrame = XMLHelper::GetTextAsInt(chNode);					
		}
		else if(strcmp(chNode->name(), "Speed") == 0)
		{
			animation->Speed = XMLHelper::GetTextAsInt(chNode);					
		}
		else if(strcmp(chNode->name(), "Loop") == 0)
		{
			animation->Loop = XMLHelper::GetTextAsBool(chNode);					
		}
		else
		{
			_DEBUG_BREAK_IF(true)
		}
	}
	return animation;
}

// Загрузить <ExecuteBase xsi:type="UnitTransform">
ExecuteBase* LoaderBehaviors::LoadExecuteTransform( SharedParams_t params, xml_node<>* sourceNode, stringc& root, UnitBehavior* behaviors, UnitAction* behavior )
{
	ExecuteTransform* execute = new ExecuteTransform(params);

	core::array<vector3df> points;
	core::array<u32> timesForWay;
	stringc transformType;
	bool loop;
	int obstacleFilterId = 0;

	for (xml_node<>* chNode = sourceNode->first_node(); chNode; chNode = chNode->next_sibling())		
	{
		if(strcmp(chNode->name(), "Type") == 0)
		{
			transformType = stringc(chNode->value());
		}
		else if(strcmp(chNode->name(), "Name") == 0)
		{
			// ничего
		}
		else if(strcmp(chNode->name(), "Loop") == 0)
		{
			loop = XMLHelper::GetTextAsBool(chNode);
		}
		else if(strcmp(chNode->name(), "ObstacleFilterId") == 0)
		{
			obstacleFilterId = XMLHelper::GetTextAsInt(chNode);
		}
		else if(strcmp(chNode->name(), "Items") == 0)
		{
			for (xml_node<>* itemNode = chNode->first_node();
				itemNode; itemNode = itemNode->next_sibling())
			{	
				_DEBUG_BREAK_IF(strcmp(itemNode->name(), "TransformItem") != 0)
				vector3df point;
				u32 time;
				for (xml_node<>* itemNodeData = itemNode->first_node();
					itemNodeData; itemNodeData = itemNodeData->next_sibling())
				{
					if (strcmp(itemNodeData->name(), "X") == 0)
					{
						point.X = XMLHelper::GetTextAsFloat(itemNodeData);
					}						
					else if (strcmp(itemNodeData->name(), "Y") == 0)
					{
						point.Y = XMLHelper::GetTextAsFloat(itemNodeData);
					}
					else if (strcmp(itemNodeData->name(), "Z") == 0)
					{
						point.Z = XMLHelper::GetTextAsFloat(itemNodeData);
					}
					else if (strcmp(itemNodeData->name(), "Time") == 0)
					{
						time = XMLHelper::GetTextAsUInt(itemNodeData);
					}
					else
					{
						_DEBUG_BREAK_IF(true)
					}
				}
				points.push_back(point);
				timesForWay.push_back(time);
			}
		}
		else if(strcmp(chNode->name(), "Clause") == 0)
		{
			// ничего
		}
		else
		{
			_DEBUG_BREAK_IF(true)
		}
	}

	execute->SetAnimator(transformType, points, timesForWay, loop, obstacleFilterId);
	return execute;	
}

// Загрузить <ExecuteBase xsi:type="ExecuteTextures">
ExecuteBase* LoaderBehaviors::LoadExecuteTextures( SharedParams_t params, xml_node<>* sourceNode, stringc& root, UnitBehavior* behaviors, UnitAction* behavior )
{
	ExecuteTextures* execute = new ExecuteTextures(params);

	core::array<video::ITexture*> textures;
	u32 timePerFrame = 1;
	bool loop = false;
	bool use32bit = false;

	core::array<stringc> texturePaths;
	for (xml_node<>* chNode = sourceNode->first_node(); chNode; chNode = chNode->next_sibling())		
	{
		if(strcmp(chNode->name(), "TimePerFrame") == 0)
		{
			timePerFrame = XMLHelper::GetTextAsUInt(chNode);
		}
		else if(strcmp(chNode->name(), "Loop") == 0)
		{
			loop = XMLHelper::GetTextAsBool(chNode);
		}
		else if(strcmp(chNode->name(), "Use32Bit") == 0)
		{
			use32bit = XMLHelper::GetTextAsBool(chNode);
		}
		else if(strcmp(chNode->name(), "Paths") == 0)
		{
			for (xml_node<>* txtNode = chNode->first_node(); txtNode; txtNode = txtNode->next_sibling())
			{
				stringc path = stringc(txtNode->value());
				texturePaths.push_back(root + path);
			}
		}
		else if(strcmp(chNode->name(), "Clause") == 0)
		{
			// ничего
		}
		else
		{
			_DEBUG_BREAK_IF(true)
		}
	}


	video::IVideoDriver* driver = params.Driver;

	for (u32 i = 0; i < texturePaths.size() ; i++)
	{
		video::ITexture* texture = TextureWorker::GetTexture(driver, texturePaths[i], use32bit);
		if (texture != NULL)
		{
			textures.push_back(texture);
		}
	}	

	TextureAnimator* textureAnimator = new TextureAnimator(params);
	textureAnimator->Init(textures, timePerFrame, loop);
	execute->SetAnimator(textureAnimator);

	return execute;	
}

// Загрузить <ExecuteBase xsi:type="ExecuteMaterial">
ExecuteBase* LoaderBehaviors::LoadExecuteMaterial( SharedParams_t params, xml_node<>* sourceNode, stringc& root, UnitBehavior* behaviors, UnitAction* behavior )
{
	ExecuteMaterial* execute = new ExecuteMaterial(params);

	for (xml_node<>* chNode = sourceNode->first_node(); chNode; chNode = chNode->next_sibling())		
	{
		if(strcmp(chNode->name(), "Type") == 0)
		{
			stringc matType = stringc(chNode->value());
			execute->MaterialType = LoaderCommon::GetMaterialType(matType);
		}
		else if(strcmp(chNode->name(), "Clause") == 0)
		{
			// ничего
		}
		else
		{
			_DEBUG_BREAK_IF(true)
		}
	}

	return execute;		
}

// Загрузить <ExecuteBase xsi:type="ExecuteMoveToPoint">
ExecuteBase* LoaderBehaviors::LoadExecuteMoveToPoint( SharedParams_t params, xml_node<>* sourceNode, stringc& root, UnitBehavior* behaviors, UnitAction* behavior )
{
	ExecuteMoveToPoint* execute = new ExecuteMoveToPoint(params);

	for (xml_node<>* chNode = sourceNode->first_node(); chNode; chNode = chNode->next_sibling())		
	{
		if(strcmp(chNode->name(), "GetPositionFromTapControl") == 0)
		{
			execute->GetPositionFromTapControl = XMLHelper::GetTextAsBool(chNode);
		}
		else if(strcmp(chNode->name(), "TapSceneName") == 0)
		{
			execute->TapSceneName = stringc(chNode->value());
		}
		else if(strcmp(chNode->name(), "TargetPosition") == 0)
		{
			execute->TargetPosition = XMLHelper::GetVector3df(chNode);
		}
		else if(strcmp(chNode->name(), "Speed") == 0)
		{
			execute->Speed = XMLHelper::GetTextAsFloat(chNode);
		}
		else if(strcmp(chNode->name(), "TargetDist") == 0)
		{
			execute->TargetDist = XMLHelper::GetTextAsFloat(chNode);
		}
		else if(strcmp(chNode->name(), "ObstacleFilterId") == 0)
		{
			execute->ObstacleFilterId = XMLHelper::GetTextAsInt(chNode);
		}
		else if(strcmp(chNode->name(), "Clause") == 0)
		{
			// ничего
		}
		else
		{
			_DEBUG_BREAK_IF(true)
		}
	}

	execute->CompleteLoading();
	return execute;	
}


// Загрузить <ExecuteBase xsi:type="ExecuteRotation">
ExecuteBase* LoaderBehaviors::LoadExecuteRotation( SharedParams_t params, xml_node<>* sourceNode, stringc& root, UnitBehavior* behaviors, UnitAction* behavior )
{
	ExecuteRotation* execute = new ExecuteRotation(params);

	for (xml_node<>* chNode = sourceNode->first_node(); chNode; chNode = chNode->next_sibling())		
	{
		if(strcmp(chNode->name(), "AddAngleFromControlCircle") == 0)
		{
			execute->AddAngleFromControlCircle = XMLHelper::GetTextAsBool(chNode);
		}
		else if(strcmp(chNode->name(), "ControlCircleName") == 0)
		{
			execute->ControlCircleName = stringc(chNode->value());
		}
		else if(strcmp(chNode->name(), "Speed") == 0)
		{
			execute->Speed = XMLHelper::GetTextAsFloat(chNode);
		}
		else if(strcmp(chNode->name(), "Rotation") == 0)
		{
			execute->Rotation = XMLHelper::GetVector3df(chNode);
		}
		else if(strcmp(chNode->name(), "Absolute") == 0)
		{
			execute->Absolute = XMLHelper::GetTextAsBool(chNode);
		}
		else if(strcmp(chNode->name(), "Clause") == 0)
		{
			// ничего
		}
		else
		{
			_DEBUG_BREAK_IF(true)
		}
	}

	return execute;	
}

// Загрузить <ExecuteBase xsi:type="ExecuteDeleteSelf">
ExecuteBase* LoaderBehaviors::LoadExecuteDeleteSelf( SharedParams_t params, xml_node<>* sourceNode, stringc& root, UnitBehavior* behavior, UnitAction* action )
{
	ExecuteDeleteSelf* execute = new ExecuteDeleteSelf(params);
	return execute;	
}

// Загрузить <ExecuteBase xsi:type="ExecuteExtAction">
ExecuteBase* LoaderBehaviors::LoadExecuteExtAction( SharedParams_t params, xml_node<>* sourceNode, stringc& root, UnitBehavior* behavior, UnitAction* action )
{
	ExecuteExtAction* execute = new ExecuteExtAction(params);

	for (xml_node<>* chNode = sourceNode->first_node(); chNode; chNode = chNode->next_sibling())		
	{
		if(strcmp(chNode->name(), "ExtActionDescriptions") == 0)
		{
			for (xml_node<>* exActDescNode = chNode->first_node(); exActDescNode; exActDescNode = exActDescNode->next_sibling())		
			{
				if(strcmp(exActDescNode->name(), "ExtActionDescription") == 0)
				{
					ExtActionDescription extActionDesc;
					extActionDesc.ActionName = stringc(exActDescNode->first_node("ActionName")->value());
					execute->ExtActionDescriptions.push_back(extActionDesc);
				}
				else
				{
					_DEBUG_BREAK_IF(true)
				}
			}
		}
		else if(strcmp(chNode->name(), "SelectSceneNodes") == 0)
		{
			for (xml_node<>* selectNode = chNode->first_node(); selectNode; selectNode = selectNode->next_sibling())		
			{
				if(strcmp(selectNode->name(), "UnitSelectSceneNodeBase") == 0)
				{
					UnitSelectSceneNodeBase* selector = 
						LoaderUnitSelectSceneNode::LoadUnitSelectSceneNodeBase(
						params, selectNode, root, behavior, action);					
					execute->AddSelector(selector);
					selector->drop();
				}
				else
				{
					_DEBUG_BREAK_IF(true)
				}
			}			
		}
		else if(strcmp(chNode->name(), "Clause") == 0)
		{
			// ничего
		}
		else
		{
			_DEBUG_BREAK_IF(true)
		}
	}
	return execute;	
}

// Загрузить <ExecuteBase xsi:type="ExecuteAddNextAction">
ExecuteBase* LoaderBehaviors::LoadExecuteAddNextAction( SharedParams_t params, xml_node<>* sourceNode, stringc& root, UnitBehavior* behavior, UnitAction* action )
{
	ExecuteAddNextAction* execute = new ExecuteAddNextAction(params);

	for (xml_node<>* chNode = sourceNode->first_node(); chNode; chNode = chNode->next_sibling())		
	{
		if(strcmp(chNode->name(), "ActionName") == 0)
		{
			execute->ActionName = stringc(chNode->value());
		}
		else if(strcmp(chNode->name(), "Clause") == 0)
		{
			// ничего
		}
		else
		{
			_DEBUG_BREAK_IF(true)
		}
	}
	return execute;	
}

// Загрузить <ExecuteBase xsi:type="ExecuteGroup">
ExecuteBase* LoaderBehaviors::LoadExecuteGroup( 
	SharedParams_t params, 
	xml_node<>* sourceNode, 
	stringc& root, 
	UnitBehavior* behavior, 
	SoundManager* soundManager,
	UnitAction* action, 
	ScriptCache* scriptCache )
{
	ExecuteGroup* execute = new ExecuteGroup(params);

	for (xml_node<>* chNode = sourceNode->first_node(); chNode; chNode = chNode->next_sibling())		
	{
		if(strcmp(chNode->name(), "Executes") == 0)
		{
			for (xml_node<>* exNode = chNode->first_node(); exNode; exNode = exNode->next_sibling())		
			{	
				if(strcmp(exNode->name(), "ExecuteBase") == 0)
				{
					ExecuteBase* executeItem = LoadExecute(
						params, exNode, root, behavior, soundManager, action, scriptCache);
					execute->Executes.push_back(executeItem);
				}
				else
				{
					_DEBUG_BREAK_IF(true)
				}
			}
		}		
		else if(strcmp(chNode->name(), "Clause") == 0)
		{
			// ничего
		}
		else
		{
			_DEBUG_BREAK_IF(true)
		}
	}
	return execute;	
}


// Загрузить <ExecuteBase xsi:type="ExecuteExtParameter">
ExecuteBase* LoaderBehaviors::LoadExecuteExtParameter( SharedParams_t params, xml_node<>* sourceNode, stringc& root, UnitBehavior* behavior, UnitAction* action )
{
	ExecuteExtParameter* execute = new ExecuteExtParameter(params);

	for (xml_node<>* chNode = sourceNode->first_node(); chNode; chNode = chNode->next_sibling())		
	{
		if (strcmp(chNode->name(), "BreakOnFirstSuccess") == 0)
		{					
			execute->BreakOnFirstSuccess = XMLHelper::GetTextAsBool(chNode);
		}
		else if (strcmp(chNode->name(), "Parameters") == 0)
		{
			for (xml_node<>* paramNode = chNode->first_node(); paramNode; paramNode = paramNode->next_sibling())		
			{
				stringc name = stringc(paramNode->first_node("Name")->value());
				stringc val = stringc(paramNode->first_node("Value")->value());
				execute->AddParameter(name, val);
			}
		}
		else if (strcmp(chNode->name(), "SelectSceneNodes") == 0)
		{
			for (xml_node<>* selectNode = chNode->first_node(); selectNode; selectNode = selectNode->next_sibling())		
			{
				if(strcmp(selectNode->name(), "UnitSelectSceneNodeBase") == 0)
				{
					UnitSelectSceneNodeBase* selector =  
						LoaderUnitSelectSceneNode::LoadUnitSelectSceneNodeBase(
							params, selectNode, root, behavior, action);
					if(selector == NULL)
					{
						_DEBUG_BREAK_IF(true)
					}
					execute->AddSelector(selector);
					selector->drop();
				}
				else
				{
					_DEBUG_BREAK_IF(true)
				}
			}
		}
		else if(strcmp(chNode->name(), "Clause") == 0)
		{
			// ничего
		}
		else
		{
			_DEBUG_BREAK_IF(true)
		}
	}
	return execute;	
}

// Загрузка <UnitExprParameter>
UnitExprParameter* LoaderBehaviors::LoadUnitExprParameter( SharedParams_t params, xml_node<>* sourceNode, stringc& root, UnitBehavior* behavior, ScriptCache* scriptCache )
{
	UnitExprParameter* exprParameter = new UnitExprParameter();	
	for (xml_node<>* chNode = sourceNode->first_node(); chNode; chNode = chNode->next_sibling())		
	{
		if(strcmp(chNode->name(), "Name") == 0)
		{
			exprParameter->Name = stringc(chNode->value());
		}
		else if(strcmp(chNode->name(), "ScriptFileName") == 0)
		{
			stringc path = stringc(chNode->value()).trim();
			_DEBUG_BREAK_IF(path == "")
			path = root + path;
			ParserAS* parser = scriptCache->GetItem(path);
			exprParameter->SetParser(parser);
		}
		else if(strcmp(chNode->name(), "Clause") == 0)
		{
			// ничего
		}
		else
		{
			_DEBUG_BREAK_IF(true)
		}
	}

	return exprParameter;
}

// Загрузка <StartParameters>
void LoaderBehaviors::LoadUnitStartParameters( SharedParams_t params, xml_node<>* sourceNode, stringc& root, UnitBehavior* behavior )
{
	for (xml_node<>* paramNode = sourceNode->first_node(); paramNode; paramNode = paramNode->next_sibling())		
	{
		if(strcmp(paramNode->name(), "Parameter") == 0)
		{					
			stringc name = stringc(paramNode->first_node("Name")->value());
			stringc val = stringc(paramNode->first_node("Value")->value());
			behavior->AddStartParameter(name, val);
		}
		else
		{
			_DEBUG_BREAK_IF(true)
		}	
	}

}

// Загрузка <ExecuteBase xsi:type="ExecuteMappingTransform">
ExecuteBase* LoaderBehaviors::LoadExecuteMappingTransform( SharedParams_t params, xml_node<>* sourceNode, stringc& root, UnitBehavior* behavior, UnitAction* action )
{
	ExecuteMappingTransform* execute = new ExecuteMappingTransform(params);
	execute->SetRoot(root);

	for (xml_node<>* chNode = sourceNode->first_node(); chNode; chNode = chNode->next_sibling())		
	{
		if(strcmp(chNode->name(), "UseThisBehavior") == 0)
		{					
			execute->UseThisBehavior = XMLHelper::GetTextAsBool(chNode);
		}
		else if(strcmp(chNode->name(), "BehaviorChildPath") == 0)
		{
			execute->BehaviorChildPath = stringc(chNode->value());
		}
		else if(strcmp(chNode->name(), "ScaleX") == 0)
		{
			execute->ScaleX = stringc(chNode->value()).trim();
		}
		else if(strcmp(chNode->name(), "ScaleY") == 0)
		{
			execute->ScaleY = stringc(chNode->value()).trim();
		}
		else if(strcmp(chNode->name(), "ScaleZ") == 0)
		{
			execute->ScaleZ = stringc(chNode->value()).trim();
		}
		else if(strcmp(chNode->name(), "PositionX") == 0)
		{
			execute->PositionX = stringc(chNode->value()).trim();
		}
		else if(strcmp(chNode->name(), "PositionY") == 0)
		{
			execute->PositionY = stringc(chNode->value()).trim();
		}
		else if(strcmp(chNode->name(), "PositionZ") == 0)
		{
			execute->PositionZ = stringc(chNode->value()).trim();
		}
		else if(strcmp(chNode->name(), "RotationX") == 0)
		{
			execute->RotationX = stringc(chNode->value()).trim();
		}
		else if(strcmp(chNode->name(), "RotationY") == 0)
		{
			execute->RotationY = stringc(chNode->value()).trim();
		}
		else if(strcmp(chNode->name(), "RotationZ") == 0)
		{
			execute->RotationZ = stringc(chNode->value()).trim();
		}
		else if(strcmp(chNode->name(), "Clause") == 0)
		{
			// ничего
		}
		else
		{
			_DEBUG_BREAK_IF(true)
		}
	}
	return execute;	
}

// Загрузка <ExecuteBase xsi:type="ExecuteChangeSceneNodeId">
ExecuteBase* LoaderBehaviors::LoadExecuteChangeSceneNodeId( SharedParams_t params, xml_node<>* sourceNode, stringc& root, UnitBehavior* behavior, UnitAction* action )
{
	ExecuteChangeSceneNodeId* execute = new ExecuteChangeSceneNodeId(params);

	for (xml_node<>* chNode = sourceNode->first_node(); chNode; chNode = chNode->next_sibling())		
	{
		if(strcmp(chNode->name(), "NodeId") == 0)
		{					
			execute->NodeId = XMLHelper::GetTextAsInt(chNode);
		}
		else if(strcmp(chNode->name(), "Clause") == 0)
		{
			// ничего
		}
		else
		{
			_DEBUG_BREAK_IF(true)
		}
	}
	return execute;	
}

ExecuteBase* LoaderBehaviors::LoadExecuteMoveToSceneNode( SharedParams_t params, xml_node<>* sourceNode, stringc& root, UnitBehavior* behavior, UnitAction* action )
{
	ExecuteMoveToSceneNode* execute = new ExecuteMoveToSceneNode(params);

	for (xml_node<>* chNode = sourceNode->first_node(); chNode; chNode = chNode->next_sibling())		
	{
		if(strcmp(chNode->name(), "SelectSceneNode") == 0)
		{					
			UnitSelectSceneNodeBase* selector = 
				LoaderUnitSelectSceneNode::LoadUnitSelectSceneNodeBase(
				params, chNode, root, behavior, action);
			execute->SetSelectSceneNode(selector);
			selector->drop();
		}
		else if(strcmp(chNode->name(), "Speed") == 0)
		{					
			execute->Speed = XMLHelper::GetTextAsFloat(chNode);
		}
		else if(strcmp(chNode->name(), "TargetDist") == 0)
		{					
			execute->TargetDist = XMLHelper::GetTextAsFloat(chNode);
		}
		else if(strcmp(chNode->name(), "ObstacleFilterId") == 0)
		{
			execute->ObstacleFilterId = XMLHelper::GetTextAsInt(chNode);
		}
		else if(strcmp(chNode->name(), "Clause") == 0)
		{
			// ничего
		}
		else
		{
			_DEBUG_BREAK_IF(true)
		}
	}
	return execute;	
}

ExecuteBase* LoaderBehaviors::LoadExecuteScript( SharedParams_t params, xml_node<>* sourceNode, stringc& root, UnitBehavior* behavior, UnitAction* action, ScriptCache* scriptCache )
{
	ExecuteScript* execute = new ExecuteScript(params);

	for (xml_node<>* chNode = sourceNode->first_node(); chNode; chNode = chNode->next_sibling())		
	{
		if(strcmp(chNode->name(), "ScriptFileName") == 0)
		{					
			ParserAS* parser = scriptCache->GetItem(
				root + stringc(chNode->value()));
			execute->SetParser(parser);
		}
		else if(strcmp(chNode->name(), "Clause") == 0)
		{
			// ничего
		}
		else
		{
			_DEBUG_BREAK_IF(true)
		}
	}
	return execute;	
}

// Загрузка <ExecuteBase xsi:type="ExecuteSetData">
ExecuteBase* LoaderBehaviors::LoadExecuteSetData( SharedParams_t params, xml_node<>* sourceNode, stringc& root, UnitBehavior* behavior, UnitAction* action )
{
	ExecuteSetData* execute = new ExecuteSetData(params);

	for (xml_node<>* chNode = sourceNode->first_node(); chNode; chNode = chNode->next_sibling())		
	{
		if(strcmp(chNode->name(), "DataItems") == 0)
		{
			for (xml_node<>* dataNodeItem = chNode->first_node(); dataNodeItem; 
				dataNodeItem = dataNodeItem->next_sibling())		
			{
				if(strcmp(dataNodeItem->name(), "ExecuteSetDataItem") == 0)
				{
					stringc name = stringc(dataNodeItem->first_node("Name")->value());
					xml_node<>* dataNodeGt = dataNodeItem->first_node("DataGetter");
					DataGetterBase* dataGetter = 
						LoaderDataGetters::LoadDataGetterBase(params, dataNodeGt, root, behavior, action);					
					execute->AddDataItem(name, dataGetter);
				}
				else
				{
					_DEBUG_BREAK_IF(true)
				}
			}
		}
		else if(strcmp(chNode->name(), "Clause") == 0)
		{
			// ничего
		}
		else
		{
			_DEBUG_BREAK_IF(true)
		}
	}
	return execute;	
}

ExecuteBase* LoaderBehaviors::LoadExecuteSound( 
	SharedParams_t params, xml_node<>* sourceNode, stringc& root, UnitBehavior* behavior, SoundManager* soundManager, UnitAction* action )
{
	ExecuteSound* execute = new ExecuteSound(params, soundManager);

	for (xml_node<>* chNode = sourceNode->first_node(); chNode; chNode = chNode->next_sibling())		
	{
		if(strcmp(chNode->name(), "Filename") == 0)
		{
			execute->Filename = root + stringc(chNode->value()).trim();
		}
		else if(strcmp(chNode->name(), "Loop") == 0) 
		{
			execute->Loop = XMLHelper::GetTextAsBool(chNode);
		}
		else if(strcmp(chNode->name(), "Format") == 0) 
		{
			stringc format = stringc(chNode->value());
			if(format == "MP3")
				execute->Format = ExecuteSoundType_MP3;
			else if(format == "MONO16_22050")
				execute->Format = ExecuteSoundType_MONO16_22050;
			else 
			{
				_DEBUG_BREAK_IF(true)
			}
		}
		else if(strcmp(chNode->name(), "Clause") == 0)
		{
			// ничего
		}
		else
		{
			_DEBUG_BREAK_IF(true)
		}
	}
	return execute;
}

ExecuteBase* LoaderBehaviors::LoadExecuteTimer( 
	SharedParams_t params, xml_node<>* sourceNode, stringc& root, UnitBehavior* behavior, SoundManager* soundManager, UnitAction* action )
{
	ExecuteTimer* execute = new ExecuteTimer(params);

	for (xml_node<>* chNode = sourceNode->first_node(); chNode; chNode = chNode->next_sibling())		
	{
		if(strcmp(chNode->name(), "TimerName") == 0)
		{
			execute->TimerName = stringc(chNode->value()).trim();
		}
		else if(strcmp(chNode->name(), "UseStageTimer") == 0)
		{
			execute->UseStageTimer = XMLHelper::GetTextAsBool(chNode);
		}
		else if(strcmp(chNode->name(), "SetTime") == 0)
		{
			execute->SetTime = XMLHelper::GetTextAsBool(chNode);
		}
		else if(strcmp(chNode->name(), "StartTimer") == 0) 
		{
			execute->StartTimer = XMLHelper::GetTextAsBool(chNode);
		}
		else if(strcmp(chNode->name(), "StopTimer") == 0) 
		{
			execute->StopTimer = XMLHelper::GetTextAsBool(chNode);
		}
		else if(strcmp(chNode->name(), "Time") == 0) 
		{
			execute->Time = XMLHelper::GetTextAsUInt(chNode);
		}
		else if(strcmp(chNode->name(), "Clause") == 0)
		{
			// ничего
		}
		else
		{
			_DEBUG_BREAK_IF(true)
		}
	}
	return execute;
}

ExecuteBase* LoaderBehaviors::LoadExecuteParticleAffector( SharedParams_t params, xml_node<>* sourceNode, stringc& root, UnitBehavior* behavior, SoundManager* soundManager, UnitAction* action )
{
	ExecuteParticleAffector* execute = new ExecuteParticleAffector(params);
	for (xml_node<>* chNode = sourceNode->first_node(); chNode; chNode = chNode->next_sibling())		
	{
		if(strcmp(chNode->name(), "Affector") == 0)
		{
			char* xsi_type = chNode->first_attribute("xsi:type")->value();
			ExParticleAffectorBase* pAffectorBase = NULL;
			if(strcmp(xsi_type, "ParticleAffectorFadeOut") == 0)
			{
				ExParticleAffectorFadeOut* pAffector = new ExParticleAffectorFadeOut();
				pAffectorBase = pAffector;
				for (xml_node<>* nodeAffector = chNode->first_node(); nodeAffector; nodeAffector = nodeAffector->next_sibling())		
				{
					if(strcmp(nodeAffector->name(), "Items") == 0)
					{
						for (xml_node<>* itemNode = nodeAffector->first_node(); itemNode; itemNode = itemNode->next_sibling())		
						{
							if(strcmp(itemNode->name(), "ParticleFadeOutItem") == 0)
							{
								for (xml_node<>* subItemNode = itemNode->first_node(); subItemNode; subItemNode = subItemNode->next_sibling())		
								{
									if(strcmp(subItemNode->name(), "Color") == 0)
									{
										pAffector->Colors.push_back(XMLHelper::GetTextAsSColor(subItemNode));
									}
									else if(strcmp(subItemNode->name(), "Time") == 0)
									{
										pAffector->TimesForWay.push_back(XMLHelper::GetTextAsUInt(subItemNode));
									}
									else 
									{
										_DEBUG_BREAK_IF(true)
									}
								}
							}
							else 
							{
								_DEBUG_BREAK_IF(true)
							}
						}
					}
					else if(strcmp(nodeAffector->name(), "Loop") == 0)
					{
						pAffector->Loop = XMLHelper::GetTextAsBool(nodeAffector);
					}
					else
					{
						_DEBUG_BREAK_IF(true)
					}
				}
			}
			else if(strcmp(xsi_type, "ParticleAffectorEmmiterParam") == 0)
			{
				ExParticleAffectorEmmiterParam* pAffector = new ExParticleAffectorEmmiterParam();
				pAffectorBase = pAffector;
				for (xml_node<>* nodeAffector = chNode->first_node(); nodeAffector; nodeAffector = nodeAffector->next_sibling())		
				{
					if(strcmp(nodeAffector->name(), "ParticlesPerSecond") == 0)
					{
						pAffector->ParticlesPerSecond = XMLHelper::GetTextAsInt(nodeAffector);
					}
					else if(strcmp(nodeAffector->name(), "TimePerSecond") == 0)
					{
						pAffector->TimePerSecond = XMLHelper::GetTextAsUInt(nodeAffector);
					}
					else
					{
						_DEBUG_BREAK_IF(true)
					}
				}
			}
			else if(strcmp(xsi_type, "ParticleAffectorAllDirection") == 0)
			{
				ExParticleAffectorAllDirection* pAffector = new ExParticleAffectorAllDirection();
				pAffectorBase = pAffector;
				for (xml_node<>* nodeAffector = chNode->first_node(); nodeAffector; nodeAffector = nodeAffector->next_sibling())		
				{
					if(strcmp(nodeAffector->name(), "Speed") == 0)
					{
						pAffector->Speed = XMLHelper::GetTextAsFloat(nodeAffector);
					}
					else if(strcmp(nodeAffector->name(), "Position") == 0)
					{
						pAffector->Position = XMLHelper::GetVector3df(nodeAffector);
					}
					else
					{
						_DEBUG_BREAK_IF(true)
					}
				}
			}
			else if(strcmp(xsi_type, "ParticleAffectorRotation") == 0)
			{
				ExParticleAffectorRotation* pAffector = new ExParticleAffectorRotation();
				pAffectorBase = pAffector;
				for (xml_node<>* nodeAffector = chNode->first_node(); nodeAffector; nodeAffector = nodeAffector->next_sibling())		
				{
					if(strcmp(nodeAffector->name(), "Speed") == 0)
					{
						pAffector->Speed = XMLHelper::GetVector3df(nodeAffector);
					}
					else if(strcmp(nodeAffector->name(), "Position") == 0)
					{
						pAffector->Position = XMLHelper::GetVector3df(nodeAffector);
					}
					else
					{
						_DEBUG_BREAK_IF(true)
					}
				}
			}
			else if(strcmp(xsi_type, "ParticleAffectorScale") == 0)
			{
				ExParticleAffectorScale* pAffector = new ExParticleAffectorScale();
				pAffectorBase = pAffector;
				for (xml_node<>* nodeAffector = chNode->first_node(); nodeAffector; nodeAffector = nodeAffector->next_sibling())		
				{
					if(strcmp(nodeAffector->name(), "Items") == 0)
					{
						for (xml_node<>* itemNode = nodeAffector->first_node(); itemNode; itemNode = itemNode->next_sibling())		
						{
							if(strcmp(itemNode->name(), "ParticleScaleItem") == 0)
							{
								for (xml_node<>* subItemNode = itemNode->first_node(); subItemNode; subItemNode = subItemNode->next_sibling())		
								{
									if(strcmp(subItemNode->name(), "TargetScale") == 0)
									{
										pAffector->TargetScales.push_back(XMLHelper::GetTextAsDimension2d(subItemNode));
									}
									else if(strcmp(subItemNode->name(), "Time") == 0)
									{
										pAffector->TimesForWay.push_back(XMLHelper::GetTextAsUInt(subItemNode));
									}
									else 
									{
										_DEBUG_BREAK_IF(true)
									}
								}
							}
							else 
							{
								_DEBUG_BREAK_IF(true)
							}
						}
					}
					else if(strcmp(nodeAffector->name(), "Loop") == 0)
					{
						pAffector->Loop = XMLHelper::GetTextAsBool(nodeAffector);
					}
					else
					{
						_DEBUG_BREAK_IF(true)
					}
				}
			}
			else
			{
				_DEBUG_BREAK_IF(true)
			}
			execute->SetAffector(pAffectorBase);
		}
		else if(strcmp(chNode->name(), "Clause") == 0)
		{
			// ничего
		}
		else
		{
			_DEBUG_BREAK_IF(true)
		}
	}	
	return execute;	
}

ExecuteBase* LoaderBehaviors::LoadExecuteParticleEmitter( SharedParams_t params, xml_node<>* sourceNode, stringc& root, UnitBehavior* behavior, SoundManager* soundManager, UnitAction* action )
{
	ExecuteParticleEmitter* execute = new ExecuteParticleEmitter(params);
	for (xml_node<>* chNode = sourceNode->first_node(); chNode; chNode = chNode->next_sibling())		
	{
		if(strcmp(chNode->name(), "Emitter") == 0)
		{
			char* xsi_type = chNode->first_attribute("xsi:type")->value();
			ExParticleEmitterBase* pEmittorBase = NULL;
			if(strcmp(xsi_type, "ParticleEmitterBox") == 0)
			{
				ExParticleEmitterBox* pEmittor = new ExParticleEmitterBox();
				pEmittorBase = pEmittor;
				for (xml_node<>* nodeEmitter = chNode->first_node(); nodeEmitter; nodeEmitter = nodeEmitter->next_sibling())		
				{
					if(strcmp(nodeEmitter->name(), "Box") == 0)
					{
						pEmittor->Box = XMLHelper::GetBoundbox(nodeEmitter);
					}
					else if(strcmp(nodeEmitter->name(), "Direction") == 0)
					{
						pEmittor->Direction = XMLHelper::GetVector3df(nodeEmitter);
					}
					else if(strcmp(nodeEmitter->name(), "MinParticlesPerSecond") == 0)
					{
						pEmittor->MinParticlesPerSecond = XMLHelper::GetTextAsUInt(nodeEmitter);
					}
					else if(strcmp(nodeEmitter->name(), "MaxParticlesPerSecond") == 0)
					{
						pEmittor->MaxParticlesPerSecond = XMLHelper::GetTextAsUInt(nodeEmitter);
					}
					else if(strcmp(nodeEmitter->name(), "MinStartColor") == 0)
					{
						pEmittor->MinStartColor = XMLHelper::GetTextAsSColor(nodeEmitter);
					}
					else if(strcmp(nodeEmitter->name(), "MaxStartColor") == 0)
					{
						pEmittor->MaxStartColor = XMLHelper::GetTextAsSColor(nodeEmitter);
					}
					else if(strcmp(nodeEmitter->name(), "LifeTimeMin") == 0)
					{
						pEmittor->LifeTimeMin = XMLHelper::GetTextAsUInt(nodeEmitter);
					}
					else if(strcmp(nodeEmitter->name(), "LifeTimeMax") == 0)
					{
						pEmittor->LifeTimeMax = XMLHelper::GetTextAsUInt(nodeEmitter);
					}
					else if(strcmp(nodeEmitter->name(), "MaxAngleDegrees") == 0)
					{
						pEmittor->MaxAngleDegrees = XMLHelper::GetTextAsInt(nodeEmitter);
					}
					else if(strcmp(nodeEmitter->name(), "MinStartSize") == 0)
					{
						pEmittor->MinStartSize = XMLHelper::GetTextAsDimension2d(nodeEmitter);
					}
					else if(strcmp(nodeEmitter->name(), "MaxStartSize") == 0)
					{
						pEmittor->MaxStartSize = XMLHelper::GetTextAsDimension2d(nodeEmitter);
					}
					else if(strcmp(nodeEmitter->name(), "UseParentRotation") == 0)
					{
						pEmittor->UseParentRotation = XMLHelper::GetTextAsBool(nodeEmitter);
					}
					else
					{
						_DEBUG_BREAK_IF(true)
					}
				}
				execute->SetParticaleEmitter(pEmittorBase);
			}
			else
			{
				_DEBUG_BREAK_IF(true)
			}
		}
		else if(strcmp(chNode->name(), "Clause") == 0)
		{
			// ничего
		}
		else
		{
			_DEBUG_BREAK_IF(true)
		}
	}	
	return execute;	
}

ExecuteBase* LoaderBehaviors::LoadExecuteColor( SharedParams_t params, xml_node<>* sourceNode, stringc& root, UnitBehavior* behavior, UnitAction* action )
{

	core::array<video::SColor> colors;
	core::array<u32> timesForWay;
	bool loop;

	for (xml_node<>* chNode = sourceNode->first_node(); chNode; chNode = chNode->next_sibling())		
	{
		if(strcmp(chNode->name(), "Loop") == 0)
		{
			loop = XMLHelper::GetTextAsBool(chNode);
		}
		else if(strcmp(chNode->name(), "Items") == 0)
		{
			for (xml_node<>* itemNode = chNode->first_node();
				itemNode; itemNode = itemNode->next_sibling())
			{	
				_DEBUG_BREAK_IF(strcmp(itemNode->name(), "TransformSColorItem") != 0)
				video::SColor color;
				u32 time;
				for (xml_node<>* itemNodeData = itemNode->first_node();
					itemNodeData; itemNodeData = itemNodeData->next_sibling())
				{
					if (strcmp(itemNodeData->name(), "Color") == 0)
					{
						color = XMLHelper::GetTextAsSColor(itemNodeData);
					}
					else if (strcmp(itemNodeData->name(), "Time") == 0)
					{
						time = XMLHelper::GetTextAsUInt(itemNodeData);
					}
					else
					{
						_DEBUG_BREAK_IF(true)
					}
				}
				colors.push_back(color);
				timesForWay.push_back(time);
			}
		}
		else if(strcmp(chNode->name(), "Clause") == 0)
		{
			// ничего
		}
		else
		{
			_DEBUG_BREAK_IF(true)
		}
	}

	ExecuteColor* execute = new ExecuteColor(params, colors, timesForWay, loop);
	return execute;	
}




