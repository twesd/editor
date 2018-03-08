#include "DebugSettings.h"
#include "../../Core/Xml/XMLFileDocument.h"

#ifdef DEBUG_VISUAL_DATA

using namespace rapidxml;


DebugSettings::DebugSettings() :
	FontPath(), FilterIds()
{
	DynamicShowParameters = false;
	DynamicShowAction = false;
	Enabled = false;
	DynamicDebugInfoMode = false;	
	DebugInfoFilterId = 0;
}

void DebugSettings::LoadFromFile(SharedParams_t params, stringc path)
{
	DebugSettings* settings = GetInstance();

	XMLFileDocument xmlFileDoc(path.c_str(), params.FileSystem);

	xml_document<>* doc = xmlFileDoc.GetDocument();
#ifdef DEBUG_MESSAGE_PRINT
	if(!doc) 
		printf("[ERROR] <DebugSettings::LoadFromFile>[xmlFile](null pointer)\n");
#endif
	stringc root = params.FileSystem->getFileDir(path) + "/";
	xml_node<>* rootNode = doc->first_node();

	for (xml_node<>* chNode = rootNode->first_node(); chNode; chNode = chNode->next_sibling())
	{		
		if(strcmp(chNode->name(), "Enabled") == 0)		
		{
			settings->Enabled = XMLHelper::GetTextAsBool(chNode);
		}
		else if(strcmp(chNode->name(), "FontPath") == 0)		
		{
			settings->FontPath = stringc(chNode->value());				
		}
		else if(strcmp(chNode->name(), "DynamicShowParameters") == 0)		
		{
			settings->DynamicShowParameters = XMLHelper::GetTextAsBool(chNode);
		}
		else if(strcmp(chNode->name(), "DynamicShowAction") == 0)		
		{
			settings->DynamicShowAction = XMLHelper::GetTextAsBool(chNode);
		}
		else if(strcmp(chNode->name(), "FilterId") == 0)		
		{
			settings->FilterIds.push_back(XMLHelper::GetTextAsInt(chNode));
		}
	}

}

// Содержит ли фильтр моделий данный индентификатор
bool DebugSettings::ContainsNodeId( int id )
{
	for (u32 i = 0; i < FilterIds.size() ; i++)
	{
		if((FilterIds[i] & id) != 0)
			return true;
	}
	return false;
}

#endif