#include "DebugNode.h"
#include "DebugSettings.h"
#include "../UnitInstance/UnitInstanceStandard.h"
#include "Core/NodeWorker.h"

#ifdef DEBUG_VISUAL_DATA

DebugNode::DebugNode(SharedParams_t params, ISceneNode* parent) : 
	Base(params), ISceneNode(parent, params.SceneManager),
	_material(), _boundBox(), _instance(NULL), 
	_prevAction(), _prevShowAction()
{
	NodeWorker::ApplyMaterialSetting(&_material);

	_textNode = new SceneText(
		params, 
		DebugSettings::GetInstance()->FontPath,
		this);
	_textNode->setID(0);
	_textNode->KerningWidth = -0.3f;
	_textNode->drop();
}

DebugNode::~DebugNode(void)
{
	removeAll();
}

// Установить юнит
void DebugNode::SetInstance( UnitInstanceStandard* instance )
{
	_instance = instance;
}

video::SMaterial& DebugNode::getMaterial(u32 i)
{
	return _material;
}


//! returns amount of materials used by this scene node.
u32 DebugNode::getMaterialCount() const
{
	return 1;
}

//! pre render event
void DebugNode::OnRegisterSceneNode()
{
	if (IsVisible)
		ISceneNode::SceneManager->registerNodeForRendering(this);

	ISceneNode::OnRegisterSceneNode();
}

//! render
void DebugNode::render()
{
	if(!IsVisible) return;
	if (_instance == NULL) return;

	DebugSettings* settings = DebugSettings::GetInstance();
	if (!settings->Enabled) 
	{
		_textNode->setVisible(false);
		return;
	}

	_textNode->setVisible(true);

	stringc actionName;

	UnitBehavior* behavior = _instance->GetBehavior();
	UnitAction* action = behavior->GetCurrentAction();
	
	stringc displayText;
	if(action != NULL)
	{
		// Отображение действий
		// 
		if(settings->DynamicShowAction)
		{		
			actionName = action->Name;
			if(actionName != _prevAction)
			{
				_prevShowAction = _prevAction;
			}
			_prevAction = actionName;

			displayText += stringc("(") + _prevShowAction + ") " + actionName + "\\n";
		}
	}

	// Отображение параметров
	//
	if(settings->DynamicShowParameters)
	{
		UnitParameters* parameters = behavior->GetParameters();
		if (parameters != NULL)
		{
			for (u32 i = 0; i < parameters->Count() ; i++)
			{
				Parameter* paramItem = parameters->GetByIndex(i);
				displayText += stringc("(") + paramItem->Name + " : " + paramItem->Value + ")\\n";
			}
		}
	}
	
	aabbox3df parentBoundBox = ISceneNode::Parent->getBoundingBox();
	vector3df parentExtent = parentBoundBox.getExtent();
	vector3df pos = getPosition();
	pos.Y += parentExtent.Y + 0.5f;		
	_textNode->setPosition(pos);
	
	_textNode->SetText(displayText);
}

//! returns the axis aligned bounding box of this node
const core::aabbox3d<f32>& DebugNode::getBoundingBox() const
{
	return _boundBox;
}

UnitInstanceStandard* DebugNode::GetInstance()
{
	return _instance;
}

#endif