#include "ControlManager.h"
#include "LoaderControlManager.h"
#include "ControlTapScene.h"
#include "../Core/Xml/XMLHelper.h"


ControlManager::ControlManager(
	SharedParams_t params, 
	float controlsScale, 
	position2di controlsOffset) : Base(params),	
	_removeControls()
{
	_activatePackageNumber = 0;
	_nexActivatePackage = -1;
	_controlsScale = controlsScale;
	_controlsOffset = controlsOffset;

	_parser = new ParserAS();	
}

ControlManager::~ControlManager(void)
{
	if(_packages.size() == 0) return;
	for(u32 iPackage = 0; iPackage < _packages.size(); iPackage++)
	{
		ControlPackage* activatePackage = &_packages[iPackage];
		for(u32 iCntrl = 0; iCntrl < activatePackage->Controls.size(); iCntrl++)
		{
			activatePackage->Controls[iCntrl]->drop();
		}
		activatePackage->Controls.clear();
	}
	delete _parser;
}

// Установить объект кэширования xml файлов
void ControlManager::SetXmlCache( XMLCache* xmlCache )
{
	_DEBUG_BREAK_IF(xmlCache == NULL)
	_xmlCache = xmlCache;
}

// Получить объект кэширования xml файлов
XMLCache* ControlManager::GetXmlCache()
{
	return _xmlCache;
}

void ControlManager::SetScriptCache( ScriptCache* scriptCache )
{
	_scriptCache = scriptCache;
}

ScriptCache* ControlManager::GetScriptCache()
{
	return _scriptCache;
}

// Установка пакетов контролов
void ControlManager::SetPackages(core::array<ControlPackage> packages)
{
	_packages = packages;

	for(u32 iPackage = 0; iPackage < _packages.size(); iPackage++)
	{
		ControlPackage* activatePackage = &_packages[iPackage];
		for(u32 iCntrl = 0; iCntrl < activatePackage->Controls.size(); iCntrl++)
		{
			activatePackage->Controls[iCntrl]->SetControlManager(this);
			activatePackage->Controls[iCntrl]->SetControlsScale(_controlsScale);
		}
	}

	// Устанавливаем пакет по умолчанию
	for(u32 iPackage = 0; iPackage < _packages.size(); iPackage++)
	{
		ControlPackage* activatePackage = &_packages[iPackage];
		if(activatePackage->IsDefault)
		{
			_activatePackageNumber = iPackage;
			break;
		}
	}
	
	RaiseEventPackageShow();
}

void ControlManager::Update(reciverInfo_t *reciverInfo)
{
	if(_packages.size() == 0) return;
	
	ControlPackage* activatePackage = &_packages[_activatePackageNumber];

	// Собираем необрабатывамые зоны для TapScene
	//
	core::array<recti> tapSceneNotProcessingSpace;
	for(u32 iCntrl = 0; iCntrl < activatePackage->Controls.size(); iCntrl++)
	{
		IControl* control = activatePackage->Controls[iCntrl];
		if (control->GetControlType() == CONTROL_TYPE_BUTTON && 
			control->IsVisible)
		{
			tapSceneNotProcessingSpace.push_back(control->GetBounds());
		}
	}
	// Устанавливаем необрабатывамые зоны для TapScene
	//
	for(u32 iCntrl = 0; iCntrl < activatePackage->Controls.size(); iCntrl++)
	{
		IControl* control = activatePackage->Controls[iCntrl];
		if (control->GetControlType() == CONTROL_TYPE_TAPSCENE)
		{
			ControlTapScene* tapScene = (ControlTapScene*)control;
			tapScene->SetNotProcessingSpace(tapSceneNotProcessingSpace);
		}
	}

	// Вызываем обработчики данных
	for(u32 iCntrl = 0; iCntrl < activatePackage->Controls.size(); iCntrl++)
	{
		activatePackage->Controls[iCntrl]->Update(reciverInfo);
	}

	if(_nexActivatePackage >= 0)
	{
		SetControlPackageByIndex(_nexActivatePackage);
	}
	// Удаляем помеченные контролы
	ExecuteRemovingControls();
}

void ControlManager::Draw()
{
	if(_packages.size() == 0) return;
	ControlPackage* activatePackage = &_packages[_activatePackageNumber];
	for(u32 iCntrl = 0; iCntrl < activatePackage->Controls.size(); iCntrl++)
	{
		activatePackage->Controls[iCntrl]->Draw();
	}
}

// Получить контрол по имени в активном пакете
IControl* ControlManager::GetControlByName( stringc name )
{
	ControlPackage* activatePackage = &_packages[_activatePackageNumber];

	for(u32 iCntrl = 0; iCntrl < activatePackage->Controls.size(); iCntrl++)
	{
		IControl* control = activatePackage->Controls[iCntrl];
		if (control->Name == name)
		{
			return control;
		}
	}
	return NULL;
}

IControl* ControlManager::GetControlById( stringc id )
{
	ControlPackage* activatePackage = &_packages[_activatePackageNumber];

	for(u32 iCntrl = 0; iCntrl < activatePackage->Controls.size(); iCntrl++)
	{
		IControl* control = activatePackage->Controls[iCntrl];
		if (control->Id == id)
		{
			return control;
		}
	}
	return NULL;
}


// Установить пакет
void ControlManager::SetControlPackage( stringc name )
{
	for (u32 i = 0; i < _packages.size() ; i++)
	{
		stringc path = Device->getFileSystem()->getFileBasename(_packages[i].Path);
		if(path == name)
		{
			_nexActivatePackage = i;
			return;
		}
	}
	_DEBUG_BREAK_IF(true);
}

// Установить пакет
void ControlManager::SetControlPackageByIndex( u32 index )
{
	_DEBUG_BREAK_IF(index < 0 || index >= _packages.size())
	
	// Удаляем помеченные контролы в текущем пакете
	ExecuteRemovingControls();

	_activatePackageNumber = index;
	_nexActivatePackage = -1;
		
	// Вызывает событие отображения нового пакета
	RaiseEventPackageShow();

	// Делаем пустой update для прохождения функций иницилизаций 
	//
	reciverInfo_t reciveInfoEmpty;
	for(u32 i=0;i<MAX_USER_ACTION_COUNT;i++)
	{
		reciveInfoEmpty.StateTouch[i] = FAIL_VALUE;
		reciveInfoEmpty.XTouch[i] = FAIL_VALUE;
		reciveInfoEmpty.YTouch[i] = FAIL_VALUE;		
	}
#ifdef WINDOWS_COMPILE
	reciveInfoEmpty.MousePressed = false;
#endif
	reciveInfoEmpty.Xaccel = reciveInfoEmpty.Yaccel = reciveInfoEmpty.Zaccel = 0;	
	Update(&reciveInfoEmpty);
}


// Получить имя текущего проекта
stringc ControlManager::GetCurrentPackageName()
{
	ControlPackage* activatePackage = &_packages[_activatePackageNumber];
	return Device->getFileSystem()->getFileBasename(activatePackage->Path);
}


// Вызывает событие отображения нового пакета
void ControlManager::RaiseEventPackageShow()
{
	ControlPackage* activatePackage = &_packages[_activatePackageNumber];
	for(u32 iCntrl = 0; iCntrl < activatePackage->Controls.size(); iCntrl++)
	{
		IControl* control = activatePackage->Controls[iCntrl];
		if(control->OnPackageShow != "")
		{
			_parser->GetUserData()->ControlManagerData = this;
			_parser->GetUserData()->BaseData = this;
			_parser->GetUserData()->ControlOwnerData = control;
			_parser->SetCode(control->OnPackageShow);
			_parser->Execute();
		}
	}
}


void ControlManager::AddControl( IControl* control )
{
	ControlPackage* activatePackage = &_packages[_activatePackageNumber];
	for(u32 iCntrl = 0; iCntrl < activatePackage->Controls.size(); iCntrl++)
	{
		IControl* pkgControl = activatePackage->Controls[iCntrl];
		if(control == pkgControl)
		{
			return;
		}
	}
	activatePackage->Controls.push_back(control);
	control->SetControlsScale(_controlsScale);
}

// Пометить контрол для удаления из текущего пакета
void ControlManager::RemoveControl( stringc& name )
{
	IControl* control = GetControlByName(name);
	_DEBUG_BREAK_IF(control == NULL)
	_removeControls.push_back(control);
}

// Удаляем помеченные контролы
void ControlManager::ExecuteRemovingControls()
{
	ControlPackage* activatePackage = &_packages[_activatePackageNumber];
	for (u32 iRemoveCntrl = 0; iRemoveCntrl <  _removeControls.size(); iRemoveCntrl++)
	{
		IControl* control = _removeControls[iRemoveCntrl];		
		s32 index = -1;		
		for(u32 iCntrl = 0; iCntrl < activatePackage->Controls.size(); iCntrl++)
		{
			if(activatePackage->Controls[iCntrl] == control)
			{
				index = iCntrl;
				break;
			}
		}		
		_DEBUG_BREAK_IF(index < 0);
		activatePackage->Controls.erase(index);
		control->drop();
	}
	_removeControls.clear();
}

// Получить коэф. масштабирования контролов
float ControlManager::GetControlsScale()
{
	return _controlsScale;
}

const position2di& ControlManager::GetControlsOffset()
{
	return _controlsOffset;
}




