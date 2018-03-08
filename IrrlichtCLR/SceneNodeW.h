#pragma once

#include "BaseW.h"
#include "core/animators/LineAnimator.h"

using namespace System::Collections::Generic;

public ref class SceneNodeW
{
public:
	SceneNodeW(ISceneNode* node, SharedParams_t* shareParams);	

	// �������������� ������
	property int Id
	{
		int get()
		{
			return (int)ScNode;
		}
	}

	// �������������� ������
	property int FilterId
	{
		int get()
		{
			return ScNode->getID();
		}
		void set(int val)
		{ 
			ScNode->setID(val);
		}
	}

	// ��������� ������
	property bool Visible
	{
		bool get()
		{
			return ScNode->isVisible();
		}
		void set(bool visible)
		{ 
			ScNode->setVisible(visible);
		}
	}

	property bool IsExist
	{
		bool get()
		{
			return GetIsExist();
		}
	}

	// �������� ������
	ISceneNode* GetNode();

	// ���������� �������� �� ���������
	void SetDefaultProperty();

	// ���������� �������� 
	void SetAnimationSpeed(float speed);

	// ���������� �����
	void SetFrameLoop(int startFrame, int endFrame);

	// ���������� ������
	void SetLoopMode(bool loop);

	// ���������� ����������������
	void EnableHalfTransparent(bool enable);
	
	// �������� �������
	Vertex3dW^ GetPosition();

	// ���������� �������
	void SetPosition(Vertex3dW^ position);

	// �������� ����� ��������
	Vertex3dW^ GetRotation();

	// ���������� ����� ��������
	void SetRotation(Vertex3dW^ rotation);

	// �������� �������
	Vertex3dW^ GetScale();

	// ���������� �������
	void SetScale(Vertex3dW^ scale);

	// ���������� ��� �������
	void SetMaterialType(int matType);

	// ���������� ��������
	void SetTexture(int index, String^ path);

	// �������� �������
	BoundboxW^ GetBoundBox();

	// ���������� ������
	void Highlight();

	// ����� ���������
	void UnHighlight();

	// �������� �������������
	void AddTransform(int type, List<Vertex3dW^>^ pointsW, List<u32>^ times);

	// ������� ��� �������������
	void ClearTransforms();
protected:
	ISceneNode* ScNode;

private:
	bool GetIsExist();

	bool GetIsNodeExistInChilds( ISceneNode* &rootNode, ISceneNode* searchNode );

	core::array<vector3df> GetTransformPoints(core::array<vector3df> points, vector3df rotation);

	IAnimatedMeshSceneNode* _animNode;
	SharedParams_t* SharedParams;
};
