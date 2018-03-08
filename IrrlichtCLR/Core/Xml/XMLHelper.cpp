#include "XMLHelper.h"

using namespace rapidxml;

bool XMLHelper::GetTextAsBool(rapidxml::xml_node<>* node)
{
	return (strcmp(node->value(), "true") == 0);
}

f32 XMLHelper::GetTextAsFloat(rapidxml::xml_node<>* node)
{	
	return core::fast_atof(node->value());
}

s32 XMLHelper::GetTextAsInt(rapidxml::xml_node<>* node)
{
	return (s32)GetTextAsFloat(node);
}

u32 XMLHelper::GetTextAsUInt(rapidxml::xml_node<>* node)
{
	return (u32)GetTextAsFloat(node);
}

// Получение позиции из тагов (x,y)
position2di XMLHelper::GetPosition2di(rapidxml::xml_node<>* node)
{
	position2di pos2d;
	for (xml_node<>* chNode = node->first_node(); chNode; chNode = chNode->next_sibling())
	{
		if(strcmp(chNode->name(), "X") == 0)
		{
			pos2d.X = XMLHelper::GetTextAsInt(chNode);
		}
		else if(strcmp(chNode->name(), "Y") == 0)
		{
			pos2d.Y = XMLHelper::GetTextAsInt(chNode);
		}
		else if(strcmp(chNode->name(), "Z") == 0)
		{
		}
		else 
		{
			_DEBUG_BREAK_IF(true)
		}
	}
	return pos2d;
}

// Получение позиции из тагов (x,y,z)
vector3df XMLHelper::GetVector3df(rapidxml::xml_node<>* node)
{
	vector3df pos;
	for (xml_node<>* chNode = node->first_node(); chNode; chNode = chNode->next_sibling())
	{
		if(strcmp(chNode->name(), "X") == 0)
		{
			pos.X = XMLHelper::GetTextAsFloat(chNode);
		}
		else if(strcmp(chNode->name(), "Y") == 0)
		{
			pos.Y = XMLHelper::GetTextAsFloat(chNode);
		}
		else if(strcmp(chNode->name(), "Z") == 0)
		{
			pos.Z = XMLHelper::GetTextAsFloat(chNode);
		}
		else 
		{
			_DEBUG_BREAK_IF(true)
		}
	}
	return pos;
}

core::array<rapidxml::xml_node<>*> XMLHelper::SelectAllByNodeName(rapidxml::xml_node<>* rootNode, const char* nodeName)
{
	core::array<rapidxml::xml_node<>*> outNodes;
	
	for (rapidxml::xml_node<>* node = rootNode->first_node(); node; node = node->next_sibling())
	{
		if(strcmp(nodeName, node->name()) == 0)
		{
			outNodes.push_back(node);
		}
		core::array<rapidxml::xml_node<>*> childNodes =  SelectAllByNodeName(node, nodeName);
		for(u32 iNode = 0; iNode < childNodes.size(); iNode++)
		{
			outNodes.push_back(childNodes[iNode]);
		}
	}
	return outNodes;
}

core::array<rapidxml::xml_node<>*> XMLHelper::Select(rapidxml::xml_node<>* rootNode, stringc xpath)
{
	core::array<rapidxml::xml_node<>*> outNodes;
	core::array<stringc> paths;
	s32 index = xpath.findFirst('/');
	while(index > 0)
	{
		paths.push_back(xpath.subString(0, index));
		xpath = xpath.subString(index + 1, xpath.size());
		index = xpath.findFirst('/');		
	}
	if(xpath != "")
		paths.push_back(xpath);
	
	core::array<rapidxml::xml_node<>*> iterNodes;
	iterNodes.push_back(rootNode);
	for(u32 iPath = 0; iPath < paths.size(); iPath++)
	{
		bool isLast = (iPath == paths.size() - 1);		
		const char* nodeName = paths[iPath].c_str();
		core::array<rapidxml::xml_node<>*> foundNodes;
		for(u32 indexIter = 0; indexIter < iterNodes.size(); indexIter++)
		{
			for (rapidxml::xml_node<>* node = iterNodes[indexIter]->first_node(); node; node = node->next_sibling())
			{
				if(strcmp(nodeName, node->name()) == 0)
				{
					foundNodes.push_back(node);
					if(isLast)
						outNodes.push_back(node);
				}
			}
		}
		if(foundNodes.size() == 0) 
			break;		
		iterNodes = foundNodes;
	}
	return outNodes;
}

// Прочитать объект типа Boundbox
irr::core::aabbox3df XMLHelper::GetBoundbox( rapidxml::xml_node<>* node )
{
	core::aabbox3df box;
	box.MinEdge = GetVector3df(node->first_node("MinPoint"));
	box.MaxEdge = GetVector3df(node->first_node("MaxPoint"));
	return box;
}

video::SColor XMLHelper::GetTextAsSColor( xml_node<>* node )
{
	video::SColor color;
	for (xml_node<>* chNode = node->first_node(); chNode; chNode = chNode->next_sibling())
	{
		if(strcmp(chNode->name(), "A") == 0)
		{
			color.setAlpha(XMLHelper::GetTextAsUInt(chNode));
		}
		else if(strcmp(chNode->name(), "R") == 0)
		{
			color.setRed(XMLHelper::GetTextAsUInt(chNode));
		}
		else if(strcmp(chNode->name(), "G") == 0)
		{
			color.setGreen(XMLHelper::GetTextAsUInt(chNode));
		}
		else if(strcmp(chNode->name(), "B") == 0)
		{
			color.setBlue(XMLHelper::GetTextAsUInt(chNode));
		}
		else 
		{
			_DEBUG_BREAK_IF(true)
		}
	}
	return color;
}

dimension2df XMLHelper::GetTextAsDimension2d( xml_node<>* node )
{
	dimension2df dimension;
	for (xml_node<>* chNode = node->first_node(); chNode; chNode = chNode->next_sibling())
	{
		if(strcmp(chNode->name(), "Width") == 0)
		{
			dimension.Width = (f32)XMLHelper::GetTextAsInt(chNode);
		}
		else if(strcmp(chNode->name(), "Height") == 0)
		{
			dimension.Height = (f32)XMLHelper::GetTextAsInt(chNode);
		}
		else 
		{
			_DEBUG_BREAK_IF(true)
		}
	}
	return dimension;
}
