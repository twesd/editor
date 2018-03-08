#include "DebugInfo.h"
#include "DebugNode.h"
#include "DebugSettings.h"
#include "../UnitManager.h"
#include "../UnitInstance/UnitInstanceStandard.h"
#include "../UnitInstance/UnitInstanceEnv.h"
#include "../UnitPart/UnitBlockAction.h"


#ifdef DEBUG_VISUAL_DATA


DebugInfo::DebugInfo(SharedParams_t params) : Base(params) 
{
}


void DebugInfo::Update(reciverInfo_t *reciverInfo, UnitManager* unitManager)
{
	for(u32 i=0;i<MAX_USER_ACTION_COUNT;i++)
	{
		int state = reciverInfo->StateTouch[i];
		int x = reciverInfo->XTouch[i];
		int y = reciverInfo->YTouch[i];
		if(state == FAIL_VALUE) continue;

		position2di screenPos(x, y);

		//printf("[ControlTapScene] state [%d] \n", state);

#ifdef IPHONE_COMPILE
		bool realesed = (state == irr::ETOUCH_ENDED || state == irr::ETOUCH_CANCELLED);
		bool down = (state == irr::ETOUCH_BEGAN);
#else	
		bool realesed = (state == irr::EMIE_LMOUSE_LEFT_UP);
		bool down = (state == irr::EMIE_LMOUSE_PRESSED_DOWN);
#endif	

		ISceneNode* node = 0;
		ISceneNode* start =  SceneManager->getRootSceneNode();
		const core::list<ISceneNode*>& list = start->getChildren();
		core::list<ISceneNode*>::ConstIterator it = list.begin();
		for (; it!=list.end(); ++it)
		{
			node = *it;
			if(node->getType() != ESNT_ANIMATED_MESH && 
				node->getType() != ESNT_MESH &&
				node->getType() != ESNT_EMPTY) 
				continue;
			node->setDebugDataVisible(EDS_OFF);
		}

		DebugSettings* dbgSettings = DebugSettings::GetInstance();

		core::line3df ray = SceneManager->getSceneCollisionManager()->getRayFromScreenCoordinates(screenPos);
		node = SceneManager->getSceneCollisionManager()->getSceneNodeFromRayBB(ray, dbgSettings->DebugInfoFilterId);
		if (node == NULL) 
		{
			return;
		}
		
		printf("\n\n\n////////////////////////////////////////////\n");
		printf("SceneNode ID : %d\n",node->getID());
		aabbox3df box = node->getTransformedBoundingBox();
		printf("SceneNode Extent [%f,%f,%f] [%f,%f,%f] \n",
			box.MinEdge.X,box.MinEdge.Y,box.MinEdge.Z,
			box.MaxEdge.X,box.MaxEdge.Y,box.MaxEdge.Z);
		node->setDebugDataVisible(EDS_MESH_WIRE_OVERLAY);

		UnitInstanceBase* instBase = NULL;

		instBase = unitManager->GetInstanceBySceneNode(node);
		if(instBase == NULL && node->getParent() != NULL)
		{
			node = node->getParent();
			instBase = unitManager->GetInstanceBySceneNode(node);
		}

		if (instBase == NULL) return;
		node->setDebugDataVisible(EDS_MESH_WIRE_OVERLAY);

		UnitInstanceStandard* standard = dynamic_cast<UnitInstanceStandard*>(instBase);
		if (standard != NULL)
		{
			PrintInfo(standard);
						
			core::array<UnitInstanceBase*> childs = standard->GetChilds();
			printf("\n=====================================\n");
			printf("\n Childs %d:\n", childs.size());
			for (u32 i = 0; i < childs.size(); i++)
			{
				printf("\n============= Child Nr [%d] ========================\n", i);
				standard = dynamic_cast<UnitInstanceStandard*>(childs[i]);
				PrintInfo(standard);
			}

		}
	}

}

void DebugInfo::PrintInfo( UnitInstanceStandard* standard )
{
	if (standard == NULL)
	{
		return;
	}

	UnitBehavior* behavior = standard->GetBehavior();
	printf("------------ Behavior ------------\n");
	printf("OwnerPath : [%s] \n", behavior->OwnerPath.c_str());

	UnitAction* action = behavior->GetCurrentAction();
	printf("------------ Action ------------\n");
	if(action != NULL)
	{
		printf("Name : [%s] \n", action->Name.c_str());
		printf("Priority : [%d] \n", action->Priority);

		UnitBlockAction* blockAction = dynamic_cast<UnitBlockAction*>(action);
		if(blockAction != NULL)
		{
			UnitAction* curBlockAct =  blockAction->GetCurrentAction();
			if(curBlockAct != NULL)
			{
				printf("\t------------ Current BlockAction ------------\n");
				printf("\tName : [%s] \n", curBlockAct->Name.c_str());
				printf("\tPriority : [%d] \n", curBlockAct->Priority);
			}
		}
	}	

	printf("------------ Parameters ------------\n");
	UnitParameters* parameters = behavior->GetParameters();
	if (parameters != NULL)
	{
		for (u32 i = 0; i < parameters->Count() ; i++)
		{
			Parameter* paramItem = parameters->GetByIndex(i);
			printf("[%s] : [%s]\n", paramItem->Name.c_str(), paramItem->Value.c_str());
		}
	}
}

#endif