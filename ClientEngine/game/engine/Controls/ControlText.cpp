#include "ControlText.h"
#include "Core/TextureWorker.h"

ControlText::ControlText(SharedParams_t params) : IControl(params)
{
	IsVisible = true;
	Text = "";
	KerningWidth = 0;
	KerningHeight = 0;
	Width = 0;
}

ControlText::~ControlText(void)
{
	_items.clear();
}

void ControlText::Init(stringc pathTextures)
{

	char symbolArray[] = {
		'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 
		'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
		'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
		'(',')','+','-',':'};
	int size = sizeof(symbolArray);
	for(int i = 0; i < size; i++)
	{		
		stringc strSymbol = "*";		
		strSymbol[0] = symbolArray[i];

		if(symbolArray[i] == '+') strSymbol = "plus";
		if(symbolArray[i] == '-') strSymbol = "minus";		
		if(symbolArray[i] == ':') strSymbol = "dots";		
		if(symbolArray[i] == '(') strSymbol = "open_bracket";		
		if(symbolArray[i] == ')') strSymbol = "close_bracket";		
		
		stringc pathTexture = pathTextures + "/" + strSymbol + ".png";		
		video::ITexture *texture = TextureWorker::GetTexture(Driver, pathTexture, true);
		if(texture == NULL)
		{
			_items.clear();
			return;
		}
		SymbolDesc_t item;
		item.Texture = texture;
		item.Symbol = symbolArray[i];
		_items.push_back(item);
	}
}

void ControlText::Draw()
{
	if(!IsVisible) return;	
	if(_items.size() == 0) return;	
	s32 offsetX = 0;
	s32 offsetY = 0;
	s32 width = _items[0].Texture->getOriginalSize().Width * ControlsScale;
	s32 height = _items[0].Texture->getOriginalSize().Height * ControlsScale;	
	bool skipNextSymbol = false;
	for(u32 i = 0; i < Text.size(); i++)
	{		
		if(skipNextSymbol)
		{
			skipNextSymbol = false;
			continue;
		}

		char symbol = Text[i];
		char nextSymbol = '?';
		if((i + 1) < Text.size())
		{
			nextSymbol = Text[i + 1];
		}

		// Если встретился \n, то переходим на новую строчку
		//
		if((symbol == '\\' && nextSymbol == 'n') || (symbol == '\n'))
		{
			offsetX = 0;
			offsetY += height + KerningHeight;
			if (symbol != '\n')
			{
				skipNextSymbol = true;
			}			
			continue;
		}

		video::ITexture *texture = GetTextureByChar(symbol);
		if(texture != NULL)
		{
			s32 x = offsetX + _position.X;
			s32 y = offsetY + _position.Y;

			if (ControlsScale != 1.0f)
			{
				dimension2di size = texture->getOriginalSize();
				recti destRect(
					_position.X, _position.Y, 
					(s32) (_position.X + width), 
					(s32) (_position.Y + height));

				Driver->draw2DImage(
					texture, 
					destRect, 
					recti(0, 0, size.Width, size.Height),
					NULL, 
					0, 
					true);
			}
			else 
			{
				TextureWorker::DrawTexture(Driver, texture,x,y);
			}
		}
#ifdef HORIZONTAL_DISPLAY
		offsetX += height;
#else
		offsetX += (width + KerningWidth);
#endif


	}
}

video::ITexture* ControlText::GetTextureByChar(char sym)
{
	char upperSym = ansi_upper(sym);
	for(u32 i = 0; i < _items.size(); i++)
	{
		if(_items[i].Symbol == upperSym)
		{
			return _items[i].Texture;
		}
	}
	return NULL;
}

void ControlText::Update( reciverInfo_t *reciverInfo )
{
	// ничего
}

irr::core::position2di ControlText::GetPosition()
{
	return _position;
}

irr::core::recti ControlText::GetBounds()
{
	return recti(0,0,0,0);
}

void ControlText::SetPosition( position2di newPosition )
{
	_position = newPosition;
}
