using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager
{
    int _order = 10;

    Stack<UI_Popup> _popupStack = new Stack<UI_Popup>();

    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root");
            if(root == null)
            {
                root = new GameObject { name = "@UI_Root" };
            }
            return root;
        }
    }

    public void SetResolution(bool isfullScreen, int multipleSize)
    {
        //320   240     :4:3 (80)
        //640   480     :4:3 (160)
        //960   720     :4:3 (240)
        //1440  1080    :4:3 (360)
        int setWidth = 320 * multipleSize; // 사용자 설정 너비
        int setHeight = 240 * multipleSize; // 사용자 설정 높이
        if (isfullScreen)
        {       
            setWidth = 1440;
            setHeight = 1080;
        }

        Screen.SetResolution(setWidth, setHeight, isfullScreen); // SetResolution 함수 제대로 사용하기
    }

    public void SetCanvas(GameObject go, bool sort = true)
    {
        Canvas canvas = Utils.GetOrAddComponet<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;

        if(sort)
        { 
            canvas.sortingOrder = _order;
            _order++;
        }
        else
            canvas.sortingOrder = 0;
    }

    public T ShowPopupUI<T>(string name = null, Transform parent = null) where T : UI_Popup
    {
        if(string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject prefab = Managers.Resource.Load<GameObject>($"Prefabs/UI/Popup/{name}");

        GameObject go = Managers.Resource.Instantiate($"UI/Popup/{name}");
        T popup = Utils.GetOrAddComponet<T>(go);
        _popupStack.Push(popup);

        if (parent != null)
            go.transform.SetParent(parent);
        else
            go.transform.SetParent(Root.transform);

        go.transform.localScale = Vector3.one;
        go.transform.localPosition = prefab.transform.position;

        return popup;
    }

    public void ClosePopupUI(UI_Popup popup)
    {
        if (_popupStack.Count == 0)
            return;

        if (_popupStack.Peek() != popup)
        {
            Debug.Log($"Failed Close Popup");
            return;
        }

        ClosePopupUI();
    }

    public void ClosePopupUI()
    {
        if (_popupStack.Count == 0)
            return;

        UI_Popup popup = _popupStack.Pop();
        Managers.Resource.Destroy(popup.gameObject);
        popup = null;
        _order--;
    }

    public void CloseAllPopupUI()
    {
        while (_popupStack.Count > 0)
            ClosePopupUI();
    }

    public int TopOrder()
    {
        return _order - 1;
    }

    public void Clear()
    {
        CloseAllPopupUI();
    }
}
