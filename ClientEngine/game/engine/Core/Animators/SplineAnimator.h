#pragma once
#include "./../Base.h"
#include "IExtendAnimator.h"

using namespace scene;

class SplineAnimator : 
	public IExtendAnimator, 	
	public ISceneNodeAnimator,
	public Base
{
public:
	SplineAnimator(SharedParams_t params);
	virtual ~SplineAnimator(void);
	//! Creates a follow spline animator.
	/** The animator modifies the position of
	the attached scene node to make it follow a hermite spline.
	It uses a subset of hermite splines: either cardinal splines
	(tightness != 0.5) or catmull-rom-splines (tightness == 0.5).
	The animator moves from one control point to the next in
	1/speed seconds. 
	If you no longer need the animator, you should call ISceneNodeAnimator::drop().
	See IReferenceCounted::drop() for more information. */
	void init(const core::array< core::vector3df >& points, f32 speed = 1.0f, f32 tightness = 0.5f);


	//*** IExtendAnimator **//
	// Сброс в начальные установки
	void ResetState();
	bool AnimationEnd();
	void SetUserParams(void* params);

	ISceneNodeAnimator* createClone(ISceneNode* node, ISceneManager* newManager);
	void animateNode(ISceneNode* node, u32 timeMs);
private:
	//! clamps a the value idx to fit into range 0..size-1
	s32 clamp(s32 idx, s32 size);

	core::array< core::vector3df > Points;
	f32 Speed;
	f32 Tightness;
	u32 StartTime;
	bool mCompleteAnimate;
	void* mUserParams;
};
