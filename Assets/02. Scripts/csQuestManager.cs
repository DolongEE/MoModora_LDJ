using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class csQuestManager : MonoBehaviour
{
    public static csQuestManager instance;

    public GameObject[] questObject;
    [Space(10)]
    public int questId;
    public int questActionIndex;    

    public Text txt_quest;

    Dictionary<int, csQuestData> questList;

    private void Awake()
    {
        instance = this;
        questList = new Dictionary<int, csQuestData>();
        GenerateData();
        txt_quest.text = CheckQuest(0);
    }

    void GenerateData()
    {
        questList.Add(10, new csQuestData("슬라임과 첫 대화", new int[] {1000} ));
        questList.Add(20, new csQuestData("슬라임에게 설명듣기", new int[] { 1000, 2000 }));
        questList.Add(30, new csQuestData("개구리 먹을거 갖다주기", new int[] { 5000, 2000 }));

        questList.Add(40, new csQuestData("문 밖으로 나가기", new int[] { 0 } ));
    }

    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }

    public string CheckQuest(int id)
    {
        if (id == questList[questId].npcId[questActionIndex])
        {
            questActionIndex++;
        }

        ControlObject();

        if (questActionIndex == questList[questId].npcId.Length)
        {
            NextQuest();

        }
        txt_quest.text = questList[questId].questName;
        return questList[questId].questName;
    }

    void NextQuest()
    {
        questId += 10;
        questActionIndex = 0;
    }

    void ControlObject()
    {
        switch(questId)
        {
            case 20:
                if (questActionIndex == 2)
                {
                    questObject[0].SetActive(true);
                }
                break;
            case 30:
                if (questActionIndex == 1)
                {
                    questObject[0].SetActive(false);
                }
                break;
        }
    }
}
