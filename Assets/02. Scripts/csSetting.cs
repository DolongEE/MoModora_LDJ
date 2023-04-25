using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class csSetting : MonoBehaviour
{
    public static csSetting instance;

    public Button[] btnSetting;
    public Canvas cvSetting;

    public bool isSetting;

    private int slotNum;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }
        cvSetting.enabled = false;
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        if (isSetting)
        {
            cvSetting.enabled = true;
            SeletSlotNum();
            Debug.Log("세팅 열림");
            if (Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log("세팅 " + slotNum +"슬롯 선택");
                ActionSlot(slotNum);
            }
        }

    }
    private void ActionSlot(int numSlot)
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
        btnSetting[slotNum].image.color = Color.black;
        isSetting = false;
        slotNum = 0;
        cvSetting.enabled = false;

        Debug.Log("세팅 닫음");
    }
}
