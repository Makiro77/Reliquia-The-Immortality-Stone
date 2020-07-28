using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    RessourcesVitalesWilliam_Scrip ressourcesVitales;
    William_Script william;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Application.persistentDataPath);
        ressourcesVitales = FindObjectOfType<RessourcesVitalesWilliam_Scrip>();
        william = FindObjectOfType<William_Script>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Save()
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Open(Application.persistentDataPath + "/" + "SaveTest.dat", FileMode.Create);

            SaveData data = new SaveData();

            SavePlayer(data);

            bf.Serialize(file, data);

            file.Close();
        }
        catch (Exception)
        {

        }
    }

    private void SavePlayer(SaveData data)
    {
        data.MyPlayerData = new PlayerData(ressourcesVitales.vieWilliam, 
            ressourcesVitales.maxVie, 
            ressourcesVitales.manaWilliam, 
            ressourcesVitales.maxMana,
            william.transform.position);
    }

    public void Load()
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Open(Application.persistentDataPath + "/" + "SaveTest.dat", FileMode.Open);

            SaveData data = (SaveData)bf.Deserialize(file);

            file.Close();

            LoadPlayer(data);
            gameManager.menuPause();
        }
        catch (Exception)
        {

        }
    }

    private void LoadPlayer(SaveData data)
    {
        ressourcesVitales.vieWilliam = data.MyPlayerData.MyLife;
        ressourcesVitales.maxVie = data.MyPlayerData.MyMaxLife;
        ressourcesVitales.SetVie(ressourcesVitales.vieWilliam);

        ressourcesVitales.manaWilliam = data.MyPlayerData.MyMana;
        ressourcesVitales.maxMana = data.MyPlayerData.MyMaxMana;
        ressourcesVitales.SetMana(ressourcesVitales.manaWilliam);

        william.transform.position = new Vector3(data.MyPlayerData.MyX, data.MyPlayerData.MyY, data.MyPlayerData.MyZ);
    }
}
