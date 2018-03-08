#include "StdAfx.h"
#include "IrrDeviceW.h"
#include "Convertor.h"
#include "Core/Base.h"
#include "Core/Scene/Billboard.h"

IrrDeviceW::IrrDeviceW(int windowId)
{
	SIrrlichtCreationParameters parameters;
	parameters.DriverType = video::EDT_OPENGL;
	parameters.WindowId = (void *)windowId;
	parameters.Stencilbuffer = false;
	parameters.Vsync = false;
	parameters.Fullscreen = false;
	parameters.Bits = 16;		
	_device = createDeviceEx(parameters);

	mTimer = _device->getTimer();
	_driver = _device->getVideoDriver();
	_sceneManager = _device->getSceneManager();
	_driver->setTextureCreationFlag(video::ETCF_ALWAYS_32_BIT, true);

	mCamera = _sceneManager->addCameraSceneNode();
	mCollisionManager = _sceneManager->getSceneCollisionManager();

	SharedParams = new SharedParams_t();
	
	// Заполняем общие параметры
	//
	SharedParams->Event	=	(void*) new Event();
	SharedParams->Device	=	_device;
	SharedParams->Driver	=	_driver;
	SharedParams->SceneManager	=	_sceneManager;		
	SharedParams->Settings	=	NULL;
	SharedParams->Timer	=	_device->getTimer();		
	SharedParams->FileSystem = _device->getFileSystem();
	SharedParams->TimeDiff = new u32();

	// Создаём дополнительные члены класса
	_selector = gcnew SelectorW(SharedParams);
	_cameraW = gcnew CameraW(SharedParams, mCamera);
	_controlsW = gcnew ControlsW(SharedParams);
}

void IrrDeviceW::Close()
{
	if(_device != NULL)
	{
		_sceneManager->clear();
		_driver->clearZBuffer();
		_driver->deleteAllDynamicLights();	
		_driver->removeAllHardwareBuffers();
		_driver->checkDriverReset();
		_sceneManager->getMeshCache()->clear();
		_driver->removeAllTextures();
		_device->drop();

		_device = NULL;
		mTimer = NULL;
		_driver = NULL;
		_sceneManager = NULL;
		mCamera = NULL;
	}
}

IrrDeviceW::~IrrDeviceW(void)
{
	Close();
}

void IrrDeviceW::DrawAll()
{
	_device->run();
	_driver->beginScene(true, true, video::SColor(255,0,0,0));
	_sceneManager->drawAll();
	_controlsW->Draw();
	_driver->endScene();
}

// Добавить модель
SceneNodeW^ IrrDeviceW::AddSceneNode(String^ path)
{
	c8* chars = Convertor::ConvertToU8(path);
	IAnimatedMesh* mesh = _sceneManager->getMesh(chars);
	delete chars;
	if(mesh == NULL) 
		return nullptr;
		//throw gcnew Exception("Модель не загружена");
	IAnimatedMeshSceneNode* meshNode = _sceneManager->addAnimatedMeshSceneNode(mesh);
	// Делаем перерисовка для обновления BoundBox
	DrawAll();
	meshNode->render();

	SceneNodeW^ nodeW = gcnew SceneNodeW(meshNode, SharedParams);
	nodeW->SetDefaultProperty();
	return nodeW;
}


// Добавить BoundBox
SceneNodeW^ IrrDeviceW::AddCube( BoundboxW^ boundbox )
{
	aabbox3df box = boundbox->GetBox();
	vector3df pos = box.getCenter();
	f32 dist = box.MinEdge.getDistanceFrom(box.MaxEdge);
	f32 sizeX = core::abs_(box.MaxEdge.X - box.MinEdge.X);
	f32 sizeY = core::abs_(box.MaxEdge.Y - box.MinEdge.Y);
	f32 sizeZ = core::abs_(box.MaxEdge.Z - box.MinEdge.Z);

	scene::ISceneNode* sceneNode = _sceneManager->addCubeSceneNode(1);
	sceneNode->setScale(vector3df(sizeX, sizeY, sizeZ));
	sceneNode->setPosition(pos);

	SceneNodeW^ nodeW = gcnew SceneNodeW(sceneNode, SharedParams);
	nodeW->SetDefaultProperty();
	return nodeW;
}


// Добавить сферу
SceneNodeW^ IrrDeviceW::AddSphere( float radius, int polyCount )
{
	scene::ISceneNode* sceneNode = _sceneManager->addSphereSceneNode(radius, polyCount);
	SceneNodeW^ nodeW = gcnew SceneNodeW(sceneNode, SharedParams);
	nodeW->SetDefaultProperty();
	return nodeW;
}

// Удалить все модели
void IrrDeviceW::DeleteSceneNodes()
{
	bool done = false;
	while(!done)
	{
		done = true;
		ISceneNode* node = 0;
		ISceneNode* start =  _sceneManager->getRootSceneNode();
		const core::list<ISceneNode*>& list = start->getChildren();
		core::list<ISceneNode*>::ConstIterator it = list.begin();
		for (; it!=list.end(); ++it)
		{
			node = *it;
			if(node->getType() != ESNT_ANIMATED_MESH &&
				node->getType() != ESNT_MESH &&
				node->getType() != ESNT_BILLBOARD &&
				node->getType() != ESNT_CUBE && 
				node->getType() != ESNT_SPHERE) 
				continue;
			node->removeAll();
			node->remove();
			done = false;
			break;
		}
	}

	DrawAll();
}


// Преобразовать координаты из экранных
Vertex3dW^ IrrDeviceW::ScreenCoordToPosition3d(int x, int y, f32 unitY)
{
	position2d<s32> screenPos(x, y);
	core::line3d<f32> ray = mCollisionManager->getRayFromScreenCoordinates(screenPos);

	plane3df plane(vector3df(0,unitY,0), vector3df(0,1,0));
	vector3df outIntersection;
	plane.getIntersectionWithLine(ray.start, ray.getVector(), outIntersection); 
	return gcnew Vertex3dW(outIntersection.X, outIntersection.Y, outIntersection.Z);
}

// Преобразовать координаты в экранные координаты
Vertex3dW^ IrrDeviceW::Position3dToSceenCoord(Vertex3dW^ pos)
{
	position2d<s32> coordPos = mCollisionManager->getScreenCoordinatesFrom3DPosition(pos->GetVector());
	return gcnew Vertex3dW((f32)coordPos.X, (f32)coordPos.Y, 0);
}

// Добавить Billboard
SceneNodeW^ IrrDeviceW::AddBillboard( float width, float height )
{
	Billboard* billboard = new Billboard(*SharedParams);
	billboard->SetDimension(dimension2df(width, height));

	BillboardW^ nodeW = gcnew BillboardW(billboard, SharedParams);
	nodeW->SetDefaultProperty();
	return nodeW;
}

// Обновить размер экрана
void IrrDeviceW::ResizeScreen(int width, int height)
{
	recti rectPort = _driver->getViewPort();
	dimension2di size = _driver->getScreenSize();
	_driver->OnResize(dimension2di(width, height));
	size = _driver->getScreenSize();
	rectPort = _driver->getViewPort();
}

// Удалить модель
void IrrDeviceW::DeleteSceneNode( SceneNodeW^ node )
{
	node->GetNode()->removeAll();
	node->GetNode()->remove();
}


