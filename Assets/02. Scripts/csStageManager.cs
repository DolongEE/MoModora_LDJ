using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class csStageManager : MonoBehaviour
{
    public GameObject player;
    PlayerData stagePlayer = new PlayerData();

    public float playTime;

    public string nowSlotName;
    void Start()
    {
        nowSlotName = "Slot" + csDataManager.instance.nowSlot;
        if (File.Exists(csDataManager.instance.path + csDataManager.instance.nowSlot))
        {
            stagePlayer = csDataManager.instance.LoadPlayerData();
            playTime = stagePlayer.gameTime;
            if (stagePlayer.slotName == "")
            {
                stagePlayer = new PlayerData(nowSlotName, player.transform.position.x, player.transform.position.y, 0, 0);
                csDataManager.instance.SavePlayerData(stagePlayer);
            }
        }
    }

    
    void Update()
    {
        PlayTime();
    }

    public float PlayTime()
    {
        playTime += Time.deltaTime;
        return playTime;
    }


}
