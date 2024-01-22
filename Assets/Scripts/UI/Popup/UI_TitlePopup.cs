using UnityEngine;

public class UI_TitlePopup : UI_Popup
{
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Managers.Input.KeyAction -= OnKeyEvent;
        Managers.Input.KeyAction += OnKeyEvent;

        return true;
    }

    void OnKeyEvent()
    {
        if (Input.anyKeyDown)
        {
            Managers.Sound.Play(Define.Sound.Effect, "PressStart");

            Managers.Input.KeyAction -= OnKeyEvent;
            ClosePopupUI();
            Managers.UI.ShowPopupUI<UI_LobbyPopup>();
        }
    }
}
