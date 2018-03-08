#pragma once

#include "Base.h"

class GeometryWorker
{
public:
	///////////////////////////// Геометрические преобразования ///////////////////////////////////

	//Угол между вектором и осью Z - у вектора начало которого endPos и конец в curPos
	/*
	*					             (curPos)
	*					            /
	*						       /
	*         (0,0) Z<____(angle)_/(endPos)
	*
	*/
	static float GetAngle(vector3df curPos,vector3df endPos);

	//Возрашает наименьшую разницу между углами, т.е. смотрит разницу в двух направлениях
	static float GetAngleDiffClosed(float angle1,float angle2);

	//Определяет направление для перехода из угла angleFrom в angleTo
	static bool IsClockwiseDirectional(float angleFrom,float angleTo);

	// Округление к 360
	static void NormalizeRotation(vector3df &rot);

	// Округление к 360
	static f32 NormalizeRotationValue(f32 angle);

	/*
	*  @brief Поворот объекта вокруг заданой точки
	*/
	static void RotateByCenter(vector3df &position,vector3df rotation, vector3df center);

	// Round vector
	static void RoundVector(vector3df &pos);
	
	// Round value
	static f32 Roundf32Value(f32 fValue);
};
