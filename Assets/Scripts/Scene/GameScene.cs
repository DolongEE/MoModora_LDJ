using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameScene : BaseScene
{
    protected override bool Init()
    {
        if (base.Init() == false)
            return false;
        SceneType = Define.Scene.Game;

        Managers.Sound.Clear();
        Managers.Sound.Play(Define.Sound.Bgm, "forest_ambiance");

        return true;
    }

    private void Update()
    {

    }
}
