using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemData
{
    public int Id;
}

[Serializable]
public class ItemDataLoader : ILoader<int, ItemData>
{
    public List<ItemData> itemDatas = new List<ItemData>();

    public Dictionary<int, ItemData> MakeDic()
    {
        Dictionary<int, ItemData> dict = new Dictionary<int, ItemData>();

        foreach (ItemData data in itemDatas)
            dict.Add(data.Id, data);

        return dict;
    }
}