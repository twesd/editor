#include "LoaderControlManager.h"

#include "ControlButton.h"
#include "ControlImage.h"
#include "ControlTapScene.h"
#include "ControlBehavior.h"
#include "ControlText.h"
#include "ControlCircle.h"
#include "ControlPanel.h"
#include "ControlComponentScroll.h"

using namespace rapidxml;

// Загрузка из <ControlPackages> в .stage
core::array<ControlPackage> LoaderControlManager::LoadControlPackagesFromXmlTag(
	SharedParams_t params, 
	rapidxml::xml_node<>* node,
	stringc root, 
	ControlManager* manager, 
	ModuleManager* moduleManager, 
	SoundManager* soundManager,
	ScriptCache* scriptCache,
	float controlsScale,
	position2di controlsOffset)
{
	core::array<ControlPackage> packages;

	stringc defaultPath;

	// Получаем путь до пакета по умолчанию
	defaultPath = root + stringc(node->first_node("DefaultPath")->value());
	
	xml_node<>* pathsNode =  node->first_node("Paths");
	for (xml_node<>* chNode = pathsNode->first_node(); chNode; chNode = chNode->next_sibling())
	{
		if(strcmp(chNode->name(), "string") == 0)
		{					
			ControlPackage package = 
						LoadControlPackage(
							params, 
							root + stringc(chNode->value()), 
							manager,
							moduleManager,
							soundManager,
							scriptCache,
							controlsScale,
							controlsOffset);
			packages.push_back(package);
		}
	}

	// Опредедяем пакет по умолчанию
	for (u32 i = 0; i < packages.size() ; i++)
	{
		ControlPackage* package = &packages[i];
		if(package->Path == defaultPath)
		{
			package->IsDefault = true;
			break;
		}
	}
	return packages;
}

// Загрузка из файла .controls
ControlPackage LoaderControlManager::LoadControlPackage(
		SharedParams_t params, 
		stringc path, 
		ControlManager* manager, 
		ModuleManager* moduleManager,
		SoundManager* soundManager,
		ScriptCache* scriptCache,
		float controlsScale,
		position2di controlsOffset)
{
	ControlPackage package;
	package.Path = path;
	package.IsDefault = false;
	
	stringc root = params.FileSystem->getFileDir(path) + "/";

	XMLFileDocument xmlFileDoc(path, params.FileSystem);
	xml_node<>* rootNode = xmlFileDoc.GetDocument()->first_node();
	
	core::array<rapidxml::xml_node<>*> tagNodes = 
		XMLHelper::SelectAllByNodeName(rootNode, "Tag");
	for(u32 iNode = 0; iNode < tagNodes.size(); iNode++)
	{
		rapidxml::xml_node<>* tagNode = tagNodes[iNode];
		rapidxml::xml_attribute<>* attr = tagNode->first_attribute("xsi:type");
				
		if(strcmp(attr->value(), "ControlButton") == 0)
		{
			IControl* cntrl = LoadControlButton(tagNode, params, root, soundManager, controlsScale, controlsOffset);
			package.Controls.push_back(cntrl);
		}
		else if(strcmp(attr->value(), "ControlImage") == 0)
		{
			IControl* cntrl = LoadControlImage(tagNode, params, root, controlsScale, controlsOffset);
			package.Controls.push_back(cntrl);
		}
		else if(strcmp(attr->value(), "ControlTapScene") == 0)
		{
			IControl* cntrl = LoadTapScene(tagNode, params, root, controlsScale, controlsOffset);
			package.Controls.push_back(cntrl);
		}
		else if(strcmp(attr->value(), "ControlBehavior") == 0)
		{
			IControl* cntrl = LoadBehavior(tagNode, params, root, manager, moduleManager, scriptCache);
			package.Controls.push_back(cntrl);
		}
		else if(strcmp(attr->value(), "ControlText") == 0)
		{
			IControl* cntrl = LoadControlText(tagNode, params, root, manager, controlsScale, controlsOffset);
			package.Controls.push_back(cntrl);
		}
		else if(strcmp(attr->value(), "ControlCircle") == 0)
		{
			IControl* cntrl = LoadControlCircle(tagNode, params, root, controlsScale, controlsOffset);
			package.Controls.push_back(cntrl);
		}
		else if(strcmp(attr->value(), "ControlPanel") == 0)
		{
			IControl* cntrl = LoadControlPanel(tagNode, params, root, controlsScale, controlsOffset);
			package.Controls.push_back(cntrl);
		}
		else if(strcmp(attr->value(), "GroupData") == 0 ||
			strcmp(attr->value(), "XRefData") == 0)
		{
			// Ничего
		}
		else
		{
			_DEBUG_BREAK_IF(true)
		}
	}


	return package;
}


bool LoaderControlManager::LoadBaseParam( 
	rapidxml::xml_node<>* node, SharedParams_t sharedParams, stringc root, IControl* control)
{
	if(strcmp(node->name(), "Id") == 0)	
	{
		control->Id = stringc(node->value());
	}
	else if(strcmp(node->name(), "Name") == 0)	
	{
		control->Name = stringc(node->value());
	}
	else if(strcmp(node->name(), "IsVisible") == 0)	
	{
		control->IsVisible = XMLHelper::GetTextAsBool(node);
	}
	else if(strcmp(node->name(), "OnPackageShow") == 0)	
	{
		control->OnPackageShow = stringc(node->value()).trim();
	}
	else if(strcmp(node->name(), "IsVisibleInEditor") == 0)
	{			
		// Используется только в редакторе		
	}
	else
	{
		return false;
	}
	return true;
}

// Загрузка контрола из XML (<Tag xsi:type="ControlButton">)
IControl* LoaderControlManager::LoadControlButton(
	rapidxml::xml_node<>* node,
	SharedParams_t sharedParams,
	stringc root,
	SoundManager* soundManager, 
	float controlsScale,
	position2di controlsOffset)
{
	stringc texturePath;
	stringc texturePathActivate;
	stringc onClickSound;
	position2di position;
	u32 holdTime = 300;
	stringc textData;
	
	ControlButton* control = new ControlButton(sharedParams, soundManager);
	
	for (xml_node<>* chNode = node->first_node(); chNode; chNode = chNode->next_sibling())
	{
		if (LoadBaseParam(chNode, sharedParams, root, control))
			continue;		
		
		if(strcmp(chNode->name(), "TextureNormal") == 0)			
		{
			texturePath = stringc(chNode->value()).trim();
			if(texturePath != "")
				texturePath = root + texturePath;
		}
		else if(strcmp(chNode->name(), "TextureActivate") == 0)		
		{
			texturePathActivate = stringc(chNode->value()).trim();
			if (texturePathActivate != "")
				texturePathActivate = root + texturePathActivate;
		}
		else if(strcmp(chNode->name(), "HoldTime") == 0)		
		{
			holdTime = XMLHelper::GetTextAsUInt(chNode);
		}
		else if(strcmp(chNode->name(), "OnClickSound") == 0)		
		{
			stringc soundPath = stringc(chNode->value()).trim();
			if (soundPath != "")
				control->OnClickSound = root + soundPath;
		}
		else if(strcmp(chNode->name(), "OnClickDown") == 0)		
		{
			control->OnClickDown = stringc(chNode->value()).trim();
		}
		else if(strcmp(chNode->name(), "OnClickUp") == 0)		
		{
			control->OnClickUp = stringc(chNode->value()).trim();
		}
		else if(strcmp(chNode->name(), "Position") == 0)
		{
			position = XMLHelper::GetPosition2di(chNode);
			position.X = (s32)(controlsScale * position.X) + controlsOffset.X;
			position.Y = (s32)(controlsScale * position.Y) + controlsOffset.Y;
		}
		else if(strcmp(chNode->name(), "IsVisibleInEditor") == 0)
		{
			// используется только в редакторе
		}		
		else
		{
			_DEBUG_BREAK_IF(true)
		}			
	}
	
	control->Init(
		texturePath, 
		texturePathActivate, 
		position, 
		holdTime);
	return control;
}	

// Загрузка контрола из XML (<Tag xsi:type="ControlCircle">)
IControl* LoaderControlManager::LoadControlCircle(
	rapidxml::xml_node<>* node,
	SharedParams_t sharedParams,
	stringc root,
	float controlsScale,
	position2di controlsOffset)
{
	stringc texturePath;
	stringc texturePathCenter;
	position2di position;
	stringc textData;

	ControlCircle* control = new ControlCircle(sharedParams);

	for (xml_node<>* chNode = node->first_node(); chNode; chNode = chNode->next_sibling())
	{
		if (LoadBaseParam(chNode, sharedParams, root, control))
			continue;		
		
		if(strcmp(chNode->name(), "TextureBackground") == 0)
		{
			texturePath = root + stringc(chNode->value());
		}
		else if(strcmp(chNode->name(), "TextureActivate") == 0)
		{
			texturePathCenter = root + stringc(chNode->value());
		}
		else if(strcmp(chNode->name(), "TextureCenter") == 0)
		{
			texturePathCenter = root + stringc(chNode->value());
		}
		else if(strcmp(chNode->name(), "Position") == 0)
		{
			position = XMLHelper::GetPosition2di(chNode);
			position.X = (s32)(controlsScale * position.X) + controlsOffset.X;
			position.Y = (s32)(controlsScale * position.Y) + controlsOffset.Y;
		}
		else if(strcmp(chNode->name(), "IsVisibleInEditor") == 0)
		{
			// Используется только в редакторе
		}
		else
		{
			_DEBUG_BREAK_IF(true)
		}	
	}

	control->Init(texturePath, texturePathCenter, position, controlsScale);
	return control;
}
// Загрузка контрола из XML (<Tag xsi:type="ControlImage">)
IControl* LoaderControlManager::LoadControlImage( 
	rapidxml::xml_node<>* node,
	SharedParams_t sharedParams,
	stringc root,
	float controlsScale,
	position2di controlsOffset)
{
	stringc texturePath;
	position2di position;

	ControlImage* control = new ControlImage(sharedParams);

	for (xml_node<>* chNode = node->first_node(); chNode; chNode = chNode->next_sibling())
	{
		if (LoadBaseParam(chNode, sharedParams, root, control))
			continue;		
		
		if(strcmp(chNode->name(), "Texture") == 0)
		{
			texturePath = root + stringc(chNode->value());
		}
		else if(strcmp(chNode->name(), "Position") == 0)
		{
			position = XMLHelper::GetPosition2di(chNode);
			position.X = (s32)(controlsScale * position.X) + controlsOffset.X;
			position.Y = (s32)(controlsScale * position.Y) + controlsOffset.Y;
		}
		else
		{
			_DEBUG_BREAK_IF(true)
		}	
	}


	control->Init(texturePath, position);
	return control;
}


// Загрузка контрола из XML (<Tag xsi:type="ControlTapScene">)
IControl* LoaderControlManager::LoadTapScene( 
	rapidxml::xml_node<>* node,
	SharedParams_t sharedParams, 
	stringc root,
	float controlsScale,
	position2di controlsOffset)
{
	position2di minPoint;
	position2di maxPoint;
	bool picUpNode;
	int filterId = -1;

	ControlTapScene* control = new ControlTapScene(sharedParams);

	for (xml_node<>* chNode = node->first_node(); chNode; chNode = chNode->next_sibling())
	{
		if (LoadBaseParam(chNode, sharedParams, root, control))
			continue;		
		
		if(strcmp(chNode->name(), "MinPoint") == 0)		
		{
			minPoint = XMLHelper::GetPosition2di(chNode);
			minPoint.X = (s32)(controlsScale * minPoint.X) + controlsOffset.X;
			minPoint.Y = (s32)(controlsScale * minPoint.Y) + controlsOffset.Y;
		}		
		else if(strcmp(chNode->name(), "MaxPoint") == 0)		
		{
			maxPoint = XMLHelper::GetPosition2di(chNode);
			maxPoint.X = (s32)(controlsScale * maxPoint.X) + maxPoint.X;
			maxPoint.Y = (s32)(controlsScale * maxPoint.Y) + maxPoint.Y;
		}		
		else if(strcmp(chNode->name(), "PickSceneNode") == 0)		
		{
			picUpNode = XMLHelper::GetTextAsBool(chNode);
		}		
		else if(strcmp(chNode->name(), "FilterNodeId") == 0)		
		{
			filterId = XMLHelper::GetTextAsInt(chNode);
		}
		else
		{
			_DEBUG_BREAK_IF(true)
		}
	}

	control->Init(minPoint, maxPoint, picUpNode, filterId);
	return control;
}

IControl* LoaderControlManager::LoadBehavior( 
	rapidxml::xml_node<>* node,
	SharedParams_t sharedParams, 
	stringc root, 
	ControlManager* manager , 
	ModuleManager* moduleManager, 
	ScriptCache* scriptCache )
{
	ControlBehavior* control = new ControlBehavior(sharedParams, manager);
	
	for (xml_node<>* chNode = node->first_node(); chNode; chNode = chNode->next_sibling())
	{
		if (LoadBaseParam(chNode, sharedParams, root, control))
			continue;		
		if(strcmp(chNode->name(), "ScriptFileName") == 0)	
		{			
			stringc path = stringc(chNode->value()).trim();
			if(path != "")
			{
				control->SetParser(scriptCache->GetItem(root + path));
			}
		}		
		else if(strcmp(chNode->name(), "ModuleName") == 0)
		{			
			stringc moduleName = stringc(chNode->value()).trim();
			if(moduleName != "")
			{
				control->SetModule(moduleName, moduleManager);
			}			
		}
		else
		{
			_DEBUG_BREAK_IF(true)
		}
	}


	return control;
}

// Загрузка контрола из XML (<Tag xsi:type="ControlText">)
IControl* LoaderControlManager::LoadControlText( 
	rapidxml::xml_node<>* node,
	SharedParams_t sharedParams, 
	stringc root, 
	ControlManager* manager,
	float controlsScale,
	position2di controlsOffset)
{
	ControlText* control = new ControlText(sharedParams);
	
	for (xml_node<>* chNode = node->first_node(); chNode; chNode = chNode->next_sibling())
	{
		if (LoadBaseParam(chNode, sharedParams, root, control))
			continue;	
		if(strcmp(chNode->name(), "Text") == 0)	
		{
			control->Text = stringc(chNode->value());
		}
		else if(strcmp(chNode->name(), "FontPath") == 0)
		{			
			control->Init(root + stringc(chNode->value()));
		}		
		else if(strcmp(chNode->name(), "Position") == 0)
		{
			position2di position = XMLHelper::GetPosition2di(chNode);
			position.X = (s32)(controlsScale * position.X) + controlsOffset.X;
			position.Y = (s32)(controlsScale * position.Y) + controlsOffset.Y;
			control->SetPosition(position);
		}		
		else if(strcmp(chNode->name(), "KerningWidth") == 0)
		{
			control->KerningWidth = XMLHelper::GetTextAsInt(chNode);
		}
		//else if(nodeName == "")
		else if(strcmp(chNode->name(), "KerningHeight") == 0)
		{
			control->KerningHeight = XMLHelper::GetTextAsInt(chNode);
		}
		else
		{
			_DEBUG_BREAK_IF(true)
		}	
	}


	return control;
}

IControl* LoaderControlManager::LoadControlPanel( 
	rapidxml::xml_node<>* node, 
	SharedParams_t sharedParams, 
	stringc root,
	float controlsScale,
	position2di controlsOffset)
{
	ControlPanel* control = new ControlPanel(sharedParams);

	dimension2di size;

	for (xml_node<>* chNode = node->first_node(); chNode; chNode = chNode->next_sibling())
	{
		if (LoadBaseParam(chNode, sharedParams, root, control))
			continue;	
		if(strcmp(chNode->name(), "Width") == 0)	
		{
			size.Width = XMLHelper::GetTextAsInt(chNode);
		}
		else if(strcmp(chNode->name(), "Height") == 0)
		{			
			size.Height = XMLHelper::GetTextAsInt(chNode);
		}
		else if(strcmp(chNode->name(), "Position") == 0)
		{
			position2di position = XMLHelper::GetPosition2di(chNode);
			position.X = (s32)(controlsScale * position.X) + controlsOffset.X;
			position.Y = (s32)(controlsScale * position.Y) + controlsOffset.Y;
			control->SetPosition(position);
		}
		else if(strcmp(chNode->name(), "ScrollHorz") == 0)
		{
			ControlComponentScroll* scroll = LoadControlComponentScroll(
				chNode, sharedParams, root, controlsScale, controlsOffset);
			control->SetHorzScroll(scroll);
		}
		else if(strcmp(chNode->name(), "ScrollVert") == 0)
		{
			ControlComponentScroll* scroll = LoadControlComponentScroll(
				chNode, sharedParams, root, controlsScale, controlsOffset);
			control->SetVertScroll(scroll);
		}
		else if(strcmp(chNode->name(), "ChildrenIds") == 0)
		{
			for (xml_node<>* chNodeId = chNode->first_node(); chNodeId; chNodeId = chNodeId->next_sibling())
			{
				if(strcmp(chNodeId->name(), "string") == 0)
				{
					control->ChildrenIds.push_back(stringc(chNodeId->value()));
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

	control->SetSize(size);

	return control;
}

ControlComponentScroll* LoaderControlManager::LoadControlComponentScroll( 
	rapidxml::xml_node<>* node, 
	SharedParams_t sharedParams, 
	stringc root,
	float controlsScale,
	position2di controlsOffset)
{
	ControlComponentScroll* control = new ControlComponentScroll(sharedParams);
	
	for (xml_node<>* chNode = node->first_node(); chNode; chNode = chNode->next_sibling())
	{
		if(strcmp(chNode->name(), "TextureBody") == 0)
		{			
			stringc textureBody = stringc(chNode->value()).trim();
			if (textureBody != "")
				control->TextureBody = root + textureBody;
		}		
		else if(strcmp(chNode->name(), "TextureBackground") == 0)
		{
			stringc textureBg = stringc(chNode->value()).trim();
			if (textureBg != "")
				control->TextureBackground = root + textureBg;
		}
		else if(strcmp(chNode->name(), "IsVertical") == 0)
		{
			control->IsVertical = XMLHelper::GetTextAsBool(chNode);
		}
		else
		{
			_DEBUG_BREAK_IF(true)
		}	
	}

	return control;
}

