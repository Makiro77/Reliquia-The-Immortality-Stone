using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{

    [SerializeField]
    private InfoItem_Script[] items;

    [SerializeField] private SavedGame[] saveSlots;

    RessourcesVitalesWilliam_Scrip ressourcesVitales;
    Inventaire_Script inventaire_Script;
    William_Script william;
    GameManager gameManager;
    SavedGame savedGame;


    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Debug.Log(Application.persistentDataPath);
        ressourcesVitales = FindObjectOfType<RessourcesVitalesWilliam_Scrip>();
        william = FindObjectOfType<William_Script>();
        gameManager = FindObjectOfType<GameManager>();
        inventaire_Script = FindObjectOfType<Inventaire_Script>();
        savedGame = FindObjectOfType<SavedGame>();

        GestionSlots();


    }

    private void ShowSavedFile(SavedGame savedGame)
    {
        if(File.Exists(Application.persistentDataPath + "/"+ savedGame.MySaveName + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + savedGame.MySaveName + ".dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);

            UnityEngine.Debug.Log("test");

            file.Close();
            savedGame.ShowInfo(data);
        }
    }

    public void GestionSlots()
    {
        foreach (SavedGame saveGame in saveSlots)
        {
            ShowSavedFile(saveGame);
        }
    }

    public void Save(SavedGame savedGame)
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Open(Application.persistentDataPath + "/" + savedGame.MySaveName + ".dat", FileMode.Create);

            SaveData data = new SaveData();

            SavePlayer(data);
            SaveScene(data);

            bf.Serialize(file, data);

            file.Close();

            GestionSlots();
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

    private void SaveScene(SaveData data)
    {
        data.MySceneData = new SceneData(SceneManager.GetActiveScene().name);
    }

    private void SaveInventory(SaveData data)
    {
        Transform inventairePanel = GameObject.Find("ParentSlot").transform;
        foreach (InfoItem_Script item in inventairePanel)
        {
            if(inventaire_Script.mItems.Count > 0)
            {
                data.MyInventoryData.ItemsInventaire.Add(new ItemData(item.NomItem));
            }
        }
    }

    public void Load(SavedGame savedGame)
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Open(Application.persistentDataPath + "/" + savedGame.MySaveName + ".dat", FileMode.Open);

            SaveData data = (SaveData)bf.Deserialize(file);

            file.Close();

            /*if (savedGame.nomSceneActuelle != SceneManager.GetActiveScene().name)
            {
                SceneManager.LoadScene(savedGame.nomSceneActuelle);
            }
            else return;*/

            LoadPlayer(data);
            //LoadScene(data);
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
    
    private void LoadScene(SaveData data)
    {
        UnityEngine.Debug.Log(data.MySceneData.NomScene);
        savedGame.nomSceneActuelle = data.MySceneData.NomScene;
    }

    private void LoadInventory(SaveData data)
    {
        UnityEngine.Debug.Log("load");
        Transform inventairePanel = GameObject.Find("ParentSlot").transform;
        foreach (ItemData itemData in inventairePanel)
        {
            InfoItem_Script infoItem = Array.Find(items, x => x.NomItem == itemData.ItemNom);
            inventaire_Script.mItems.Add(infoItem);
        }
    }
}
