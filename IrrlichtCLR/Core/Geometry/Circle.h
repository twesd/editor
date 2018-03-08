#pragma once
#include "./../Base.h"

using namespace scene;

class Circle
{
public:
	Circle();

	Circle(vector3df center, f32 radius);

	/*
	Аппроксимация
		@step - шаг аппроксимации (0, 1)
	*/
	//core::array<vector3df> approximate(f32 step);

	// Пересекается ли круг с отрезком
	// @line - линия с которой ищется пересечение
	// @infinityLine - является линия бесконечной
	core::array<vector3df> intersectWith(line3df line, bool infinityLine);

	// Радиус круга
	f32 Radius;
	// Центр круга
	vector3df Center;
private:
	
};
