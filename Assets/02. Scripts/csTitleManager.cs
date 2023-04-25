using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.EventSystems;

public class csTitleManager : MonoBehaviour
{
    public Canvas cvTitle;
    public GameObject pnlLobby;
    public GameObject imgBG;
    public Button[] slots;
    public Button btnTemp;
    public Button[] btnTemps;
    private PlayerData playerData = new PlayerData();
    //private GameObject newSlot;
    //private GameObject LoadSlot;

    private int slotNum;
    private int tempNum;
    public bool[] saveFileExist;

    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            if (csDataManager.instance.IsFileExist(i))
            {
                slots[i].transform.GetChild(0).GetComponent<Text>().text = csDataManager.instance.LoadPlayerData().slotName;
                slots[i].transform.GetChild(1).gameObject.SetActive(true);
                slots[i].transform.GetChild(1).GetComponent<Text>().text = TextTime();
            }
        }
    }

    void Update()
    {
        if (!csSetting.instance.isSetting)
        {
            if (Input.anyKeyDown)
            {
                imgBG.SetActive(false);
                pnlLobby.SetActive(true);
            }

            SlotSelectNum();

            if (Input.GetKeyDown(KeyCode.A))
            {
                if (btnTemp == null && btnTemps[0] == null)
                {
                    ActionSlot(slotNum);
                }
                else if (btnTemp != null && btnTemps[0] == null)
                {
                    Debug.Log("파일생성");
                    CreateJson();
                    GoGame();
                }
                else if (btnTemp == null && btnTemps[0] != null)
                {
                    Debug.Log("파일불러옴");
                    LoadJson();
                    if (tempNum == 0)
                    {
                        Debug.Log("게임 시작");
                        GoGame();
                    }
                    else
                    {
                        Debug.Log("삭제 실행");
                        OnClickBack();
                        DeleteJson();
                    }
                }
            }
        
            if(btnTemp != null)
            {
                btnTemp.Select();
            }

            if(btnTemps[0] != null)
            {
                TempSelectNum();
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                OnClickBack();
            }
        }
    }


    private void ActionSlot(int numSlot)
    {
        if (numSlot < 5)
        {
            csDataManager.instance.nowSlot = numSlot;
            if (!csDataManager.instance.IsFileExist(numSlot))
            {
                GameObject makeData = slots[numSlot].transform.Find("newSlot").gameObject;
                makeData.SetActive(true);
                btnTemp = makeData.GetComponent<Button>();
            }
            else
            {
                GameObject makeData = slots[numSlot].transform.Find("LoadSlot").gameObject;
                Button[] makeChild = makeData.GetComponentsInChildren<Button>();
                makeData.SetActive(true);
                for (int i = 0; i < makeChild.Length; i++)
                {
                    btnTemps[i] = makeChild[i];
                }
            }
        }
        slots[numSlot].onClick.Invoke();
    }

    //슬롯 이동
    private void SlotSelectNum()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && slotNum >= 0 && btnTemp == null)
        {
            slotNum--;
            if (slotNum == -1)
            {
                slotNum = 6;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && slotNum < 7 && btnTemp == null)
        {
            slotNum++;
            if (slotNum == 7)
            {
                slotNum = 0;
            }
        }
        slots[slotNum].Select();
    }
    private void TempSelectNum()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && tempNum > 0)
        {
            tempNum--;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && tempNum < 1)
        {
            tempNum++;
        }
        btnTemps[tempNum].Select();
    }


    public void OnClickBack()
    {
        if (btnTemp != null)
        {
            btnTemp = null;
        }
        if (btnTemps[0] != null)
        {
            btnTemps[0].transform.parent.gameObject.SetActive(false);
            btnTemps[0] = null;
            btnTemps[1] = null;
            return;
        }
        if (!EventSystem.current.currentSelectedGameObject.CompareTag("Slot"))
        {
            EventSystem.current.currentSelectedGameObject.SetActive(false);
        }
    }



    public void OnClickSetting()
    {
        slotNum = 0;
        
        csSetting.instance.isSetting = true;
    }

    public void OnClickExitGame()
    {
        Application.Quit();
    }

    private void CreateJson()
    {
        csDataManager.instance.SavePlayerData(playerData);
    }

    private void LoadJson()
    {
        csDataManager.instance.LoadPlayerData();
    }
    private void DeleteJson()
    {
        Debug.Log("json데이터 삭제");
        slots[slotNum].transform.GetChild(0).GetComponent<Text>().text = "빈 파일";
        csDataManager.instance.DeletePlayerData();
    }

    private void GoGame()
    {
        SceneManager.LoadScene("scStage");
    }
    
    private string TextTime()
    {
        string setTime;
        int time = (int)csDataManager.instance.LoadPlayerTime();
        setTime = ((int)time / 3600).ToString("D2") + ":" + ((int)time / 60 % 60).ToString("D2") + ":" + ((int)time % 60).ToString("D2");
        return setTime;
    }
}
