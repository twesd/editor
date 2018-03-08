#pragma once
#include "../Base.h"

class Billboard : public Base, public ISceneNode
{
public:
	Billboard(SharedParams_t params);
	virtual ~Billboard(void);

	void SetDimension(dimension2df dimension);
	
	void SetUseUpVector(bool useIt);

	void SetUpVector(vector3df up);

	void SetUseViewVector(bool useIt);

	void SetViewVector(vector3df vec);

	video::SColor GetColor() const;

	void SetColor(const video::SColor& color);

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

	video::SColor _color;

	dimension2df _dimension;

	core::matrix4 _matTransform;

	video::S3DVertex _vertices[4];
	
	u16 _indices[12];

	video::SMaterial _material;

	core::aabbox3d<f32> _boundBox;

	bool _useUpVector;
	
	vector3df _upVector;

	bool _useViewVector;

	vector3df _viewVector;
};
