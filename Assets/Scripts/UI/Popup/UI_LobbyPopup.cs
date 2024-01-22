using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_LobbyPopup : UI_Popup
{
    enum GameObjects
    {

    }
    enum Buttons
    {
        UI_Slot_1,
        UI_Slot_2,
        UI_Slot_3,
        UI_Slot_4,
        UI_Slot_5,
        UI_SlotSetting,
        UI_SlotExit,
    }

    enum Slots
    {
        UI_Slot_1,
        UI_Slot_2,
        UI_Slot_3,
        UI_Slot_4,
        UI_Slot_5,
        UI_SlotSetting,
        UI_SlotExit,
    }

    int _lastSlot;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Managers.Input.KeyAction -= OnKeyEvent;
        Managers.Input.KeyAction += OnKeyEvent;

        BindObject(typeof(GameObjects));
        BindButton(typeof(Buttons));
        Bind<UI_Slot>(typeof(Slots));

        Managers.Sound.Clear();
        Managers.Sound.Play(Define.Sound.Bgm, "title2");

        Get<UI_Slot>((int)Slots.UI_Slot_1).SetInfo();
        Get<UI_Slot>((int)Slots.UI_Slot_2).SetInfo();
        Get<UI_Slot>((int)Slots.UI_Slot_3).SetInfo();
        Get<UI_Slot>((int)Slots.UI_Slot_4).SetInfo();
        Get<UI_Slot>((int)Slots.UI_Slot_5).SetInfo();
        Get<UI_Slot>((int)Slots.UI_SlotSetting).SetInfo(false);
        Get<UI_Slot>((int)Slots.UI_SlotExit).SetInfo(false);

        GetButton((int)Slots.UI_Slot_1).Select();

        _lastSlot = Get<UI_Slot>((int)Slots.UI_Slot_1).slotNum;

        return true;
    }
    private void Update()
    {
        if(EventSystem.current.currentSelectedGameObject == null)
        {
            GetButton(_lastSlot - 1).Select();
        }
    }

    private void OnKeyEvent()
    {
        if (GetComponent<Canvas>().sortingOrder == Managers.UI.TopOrder())
        {
            if  (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                Managers.Sound.Play(Define.Sound.UI, "UIchoice2");
            }
        }
    }

    public override void OnSelect()
    {
        if (GetComponent<Canvas>().sortingOrder == Managers.UI.TopOrder())
        {
            Managers.Sound.Play(Define.Sound.UI, "UIok2");

            GameObject go = EventSystem.current.currentSelectedGameObject;

            if (go.name.Contains("UI_Slot_"))
            {
                UI_SaveSlotPopup popup = Managers.UI.ShowPopupUI<UI_SaveSlotPopup>("UI_SaveSlotPopup", go.transform);
                popup.IsCheck = go.GetComponent<UI_Slot>().isCheck;
                _lastSlot = go.GetComponent<UI_Slot>().slotNum;
            }
            else if (go.name.Contains("Setting"))
            {
                UI_SettingPopup popup = Managers.UI.ShowPopupUI<UI_SettingPopup>();
                _lastSlot = 6;
            }
            else
            {
                Managers.Clear();
                Application.Quit();
            }
            
        }
    }
    
}
