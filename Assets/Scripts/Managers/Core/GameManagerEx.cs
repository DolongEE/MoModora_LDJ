using System.IO;
using UnityEngine;

public class GameData
{
    public int Hp;
    public int MaxHp;
    // TODO: 아이템 추가

    public float PlayTime;
}

public class GameManagerEx
{
    GameData _gameData = new GameData();
    public GameData SaveData { get { return _gameData; } set { _gameData = value; } }

    public float PlayTime 
    { 
        get { return _gameData.PlayTime; } 
        set { _gameData.PlayTime = value; } 
    }
    string _path;

    public void Init()
    {
        _path = Application.dataPath + "/SaveData.json";
    }

    #region Save & Load
    public void SaveGame()
    {
        string jsonStr = JsonUtility.ToJson(Managers.Game.SaveData);
        File.WriteAllText(_path, jsonStr);
        Debug.Log($"Save Data Complete: {_path}");
    }

    public bool LoadGame()
    {
        if (File.Exists(_path) == false)
            return false;

        string jsonStr = File.ReadAllText(_path);
        GameData data = JsonUtility.FromJson<GameData>(jsonStr);
        if (data != null)
            Managers.Game.SaveData = data;

        Debug.Log($"Load Data Complete: {_path}");
        return true;
    } 
    #endregion
}
