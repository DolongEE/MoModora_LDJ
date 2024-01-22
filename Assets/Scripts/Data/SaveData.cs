using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    public bool isCheck;
    public int slotNumber;
    public int playTime;
    public float posX;
    public float posY;
}

[Serializable]
public class SaveDataLoader : ILoader<int, SaveData>
{
    public List<SaveData> saveDatas = new List<SaveData>();

    public Dictionary<int, SaveData> MakeDic()
    {
        Dictionary<int, SaveData> dict = new Dictionary<int, SaveData>();

        foreach (SaveData data in saveDatas)
            dict.Add(data.slotNumber, data);

        return dict;
    }
}