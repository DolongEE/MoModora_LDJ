using System.Diagnostics;
using UnityEngine.UI;

public class UI_Popup : UI_Base
{ 
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Managers.Input.KeyActionUI -= OnSelectKey;
        Managers.Input.KeyActionUI += OnSelectKey;

        Managers.UI.SetCanvas(gameObject, true);
        return true;
    }

    public virtual void ClosePopupUI()
    {
        Managers.Input.KeyActionUI -= OnSelectKey;
        Managers.UI.ClosePopupUI(this);
    }

    public void OnSelectKey(Define.KeyActionUI type)
    {
        
        switch (type)
        {
            case Define.KeyActionUI.Select:
                OnSelect();
                break;
            case Define.KeyActionUI.Back:
                OnBack();
                break;
        }
    }

    public virtual void OnSelect() { }
    public virtual void OnBack() { }

    public void SetButtons<T>()
    {
        int count = GetUICount<T>();
        for (int i = 0; i < count; i++)
        {
            Navigation nav = new Navigation();
            nav.mode = Navigation.Mode.Explicit;
            int up = i == 0 ? count - 1 : i - 1;
            int down = i == count - 1 ? 0 : i + 1;
            nav.selectOnUp = GetButton(up);
            nav.selectOnDown = GetButton(down);
            GetButton(i).navigation = nav;
        }
    }
}
