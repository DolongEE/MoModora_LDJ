using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class csSetting : MonoBehaviour
{
    public static csSetting instance;

    public Button[] btnSetting;
    public Canvas cvSetting;

    public bool settingOpen;

    private int slotNum;

    private void Awake()
    {
        instance = this;
        cvSetting.enabled = settingOpen;
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        if(settingOpen)
        {
            cvSetting.enabled = settingOpen;
            SeletSlotNum();
        }

        if (Input.GetKeyDown(KeyCode.A) && settingOpen)
        {
            SelectSlot(slotNum);
        }
    }
    private void SelectSlot(int numSlot)
    {
        btnSetting[numSlot].onClick.Invoke();
    }

    private void SeletSlotNum()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && slotNum > 0)
        {
            slotNum--;
            btnSetting[slotNum + 1].image.color = Color.black;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && slotNum < 4)
        {
            slotNum++;
            btnSetting[slotNum - 1].image.color = Color.black;
        }

        btnSetting[slotNum].image.color = Color.white;
        btnSetting[slotNum].Select();
    }

    public void OnClicEffect()
    {
        
    }

    public void OnClicBgm()
    {
        
    }
    public void OnClickKeyguide()
    {
        
    }

    public void OnClickSaveSet()
    {
        
    }

    public void OnClickBack()
    {
        settingOpen = false;
    }
}
