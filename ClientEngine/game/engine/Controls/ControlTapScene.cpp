#include "ControlTapScene.h"
#include "Core/GeometryWorker.h"

//#define DEBUG_CONTROL_TAP_SCENE

ControlTapScene::ControlTapScene(SharedParams_t params) : IControl(params),
	_bounds(), _pickUpNode(false), _notProcessingRects(), _filterNodeId(-1)
{
	_collisionManager = SceneManager->getSceneCollisionManager();
}

ControlTapScene::~ControlTapScene(void)
{
}

void ControlTapScene::Init(position2di minPoint, position2di maxPoint, bool pickUpNode, int filterNodeId)
{
	_pickUpNode = pickUpNode;
	_bounds.UpperLeftCorner = minPoint;
	_bounds.LowerRightCorner = maxPoint;
	_filterNodeId = filterNodeId;

	_controlTapSceneDesc.Name = Name;
	_controlTapSceneDesc.State = BUTTON_STATE_NONE;
	_controlTapSceneDesc.Node = NULL;
}


void ControlTapScene::Update(reciverInfo_t *reciverInfo) 
{
	for(u32 i=0;i<MAX_USER_ACTION_COUNT;i++)
	{
		int state = reciverInfo->StateTouch[i];
		int x = reciverInfo->XTouch[i];
		int y = reciverInfo->YTouch[i];
		if(state == FAIL_VALUE)
			continue;
#ifdef IPHONE_COMPILE
		if(state == irr::ETOUCH_NONE)
			continue;
#endif
		
		position2di screenPos(x, y);

		//printf("[ControlTapScene] state [%d] \n", state);

#ifdef IPHONE_COMPILE
		bool realesed = (state == irr::ETOUCH_ENDED || state == irr::ETOUCH_CANCELLED);
		bool down = (state == irr::ETOUCH_BEGAN);
#else	
		bool realesed = (state == irr::EMIE_LMOUSE_LEFT_UP);
		bool down = (state == irr::EMIE_LMOUSE_PRESSED_DOWN);
#endif	

		if(down)
		{
			if(_controlTapSceneDesc.State == BUTTON_STATE_DOWN || _controlTapSceneDesc.State == BUTTON_STATE_PRESSED)
				_controlTapSceneDesc.State = BUTTON_STATE_PRESSED;
			else
				_controlTapSceneDesc.State = BUTTON_STATE_DOWN;	
		} 
		else if (realesed)
		{
			_controlTapSceneDesc.State = BUTTON_STATE_UP;
		}
		else
		{
			_controlTapSceneDesc.State = BUTTON_STATE_NONE;
		}

		if(_controlTapSceneDesc.State == BUTTON_STATE_NONE) continue;
		// TODO: fast fix
		//if(!_bounds.isPointInside(screenPos)) continue;

		// Проверяем попадает ли точка в необрабатываемые зоны
		//
		bool notProcess = false;
		for (u32 indexSpace = 0; indexSpace < _notProcessingRects.size() ; indexSpace++)
		{
			recti notProcessRect = _notProcessingRects[indexSpace];
			if (notProcessRect.isPointInside(screenPos))
			{	
				notProcess = true;
				break;
			}
		}
		if (notProcess)
		{
			_controlTapSceneDesc.State = BUTTON_STATE_NONE;
			continue;
		}

		core::line3df ray = _collisionManager->getRayFromScreenCoordinates(screenPos);
		
		if(_pickUpNode)
			_controlTapSceneDesc.Node = _collisionManager->getSceneNodeFromRayBB(ray, _filterNodeId);
		else
			_controlTapSceneDesc.Node = NULL;
				
		_controlTapSceneDesc.ScreenPoint = screenPos;
		_controlTapSceneDesc.SceneRay = ray;

		GeometryWorker::RoundVector(ray.start);
		GeometryWorker::RoundVector(ray.end);

#ifdef DEBUG_CONTROL_TAP_SCENE
		stringc nodeName;
		s32 nodeId = 0;
		if(_controlTapSceneDesc.Node != NULL) 
		{
			nodeId = _controlTapSceneDesc.Node->getID();
			nodeName = stringc(_controlTapSceneDesc.Node->getDebugName());
		}
		printf("[ControlTapScene] state [%d] nodeId[%d] nodeName[%s] \n",_controlTapSceneDesc.State, nodeId, nodeName.c_str());
#endif

		EventParameters_t params;
		params.CommonParam = &_controlTapSceneDesc;
		EventBase->PostEvent(Event::ID_UNIT_MANAGER, EVENT_CONTROL_TAPSCENE, params);
	}
}

void ControlTapScene::Draw()
{
}


// Получить границы контрола
irr::core::recti ControlTapScene::GetBounds()
{
	return _bounds;
}

// Установить не обрабатываемые области
void ControlTapScene::SetNotProcessingSpace( core::array<recti> &rects )
{
	_notProcessingRects = rects;
}

void ControlTapScene::SetPosition( position2di newPosition )
{
	// Не реализовано
}

irr::core::position2di ControlTapScene::GetPosition()
{
	return _bounds.UpperLeftCorner;
}

ControlTapSceneDesc_t ControlTapScene::GetTapSceneDesc()
{
	return _controlTapSceneDesc; 
}
