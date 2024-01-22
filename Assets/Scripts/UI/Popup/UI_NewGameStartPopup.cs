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
    enum Difficult { ����, ����, �����, �Ǹ� }
    string[] _difficultText = 
        {   "���� ���ӿ� �ͼ����� ���� ����� ���� ���̵�",
            "Ⱦ��ũ�� ���ӿ� �ͼ��� ����� ���� ���̵�",
            "���� ����� ������ ���ϴ� ����� ���� ���̵�!\r\n�ʺ��ڿ��Դ� ��õ���� �ʽ��ϴ�",
            "�õ��� ������ ���� ���ƿ�.",
        };
    Difficult _difficultEnum = Difficult.����;

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

        GetText((int)Texts.SelectText).text = "���۹�� �����ϱ�";
        GetText((int)Texts.ExplainText).text = "Ű����";
        _difficultNum = 1;

        return true;
    }

    void DiffiecultSet()
    {

        if (_difficultNum == 0)
            _difficultEnum = Difficult.����;
        if (_difficultNum == 1)
            _difficultEnum = Difficult.����;
        if (_difficultNum == 2)
            _difficultEnum = Difficult.�����;
        if (_difficultNum == 3)
            _difficultEnum = Difficult.�Ǹ�;

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
            GetText((int)Texts.SelectText).text = "���̵� ����";
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

            GetText((int)Texts.SelectText).text = "���۹�� �����ϱ�";
            GetText((int)Texts.ExplainText).text = "Ű����";
        }
    }
}
