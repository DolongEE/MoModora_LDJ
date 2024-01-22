using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SettingPopup : UI_Popup
{
    enum Buttons
    {
        EffectButton,
        BGMButton,
        DisplayResolutionButton,
        ResoultionButton,
        DisplayVibrationButton,
        VibrationButton,
        KeyTypeButton,
        KeySetButton,
        LanguageButton,
        DefaultButton,
        ChangesButton,
        BackButton,
    }
    enum Texts
    {
        DisplayResolutionText,
        ResoultionText,
        DisplayVibrationText,
        VibrationText,
        KeyTypeText,
        LanguageText,
    }
    enum Sliders
    {
        EffectSlider,
        BGMSlider,
    }

    int _num;
    int _buttonCount;
    int _resolution;
    int _lastSlot;
    bool _isfullScreen;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Managers.Input.KeyAction -= OnKeyEvent;
        Managers.Input.KeyAction += OnKeyEvent;

        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        Bind<Slider>(typeof(Sliders));

        GetButton((int)Buttons.EffectButton).Select();

        Get<Slider>((int)Sliders.EffectSlider).value = Managers.Sound.EffectVolume;
        Get<Slider>((int)Sliders.BGMSlider).value = Managers.Sound.BgmVolume;

        SetButtons<Buttons>();
        _buttonCount = GetUICount<Buttons>();
        _resolution = PlayerPrefs.GetInt("Screenmanager Resolution Width", 640) / 320 ;
        ResolutionText();

        return true;
    }

    private void Update()
    {
        if(EventSystem.current.currentSelectedGameObject.name != GetButton(_num).name)
            GetButton(_num).Select();
    }

    void SetSound()
    {
        if (_num < 2)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
                Get<Slider>(_num).value += 0.1f;
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                Get<Slider>(_num).value -= 0.1f;
        }
    }

    void ResolutionText()
    {
        if (_resolution == 4)
        {
            _isfullScreen = true;
            GetText((int)Texts.DisplayResolutionText).text = $"전체화면";
            GetText((int)Texts.DisplayResolutionText).fontSize = 24;
        }
        else
        {
            _isfullScreen = false;
            GetText((int)Texts.DisplayResolutionText).text = $"{_resolution}X";
            GetText((int)Texts.DisplayResolutionText).fontSize = 36;
        }
    }

    void SetResolution()
    {
        if(_num == 2)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _resolution += 1;
                if (_resolution >= 4)
                {
                    _resolution = 4;
                }
            }               
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _resolution -= 1;
                if (_resolution < 1)
                    _resolution = 1;
            }
            ResolutionText();
        }
    }

    void OnKeyEvent()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (_num == 0)
                _num = _buttonCount;
            _num = (_num - 1) % _buttonCount;
            Managers.Sound.Play(Define.Sound.UI, "UIchoice3");

        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _num = (_num + 1) % _buttonCount;
            Managers.Sound.Play(Define.Sound.UI, "UIchoice3");
        }

        SetSound();        // EffectButton, BGMButton
        SetResolution();   // DisplayResolutionButton
    }

    public override void OnSelect()
    {
        GameObject go = EventSystem.current.currentSelectedGameObject;
        Enum currentName = Enum.Parse<Buttons>(go.name);
        
        switch(currentName)
        {
            case Buttons.DisplayResolutionButton:
                break;
            case Buttons.DefaultButton:
                Get<Slider>((int)Sliders.EffectSlider).value = 1.0f;
                Get<Slider>((int)Sliders.BGMSlider).value = 1.0f;
                break;
            case Buttons.ChangesButton:
                Managers.Sound.SaveSoundSetting(Get<Slider>((int)Sliders.EffectSlider).value, Get<Slider>((int)Sliders.BGMSlider).value);
                Managers.Sound.Play(Define.Sound.UI, "UIok2");
                Managers.UI.SetResolution(_isfullScreen, _resolution);

                Managers.Input.KeyAction -= OnKeyEvent;
                ClosePopupUI();
                break;
            case Buttons.BackButton:
                Managers.Sound.Play(Define.Sound.UI, "UIcancel3");

                Managers.Input.KeyAction -= OnKeyEvent;
                ClosePopupUI();
                break;
        }
    }    
}
