using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class LobbyScene : BaseScene
{
    protected override bool Init()
    {
        if(base.Init() == false)
            return false;

        SceneType = Define.Scene.Lobby;

        Managers.Sound.Clear();
        Managers.Sound.Play(Define.Sound.Bgm, "title");

        Managers.UI.ShowPopupUI<UI_TitlePopup>();
        return true;
    }
}
