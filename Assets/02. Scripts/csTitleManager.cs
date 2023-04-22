using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class csTitleManager : MonoBehaviour
{
    public Canvas cvTitle;
    public GameObject pnlLobby;
    public GameObject imgBG;

    private int slotNum;
    public Button[] saveSlots;

    private void Awake()
    {

    }
    
    void Start()
    {
        
    }

    void Update()
    {
        if (!csSetting.instance.settingOpen)
        {
            if (Input.anyKeyDown)
            {
                imgBG.SetActive(false);
                pnlLobby.SetActive(true);
            }

            SlotSelectNum();

            if (Input.GetKeyDown(KeyCode.A))
            {
                SelectSlot(slotNum);
            }
        }

    }

    private void SelectSlot(int numSlot)
    {
        if (numSlot < 4)
        {

        }
        saveSlots[numSlot].onClick.Invoke();
    }

    private void SlotSelectNum()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && slotNum > 0)
        {
            slotNum--;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && slotNum < 6)
        {
            slotNum++;
        }
        saveSlots[slotNum].Select();
    }

    public void OnClickSetting()
    {
        csSetting.instance.settingOpen = true;
    }

    public void OnClickExitGame()
    {
        Application.Quit();
    }

    public void NewSlot()
    {

    }
    
    
}
