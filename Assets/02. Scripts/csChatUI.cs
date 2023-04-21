using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class csChatUI : MonoBehaviour
{
    public csTalkManger script_Talk;
    public csQuestManager script_QuestManager;
    public Canvas canvas_Chat;
    public GameObject pnl_Chat;
    public Text txt_Chat;
    public Text txt_Name;
    public int talkIndex;

    public GameObject scanObject;

    public bool isAction;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        canvas_Chat.enabled = false;
    }

    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;
        txt_Name.text = scanObj.name;
        csObjectData objData = scanObject.GetComponent<csObjectData>();
        Talk(objData.id, objData.isNpc);

        canvas_Chat.enabled = isAction;
    }

    private void Talk(int id, bool isNPC)
    {
        int questTalkIndex = script_QuestManager.GetQuestTalkIndex(id);
        string talkData = script_Talk.GetTalk(id + questTalkIndex, talkIndex);

        //EndTalk
        if (talkData == null) 
        {
            isAction = false;
            talkIndex = 0;
            Debug.Log(script_QuestManager.CheckQuest(id));
            
            return;
        }

        if(isNPC)
        {
            txt_Chat.text = talkData;
        }
        else
        {
            txt_Chat.text = talkData;
        }
        isAction = true;
        talkIndex++;
    }
}
