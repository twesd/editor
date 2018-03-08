#include "DebugReciver.h"
#include <iostream>
#include <sstream>

#ifdef DEBUG_VISUAL_DATA

DebugReciver::DebugReciver(IEventReceiver* baseReciver, ISceneManager* sceneManager, Base* baseItem)
{
	_baseReciver = baseReciver;
	_sceneManager = sceneManager;
	_baseItem = baseItem;	
	_cameraGame = NULL;
	_boundsShowing = false;
	_transparentShowing = false;
}

bool DebugReciver::OnEvent(const SEvent& event)
{
	DebugSettings* settings = DebugSettings::GetInstance();
	
	int filterId = 0;
	std::string str;

#ifdef IPHONE_COMPILE
	if (event.EventType == irr::EET_TOUCH_INPUT_EVENT)
	{
	}
#else
	if (event.EventType == irr::EET_KEY_INPUT_EVENT)
	{
		if(event.KeyInput.PressedDown)
		{
			int key = event.KeyInput.Key;			
			switch(key)
			{
			case irr::KEY_KEY_1:
				settings->Enabled = !settings->Enabled;
				break;
			case irr::KEY_KEY_2:
				settings->DynamicShowAction = !settings->DynamicShowAction;
				break;
			case irr::KEY_KEY_3:
				settings->DynamicShowParameters = !settings->DynamicShowParameters;
				break;
			case irr::KEY_KEY_4:
				_transparentShowing = !_transparentShowing;
				ShowSceneNodeTransparent(_transparentShowing);
				break;
			case irr::KEY_KEY_5:
				_boundsShowing = !_boundsShowing;
				ShowSceneNodeBoundBox(_boundsShowing);
				break;
			case irr::KEY_KEY_B:
				RotateCamera();
				break;
			case irr::KEY_KEY_G:
				ShowAllGlobalParameters();
				break;
			case irr::KEY_KEY_I:
				settings->DynamicDebugInfoMode = !settings->DynamicDebugInfoMode;
				break;
			case irr::KEY_KEY_U:
				printf("Input filter Id for Dynamic Info Mode :");
				std::cin.clear();
				getline(std::cin, str);
				settings->DebugInfoFilterId = (int)core::fast_atof(str.c_str());
				break;
			case irr::KEY_KEY_O:
				PrintCameraData();
				break;
			case irr::KEY_KEY_P:
				if (_cameraGame != NULL)
				{
					vector3df target = _cameraFPS->getTarget();
					vector3df pos = _cameraFPS->getPosition();

					_cameraGame->setPosition(_cameraFPS->getPosition());
					_cameraGame->setTarget(_cameraFPS->getTarget());

					_sceneManager->setActiveCamera(_cameraGame);
					_cameraGame = NULL;
				}
				else
				{
					_cameraGame = _sceneManager->getActiveCamera();		

					_cameraFPS = _sceneManager->addCameraSceneNodeFPS(NULL, 15, 0.05f);
					_sceneManager->setActiveCamera(_cameraFPS);

					_cameraFPS->setPosition(_cameraGame->getPosition());
					_cameraFPS->setTarget(_cameraGame->getTarget());

					_sceneManager->drawAll();

					_cameraFPS->setPosition(_cameraGame->getPosition());
					_cameraFPS->setTarget(_cameraGame->getTarget());

				}							
				break;
			}
		}
	}
#endif

	return _baseReciver->OnEvent(event);
}

// Показать все глобальные параметры
void DebugReciver::ShowAllGlobalParameters()
{
	printf(" --------------- GlobalParameters --------------- \n");
	SharedParams_t sharedParams = _baseItem->GetSharedParams();
	for(u32 i = 0; i < sharedParams.GlobalParams->size(); i++)
	{		
		printf("%s : %s \n", 
			sharedParams.GlobalParams[0][i].Name.c_str(), 
			sharedParams.GlobalParams[0][i].Value.c_str());
	}
}

// Показать данные камеры
void DebugReciver::PrintCameraData()
{
	ICameraSceneNode* camera = _sceneManager->getActiveCamera();
	vector3df target = camera->getTarget();
	vector3df pos = camera->getPosition();
	printf("Camera Position: %f,%f,%f\n", pos.X,pos.Y, pos.Z);
	printf("Camera Target: %f,%f,%f\n", target.X,target.Y, target.Z);
}

// Показать границы объектов
void DebugReciver::ShowSceneNodeBoundBox(bool show)
{
	SharedParams_t sharedParams = _baseItem->GetSharedParams();
	ISceneNode* node = 0;
	ISceneNode* start =  sharedParams.SceneManager->getRootSceneNode();
	const core::list<ISceneNode*>& list = start->getChildren();
	core::list<ISceneNode*>::ConstIterator it = list.begin();
	for (; it!=list.end(); ++it)
	{
		node = *it;
		if(node->getType() == ESNT_ANIMATED_MESH ||
			node->getType() == ESNT_MESH)
		{
			int newFlags = (EDS_BBOX | EDS_MESH_WIRE_OVERLAY);
			int flags = node->isDebugDataVisible();
			if(show)
				node->setDebugDataVisible(node->isDebugDataVisible() | newFlags);
			else if(flags & newFlags)
				node->setDebugDataVisible(node->isDebugDataVisible() ^ newFlags);
		}
	}
}

// Показать объекты прозрачными
void DebugReciver::ShowSceneNodeTransparent( bool show )
{
	SharedParams_t sharedParams = _baseItem->GetSharedParams();
	ISceneNode* node = 0;
	ISceneNode* start =  sharedParams.SceneManager->getRootSceneNode();
	const core::list<ISceneNode*>& list = start->getChildren();
	core::list<ISceneNode*>::ConstIterator it = list.begin();
	for (; it!=list.end(); ++it)
	{
		node = *it;
		if(node->getType() == ESNT_ANIMATED_MESH ||
			node->getType() == ESNT_MESH)
		{
			int newFlags = (EDS_HALF_TRANSPARENCY);
			int flags = node->isDebugDataVisible();
			if(show)
				node->setDebugDataVisible(node->isDebugDataVisible() | newFlags);
			else if(flags & newFlags)
				node->setDebugDataVisible(node->isDebugDataVisible() ^ newFlags);
		}
	}
}

void DebugReciver::RotateCamera()
{
	ICameraSceneNode* camera = _sceneManager->getActiveCamera();
	vector3df rot = camera->getRotation();
	rot.Z += 10;
	//rot.X += 10;
	camera->setRotation(rot);

	vector3df trg = camera->getTarget();
	//vector3df offset = camera->getAbsolutePosition() - camera->getTarget();

	// get transformation matrix of node
	irr::core::matrix4 m;
	m.setRotationDegrees(rot);

	// transform upvector of camera
	irr::core::vector3df upv = irr::core::vector3df (0.0f, 1.0f, 0.0f);
	m.transformVect(upv);

	// transform camera offset (thanks to Zeuss for finding it was missing)
	//m.transformVect(offset);

	// set camera
	//camera->setPosition(trg + offset); //position camera in front of the ship
	camera->setUpVector(upv); //set up vector of camera
	camera->setTarget(trg); //set target of camera (look at point) (thx Zeuss for correcting it)
}



#endif