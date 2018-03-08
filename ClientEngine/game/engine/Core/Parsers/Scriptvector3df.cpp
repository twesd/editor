#include "scriptvector3df.h"
#include <assert.h> // assert()

using namespace std;

static vector3df Vector3dfFactory(asUINT length, const char *s)
{
	return vector3df(0, 0, 0);
}

static void ConstructVector3df(vector3df *thisPointer)
{
	new(thisPointer) vector3df();
}

static void CopyConstructVector3df(const vector3df &other, vector3df *thisPointer)
{
	new(thisPointer) vector3df(other);
}

static void DestructVector3df(vector3df *thisPointer)
{
	thisPointer->~vector3df();
}

static float Vector3df_GetX(const vector3df &vec)
{
	return (float)vec.X;
}

static float Vector3df_GetY(const vector3df &vec)
{
	return (float)vec.Y;
}

static float Vector3df_GetZ(const vector3df &vec)
{
	return (float)vec.Z;
}

static void Vector3df_SetX(float x, vector3df &vec)
{
	vec.X = x;
}

static void Vector3df_SetY(float y, vector3df &vec)
{
	vec.Y = y;
}

static void Vector3df_SetZ(float z, vector3df &vec)
{
	vec.Z = z;
}

void RegisterVector3df(asIScriptEngine *engine)
{
	int r;

	// Register the type
	r = engine->RegisterObjectType("vector3df", sizeof(vector3df), asOBJ_VALUE | asOBJ_APP_CLASS_CDAK); assert( r >= 0 );

	// Register the factory
	//r = engine->RegisterStringFactory("vector3df", asFUNCTION(Vector3dfFactory), asCALL_CDECL); assert( r >= 0 );

	// Register the object operator overloads
	r = engine->RegisterObjectBehaviour("vector3df", asBEHAVE_CONSTRUCT,  "void f()",                    asFUNCTION(ConstructVector3df), asCALL_CDECL_OBJLAST); assert( r >= 0 );
	r = engine->RegisterObjectBehaviour("vector3df", asBEHAVE_CONSTRUCT,  "void f(const vector3df &in)",    asFUNCTION(CopyConstructVector3df), asCALL_CDECL_OBJLAST); assert( r >= 0 );
	r = engine->RegisterObjectBehaviour("vector3df", asBEHAVE_DESTRUCT,   "void f()",                    asFUNCTION(DestructVector3df),  asCALL_CDECL_OBJLAST); assert( r >= 0 );
	r = engine->RegisterObjectMethod("vector3df", "vector3df &opAssign(const vector3df &in)", asMETHODPR(vector3df, operator =, (const vector3df&), vector3df&), asCALL_THISCALL); assert( r >= 0 );
	r = engine->RegisterObjectMethod("vector3df", "vector3df &opAddAssign(const vector3df &in)", asMETHODPR(vector3df, operator+=, (const vector3df&), vector3df&), asCALL_THISCALL); assert( r >= 0 );

	r = engine->RegisterObjectMethod("vector3df", "float get_X() const", asFUNCTION(Vector3df_GetX), asCALL_CDECL_OBJLAST); assert( r >= 0 );
	r = engine->RegisterObjectMethod("vector3df", "float get_Y() const", asFUNCTION(Vector3df_GetY), asCALL_CDECL_OBJLAST); assert( r >= 0 );
	r = engine->RegisterObjectMethod("vector3df", "float get_Z() const", asFUNCTION(Vector3df_GetZ), asCALL_CDECL_OBJLAST); assert( r >= 0 );

	r = engine->RegisterObjectMethod("vector3df", "void set_X(float)", asFUNCTION(Vector3df_SetX), asCALL_CDECL_OBJLAST); assert( r >= 0 );
	r = engine->RegisterObjectMethod("vector3df", "void set_Y(float)", asFUNCTION(Vector3df_SetY), asCALL_CDECL_OBJLAST); assert( r >= 0 );
	r = engine->RegisterObjectMethod("vector3df", "void set_Z(float)", asFUNCTION(Vector3df_SetZ), asCALL_CDECL_OBJLAST); assert( r >= 0 );
}
