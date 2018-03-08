#include "Spline.h"

Spline::Spline(Spline::SPLINE_TYPE splineType){
	mType = splineType;
	setDefaultValues();
}

Spline::Spline(Spline::SPLINE_TYPE splineType, const core::array<vector3df>& points)
{
	mType = splineType;
	mPoints = points;
	setDefaultValues();
}

void Spline::setDefaultValues(){
	mTightness = 0.8f;
}

void Spline::setPoints(core::array<vector3df>& points){
	mPoints = points;
}

void Spline::addPoint(vector3df point){
	mPoints.push_back(point);
}

//Определяет вид HERMITE сплайна
void Spline::setHermiteTightness(f32 tightness){
	mTightness = tightness;
}

inline s32 Spline::clamp(s32 idx, s32 size)
{
	return ( idx<0 ? 0 : ( idx >= size ? size - 1 : idx ) );
}

core::array<vector3df> Spline::approximate(f32 step){
	core::array<vector3df> outPoints;

	const u32 pSize = mPoints.size();
	if (pSize==0 || pSize==1 || step < 0 || step > 1)
		return outPoints;

	if(mType == HERMITE_SPLINE_TYPE){
		f32 t = 0;
		while(t <= pSize - 1){
			const f32 dt = t;
			const f32 u = core::fract ( dt );
			const s32 idx = core::floor32( dt ) % pSize;
			//const f32 u = 0.001f * fmodf( dt, 1000.0f );

			const core::vector3df& p0 = mPoints[ clamp( idx - 1, pSize ) ];
			const core::vector3df& p1 = mPoints[ clamp( idx + 0, pSize ) ]; // starting point
			const core::vector3df& p2 = mPoints[ clamp( idx + 1, pSize ) ]; // end point
			const core::vector3df& p3 = mPoints[ clamp( idx + 2, pSize ) ];

			// hermite polynomials
			const f32 h1 = 2.0f * u * u * u - 3.0f * u * u + 1.0f;
			const f32 h2 = -2.0f * u * u * u + 3.0f * u * u;
			const f32 h3 = u * u * u - 2.0f * u * u + u;
			const f32 h4 = u * u * u - u * u;

			// tangents
			const core::vector3df t1 = ( p2 - p0 ) * mTightness;
			const core::vector3df t2 = ( p3 - p1 ) * mTightness;

			// interpolated point
			vector3df interpolatePoint = p1 * h1 + p2 * h2 + t1 * h3 + t2 * h4;
			outPoints.push_back(interpolatePoint);

			t += step;
		}
	} else {
	//				//Квадратичная кривая Безье (n = 2) задаётся 3-мя опорными точками: P0, P1 и P2.				
	//				//B(t) = (1 - t)^2*P0 + 2*t*(1-t)*P1 + t^2*P2, t = [0,1]
		//x(t) = ((a3t + a2)t + a1)t + a0
		//a3 = (-xi-1 + 3xi - 3xi+1 + xi+2)/6
		//a2 = (xi-1 - 2xi + xi+1)/2
		//a1 = (-xi-1 + xi+1)/2
		//a0 = (xi-1 + 4xi+ xi+1)/6
		
		//not implementent

		//f32 t = 0;
		//while(t <= pSize - 1){
		//	f32 dt = t;			
		//	const s32 idx = core::floor32( dt ) % pSize;

		//	const core::vector3df& p0 = mPoints[ clamp( idx - 1, pSize ) ];
		//	const core::vector3df& p1 = mPoints[ clamp( idx + 0, pSize ) ]; // starting point
		//	const core::vector3df& p2 = mPoints[ clamp( idx + 1, pSize ) ]; // end point
		//	const core::vector3df& p3 = mPoints[ clamp( idx + 2, pSize ) ];

		//	// hermite polynomials
		//	vector3df a0 = (p0 + p1*4 + p2);
		//	vector3df a1 = (-p0 + p2) / 2;
		//	vector3df a2 = (p0 + p1*2 + p2) / 2;
		//	vector3df a3 = (-p0 + p1*3 - p2*3 + p3) / 6;

		//	// interpolated point
		//	while(dt > 1) dt -= 1;
		//	vector3df interpolatePoint = ((a3*t + a2)*t + a1)*t + a0;
		//	outPoints.push_back(interpolatePoint);

		//	t += step;
		//}
	}
	return outPoints;
}
