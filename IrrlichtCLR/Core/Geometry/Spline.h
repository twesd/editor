#pragma once
#include "./../Base.h"

using namespace scene;

class Spline
{
public:
	enum SPLINE_TYPE{
		B_SPLINE_TYPE,
		HERMITE_SPLINE_TYPE
	};
	Spline(SPLINE_TYPE splineType);
	Spline(SPLINE_TYPE splineType,const core::array<vector3df>& points);
	void setPoints(core::array<vector3df>& points);
	void addPoint(vector3df point);

	//Определяет вид HERMITE сплайна
	void setHermiteTightness(f32 tightness);

	/*
	Аппроксимация
		@step - шаг аппроксимации (0, 1)
	*/
	core::array<vector3df> approximate(f32 step);
private:
	//Устанавливает параметры по умолчанию
	void setDefaultValues();
	//! clamps a the value idx to fit into range 0..size-1
	s32 clamp(s32 idx, s32 size);


	//Тип слайна
	SPLINE_TYPE mType;
	//Узловые точки сплайна
	core::array<vector3df> mPoints;

	//Параметры HERMITE сплайна
	f32 mTightness;
};
