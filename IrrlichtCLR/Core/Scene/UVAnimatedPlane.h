#pragma once
#include "../Base.h"

class UVAnimatedPlane :
	public Base, public scene::ISceneNode
{
public:
	UVAnimatedPlane(SharedParams_t params);
	virtual ~UVAnimatedPlane(void);
	void draw();
	void setTexture(video::ITexture *texture);
	void setMaterialType(video::E_MATERIAL_TYPE type);	
	void setDimension(dimension2df dimension);
	void setParent(scene::ISceneNode* parent);
	void setPosition(vector3df position);
	void setRotationByCenter(vector3df rotation,vector3df center);

	inline vector3df getPosition(){
		return mPosition;
	}
	void setRotation(vector3df rotation);

	inline vector3df getRotation(){
		return mRotation;
	}
	inline void setVisible(bool visible){
		mIsVisible = visible;
	}
	inline bool getVisible(){
		return mIsVisible;
	}
	inline f32 getAnimationStep(){
		return mPosStep;
	}
	//step - is step that increment block position each frame
	void setAnimationStep(f32 step);
	//speed - is time per frame
	void setAnimationSpeed(u32 speed);
	//устанавливаем начальный размер блока в текстуре, который будет двигаться до 1.0f
	//size должен быть меньше еденицы
	void setAnimationBlockSize(f32 size);

	//устанавливаем двойную сторону, т.е. со всех сторон будет видна плоскость
	void setDoubleSide(bool value){
		mIsDoubleSide = value;
	}	

	bool getDoubleSide(){
		return mIsDoubleSide;
	}


	//OVERVRIDE
	//! pre render event
	virtual void OnRegisterSceneNode();

	//! render
	virtual void render();

	//! returns the axis aligned bounding box of this node
	virtual const core::aabbox3d<f32>& getBoundingBox() const;

	video::SMaterial& getMaterial(u32 i);
	u32 getMaterialCount() const;
	
private:
	u32 mAnimationSpeed;
	u32 mFireTime;
	f32 mBlockSize;
	f32 mPosUV;
	f32 mPosStep;
	vector3df mPosition;
	vector3df mRotation;
	vector3df mRotationByCenter;
	vector3df mCenterRotation;
	bool mIsRotateByCenterEnabled;
	dimension2df mDimension;
	scene::ISceneNode* mParentNode;
	bool mIsVisible;
	bool mIsDoubleSide;
	core::matrix4 mMatTransform;
	core::aabbox3d<f32> mBox;

	video::S3DVertex mVertices[4];
	video::S3DVertex mDoubleVertices[4];
	u16 mIndices[6];
	video::SMaterial mMaterial;
};
