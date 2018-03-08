#include "StdAfx.h"
#include "CameraW.h"


CameraW::CameraW(SharedParams_t* sharedParams,ICameraSceneNode* camera) : BaseW(sharedParams)
{
	_camera = camera;
}

// ������������ �� ���� ��������
void CameraW::FullZoom()
{
	aabbox3df fullBounds(vector3df(0,0,0),vector3df(0,0,0));	

	ISceneNode* node = 0;
	ISceneNode* start =  SceneManager->getRootSceneNode();
	const core::list<ISceneNode*>& list = start->getChildren();
	core::list<ISceneNode*>::ConstIterator it = list.begin();
	for (; it!=list.end(); ++it)
	{
		node = *it;
		if(node->getType() != ESNT_ANIMATED_MESH && node->getType() != ESNT_MESH) 
			continue;
		aabbox3df bounds = node->getTransformedBoundingBox();
		fullBounds.addInternalBox(bounds);
	}
	if(fullBounds.isEmpty()) return;
	vector3df center = fullBounds.getCenter();
	vector3df offset = (fullBounds.MaxEdge - fullBounds.MinEdge);	
	vector3df position = fullBounds.MaxEdge;
	offset.Y = 0;
	position.Y += offset.getLength();
	_camera->setPosition(position);	
	_camera->setTarget(center);	
}

// ������������ � ������
void CameraW::ZoomToNode(SceneNodeW^ node)
{
	ISceneNode* meshNode = node->GetNode();
	if(meshNode == NULL) return;	
	aabbox3df bounds = meshNode->getTransformedBoundingBox();
	vector3df center = bounds.getCenter();
	vector3df offset = (bounds.MaxEdge - bounds.MinEdge);	
	
	//offset.Y = 0;
	//position.Y += offset.getLength();
	// 
	vector3df camPos = _camera->getPosition();
	vector3df mainVec = camPos - center;
	f32 lengthDiff = offset.getLength() / mainVec.getLength();
	mainVec *= lengthDiff;
	vector3df position = center + mainVec;

	_camera->setPosition(position);	
	_camera->setTarget(center);	
}


// ���������� �������� ������
void CameraW::SetCameraPosition(Vertex3dW^ vertex)
{
	_camera->setPosition(vertex->GetVector());
}

// �������� �������� ������
Vertex3dW^ CameraW::GetCameraPosition()
{
	return gcnew Vertex3dW(_camera->getPosition());
}

// ���������� �������� ������
void CameraW::SetCameraTarget(Vertex3dW^ vertex)
{
	_camera->setTarget(vertex->GetVector());
}

// �������� �������� ������
Vertex3dW^ CameraW::GetCameraTarget()
{
	return gcnew Vertex3dW(_camera->getTarget());
}

// ��������� ������
void CameraW::RotateCamera(Vertex3dW^ vertex)
{
	vector3df rotation = vertex->GetVector();

	// ������� �� ��� Y
	//
	core::matrix4 m;
	m.setRotationDegrees(vector3df(0,rotation.Y, 0));
	vector3df cameraPos = _camera->getPosition();
	vector3df targetPos = _camera->getTarget();
	vector3df vectorCamPos = cameraPos - targetPos;
	m.transformVect(vectorCamPos);	
	_camera->setPosition(targetPos + vectorCamPos);

	// ������� �� ��� X,Z
	//
	cameraPos = _camera->getPosition();
	targetPos = _camera->getTarget();
	core::vector3df target = (cameraPos - targetPos);
	core::vector3df relativeRotation = target.getHorizontalAngle();

	core::vector3df resVect(0, 0, target.getLength());

	core::matrix4 mat;
	relativeRotation.X -= rotation.X;
	mat.setRotationDegrees(relativeRotation);
	mat.transformVect(resVect);
	_camera->setPosition(resVect + targetPos);
}

// �������� / ���������� ������
void CameraW::WheelCamera(float distance)
{
	vector3df cameraPos = _camera->getPosition();
	vector3df targetPos = _camera->getTarget();
	vector3df vectorCamPos = cameraPos - targetPos;
	vector3df offset = vectorCamPos;
	offset = offset.normalize() * distance;
	if(distance < 0 && (vectorCamPos.getLength() < offset.getLength()))
		return;
	_camera->setPosition(cameraPos + offset);
}

// ����������� ������
void CameraW::MoveCamera(Vertex3dW^ step)
{
	vector3df cameraPos = _camera->getPosition();
	vector3df targetPos = _camera->getTarget();
	vector3df stepVect = step->GetVector();

	// �������� ����
	core::vector3df target = (_camera->getTarget() - _camera->getPosition());
	//core::vector3df relativeRotation = target.getHorizontalAngle();
	
	core::matrix4 mat;

	core::vector3df strafevect = target;
	// ��������� ������������
	// - ��� ������������, ���������������� ���������, ����������� �� ���� ������������
	strafevect = strafevect.crossProduct(vector3df(0,1,0));
	strafevect.normalize();
	if (stepVect.X != 0)
	{
		cameraPos += (strafevect * stepVect.X);
		targetPos += (strafevect * stepVect.X);
	}

	if (stepVect.Y != 0)
	{
		cameraPos.Y += stepVect.Y;
		targetPos.Y += stepVect.Y;
	}
	
	_camera->setPosition(cameraPos);
	_camera->setTarget(targetPos);
}
