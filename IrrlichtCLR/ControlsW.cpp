#include "StdAfx.h"
#include "ControlsW.h"
#include "Convertor.h"
#include "Controls/ControlButton.h"
#include "Controls/ControlText.h"
#include "Controls/ControlRect.h"
#include "Controls/ControlLine.h"

ControlsW::ControlsW(SharedParams_t* sharedParams) : BaseW(sharedParams)
{
	_controlManager = new ControlManager(*sharedParams);
	ControlPackage package;
	core::array<ControlPackage> packages;
	packages.push_back(package);
	_controlManager->SetPackages(packages);
}


// Отрисовка изображений
void ControlsW::Draw()
{
	_controlManager->Draw();
}

void ControlsW::Remove(String^ name)
{
	c8* cName = Convertor::ConvertToU8(name);
	stringc sName(cName);
	_controlManager->RemoveControl(sName);
}

void ControlsW::Clear()
{
	_controlManager->Clear();
}

void ControlsW::AddButton(String^ name,String^ texture, Vertex3dW^ positionW)
{
	c8* cName = Convertor::ConvertToU8(name);
	c8* cTexture = Convertor::ConvertToU8(texture);
	vector3df vector = positionW->GetVector();
	position2di pos = position2di((s32)vector.X,(s32)vector.Y);
	stringc sName(cName);
	stringc sTexture(cTexture);
	
	ControlButton* btn = new ControlButton(*SharedParams);
	btn->Init(sTexture, sTexture, pos, 500);
	btn->Name = sName;
	_controlManager->AddControl(btn);
}

void ControlsW::AddRect(Vertex3dW^ positionW, int width, int height, u32 color, bool outline)
{
	vector3df vector = positionW->GetVector();
	position2di pos = position2di((s32)vector.X,(s32)vector.Y);
	stringc sName("Rect");
	ControlRect* control = new ControlRect(*SharedParams);
	control->Init(sName, pos, width, height, color, outline);
	_controlManager->AddControl(control);
}


void ControlsW::AddText( String^ text,String^ font, Vertex3dW^ positionW,  int kerningWidth, int kerningHeight )
{
	c8* cText = Convertor::ConvertToU8(text);
	c8* cFont = Convertor::ConvertToU8(font);
	vector3df vector = positionW->GetVector();
	position2di pos = position2di((s32)vector.X,(s32)vector.Y);
	stringc sText(cText);
	stringc sFont(cFont);

	ControlText* control = new ControlText(*SharedParams);
	control->Init(sFont);
	control->Text = sText;
	control->Name = "Text";
	control->KerningWidth = kerningWidth;
	control->KerningHeight = kerningHeight;
	control->SetPosition(pos);
	_controlManager->AddControl(control);
}

void ControlsW::AddLine(Vertex3dW^ startPnt, Vertex3dW^ endPnt, int width, u32 color )
{
	vector3df vectorStart = startPnt->GetVector();
	vector3df vectorEnd = endPnt->GetVector();

	vector2di vectorStart2d = vector2di((s32)vectorStart.X,(s32)vectorStart.Y);
	vector2di vectorEnd2d = vector2di((s32)vectorEnd.X,(s32)vectorEnd.Y);

	stringc sName("Line");
	ControlLine* control = new ControlLine(*SharedParams);
	control->Init(sName, vectorStart2d, vectorEnd2d, width, color);
	_controlManager->AddControl(control);
}
