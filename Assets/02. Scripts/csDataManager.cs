using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class csDataManager : MonoBehaviour
{
    public static csDataManager instance;

    PlayerData nowPlayer = new PlayerData();

    public string path;
    public int nowSlot;

    private void Awake()
    {
        #region 싱글톤
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        #endregion

        path = Application.persistentDataPath + "/Slot";
        
    }


    public void SavePlayerData(PlayerData playerData)
    {
        string data = JsonUtility.ToJson(playerData);
        File.WriteAllText(path + nowSlot.ToString(), data);
    }

    public PlayerData LoadPlayerData()
    {
        string data = File.ReadAllText(path + nowSlot.ToString());
        nowPlayer = JsonUtility.FromJson<PlayerData>(data);
        return nowPlayer;
    }

    public float LoadPlayerTime()
    {        
        return nowPlayer.gameTime;
    }
    public void DeletePlayerData()
    {
        File.Delete(path + nowSlot.ToString());
    }

    public bool IsFileExist(int slotNumber)
    {
        return File.Exists(path + slotNumber.ToString());    
    }
}
