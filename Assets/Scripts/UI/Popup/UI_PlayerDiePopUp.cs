using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_PlayerDiePopUp : UI_Popup
{
    enum Buttons
    {
        RetryButton,
        ReturnTitleButton,
        ExitButton,
    }

    int _buttonCount;
    [SerializeField]
    int _num;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Managers.Input.KeyAction -= OnKeyEvent;
        Managers.Input.KeyAction += OnKeyEvent;
        Managers.Sound.Clear();

        BindButton(typeof(Buttons));

        GetButton((int)Buttons.RetryButton).Select();
        _buttonCount = GetUICount<Buttons>();

        SetButtons<Buttons>();

        return true;
    }

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            GetButton(_num).Select();
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
    }
    public override void OnSelect()
    {
        Buttons btn = (Buttons)_num;
        Debug.Log(btn);
        Managers.Sound.Play(Define.Sound.UI, "UIok2");
        switch (btn)
        {
            case Buttons.RetryButton:
                // TODO: 저장된 데이터 불러옴
                // 마지막 저장 위치
                break;
            case Buttons.ReturnTitleButton:
                Managers.Scene.LoadScene(Define.Scene.Lobby);
                break;
            case Buttons.ExitButton:
                StopAllCoroutines();
                Managers.Clear();
                Application.Quit();
                break;
        }
        

    }
}
