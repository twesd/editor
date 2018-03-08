#pragma once
#include "../Base.h"

class PolylineSceneNode : public Base, public ISceneNode
{
public:
	PolylineSceneNode(SharedParams_t params);

	virtual ~PolylineSceneNode(void);

	void Clear();

	void AddPoint(vector3df point);

	// Установка ширины линии
	void SetWidth( f32 width );

	// Установить видимость вершин
	void SetVertexVisible(bool visible);

	void SetColor(video::SColor& color);

	void SetVertexColor(video::SColor& color);

	//---- ISceneNode ----

	//! pre render event
	virtual void OnRegisterSceneNode();
	//! render
	virtual void render();
	//! returns the axis aligned bounding box of this node
	virtual const core::aabbox3d<f32>& getBoundingBox() const;
	video::SMaterial& getMaterial(u32 i);
	u32 getMaterialCount() const;
	
private:
	void Resize( SMeshBuffer* buf, f32 size);

	void SetVertexColors(IMesh* mesh, video::SColor color);

	video::SMaterial _material;

	SMesh _mesh;

	core::array<vector3df> _points;

	core::aabbox3d<f32> _box;

	// Цвет линий
	video::SColor _color;

	// Цвет вершин
	video::SColor _vertexColor;

	// Ширина линии
	f32 _width;

	// Видимость вершин
	bool _vertexVisible;
};
