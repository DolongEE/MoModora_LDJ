using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csSavePoint : MonoBehaviour
{
    PlayerData playerData;
    csStageManager stageManager;
    csKahoCtrl kaho;

    private void Awake()
    {
        kaho = GameObject.FindGameObjectWithTag("Player").GetComponent<csKahoCtrl>();
        stageManager = GameObject.FindGameObjectWithTag("StageManager").GetComponent<csStageManager>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && Input.GetKeyDown(KeyCode.UpArrow))
        {
            playerData = new PlayerData(stageManager.nowSlotName, this.transform.position.x, this.transform.position.y, kaho.starDust, stageManager.PlayTime());
            csDataManager.instance.SavePlayerData(playerData);
            Debug.Log(stageManager.PlayTime());
            Debug.Log("세이브포인트 저장");
        }
    }
}
