#include "SceneText.h"
#include "Core/NodeWorker.h"
#include "Core/GeometryWorker.h"
#include "Core/TextureWorker.h"

SceneText::SceneText(SharedParams_t params, stringc pathTextures, ISceneNode* parent) : 
	Base(params), ISceneNode(parent, params.SceneManager),
	_material(), _text()
{
	KerningWidth = 0;
	KerningHeight = 0;
	_textHeight = 0.5;

	_indices[0] = 0;
	_indices[1] = 2;
	_indices[2] = 1;
	_indices[3] = 0;
	_indices[4] = 3;
	_indices[5] = 2;

	_indices[6] = 1;
	_indices[7] = 2;
	_indices[8] = 0;
	_indices[9] = 2;
	_indices[10] = 3;
	_indices[11] = 0;

	video::SColor shadeSide = 0xFFFFFFFF;

	_vertices[0].TCoords.set(1.0f, 1.0f);
	_vertices[0].Color = shadeSide;

	_vertices[1].TCoords.set(1.0f, 0.0f);
	_vertices[1].Color = shadeSide;

	_vertices[2].TCoords.set(0.0f, 0.0f);
	_vertices[2].Color = shadeSide;

	_vertices[3].TCoords.set(0.0f, 1.0f);
	_vertices[3].Color = shadeSide;

	for (s32 i=0; i<4; ++i)
		_vertices[i].Normal = vector3df(0,-1,0);

	NodeWorker::ApplyMaterialSetting(&_material);
	_material.MaterialType =	video::EMT_TRANSPARENT_ALPHA_CHANNEL;

	SetFontPath(pathTextures);
}

SceneText::~SceneText(void)
{
	_items.clear();
}

void SceneText::SetFontPath(stringc pathTextures)
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
		video::ITexture *texture = TextureWorker::GetTexture(Driver, pathTexture,true);
		if(texture == NULL)
		{
			_items.clear();
			return;
		}
		symbol_desc_t item;
		item.texture = texture;
		item.symbol = symbolArray[i];
		_items.push_back(item);
	}
}

video::ITexture* SceneText::GetTextureByChar(char sym)
{
	char upperSym = ansi_upper(sym);
	for(u32 i = 0; i < _items.size(); i++)
	{
		//char t1 = mItems[i].symbol;
		if(_items[i].symbol == upperSym)
		{
			return _items[i].texture;
		}
	}
	return NULL;
}



video::SMaterial& SceneText::getMaterial(u32 i)
{
	return _material;
}


//! returns amount of materials used by this scene node.
u32 SceneText::getMaterialCount() const
{
	return 1;
}

//! pre render event
void SceneText::OnRegisterSceneNode()
{
	if (IsVisible)
		ISceneNode::SceneManager->registerNodeForRendering(this, ESNRP_TRANSPARENT);
	ISceneNode::OnRegisterSceneNode();
}

//! render
void SceneText::render()
{
	if(!IsVisible) return;
	if(_items.size() == 0) return;	
	if(_text == "") return;	
	if (_textHeight <= 0 ) return;

	f32 offsetX = 0;
	f32 offsetY = 0;
	f32 textureCoeff = (f32)_items[0].texture->getOriginalSize().Width / _items[0].texture->getOriginalSize().Height;
	f32 symHeight = _textHeight;	
	f32 symWidth =  _textHeight * textureCoeff;
	bool skipNextSymbol = false;

	// Считаем максимальную длину
	u32 maxLineSize = 0;
	u32 curLineSize = 0;
	for(u32 i = 0; i < _text.size(); i++)
	{	
		curLineSize++;
		char symbol = _text[i];
		char nextSymbol = '?';
		if((i + 1) < _text.size())
		{
			nextSymbol = _text[i + 1];
		}
		// Если встретился \n, то переходим на новую строчку
		//
		if(symbol == '\\' && nextSymbol == 'n')
		{
			if(maxLineSize < curLineSize) 
				maxLineSize = curLineSize;
			curLineSize = 0;
			continue;
		}
	}
	if (maxLineSize == 0) maxLineSize = _text.size();
	f32 lineWidth = (symWidth * maxLineSize)  + (KerningWidth * maxLineSize);

	scene::ICameraSceneNode *camera = ISceneNode::SceneManager->getActiveCamera();
	if(camera == NULL) return;	
	core::vector3df campos = camera->getAbsolutePosition();
	core::vector3df target = camera->getTarget();
	core::vector3df up = camera->getUpVector();
	core::vector3df view = target - campos;
	view.normalize();
	core::vector3df horizontal = up.crossProduct(view);
	if ( horizontal.getLength() == 0 )
	{
		horizontal.set(up.Y,up.X,up.Z);
	}
	horizontal.normalize();
	core::vector3df space = horizontal;
	
	core::vector3df vertical = horizontal.crossProduct(view);
	vertical.normalize();


	vector3df absPos = getAbsolutePosition();	
	absPos += (space*(-0.5f * lineWidth));	

	for(u32 i = 0; i < _text.size(); i++)
	{		
		if(skipNextSymbol)
		{
			skipNextSymbol = false;
			continue;
		}

		char symbol = _text[i];
		char nextSymbol = '?';
		if((i + 1) < _text.size())
		{
			nextSymbol = _text[i + 1];
		}

		// Если встретился \n, то переходим на новую строчку
		//
		if(symbol == '\\' && nextSymbol == 'n')
		{
			offsetX = 0;
			offsetY += symHeight + KerningHeight;

			skipNextSymbol = true;
			continue;
		}

		video::ITexture *texture = GetTextureByChar(symbol);
		if(texture != NULL)
		{
			//s32 x = offsetX + Position.X;
			//s32 y = offsetY + Position.Y;

			//DrawTexture(texture,x,y);			
			//ISceneNode::Parent->getTransformedBoundingBox();
			vector3df charPos = absPos + (space * offsetX) + (vertical * offsetY);
			
			DrawChar(texture, charPos);
		}
		offsetX += (symWidth + KerningWidth);
	}

}

//! returns the axis aligned bounding box of this node
const core::aabbox3d<f32>& SceneText::getBoundingBox() const
{
	return _boundBox;
}

// Установить содержимое текста
void SceneText::SetText( stringc text )
{
	_text = text;
}

// Установить высоту текста
void SceneText::SetTextHeight( f32 height )
{
	_DEBUG_BREAK_IF(height <= 0)
	_textHeight = height;
}

// Отрисовать символ
void SceneText::DrawChar( video::ITexture* texture, vector3df position )
{
	if(texture == NULL) return;
	
	f32 angle = 0;

	scene::ICameraSceneNode *camera = ISceneNode::SceneManager->getActiveCamera();
	if(camera == NULL) return;	

	core::vector3df campos = camera->getAbsolutePosition();
	angle = GeometryWorker::GetAngle(campos, position);

	core::vector3df scale = RelativeScale; //AbsoluteTransformation.getScale();
	core::vector3df rotation = RelativeRotation; //AbsoluteTransformation.getRotationDegrees();

	f32 dimensionWidth = _textHeight * scale.X;
	f32 dimensionHeight = _textHeight * scale.Y;

	_vertices[0].Pos = vector3df(-dimensionWidth/2,-dimensionHeight/2,0);
	_vertices[1].Pos = vector3df(-dimensionWidth/2,dimensionHeight/2,0);
	_vertices[2].Pos = vector3df(dimensionWidth/2,dimensionHeight/2,0);
	_vertices[3].Pos = vector3df(dimensionWidth/2,-dimensionHeight/2,0);

	_matTransform.setRotationDegrees(rotation + vector3df(0,angle,0));
	_matTransform.setTranslation(position);
	_matTransform.transformVect(_vertices[0].Pos);
	_matTransform.transformVect(_vertices[1].Pos);
	_matTransform.transformVect(_vertices[2].Pos);
	_matTransform.transformVect(_vertices[3].Pos);

	_material.setTexture(0, texture);

	Driver->setTransform(video::ETS_WORLD, core::IdentityMatrix);
	Driver->setMaterial(_material);
	Driver->drawIndexedTriangleList(_vertices, 4, _indices, 4);
}


