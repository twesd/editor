#pragma  once

enum MANAGER_EVENTS
{
	// Стадия завершена
	MANAGER_EVENT_STAGE_COMPLETE = 1000,
	// Загрузка стадии
	MANAGER_EVENT_STAGE_LOAD,
	// Перезагрузка стадии
	MANAGER_EVENT_STAGE_RESTART,
	// Показать рекламу
	MANAGER_EVENT_AD_SHOW,
	// Скрыть рекламу
	MANAGER_EVENT_AD_HIDE,
	// Поставить оецнку приложению
	MANAGER_EVENT_RATE_APP
};