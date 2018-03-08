#pragma once


public ref class Vertex3dW
{
public:
	Vertex3dW(void)
	{
		_vertex = new vector3df();
	}

	Vertex3dW(float x,float y, float z)
	{
		_vertex = new vector3df(x, y, z);
	}

	Vertex3dW(core::vector3df vector)
	{
		_vertex = new vector3df(vector);
	}

	~Vertex3dW()
	{
		if(_vertex)
			delete _vertex;		
		_vertex = NULL;
		this->!Vertex3dW();
	}

	
	property float X 
	{
      float get() 
	  {
         return _vertex->X;
      }
	}

	property float Y 
	{
      float get() 
	  {
         return _vertex->Y;
      }
	}

	property float Z 
	{
      float get() 
	  {
         return _vertex->Z;
      }
	}

	vector3df GetVector()
	{
		return vector3df(_vertex->X,_vertex->Y, _vertex->Z);
	}

protected:
	!Vertex3dW()
	{
		if(_vertex) delete _vertex;
		_vertex = NULL;
		//Console::WriteLine("release resource");
	}

private:
	vector3df* _vertex;
};
