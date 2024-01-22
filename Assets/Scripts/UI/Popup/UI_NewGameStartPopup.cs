using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_NewGameStartPopup : UI_Popup
{
    enum GameObjects
    {
        KeyTap,
        DifficultTap,
    }
    enum Texts
    {
        SelectText,
        ExplainText,
        DifficultText,
    }
    enum Difficult { 쉬움, 보통, 어려움, 악몽 }
    string[] _difficultText = 
        {   "비디오 게임에 익숙하지 않은 사람을 위한 난이도",
            "횡스크롤 게임에 익숙한 사람을 위한 난이도",
            "조금 어려운 도전을 원하는 사람을 위한 난이도!\r\n초보자에게는 추천하지 않습니다",
            "시도할 생각도 하지 말아요.",
        };
    Difficult _difficultEnum = Difficult.보통;

    int _difficultNum;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Managers.Input.KeyAction -= OnKeyEvent;
        Managers.Input.KeyAction += OnKeyEvent;

        BindObject(typeof(GameObjects));
        BindText(typeof(Texts));

        GetObject((int)GameObjects.DifficultTap).SetActive(false);

        GetText((int)Texts.SelectText).text = "조작방법 선택하기";
        GetText((int)Texts.ExplainText).text = "키보드";
        _difficultNum = 1;

        return true;
    }

    void DiffiecultSet()
    {

        if (_difficultNum == 0)
            _difficultEnum = Difficult.쉬움;
        if (_difficultNum == 1)
            _difficultEnum = Difficult.보통;
        if (_difficultNum == 2)
            _difficultEnum = Difficult.어려움;
        if (_difficultNum == 3)
            _difficultEnum = Difficult.악몽;

        GetText((int)Texts.ExplainText).text = Utils.GetEnumName<Difficult>(_difficultEnum);
        GetText((int)Texts.DifficultText).text = _difficultText[_difficultNum];
    }

    void OnKeyEvent()
    {
        if(GetObject((int)GameObjects.DifficultTap).activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _difficultNum = (_difficultNum + 1) % _difficultText.Length;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (_difficultNum == 0)
                    _difficultNum = _difficultText.Length;
                _difficultNum = (_difficultNum - 1) % _difficultText.Length;
            }
            DiffiecultSet();
        }
    }

    public override void OnSelect()
    {
        if (GetObject((int)GameObjects.KeyTap).activeSelf)
        {
            GetObject((int)GameObjects.KeyTap).SetActive(false);
            GetObject((int)GameObjects.DifficultTap).SetActive(true);
            GetText((int)Texts.SelectText).text = "난이도 선택";
            GetText((int)Texts.ExplainText).text = Utils.GetEnumName<Difficult>(_difficultEnum);
            GetText((int)Texts.DifficultText).text = _difficultText[_difficultNum];

        }
        else if (GetObject((int)GameObjects.DifficultTap).activeSelf)
        {
            Managers.Scene.LoadScene(Define.Scene.Game);
        }
    }

    public override void OnBack()
    {
        if (GetObject((int)GameObjects.DifficultTap).activeSelf)
        {
            GetObject((int)GameObjects.DifficultTap).SetActive(false);
            GetObject((int)GameObjects.KeyTap).SetActive(true);

            GetText((int)Texts.SelectText).text = "조작방법 선택하기";
            GetText((int)Texts.ExplainText).text = "키보드";
        }
    }
}
