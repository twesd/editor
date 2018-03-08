#pragma once
#include "../engine/Core/Base.h"
#include "../engine/IModule.h"
#include "../engine/Stage/UnitManager.h"
#include "../engine/Controls/ControlManager.h"
#include "../engine/Controls/ControlText.h"
#include "../engine/Controls/ControlButton.h"

class ModuleMarket : public IModule, Base
{
public:
	ModuleMarket(SharedParams_t params);
	virtual ~ModuleMarket(void);

	stringc GetName();

	void Execute();

	void LoadStage();

	void ClearData();

private:
	typedef struct
	{
		IControl* Control;
		position2di StartPos;
		bool IsSelected;
		s32 SelectedIndex;
		position2di DestPos;
		bool IsAnimated;		
		bool IsEnabled;		
		s32 Price;
	}ControlItem_t;

	void Init();

	ControlItem_t* InitItem(stringc cntrlName );

	void UpdateEnabledItem( stringc cntrlName );

	void UpdatePackageMain();

	void UpdatePackageBuyItem();
	
	bool _isInit;

	bool _isFirst;

	int _completeTime;

	bool _isDone;

	s32 _selectedCount;

	core::array<ControlItem_t> _items;

	s32 _yBtnSize;

	s32 _topOffset;

	s32 _maxCount;

	ControlButton* _startBtn;

	IControl* _startBtnDisable;

	ControlText* _moneyText;

	s32 _money;

	ControlItem_t* _buyItem;
};
