#include "SplineAnimator.h"

SplineAnimator::SplineAnimator(SharedParams_t params) : Base(params)
{
	mCompleteAnimate = true;
	mUserParams = NULL;
}

SplineAnimator::~SplineAnimator(void)
{
}

inline s32 SplineAnimator::clamp(s32 idx, s32 size)
{
	return ( idx<0 ? size+idx : ( idx>=size ? idx-size : idx ) );
}

void SplineAnimator::init(const core::array< core::vector3df >& points, f32 speed, f32 tightness){
	if(points.size() < 2){
		printf("ERROR: [SplineAnimator::init] invalid params");
		mCompleteAnimate = true;
		return;
	}
	mCompleteAnimate = false;

	Points = points;
	Speed = speed;
	Tightness = tightness;
	StartTime = GetNowTime();

	//
	// Hарисовать кривую Безье
	//R(t) = P0*(1-t)^3 + P1 * t * (1-t)^2 + P2 * t^2 * (1-t) + P3 * t^3 ,

	//Добавляем первую точку
	//Points.push_front(vector3df(0,0,0));
}

void SplineAnimator::SetUserParams(void* params){
	mUserParams = params;
}

bool SplineAnimator::AnimationEnd(){
	return mCompleteAnimate;
}


ISceneNodeAnimator* SplineAnimator::createClone(ISceneNode* node, ISceneManager* newManager){
	//TODO : create valid clone
	//SplineAnimator* newAnimator = new SplineAnimator(SharedParams);	
	//return newAnimator;	
	return NULL;
}

void SplineAnimator::animateNode(ISceneNode* node, u32 timeMs){
	if(mCompleteAnimate) return;
	
	const u32 pSize = Points.size();
	if (pSize==0 || pSize==1)
		return;

	const f32 dt = ( (timeMs-StartTime) * Speed * 0.001f );
	const f32 u = core::fract ( dt );
	const s32 idx = core::floor32( dt ) % pSize;
	//const f32 u = 0.001f * fmodf( dt, 1000.0f );
	
	const core::vector3df& p0 = Points[ clamp( idx - 1, pSize ) ];
	const core::vector3df& p1 = Points[ clamp( idx + 0, pSize ) ]; // starting point
	const core::vector3df& p2 = Points[ clamp( idx + 1, pSize ) ]; // end point
	const core::vector3df& p3 = Points[ clamp( idx + 2, pSize ) ];

	// hermite polynomials
	const f32 h1 = 2.0f * u * u * u - 3.0f * u * u + 1.0f;
	const f32 h2 = -2.0f * u * u * u + 3.0f * u * u;
	const f32 h3 = u * u * u - 2.0f * u * u + u;
	const f32 h4 = u * u * u - u * u;

	// tangents
	const core::vector3df t1 = ( p2 - p0 ) * Tightness;
	const core::vector3df t2 = ( p3 - p1 ) * Tightness;

	// interpolated point
	vector3df newPos = p1 * h1 + p2 * h2 + t1 * h3 + t2 * h4;
	//node->setPosition(p1 * h1 + p2 * h2 + t1 * h3 + t2 * h4);
	//newPos = node->getPosition() - newPos;
	node->setPosition(newPos);
}

// Сброс в начальные установки
void SplineAnimator::ResetState()
{

}
