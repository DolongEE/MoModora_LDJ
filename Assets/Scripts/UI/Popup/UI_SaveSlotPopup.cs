using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_SaveSlotPopup : UI_Popup
{
    enum GameObjects
    {
        Slot_NewTap,
        Slot_LoadTap,
    }
    enum Buttons
    {
        NewGameButton,
        LoadGameButton,
        DeleteGameButton,
    }
    public bool IsCheck { get; set; }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Managers.Input.KeyAction -= OnKeyEvent;
        Managers.Input.KeyAction += OnKeyEvent;

        BindObject(typeof(GameObjects));
        BindButton(typeof(Buttons));

        GetObject((int)GameObjects.Slot_LoadTap).gameObject.SetActive(false);
        GetObject((int)GameObjects.Slot_NewTap).gameObject.SetActive(false);

        if (IsCheck)
        {
            GetObject((int)GameObjects.Slot_LoadTap).gameObject.SetActive(true);
            GetButton((int)Buttons.LoadGameButton).Select();
        }
        else
        {
            GetObject((int)GameObjects.Slot_NewTap).gameObject.SetActive(true);
            GetButton((int)Buttons.NewGameButton).Select();
        }

        GetComponent<RectTransform>().anchoredPosition = new Vector2(1, 0);

        return true;
    }

    public override void OnSelect()
    {
        Managers.Sound.Play(Define.Sound.UI, "UIok2");
        if (IsCheck)
        {
            Debug.Log($"불러오기 or 삭제:");
        }
        else
        {
            Managers.UI.ShowPopupUI<UI_NewGameStartPopup>();
            Debug.Log($"시작 버튼");
            ClosePopupUI();
        }           
    }

    void OnKeyEvent()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Managers.Sound.Play(Define.Sound.UI, "UIchoice2");
        }
    }

    public override void OnBack()
    {
        Managers.Sound.Play(Define.Sound.UI, "UIcancel2");

        Managers.Input.KeyAction -= OnKeyEvent;
        ClosePopupUI();
    }

    void StartGame(bool check)
    {
        Debug.Log("새로운 게임 시작");
        //Managers.Scene.LoadScene(Define.Scene.Game);
    }
}
