#include "UVAnimatedPlane.h"
#include "Core/NodeWorker.h"

UVAnimatedPlane::UVAnimatedPlane(SharedParams_t params) : 
Base(params), ISceneNode(params.SceneManager->getRootSceneNode(), params.SceneManager) 
{
	mIsVisible = false;
	mIsDoubleSide = false;
	mIsRotateByCenterEnabled = false;
	mParentNode = NULL;
	mAnimationSpeed = 0;
	mBlockSize = 0;
	mFireTime = 0;
	mPosUV = 0;
	mPosStep = 0.01f;

	mIndices[0] = 0;
	mIndices[1] = 2;
	mIndices[2] = 1;
	mIndices[3] = 0;
	mIndices[4] = 3;
	mIndices[5] = 2;

	video::SColor shadeSide = 0xFFFFFFFF;

	mVertices[0].TCoords.set(1.0f, 1.0f);
	mVertices[0].Color = shadeSide;
	mVertices[1].TCoords.set(1.0f, 0.0f);
	mVertices[1].Color = shadeSide;
	mVertices[2].TCoords.set(0.0f, 0.0f);
	mVertices[2].Color = shadeSide;
	mVertices[3].TCoords.set(0.0f, 1.0f);
	mVertices[3].Color = shadeSide;
	for (s32 i=0; i<4; ++i)
		mVertices[i].Normal = vector3df(0,-1,0);

	mDoubleVertices[0].TCoords.set(1.0f, 1.0f);
	mDoubleVertices[0].Color = shadeSide;
	mDoubleVertices[1].TCoords.set(1.0f, 0.0f);
	mDoubleVertices[1].Color = shadeSide;
	mDoubleVertices[2].TCoords.set(0.0f, 0.0f);
	mDoubleVertices[2].Color = shadeSide;
	mDoubleVertices[3].TCoords.set(0.0f, 1.0f);
	mDoubleVertices[3].Color = shadeSide;
	for (s32 i=0; i<4; ++i)
		mDoubleVertices[i].Normal = vector3df(0,-1,0);

	NodeWorker::ApplyMaterialSetting(&mMaterial);
	NodeWorker::ApplyMeshSetting(this);
}

UVAnimatedPlane::~UVAnimatedPlane(void)
{
	removeAll();
	remove();
}

void UVAnimatedPlane::setTexture(video::ITexture *texture)
{
	mMaterial.setTexture(0,texture);	
}

void UVAnimatedPlane::setMaterialType(video::E_MATERIAL_TYPE materialType)
{
	mMaterial.MaterialType = materialType;	
}

void UVAnimatedPlane::setPosition(vector3df position)
{	
	mPosition = position;
}

void UVAnimatedPlane::setRotation(vector3df rotation){
	mRotation = rotation;	
}

void UVAnimatedPlane::setRotationByCenter(vector3df rotation,vector3df center){
	mIsRotateByCenterEnabled = true;
	mRotationByCenter = rotation;
	mCenterRotation = center;
}

void UVAnimatedPlane::setDimension(dimension2df dimension){
	mDimension = dimension;
}

void UVAnimatedPlane::setParent(scene::ISceneNode* parent){
	mParentNode = parent;
}

void UVAnimatedPlane::setAnimationSpeed(u32 speed){
	mAnimationSpeed = speed;
}

//устанавливаем начальный размер блока в текстуре, который будет двигаться до 1.0f
//size должен быть меньше еденицы
void UVAnimatedPlane::setAnimationBlockSize(f32 size){
	if(size > 0.0f && size < 1.0f){
		mBlockSize = size;
		mVertices[0].TCoords.set(1.0f, mBlockSize);
		mVertices[1].TCoords.set(1.0f, 0.0f);
		mVertices[2].TCoords.set(0.0f, 0.0f);
		mVertices[3].TCoords.set(0.0f, mBlockSize);
	}

}

//step - is step that increment block position each frame
void UVAnimatedPlane::setAnimationStep(f32 step){
	mPosStep = step;
}

void UVAnimatedPlane::draw(){
	if(!mIsVisible) return;

	if(mAnimationSpeed != 0 && mBlockSize != 0){
		u32 nowTime = GetNowTime();
		if(nowTime > mFireTime){
			if((mPosUV) > (1.0f+mBlockSize)) mPosUV -= 1.0f;
			mFireTime = nowTime + mAnimationSpeed;
			mVertices[0].TCoords.set(1.0f, mPosUV+mBlockSize);
			mVertices[1].TCoords.set(1.0f, mPosUV);
			mVertices[2].TCoords.set(0.0f, mPosUV);
			mVertices[3].TCoords.set(0.0f, mPosUV+mBlockSize);
			mPosUV += mPosStep;
		}
	}

	mVertices[0].Pos = vector3df(-mDimension.Width/2,-mDimension.Height/2,0);
	mVertices[1].Pos = vector3df(-mDimension.Width/2,mDimension.Height/2,0);
	mVertices[2].Pos = vector3df(mDimension.Width/2,mDimension.Height/2,0);
	mVertices[3].Pos = vector3df(mDimension.Width/2,-mDimension.Height/2,0);
	mBox.MinEdge = mVertices[0].Pos;
	mBox.MaxEdge = mVertices[3].Pos;

	updateAbsolutePosition();

	if(mIsDoubleSide){
		mDoubleVertices[0].Pos = mVertices[0].Pos;
		mDoubleVertices[1].Pos = mVertices[1].Pos;
		mDoubleVertices[2].Pos = mVertices[2].Pos;
		mDoubleVertices[3].Pos = mVertices[3].Pos;

		mDoubleVertices[0].TCoords = mVertices[0].TCoords;
		mDoubleVertices[1].TCoords = mVertices[1].TCoords;
		mDoubleVertices[2].TCoords = mVertices[2].TCoords;
		mDoubleVertices[3].TCoords = mVertices[3].TCoords;
	}

	mMatTransform.setRotationDegrees(mRotation);
	mMatTransform.setTranslation(vector3df(0,0,0));
	mMatTransform.transformVect(mVertices[0].Pos);
	mMatTransform.transformVect(mVertices[1].Pos);
	mMatTransform.transformVect(mVertices[2].Pos);
	mMatTransform.transformVect(mVertices[3].Pos);

	if(mIsDoubleSide){
		mMatTransform.setRotationDegrees(mRotation - vector3df(0,180,0));
		mMatTransform.setTranslation(vector3df(0,0,0));
		mMatTransform.transformVect(mDoubleVertices[0].Pos);
		mMatTransform.transformVect(mDoubleVertices[1].Pos);
		mMatTransform.transformVect(mDoubleVertices[2].Pos);
		mMatTransform.transformVect(mDoubleVertices[3].Pos);
	}

	//Поварачиваем объект вокруг заданного центра
	if(mIsRotateByCenterEnabled){
		vector3df rotateCenter = mPosition - mCenterRotation;

		mMatTransform.setTranslation(rotateCenter);
		mMatTransform.setRotationDegrees(vector3df(0,0,0));
		mMatTransform.transformVect(mVertices[0].Pos);
		mMatTransform.transformVect(mVertices[1].Pos);
		mMatTransform.transformVect(mVertices[2].Pos);
		mMatTransform.transformVect(mVertices[3].Pos);

		mMatTransform.setTranslation(vector3df(0,0,0));
		mMatTransform.setRotationDegrees(mRotationByCenter);		
		mMatTransform.transformVect(mVertices[0].Pos);
		mMatTransform.transformVect(mVertices[1].Pos);
		mMatTransform.transformVect(mVertices[2].Pos);
		mMatTransform.transformVect(mVertices[3].Pos);
		//return to original position
		mMatTransform.setTranslation(-rotateCenter);
		mMatTransform.setRotationDegrees(vector3df(0,0,0));
		mMatTransform.transformVect(mVertices[0].Pos);
		mMatTransform.transformVect(mVertices[1].Pos);
		mMatTransform.transformVect(mVertices[2].Pos);
		mMatTransform.transformVect(mVertices[3].Pos);
		if(mIsDoubleSide){
			mMatTransform.setTranslation(rotateCenter);
			mMatTransform.setRotationDegrees(vector3df(0,0,0));
			mMatTransform.transformVect(mDoubleVertices[0].Pos);
			mMatTransform.transformVect(mDoubleVertices[1].Pos);
			mMatTransform.transformVect(mDoubleVertices[2].Pos);
			mMatTransform.transformVect(mDoubleVertices[3].Pos);

			mMatTransform.setTranslation(vector3df(0,0,0));
			mMatTransform.setRotationDegrees(mRotationByCenter);		
			mMatTransform.transformVect(mDoubleVertices[0].Pos);
			mMatTransform.transformVect(mDoubleVertices[1].Pos);
			mMatTransform.transformVect(mDoubleVertices[2].Pos);
			mMatTransform.transformVect(mDoubleVertices[3].Pos);
			//return to original position
			mMatTransform.setTranslation(-rotateCenter);
			mMatTransform.setRotationDegrees(vector3df(0,0,0));
			mMatTransform.transformVect(mDoubleVertices[0].Pos);
			mMatTransform.transformVect(mDoubleVertices[1].Pos);
			mMatTransform.transformVect(mDoubleVertices[2].Pos);
			mMatTransform.transformVect(mDoubleVertices[3].Pos);
		}
	}

	mMatTransform.setRotationDegrees(vector3df(0,0,0));
	mMatTransform.setTranslation(mPosition);
	mMatTransform.transformVect(mVertices[0].Pos);
	mMatTransform.transformVect(mVertices[1].Pos);
	mMatTransform.transformVect(mVertices[2].Pos);
	mMatTransform.transformVect(mVertices[3].Pos);

	if(mIsDoubleSide){
		mMatTransform.setRotationDegrees(vector3df(0,0,0));
		mMatTransform.setTranslation(mPosition);
		mMatTransform.transformVect(mDoubleVertices[0].Pos);
		mMatTransform.transformVect(mDoubleVertices[1].Pos);
		mMatTransform.transformVect(mDoubleVertices[2].Pos);
		mMatTransform.transformVect(mDoubleVertices[3].Pos);
	}

	Driver->setTransform(video::ETS_WORLD, core::IdentityMatrix);
	Driver->setMaterial(mMaterial);
	Driver->drawIndexedTriangleList(mVertices, 4, mIndices, 2);
	if(mIsDoubleSide){
		Driver->drawIndexedTriangleList(mDoubleVertices, 4, mIndices, 2);
	}
}

video::SMaterial& UVAnimatedPlane::getMaterial(u32 i)
{
	return mMaterial;
}


//! returns amount of materials used by this scene node.
u32 UVAnimatedPlane::getMaterialCount() const
{
	return 1;
}

//! pre render event
void UVAnimatedPlane::OnRegisterSceneNode()
{
	if (mIsVisible)
		ISceneNode::SceneManager->registerNodeForRendering(this);

	ISceneNode::OnRegisterSceneNode();
}

//! render
void UVAnimatedPlane::render()
{
	draw();
}

//! returns the axis aligned bounding box of this node
const core::aabbox3d<f32>& UVAnimatedPlane::getBoundingBox() const
{
	return mBox;
}