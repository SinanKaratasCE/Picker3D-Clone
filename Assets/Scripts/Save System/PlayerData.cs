using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int levelIndex;
    public float gold;

    public PlayerData(GameManager gameManager)
    {
        levelIndex = gameManager.levelIndex;
        gold = gameManager.gold;
    }
}