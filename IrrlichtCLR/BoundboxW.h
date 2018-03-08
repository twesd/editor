#pragma once


public ref class BoundboxW
{
public:
	BoundboxW(void)
	{
		_minPoint = gcnew Vertex3dW();
		_maxPoint = gcnew Vertex3dW();
	}

	BoundboxW(Vertex3dW^ minPoint, Vertex3dW^ maxPoint)
	{
		_minPoint = minPoint;
		_maxPoint = maxPoint;
	}

	BoundboxW(core::aabbox3df box)
	{
		_minPoint = gcnew Vertex3dW(box.MinEdge);
		_maxPoint = gcnew Vertex3dW(box.MaxEdge);
	}
	
	property Vertex3dW^ MinPoint
	{
		Vertex3dW^ get() 
		{
			return _minPoint;
		}
	}

	property Vertex3dW^ MaxPoint 
	{
      Vertex3dW^ get() 
	  {
         return _maxPoint;
      }
	}

	core::aabbox3df GetBox()
	{
		return core::aabbox3df(_minPoint->GetVector(), _maxPoint->GetVector());
	}

private:
	Vertex3dW^ _minPoint;

	Vertex3dW^ _maxPoint;
};
