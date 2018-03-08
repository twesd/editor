#pragma once
#include "ExecuteBase.h"

enum CreationTypeEnum
{
	CreationType_Child,
	CreationType_Externals
};

class ExecuteCreateUnit : public ExecuteBase
{
public:
	ExecuteCreateUnit(SharedParams_t params);
	virtual ~ExecuteCreateUnit(void);

	// Установить начальную позицию
	void SetStartPosition( vector3df pos );

	// Установить начальный поворот
	void SetStartRotation( vector3df rotation );

	// Установить основную директорию
	void SetRoot(stringc root);
	
	// Выполнить действие
	virtual void Run(scene::ISceneNode* node, core::array<Event_t*>& events);

	// Путь до поведений
	stringc BehaviorsPath;

	// Позволить создавать несколько экземпляров
	bool AllowSeveralInstances;

	// Тип созданию юнита
	CreationTypeEnum CreationType;

	// Получить позицию из TapScene
	bool GetPositionFromTapScene;

	// Наименование TapScene
	stringc TapSceneName;

	// Наименование кости
	stringc JointName;

	// Начальный скрипт
	stringc StartScriptFileName;
private:
	// Начальная позиция
	vector3df _startPosition;

	// Начальное вращение
	vector3df _startRotation;

	// Основная директория
	stringc _root;
};
