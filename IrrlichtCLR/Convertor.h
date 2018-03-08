#pragma once

class Convertor
{
public:
	Convertor(void){};
	~Convertor(void){};

	static c8* ConvertToU8(String^ inputString)
	{
		//pin_ptr<const WCHAR> str = PtrToStringChars(inputString);		
		cli::array<WCHAR>^ chars = inputString->ToCharArray();
		int length = chars->Length;
		c8* outChars = new c8[length+1];	
		for(int i = 0; i < length; i++)
		{
			outChars[i] = (c8)chars[i];
		}	
		outChars[length] = 0;
		return outChars;
	}
	//static SharedParams_t ConvertToSharedParam(SharedParams_t* params)
	//{
	//	SharedParams_t outParams;
	//	outParams.device =	params->device;
	//	outParams.driver	=	params->driver;
	//	outParams.sceneManager = params->sceneManager;
	//	outParams.timer	=	params->timer;
	//	outParams.event	=	params->event;
	//	outParams.fileSystem = params->fileSystem;
	//	return outParams;
	//}
};

