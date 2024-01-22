using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager
{
    public Action KeyAction = null;
    public Action<Define.KeyActionUI> KeyActionUI = null;
 
    public void OnUpdate()
    {
        if (Input.anyKeyDown && KeyAction != null)        
            KeyAction.Invoke();        

        if (Input.anyKeyDown && KeyActionUI != null && EventSystem.current.currentSelectedGameObject.IsUnityNull() == false)
        {
            if (Input.GetKeyDown(KeyCode.A))            
                KeyActionUI.Invoke(Define.KeyActionUI.Select);            
            if (Input.GetKeyDown(KeyCode.S))            
                KeyActionUI.Invoke(Define.KeyActionUI.Back);
        }
    }

    public void Clear()
    {
        KeyAction = null;
        KeyActionUI = null;
    }
}
