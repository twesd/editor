#pragma once
#include "../ICoreNode.h"

class SceneText : public Base, public ISceneNode
{
public:
	SceneText(SharedParams_t params, stringc pathTextures, ISceneNode* parent);
	virtual ~SceneText(void);

	// Установить путь до директории с шрифтом
	void SetFontPath(stringc pathTextures);

	// Установить содержимое текста
	void SetText(stringc text);

	// Установить высоту текста
	void SetTextHeight(f32 height);

	//---- ISceneNode ----

	//! pre render event
	virtual void OnRegisterSceneNode();
	//! render
	virtual void render();
	//! returns the axis aligned bounding box of this node
	virtual const core::aabbox3d<f32>& getBoundingBox() const;
	video::SMaterial& getMaterial(u32 i);
	u32 getMaterialCount() const;

	// --- End ISceneNode ---

	// Отступ между буквами
	f32 KerningWidth;

	// Отступ между буквами по высоте
	f32 KerningHeight;
private:
	typedef struct
	{
		video::ITexture* texture;
		char symbol;
	}symbol_desc_t;

	// Получить текстуру для символа
	video::ITexture *GetTextureByChar(char sym);

	// Отрисовать символ
	void DrawChar(video::ITexture* texture, vector3df pos);

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

	core::array<symbol_desc_t> _items;

	// Содержимое текста
	stringc _text;

	// Высота текста
	f32 _textHeight;

	video::S3DVertex _vertices[4];
	u16 _indices[12];
	video::SMaterial _material;
	core::matrix4 _matTransform;
	core::aabbox3d<f32> _boundBox;
};
