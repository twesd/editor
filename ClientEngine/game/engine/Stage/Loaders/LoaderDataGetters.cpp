#include "LoaderDataGetters.h"
#include "LoaderCommon.h"

#include "../../Controls/ControlEvent.h"
#include "../../Core/Xml/XMLHelper.h"

#include "../UnitPart/DataGetters/DataGetterTapControl.h"
#include "../UnitPart/DataGetters/DataGetterId.h"
#include "../UnitPart/DataGetters/DataGetterDistance.h"

using namespace rapidxml;

// Загрузка получателся данных <DataGetter xsi:type="....">
DataGetterBase* LoaderDataGetters::LoadDataGetterBase( SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc& root, UnitBehavior* behavior, UnitAction* action )
{
	DataGetterBase* dataGetter = NULL;

	stringc xsi_type(sourceNode->first_attribute("xsi:type")->value());
	if(xsi_type == "DataGetterTapControl")
	{
		dataGetter = LoadDataGetterTapControl(params, sourceNode, root, behavior, action);
	}
	else if(xsi_type == "DataGetterId")
	{
		dataGetter = LoadDataGetterId(params, sourceNode, root, behavior, action);
	}
	else if(xsi_type == "DataGetterDistance")
	{
		dataGetter = LoadDataGetterDistance(params, sourceNode, root, behavior, action);
	}
	else
	{
		_DEBUG_BREAK_IF(true)
	}

	dataGetter->SetUnitBehavior(behavior);

	return dataGetter;
}

// Загрузка <DataGetter xsi:type="DataGetterTapControl">
DataGetterBase* LoaderDataGetters::LoadDataGetterTapControl( SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc& root, UnitBehavior* behavior, UnitAction* action )
{
	DataGetterTapControl* getter = new DataGetterTapControl(params);
	for (xml_node<>* chNode = sourceNode->first_node(); chNode; chNode = chNode->next_sibling())		
	{
		if(strcmp(chNode->name(), "TapSceneName") == 0)
		{
			getter->TapSceneName = stringc(chNode->value());
		}
		else if(strcmp(chNode->name(), "GetterType") == 0)
		{
			stringc getterType =  stringc(chNode->value());
			getter->SetGetterType(getterType);
		}
		else
		{
			_DEBUG_BREAK_IF(true)
		}
	}

	return getter;
}

DataGetterBase* LoaderDataGetters::LoadDataGetterId( SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc& root, UnitBehavior* behavior, UnitAction* action )
{
	DataGetterId* getter = new DataGetterId(params);
	for (xml_node<>* chNode = sourceNode->first_node(); chNode; chNode = chNode->next_sibling())		
	{
		if(strcmp(chNode->name(), "FilterNodeId") == 0)
		{
			getter->FilterNodeId = XMLHelper::GetTextAsInt(chNode);
		}
		else
		{
			_DEBUG_BREAK_IF(true)
		}
	}
	return getter;
}

DataGetterBase* LoaderDataGetters::LoadDataGetterDistance( 
	SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc& root, UnitBehavior* behavior, UnitAction* action )
{
	DataGetterDistance* getter = new DataGetterDistance(params);
	for (xml_node<>* chNode = sourceNode->first_node(); chNode; chNode = chNode->next_sibling())		
	{
		if(strcmp(chNode->name(), "FilterNodeId") == 0)
		{
			getter->FilterNodeId = XMLHelper::GetTextAsInt(chNode);
		}
		else if(strcmp(chNode->name(), "Distance") == 0)
		{
			getter->Distance =  XMLHelper::GetTextAsFloat(chNode);
		}
		else if(strcmp(chNode->name(), "CompareType") == 0)
		{
			getter->CompareType = LoaderCommon::GetCompareType( stringc(chNode->value()));
		}
		else if(strcmp(chNode->name(), "UseUnitSize") == 0)
		{
			getter->UseUnitSize =  XMLHelper::GetTextAsBool(chNode);
		}				
		else
		{
			_DEBUG_BREAK_IF(true);
		}
	}
	return getter;
}
