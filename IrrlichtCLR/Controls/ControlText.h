#pragma once
#include "IControl.h"
#include "../Core/Parsers/ParserAS.h"

class ControlText : public IControl
{
public:
	ControlText(SharedParams_t params);
	virtual ~ControlText(void);
	
	CONTROL_TYPE GetControlType()
	{
		return CONTROL_TYPE_TEXT;
	};

	void Init(stringc pathTextures);
	
	///////////////// IControl ////////////////////
	
	void Draw();	
	
	void Update(reciverInfo_t *reciverInfo);

	// Установить позицию контрола
	void SetPosition(position2di newPosition);

	// Получить позицию контрола
	position2di GetPosition();

	// Получить границы контрола
	recti GetBounds();


	/////////////////////////////////////

	s32 Width;

	stringc Text;

	// Отступ между буквами
	s32 KerningWidth;

	// Отступ между буквами по высоте
	s32 KerningHeight;
private:
	typedef struct
	{
		video::ITexture *Texture;
		char Symbol;
	}SymbolDesc_t;

	video::ITexture* GetTextureByChar(char sym);

	//! Returns a character converted to lower case
	inline char ansi_lower ( u32 x ) const
	{
		return x >= 'A' && x <= 'Z' ? (char) x + 0x20 : (char) x;
	}

	//! Returns a character converted to lower case
	inline char ansi_upper ( u32 x ) const
	{
		return x >= 'a' && x <= 'z' ? (char) x - 0x20 : (char) x;
	}

	core::array<SymbolDesc_t> _items;

	position2di _position;
};
