using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string slotName = "";
    public float posX;
    public float posY;
    public int starDust;

    public float gameTime;

    public PlayerData()
    {

    }

    public PlayerData(string name, float x, float y, int star, float time)
    {
        slotName = name;
        posX = x;
        posY = y;
        starDust = star;

        gameTime = time;
    }
}