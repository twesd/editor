#include "ExecuteDeleteUnitsAll.h"
#include "../../UnitInstance/UnitInstanceStandard.h"

ExecuteDeleteUnitsAll::ExecuteDeleteUnitsAll(SharedParams_t params) : 
	ExecuteBase(params), JointName()
{
}

ExecuteDeleteUnitsAll::~ExecuteDeleteUnitsAll()
{

}

// Выполнить действие
void ExecuteDeleteUnitsAll::Run(scene::ISceneNode* node, core::array<Event_t*>& events)
{
	ExecuteBase::Run(node, events);

	_DEBUG_BREAK_IF(UnitInstance == NULL)

	// Возможный наименования костей
	stringc jointName2 = stringc("Frame_") + JointName;
	stringc jointName3 = stringc("Anim_MatrixFrame_") + JointName;

	core::array<UnitInstanceBase*> childs =	UnitInstance->GetChilds();
	for (u32 iCh = 0; iCh < childs.size(); iCh++)
	{
		UnitInstanceBase* instance = childs[iCh];
		if (instance->SceneNode == NULL)
		{
			continue;
		}
		ISceneNode* pNode = instance->SceneNode->getParent();
		if (pNode->getType() == ESNT_UNKNOWN)// В irrlicht тип joint объявлен как Unknown
		{	
			stringc jointName = stringc(pNode->getName());
			if (jointName == JointName ||
				jointName == jointName2 ||
				jointName == jointName3)
			{
				instance->Erase();
			}
		}

	}
}
