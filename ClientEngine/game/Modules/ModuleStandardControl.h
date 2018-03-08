#pragma once
#include "Core/Base.h"
#include "IModule.h"
#include "Controls/ControlManager.h"
#include "Controls/ControlButton.h"

class ModuleStandardControl : public IModule, Base
{
public:
	ModuleStandardControl(SharedParams_t params);

	virtual stringc GetName();

	virtual void Execute();

	virtual void ClearData();

private:
	typedef struct
	{
		ControlButton* Button;
		bool IsSelected;
		int SlotNr;
	}Weapon_t;

	void Init();

	void InitWeaponItem( const stringc& weaponStr, const int slotNr );

	void UpdateLifeIndicator( UnitInstanceStandard* playerInstance );

	void UpdateWeapons(UnitInstanceStandard* playerInstance);

	void SetWeapon( UnitInstanceStandard* playerInstance, Weapon_t* wp );

	void UpdatePlayerGun(UnitInstanceStandard* playerInstance, UnitInstanceStandard* playerInstanceUp);

	bool _isInit;

	bool _isFirstWeponSet;

	u32 _completeTime;

	IControl* _controlEnergy;

	core::array<Weapon_t> _weapons;

	bool _isPlayerInGun;

	Weapon_t* _weaponCurrent;

	UnitInstanceStandard* _playerGun;
};
