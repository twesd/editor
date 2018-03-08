#pragma  once


enum STAGE_MANAGER_EVENTS
{
	// Отключить обработку UnitManager
	STAGE_MANAGER_EVENT_STOP_UNIT_MANAGER = 1000,
	// Включить обработку UnitManager
	STAGE_MANAGER_EVENT_START_UNIT_MANAGER,
	// Отключить SceneManager->drawAll()
	STAGE_MANAGER_EVENT_STOP_SCENE_MANAGER = 1000,
	// Включить SceneManager->drawAll()
	STAGE_MANAGER_EVENT_START_SCENE_MANAGER = 1000,
};