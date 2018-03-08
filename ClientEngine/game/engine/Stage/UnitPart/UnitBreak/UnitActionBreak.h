#pragma once
#include "../UnitParameters.h"
#include "../UnitEvent/UnitEventControlButton.h"

class UnitActionBreak : public Base
{
public:
	UnitActionBreak(SharedParams_t params);
	virtual ~UnitActionBreak(void);

	// Типы аниматоров
	enum
	{
		AnimatorNone,
		AnimatorAny,
		AnimatorMoveToPoint,
		AnimatorMoveToSceneNode
	};

	// Отменить поведение, если начальные условия не выполняются
	bool StartClauseNotApproved;

	// Отмена, если начальные условия выполняются
	bool StartClauseApproved;

	// Отмена, если после завершения анимации
	bool AnimationEnd;

	// Отмена, если поведение должно завершится после выполнения
	bool IsExecuteOnly;

	// Отмена, если завершился аниматор
	int AnimatorEnd;	

	// Выражение
	stringc ScriptFileName;

	
};
