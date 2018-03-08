#include "GeometryWorker.h"



void GeometryWorker::RoundVector(vector3df &pos)
{
	pos.X = Roundf32Value(pos.X);
	pos.Y = Roundf32Value(pos.Y);
	pos.Z = Roundf32Value(pos.Z);
}

// Round value
f32 GeometryWorker::Roundf32Value(f32 fValue)
{
	f32 fract;
	f32 intPart;	
	fract = modff(fValue, &intPart);
#ifdef WINDOWS_COMPILE
	//floor -- round to largest integral value not greater than x
	fract = floorf((fract*1000.0f) + 0.5f);
#else
	fract = roundf(fract*1000.0f);
#endif
	return intPart + (fract / 1000.0f);		
}


//Угол между вектором и осью Z - у вектора начало которого endPos и конец в curPos
/*
*					             (curPos)
*					            /
*						       /
*         (0,0) Z<____(angle)_/(endPos)
*
*/
float GeometryWorker::GetAngle(vector3df curPos,vector3df endPos){
	RoundVector(curPos);
	RoundVector(endPos);
	f64 angle64 = core::vector2df((curPos.X-endPos.X), (curPos.Z-endPos.Z)).getAngle();	
	float angle = Roundf32Value(90.0f - (360.0f - (f32)angle64));
	while(angle>360) angle-=360;
	while(angle<0) angle+=360;
	return angle;
}

bool GeometryWorker::IsClockwiseDirectional(float angleFrom,float angleTo){
	float diffFromTo = NormalizeRotationValue(angleFrom - angleTo);
	float diffToFrom = NormalizeRotationValue(angleTo - angleFrom);
	if(diffFromTo < diffToFrom) return true;
	return false;
}


float GeometryWorker::GetAngleDiffClosed(float angle1,float angle2){
	float diffAngle = NormalizeRotationValue(angle1 - angle2);
	float diffAngle2 = NormalizeRotationValue(angle2 - angle1);
	if(diffAngle>diffAngle2) return diffAngle2;
	return diffAngle;
}


void GeometryWorker::RotateByCenter(vector3df &position,vector3df rotation, vector3df center)
{
	core::matrix4 mat;

	rotation -= center;

	mat.setTranslation(vector3df(0,0,0));
	mat.setRotationDegrees(rotation);			
	mat.transformVect(position);

	rotation += center;
}


void GeometryWorker::NormalizeRotation( vector3df &rot )
{
	rot.X -= floor( rot.X / 360.0f ) * 360.0f;
	if ( rot.X > 360.0f )
		rot.X -= 360.0f;
	else if ( rot.X < 0.0f )
		rot.X += 360.0f;
	rot.Y -= floor( rot.Y / 360.0f ) * 360.0f;
	if ( rot.Y > 360.0f )
		rot.Y -= 360.0f;
	else if ( rot.Y < 0.0f )
		rot.Y += 360.0f;
	rot.Z -= floor( rot.Z / 360.0f ) * 360.0f;
	if ( rot.Z > 360.0f )
		rot.Z -= 360.0f;
	else if ( rot.Z < 0.0f )
		rot.Z += 360.0f;
}

f32 GeometryWorker::NormalizeRotationValue( f32 angle )
{
	angle -= floor( angle / 360.0f ) * 360.0f;
	if (angle > 360.0f )
		angle-= 360.0f;
	else if ( angle < 0.0f )
		angle += 360.0f;
	return angle;
}
