#include "UnitModelAnim.h"
#include "Core/NodeWorker.h"

UnitModelAnim::UnitModelAnim( SharedParams_t params ) : UnitModelBase(params),
	ModelPath()
{
	Use32Bit = false;
	Culling = true;
}

ISceneNode* UnitModelAnim::LoadSceneNode()
{
	if(ModelPath != "")
	{
		IAnimatedMeshSceneNode* node = NodeWorker::AddNode( 
			SceneManager, Driver,
			ModelPath, Culling, false, Use32Bit);
		if(node == NULL)
		{			
			_DEBUG_BREAK_IF(true)
			return NULL;
		}
		//node->getMesh()->setHardwareMappingHint(EHM_STREAM, EBT_VERTEX_AND_INDEX); // EHM_DYNAMIC, EBT_VERTEX
		return node;
	}
	return NULL;
}
