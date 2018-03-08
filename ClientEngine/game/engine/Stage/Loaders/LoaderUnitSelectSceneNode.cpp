#include "LoaderUnitSelectSceneNode.h"
#include "../../Controls/ControlEvent.h"
#include "../../Core/Xml/XMLHelper.h"
#include "../../Core/Animators/TextureAnimator.h"

#include "LoaderCommon.h"

#include "../UnitPart/UnitSelectSceneNodes/UnitSelectSceneNodeDistance.h"
#include "../UnitPart/UnitSelectSceneNodes/UnitSelectSceneNodeTapControl.h"
#include "../UnitPart/UnitSelectSceneNodes/UnitSelectSceneNodeData.h"
#include "../UnitPart/UnitSelectSceneNodes/UnitSelectSceneNodeId.h"

using namespace rapidxml;

UnitSelectSceneNodeBase* LoaderUnitSelectSceneNode::LoadUnitSelectSceneNodeBase( 
	SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc& root, UnitBehavior* behavior, UnitAction* action )
{
	UnitSelectSceneNodeBase* selector = NULL;

	char* xsi_type = sourceNode->first_attribute("xsi:type")->value();
	if(strcmp(xsi_type, "UnitSelectSceneNodeDistance") == 0)
	{
		selector = LoadUnitSelectSceneNodeDistance(params, sourceNode, root, behavior, action);
	}
	else if(strcmp(xsi_type, "UnitSelectSceneNodeTapControl") == 0)
	{
		selector = LoadUnitSelectSceneNodeTapControl(params, sourceNode, root, behavior, action);
	}
	else if(strcmp(xsi_type, "UnitSelectSceneNodeData") == 0)
	{
		selector = LoadUnitSelectSceneNodeData(params, sourceNode, root, behavior, action);
	}
	else if(strcmp(xsi_type, "UnitSelectSceneNodeId") == 0)
	{
		selector = LoadUnitSelectSceneNodeId(params, sourceNode, root, behavior, action);
	}
	else
	{
		_DEBUG_BREAK_IF(true)
	}
	return selector;
}

// Загрузить <UnitSelectSceneNodeBase xsi:type="UnitSelectSceneNodeDistance">
UnitSelectSceneNodeBase* LoaderUnitSelectSceneNode::LoadUnitSelectSceneNodeDistance( 
	SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc& root, UnitBehavior* behavior, UnitAction* action )
{
	UnitSelectSceneNodeDistance* selector = new UnitSelectSceneNodeDistance(params);
	
	for (xml_node<>* chNode = sourceNode->first_node(); chNode; chNode = chNode->next_sibling())
	{
		if(strcmp(chNode->name(), "FilterNodeId") == 0)
		{
			selector->FilterNodeId = XMLHelper::GetTextAsInt(chNode);
		}
		else if(strcmp(chNode->name(), "SelectChilds") == 0)
		{
			selector->SelectChilds = XMLHelper::GetTextAsBool(chNode);
		}
		else if(strcmp(chNode->name(), "Distance") == 0)
		{
			selector->Distance = XMLHelper::GetTextAsFloat(chNode);
		}
		else if(strcmp(chNode->name(), "CompareType") == 0)
		{
			selector->CompareType = LoaderCommon::GetCompareType(stringc(chNode->value()));
		}
		else if(strcmp(chNode->name(), "UseUnitSize") == 0)
		{
			selector->UseUnitSize = XMLHelper::GetTextAsBool(chNode);
		}				
		else
		{
			_DEBUG_BREAK_IF(true);
		}
	}	

	return selector;
}


// Загрузить <UnitSelectSceneNodeBase xsi:type="UnitSelectSceneNodeTapControl">
UnitSelectSceneNodeBase* LoaderUnitSelectSceneNode::LoadUnitSelectSceneNodeTapControl( SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc& root, UnitBehavior* behavior, UnitAction* action )
{
	UnitSelectSceneNodeTapControl* selector = new UnitSelectSceneNodeTapControl(params);

	for (xml_node<>* chNode = sourceNode->first_node(); chNode; chNode = chNode->next_sibling())
	{
		if(strcmp(chNode->name(), "FilterNodeId") == 0)
		{
			selector->FilterNodeId = XMLHelper::GetTextAsInt(chNode);
		}
		else if(strcmp(chNode->name(), "SelectChilds") == 0)
		{
			selector->SelectChilds = XMLHelper::GetTextAsBool(chNode);
		}
		else if(strcmp(chNode->name(), "TapSceneName") == 0)
		{
			selector->TapSceneName = stringc(chNode->value());
		}
		else
		{
			_DEBUG_BREAK_IF(true)
		}
	}	

	return selector;
}

// Загрузить <UnitSelectSceneNodeBase xsi:type="UnitSelectSceneNodeData">
UnitSelectSceneNodeBase* LoaderUnitSelectSceneNode::LoadUnitSelectSceneNodeData( 
	SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc& root, UnitBehavior* behavior, UnitAction* action )
{
	UnitSelectSceneNodeData* selector = new UnitSelectSceneNodeData(params);

	for (xml_node<>* chNode = sourceNode->first_node(); chNode; chNode = chNode->next_sibling())
	{
		if(strcmp(chNode->name(), "FilterNodeId") == 0)
		{
			selector->FilterNodeId = XMLHelper::GetTextAsInt(chNode);
		}
		else if(strcmp(chNode->name(), "SelectChilds") == 0)
		{
			selector->SelectChilds = XMLHelper::GetTextAsBool(chNode);
		}
		else if(strcmp(chNode->name(), "DataName") == 0)
		{
			selector->DataName = stringc(chNode->value());
		}
		else
		{
			_DEBUG_BREAK_IF(true)
		}
	}	

	return selector;
}

// Загрузить <UnitSelectSceneNodeBase xsi:type="UnitSelectSceneNodeId">
UnitSelectSceneNodeBase* LoaderUnitSelectSceneNode::LoadUnitSelectSceneNodeId( SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc& root, UnitBehavior* behavior, UnitAction* action )
{
	UnitSelectSceneNodeId* selector = new UnitSelectSceneNodeId(params);

	for (xml_node<>* chNode = sourceNode->first_node(); chNode; chNode = chNode->next_sibling())
	{
		if(strcmp(chNode->name(), "FilterNodeId") == 0)
		{
			selector->FilterNodeId = XMLHelper::GetTextAsInt(chNode);
		}
		else if(strcmp(chNode->name(), "SelectChilds") == 0)
		{
			selector->SelectChilds = XMLHelper::GetTextAsBool(chNode);
		}
		else
		{
			_DEBUG_BREAK_IF(true)
		}
	}	

	return selector;
}