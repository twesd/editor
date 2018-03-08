#include "LoaderUnitGenInstance.h"
#include "LoaderBehaviors.h"
#include "LoaderCommon.h"
#include "../../Core/Xml/XMLHelper.h"

#include "../UnitGenInstance/UnitCreationTimer.h"
#include "../UnitGenInstance/UnitCreationDistance.h"
#include "../UnitGenInstance/UnitCreationBBox.h"
#include "../UnitGenInstance/UnitCreationGlobalParameters.h"

using namespace rapidxml;

// Загрузка описания юнита <TreeView>
core::array<UnitGenInstanceBase*> LoaderUnitGenInstance::LoadUnitGensFromXmlTag(
	SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc& root)
{
	core::array<UnitGenInstanceBase*> getInstances;
	
	// "TreeView" - sourceNode
	// Получить все узлы с именем SerializableTreeNode, включая дочерние
	core::array<rapidxml::xml_node<>*> serialNodes = 
		XMLHelper::SelectAllByNodeName(sourceNode, "SerializableTreeNode");
	for(u32 iNode = 0; iNode < serialNodes.size(); iNode++)
	{
		rapidxml::xml_node<>* tagNode = serialNodes[iNode]->first_node("Tag");
		if(tagNode == NULL)
			continue;
		rapidxml::xml_attribute<>* attr = tagNode->first_attribute("xsi:type");
		char* unitType = attr->value();
		if(strcmp(unitType, "UnitInstanceStandard") == 0)
		{
			UnitGenInstanceStandard* unitInstance = LoadInstanceStandard(params, tagNode, root);
			_DEBUG_BREAK_IF(unitInstance == NULL)
			getInstances.push_back(unitInstance);
		}
		else if(strcmp(unitType, "UnitInstanceBillboard") == 0)
		{
			UnitGenInstanceBillboard* unitInstance = LoadInstanceBillboard(params, tagNode, root);
			_DEBUG_BREAK_IF(unitInstance == NULL)
			getInstances.push_back(unitInstance);
		}
		else if(strcmp(unitType, "UnitInstanceEmpty") == 0)
		{
			UnitGenInstanceEmpty* unitInstance = LoadInstanceEmpty(params, tagNode, root);
			_DEBUG_BREAK_IF(unitInstance == NULL)
			getInstances.push_back(unitInstance);
		}
		else if(strcmp(unitType, "UnitInstanceEnv") == 0)
		{
			UnitGenInstanceEnv* unitInstance = LoadInstanceEnv(params, tagNode, root);
			_DEBUG_BREAK_IF(unitInstance == NULL)
			getInstances.push_back(unitInstance);
		}
		else if(strcmp(unitType, "UnitInstanceCamera") == 0)
		{
			UnitGenInstanceCamera* unitInstance = LoadInstanceCamera(params, tagNode, root);
			_DEBUG_BREAK_IF(unitInstance == NULL)
			getInstances.push_back(unitInstance);
		}
		else if(strcmp(unitType, "GroupData") == 0)
		{
			// Груповой узел, пропускаем
		}
		else 
		{
			// unknown unitType
			_DEBUG_BREAK_IF(true)
		}
	}

	return getInstances;
}

// Загрузка из <Tag xsi:type="UnitInstanceStandard">
UnitGenInstanceStandard* LoaderUnitGenInstance::LoadInstanceStandard(
	SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc& root)
{
	UnitGenInstanceStandard* instance = new UnitGenInstanceStandard(params);
	for (xml_node<>* chNode = sourceNode->first_node(); chNode; chNode = chNode->next_sibling())		
	{
		if(strcmp(chNode->name(), "Name") == 0)
		{
			instance->UnitName = stringc(chNode->value());
		} 
		else if(strcmp(chNode->name(), "StartPosition") == 0)
		{
			instance->StartPosition = XMLHelper::GetVector3df(chNode);
		}
		else if(strcmp(chNode->name(), "StartRotation") == 0)
		{
			instance->StartRotation = XMLHelper::GetVector3df(chNode);
		}
		else if(strcmp(chNode->name(), "RandomValueCreation") == 0)
		{
			// ничего
		}
		else if(strcmp(chNode->name(), "Creations") == 0)
		{
			instance->Creations = LoadCreations(params, chNode, root, instance);
		}
		else if(strcmp(chNode->name(), "BehaviorsPath") == 0)
		{
			stringc pathBehaviors(chNode->value());
			instance->PathBehavior = root + pathBehaviors;
		}
		else if(strcmp(chNode->name(), "EditorModelId") == 0)
		{
			// Пропускаем 
		}
		else
		{
			_DEBUG_BREAK_IF(true)
		}
	}
	return instance;
}

// Загрузка из <Tag xsi:type="UnitInstanceArea">
UnitGenInstanceBillboard* LoaderUnitGenInstance::LoadInstanceBillboard( SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc root )
{
	UnitGenInstanceBillboard* instance = new UnitGenInstanceBillboard(params);
	for (xml_node<>* chNode = sourceNode->first_node(); chNode; chNode = chNode->next_sibling())		
	{
		if(strcmp(chNode->name(), "Name") == 0)
		{
			instance->UnitName = stringc(chNode->value());
		} 
		else if(strcmp(chNode->name(), "StartPosition") == 0)
		{
			instance->StartPosition = XMLHelper::GetVector3df(chNode);
		}
		else if(strcmp(chNode->name(), "StartRotation") == 0)
		{
			instance->StartRotation = XMLHelper::GetVector3df(chNode);
		}
		else if(strcmp(chNode->name(), "RandomValueCreation") == 0)
		{
			// ничего
		}
		else if(strcmp(chNode->name(), "Creations") == 0)
		{
			instance->Creations = LoadCreations(params, chNode, root, instance);
		}
		else if(strcmp(chNode->name(), "Width") == 0)
		{
			instance->Width = XMLHelper::GetTextAsFloat(chNode);
		}
		else if(strcmp(chNode->name(), "Height") == 0)
		{
			instance->Height = XMLHelper::GetTextAsFloat(chNode);
		}
		else if(strcmp(chNode->name(), "Texture") == 0)
		{
			stringc texturePath = stringc(chNode->value()).trim();
			if(texturePath != "")
				instance->TexturePath = root + texturePath;
		}
		else if(strcmp(chNode->name(), "Use32Bit") == 0)
		{
			instance->Use32Bit = XMLHelper::GetTextAsBool(chNode);
		}
		else if(strcmp(chNode->name(), "MaterialType") == 0)
		{
			instance->MaterialType = LoaderCommon::GetMaterialType(stringc(chNode->value()));
		}
		else if(strcmp(chNode->name(), "UseUpVector") == 0)
		{
			instance->UseUpVector = XMLHelper::GetTextAsBool(chNode);
		}
		else if(strcmp(chNode->name(), "UpVector") == 0)
		{
			instance->UpVector = XMLHelper::GetVector3df(chNode);
		}
		else if(strcmp(chNode->name(), "UseViewVector") == 0)
		{
			instance->UseViewVector = XMLHelper::GetTextAsBool(chNode);
		}
		else if(strcmp(chNode->name(), "ViewVector") == 0)
		{
			instance->ViewVector = XMLHelper::GetVector3df(chNode);
		}
		else if(strcmp(chNode->name(), "NodeId") == 0)
		{
			instance->NodeId = XMLHelper::GetTextAsInt(chNode);
		}
		else if(strcmp(chNode->name(), "EditorModelId") == 0)
		{
			// Пропускаем
		}
		else
		{
			_DEBUG_BREAK_IF(true)
		}
	}
	return instance;
}

// Загрузка из <Tag xsi:type="UnitInstanceEnv">
UnitGenInstanceEnv* LoaderUnitGenInstance::LoadInstanceEnv(SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc& root)
{
	UnitGenInstanceEnv* instance = new UnitGenInstanceEnv(params);

	for (xml_node<>* chNode = sourceNode->first_node(); chNode; chNode = chNode->next_sibling())		
	{
		if(strcmp(chNode->name(), "Name") == 0)				
		{
			stringc name(chNode->value());
			instance->UnitName = name;
		} 
		else if(strcmp(chNode->name(), "StartPosition") == 0)
		{
			instance->StartPosition = XMLHelper::GetVector3df(chNode);
		}
		else if(strcmp(chNode->name(), "StartRotation") == 0)
		{
			instance->StartRotation = XMLHelper::GetVector3df(chNode);
		}
		else if(strcmp(chNode->name(), "RandomValueCreation") == 0)
		{
			// ничего
		}
		else if(strcmp(chNode->name(), "Creations") == 0)
		{
			instance->Creations = LoadCreations(params, chNode, root, instance);
		}
		else if(strcmp(chNode->name(), "ModelPath") == 0)
		{
			stringc path(chNode->value());
			if(path != "")
				instance->ModelPath = root + path;
		}
		else if(strcmp(chNode->name(), "NodeId") == 0)
		{
			instance->NodeId = XMLHelper::GetTextAsInt(chNode);
		}
		else if(strcmp(chNode->name(), "EditorModelId") == 0)
		{
			// Пропускаем
		}
		else 
		{
			_DEBUG_BREAK_IF(true)
		}
	}

	return instance;
}

// Загрузка из <Tag xsi:type="UnitInstanceCamera">
UnitGenInstanceCamera* LoaderUnitGenInstance::LoadInstanceCamera( SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc root )
{
	UnitGenInstanceCamera* instance = new UnitGenInstanceCamera(params);
	
	for (xml_node<>* chNode = sourceNode->first_node(); chNode; chNode = chNode->next_sibling())		
	{
		if(strcmp(chNode->name(), "Name") == 0)
		{
			instance->UnitName = stringc(chNode->value());
		} 
		else if(strcmp(chNode->name(), "StartPosition") == 0)
		{
			instance->StartPosition = XMLHelper::GetVector3df(chNode);
		}
		else if(strcmp(chNode->name(), "StartRotation") == 0)
		{
			instance->StartRotation = XMLHelper::GetVector3df(chNode);
		}
		else if(strcmp(chNode->name(), "Creations") == 0)
		{
			instance->Creations = LoadCreations(params, chNode, root, instance);
		}
		else if(strcmp(chNode->name(), "Behavior") == 0)
		{
			char* behaviorType = chNode->first_attribute("xsi:type")->value();
			// Загружаем поведение камеры
			
			if(strcmp(behaviorType, "CameraBehaviorFollowToNode") == 0)
			{
				CameraGenFollowToNode* followToNode = new CameraGenFollowToNode();
				for (xml_node<>* camNode = chNode->first_node(); camNode; camNode = camNode->next_sibling())		
				{	
				    if(strcmp(camNode->name(), "UnitInstanceName") == 0)
					{
						followToNode->UnitInstanceName = stringc(camNode->value());
					}
					else if(strcmp(camNode->name(), "RotateWithUnit") == 0)
					{
						followToNode->RotateWithUnit = XMLHelper::GetTextAsBool(camNode);
					}	
					else if(strcmp(camNode->name(), "MapSize2d") == 0)
					{
						followToNode->MapSize2d = XMLHelper::GetTextAsUInt(camNode);
					}
					else if(strcmp(camNode->name(), "MapSize3d") == 0)
					{
						followToNode->MapSize3d = XMLHelper::GetTextAsFloat(camNode);
					}
					else if(strcmp(camNode->name(), "ObstacleFilterId") == 0)
					{
						followToNode->ObstacleFilterId = XMLHelper::GetTextAsInt(camNode);
					}
					else
					{
						_DEBUG_BREAK_IF(true)
					}
				}
				instance->CameraGenBaseData = followToNode;
			}
			else if(strcmp(behaviorType, "CameraBehaviorStatic") == 0)
			{
				instance->CameraGenBaseData = new CameraGenStatic();
			}
			else
			{
				_DEBUG_BREAK_IF(true)
			}			
		}
		else if(strcmp(chNode->name(), "StartTarget") == 0)
		{
			instance->StartTarget = XMLHelper::GetVector3df(chNode);
		}
		else if(strcmp(chNode->name(), "FarValue") == 0)
		{
			instance->FarValue = XMLHelper::GetTextAsFloat(chNode);
		}
		else if(strcmp(chNode->name(), "EditorModelId") == 0)
		{
			// Пропускаем
		}
		else 
		{
			_DEBUG_BREAK_IF(true)
		}
	}

	return instance;
}


// Загрузка из <Creations>
core::array<UnitCreationBase*> LoaderUnitGenInstance::LoadCreations(
	SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc& root, UnitGenInstanceBase* genInstance)
{
	core::array<UnitCreationBase*> creationItems;

	for (xml_node<>* chNode = sourceNode->first_node(); chNode; chNode = chNode->next_sibling())		
	{
		if(strcmp(chNode->name(), "UnitCreationBase") == 0)
		{
			char* attrName = chNode->first_attribute("xsi:type")->value();

			if(strcmp(attrName, "UnitCreationTimer") == 0)
			{
				UnitCreationBase* creation = LoadCreationTimer(params, chNode);
				creation->SetGenInstance(genInstance);
				creationItems.push_back(creation);
			}
			else if(strcmp(attrName, "UnitCreationDistance") == 0)
			{
				UnitCreationBase* creation = LoadCreationDistance(params, chNode);
				creation->SetGenInstance(genInstance);
				creationItems.push_back(creation);
			}
			else if(strcmp(attrName, "UnitCreationBBox") == 0)
			{
				UnitCreationBase* creation = LoadCreationBBox(params, chNode);
				creation->SetGenInstance(genInstance);
				creationItems.push_back(creation);
			}
			else if(strcmp(attrName, "UnitCreationGlobalParameters") == 0)
			{
				UnitCreationBase* creation = LoadCreationGlobalParameters(params, chNode);
				creation->SetGenInstance(genInstance);
				creationItems.push_back(creation);
			}
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
	return creationItems;
}

// Загрузка из <UnitCreationBase xsi:type="UnitCreationTimer">
UnitCreationBase* LoaderUnitGenInstance::LoadCreationTimer(SharedParams_t params, rapidxml::xml_node<>* sourceNode)
{
	UnitCreationTimer* timer = new UnitCreationTimer(params);

	for (xml_node<>* chNode = sourceNode->first_node(); chNode; chNode = chNode->next_sibling())		
	{
		if(strcmp(chNode->name(), "StartTime") == 0)		
		{
			timer->StartTime = XMLHelper::GetTextAsUInt(chNode);
		}
		else if(strcmp(chNode->name(), "EndTime") == 0)
		{
			timer->EndTime = XMLHelper::GetTextAsUInt(chNode);
		}
		else if(strcmp(chNode->name(), "Interval") == 0)
		{
			timer->Interval = XMLHelper::GetTextAsUInt(chNode);
		}
		else if(strcmp(chNode->name(), "UpdateTimesOnFirstCheck") == 0)
		{
			timer->UpdateTimesOnFirstCheck = XMLHelper::GetTextAsBool(chNode);
		}
		else 
		{
			_DEBUG_BREAK_IF(true)
		}
	}
	return timer;
}

UnitCreationBase* LoaderUnitGenInstance::LoadCreationDistance( SharedParams_t params, rapidxml::xml_node<>* sourceNode )
{
	UnitCreationDistance* creation = new UnitCreationDistance(params);
	
	for (xml_node<>* chNode = sourceNode->first_node(); chNode; chNode = chNode->next_sibling())		
	{
		if(strcmp(chNode->name(), "FilterNodeId") == 0)
		{
			creation->FilterNodeId = XMLHelper::GetTextAsInt(chNode);
		}
		else if(strcmp(chNode->name(), "Distance") == 0)
		{
			creation->Distance =  XMLHelper::GetTextAsFloat(chNode);
		}
		else if(strcmp(chNode->name(), "CompareType") == 0)
		{
			creation->CompareType = LoaderCommon::GetCompareType( stringc(chNode->value()));
		}
		else if(strcmp(chNode->name(), "UseUnitSize") == 0)
		{
			creation->UseUnitSize =  XMLHelper::GetTextAsBool(chNode);
		}
		else if(strcmp(chNode->name(), "CountNodes") == 0)
		{
			creation->CountNodes =  XMLHelper::GetTextAsInt(chNode);
		}		
		else
		{
			_DEBUG_BREAK_IF(true);
		}
	}
	return creation;
}

UnitCreationBase* LoaderUnitGenInstance::LoadCreationBBox( SharedParams_t params, rapidxml::xml_node<>* sourceNode )
{
	UnitCreationBBox* creation = new UnitCreationBBox(params);

	for (xml_node<>* chNode = sourceNode->first_node(); chNode; chNode = chNode->next_sibling())		
	{
		if(strcmp(chNode->name(), "FilterNodeId") == 0)
		{
			creation->FilterNodeId = XMLHelper::GetTextAsInt(chNode);
		}
		else if(strcmp(chNode->name(), "Boundbox") == 0)
		{
			creation->Boundbox =  XMLHelper::GetBoundbox(chNode);
		}
		else if(strcmp(chNode->name(), "CountNodes") == 0)
		{
			creation->CountNodes =  XMLHelper::GetTextAsInt(chNode);
		}		
		else
		{
			_DEBUG_BREAK_IF(true);
		}
	}
	return creation;
}

UnitGenInstanceEmpty* LoaderUnitGenInstance::LoadInstanceEmpty( 
	SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc root )
{
	UnitGenInstanceEmpty* instance = new UnitGenInstanceEmpty(params);
	for (xml_node<>* chNode = sourceNode->first_node(); chNode; chNode = chNode->next_sibling())		
	{
		if(strcmp(chNode->name(), "Name") == 0)			
		{
			instance->UnitName = stringc(chNode->value());
		} 
		else if(strcmp(chNode->name(), "StartPosition") == 0)
		{
			instance->StartPosition = XMLHelper::GetVector3df(chNode);
		}
		else if(strcmp(chNode->name(), "StartRotation") == 0)
		{
			instance->StartRotation = XMLHelper::GetVector3df(chNode);
		}
		else if(strcmp(chNode->name(), "Creations") == 0)
		{
			instance->Creations = LoadCreations(params, chNode, root, instance);
		}
		else if(strcmp(chNode->name(), "Scale") == 0)
		{
			instance->Scale = XMLHelper::GetVector3df(chNode);
		}
		else if(strcmp(chNode->name(), "NodeId") == 0)
		{
			instance->NodeId = XMLHelper::GetTextAsInt(chNode);									
		}
		else if(strcmp(chNode->name(), "EditorModelId") == 0)
		{
			// Пропускаем
		}
		else
		{
			_DEBUG_BREAK_IF(true)
		}
	}
	return instance;
}

UnitCreationBase* LoaderUnitGenInstance::LoadCreationGlobalParameters( SharedParams_t params, xml_node<>* sourceNode )
{
	UnitCreationGlobalParameters* creation = new UnitCreationGlobalParameters(params);

	for (xml_node<>* chNode = sourceNode->first_node(); chNode; chNode = chNode->next_sibling())		
	{
		if(strcmp(chNode->name(), "Parameters") == 0)
		{
			for (xml_node<>* paramNode = chNode->first_node(); paramNode; paramNode = paramNode->next_sibling())		
			{
				if(strcmp(paramNode->name(), "Parameter") == 0)
				{	
					Parameter p;
					p.Name = stringc(paramNode->first_node("Name")->value());
					p.Value = stringc(paramNode->first_node("Value")->value());					
					creation->Parameters.push_back(p);
				}
				else
				{
					_DEBUG_BREAK_IF(true)
				}	
			}
		}		
		else
		{
			_DEBUG_BREAK_IF(true);
		}
	}
	return creation;

}


