#include "UserSettingsWorker.h"
#include "../Core/Xml/XMLFileDocument.h"

#if defined(WINDOWS_COMPILE) || defined(ANDROID_COMPILE)

using namespace rapidxml;

void UserSettingsWorker::Load( SharedParams_t sharedParams, UserSettings* settings )
{
	if(!sharedParams.FileSystem->existFile("UserSettings.xml"))
		return;

	XMLFileDocument xmlFileDoc("UserSettings.xml", sharedParams.FileSystem);

	xml_document<>* doc = xmlFileDoc.GetDocument();
	if(!doc) 
		return;
	xml_node<>* rootNode = doc->first_node();

	for (xml_node<>* chNode = rootNode->first_node(); chNode; chNode = chNode->next_sibling())
	{
		stringc name(chNode->first_attribute("Name")->value());
		stringc val(chNode->first_attribute("Value")->value());
		sharedParams.Settings->SetTextSetting(name, val);
	}
	
}

void UserSettingsWorker::Save( SharedParams_t sharedParams, UserSettings* settings )
{
	io::IWriteFile* writeFile = sharedParams.FileSystem->createAndWriteFile("UserSettings.xml");
	if(writeFile == NULL) return;

	core::array<Parameter> params = settings->GetParameters();

	stringc text;
	text = "<Root>\n";
	writeFile->write(text.c_str(), text.size());
	for (u32 i = 0; i < params.size() ; i++)
	{
		stringw name = params[i].Name;
		stringw val = params[i].Value;
		text = stringc("<Text Name=\"") + name + 
			stringc("\" Value=\"") + val + stringc("\" ></Text>\n");
		writeFile->write(text.c_str(), text.size());
	}
	text = "</Root>\n";
	writeFile->write(text.c_str(), text.size());

	writeFile->drop();
}



#endif
