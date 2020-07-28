using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    public PlayerData MyPlayerData { get; set; }

    public SaveData()
    {

    }
}

[Serializable]
public class PlayerData
{
    public int MyLife { get; set; }

    public int MyMaxLife { get; set; }

    public int MyMana { get; set; }

    public int MyMaxMana { get; set; }

    public float MyX { get; set; }

    public float MyY { get; set; }

    public float MyZ { get; set; }

    public PlayerData(int life, int maxLife, int mana, int maxMana, Vector3 position)
    {
        MyLife = life;
        MyMaxLife = maxLife;

        MyMana = mana;
        MyMaxMana = maxMana;

        MyX = position.x;
        MyY = position.y;
        MyZ = position.z;
    }
}

[Serializable]
public class ItemData
{

}

[Serializable]
public class InventoryData
{
    
}
