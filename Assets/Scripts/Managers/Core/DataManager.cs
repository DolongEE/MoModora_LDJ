using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDic();
}

public class DataManager
{ 
    public Dictionary<int, SaveData> SaveDict { get; private set; }

    public void Init()
    {
        SaveDict = LoadJson<SaveDataLoader, int, SaveData>("SaveData").MakeDic();
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }

    public void DataClear()
    {
        
    }
    
}