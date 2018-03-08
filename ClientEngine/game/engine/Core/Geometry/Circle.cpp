#include "Circle.h"

Circle::Circle()
{
	Center = vector3df(0,0,0);
	Radius = 1;
}

Circle::Circle(vector3df center, f32 radius)
{
	Center = center;
	Radius = radius;
}

// Пересекается ли круг с отрезком
// @line - линия с которой ищется пересечение
// @infinityLine - является линия бесконечной
core::array<vector3df> Circle::intersectWith(line3df line, bool infinityLine)
{
	core::array<vector3df> outPoints;
	vector3df outPoint;

	vector3df p1 = line.start;
	vector3df p2 = line.end;

	// Точки линии совпадают, определить пересечение не возможно
	if(p1 == p2) return outPoints;

	f32 a, b, c, mu, i ;
	vector3df dp = line.getVector();
	
	a =  dp.X * dp.X + dp.Y * dp.Y + dp.Z * dp.Z;
	b = 2 * (dp.X * (p1.X - Center.X) + dp.Y * (p1.Y - Center.Y) + dp.Z * (p1.Z - Center.Z));
	c = Center.X * Center.X + Center.Y * Center.Y + Center.Z * Center.Z;
	c += p1.X * p1.X + p1.Y * p1.Y + p1.Z * p1.Z;
	c -= 2 * (Center.X * p1.X + Center.Y * p1.Y + Center.Z * p1.Z);
	c -= Radius * Radius;
	i =   b * b - 4 * a * c ;

	if ( i < 0.0f )
	{
		// Нет пересечений
		return outPoints;
	}
	else if ( i == 0.0f )
	{
		// Одно пересечение
		//
		mu = -b/(2*a) ;
		outPoint = p1 + mu*(p2-p1);
		if(infinityLine)
		{
			outPoints.push_back(outPoint);
		}else if(line.isPointBetweenStartAndEnd(outPoint))
		{
			outPoints.push_back(outPoint);
		}
	}
	else if ( i > 0.0 )
	{
		// Два пересечения
		//
		mu = (-b + sqrt( b*b - 4*a*c )) / (2*a);
		outPoint = p1 + mu*(p2-p1);
		if(infinityLine)
		{
			outPoints.push_back(outPoint);
		}else if(line.isPointBetweenStartAndEnd(outPoint))
		{
			outPoints.push_back(outPoint);
		}

		mu = (-b - sqrt( b * b - 4*a*c )) / (2*a);
		outPoint = p1 + mu*(p2-p1);
		if(infinityLine)
		{
			outPoints.push_back(outPoint);
		}else if(line.isPointBetweenStartAndEnd(outPoint))
		{
			outPoints.push_back(outPoint);
		}
	}	
	
	return outPoints;
}