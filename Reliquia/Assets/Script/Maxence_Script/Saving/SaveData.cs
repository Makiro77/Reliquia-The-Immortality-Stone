using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    public PlayerData MyPlayerData { get; set; }

    public SceneData MySceneData { get; set; }

    public ItemData MyItemData { get; set; }

    public InventoryData MyInventoryData { get; set; }

    public DateTime MyDateTime { get; set; }

    public SaveData()
    {
        MyInventoryData = new InventoryData();
        MyDateTime = DateTime.Now;
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
public class SceneData
{
    public string NomScene { get; set; }

    public SceneData(string sceneNom)
    {
        NomScene = sceneNom;
    }
}

[Serializable]
public class ItemData
{
    public string ItemNom { get; set; }

    public ItemData(string itemNom)
    {
        ItemNom = itemNom;
    }
}

[Serializable]
public class InventoryData
{
    public List<ItemData> ItemsInventaire {get; set;}

    public InventoryData()
    {
        ItemsInventaire = new List<ItemData>();
    }
}
