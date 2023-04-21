using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csTalkManger : MonoBehaviour
{
    Dictionary<int, string[]> talkData;

    private void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }

    void GenerateData()
    {
        talkData.Add(1000, new string[] { "넌 누구지?", "난 이름 없는 슬라임이다." });

        //Quest Talk
        talkData.Add(10 + 1000, new string[] {  "넌 누구지?",
                                                "난 이름 없는 슬라임이다." });

        //talkData.Add(20 + 1000, new string[] {  "개구리한테 가봐"});

        talkData.Add(20 + 1000, new string[] {  "여긴 어디냐고?",
                                                "여긴 깊은 동굴이다.",
                                                "빠져나가고 싶으면 안쪽에있는 개구리를 진정시켜"});
        talkData.Add(21 + 2000, new string[] {  "난 지금 배고프다. 개굴.",
                                                "먹을 것을 가져와라. 개굴."});

        talkData.Add(30 + 2000, new string[] {  "먹을 것을 가져와라. 개굴."});
        talkData.Add(30 + 5000, new string[] {  "먹을것을 찾았다.",
                                                "개구리한테 돌아가자."});
        talkData.Add(31 + 2000, new string[] {  "고맙다. 개굴.",
                                                "지나가도 좋다. 개굴."});

    }

    public string GetTalk(int id, int talkIndex)
    {
        if(!talkData.ContainsKey(id))
        {
            if(!talkData.ContainsKey(id - id % 10))
            {
                return GetTalk(id - id % 100, talkIndex);
            }
            else
            {
                return GetTalk(id - id % 10, talkIndex);
            }            
        }

        if (talkIndex == talkData[id].Length) 
        {
            return null;
        }
        else
        {
            return talkData[id][talkIndex];
        }
    }
  

}
