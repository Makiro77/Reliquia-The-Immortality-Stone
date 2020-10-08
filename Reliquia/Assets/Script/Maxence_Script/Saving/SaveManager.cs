using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{

    [SerializeField]
    private InfoItem_Script[] items;

    [SerializeField] private Image fondTransition;

    public List<GameObject> saveSlots = new List<GameObject>();

    Inventaire_Script inventaire_Script;
    SavedGame savedGame;

    public static SaveManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }

        /*saveSlots.Clear();

        saveSlots.AddRange(GameObject.FindGameObjectsWithTag("Save"));
        saveSlots.AddRange(GameObject.FindGameObjectsWithTag("Load"));

        StartCoroutine(affichageSaveLoad());*/
    }

    // Start is called before the first frame update
    void Start()
    {
        inventaire_Script = FindObjectOfType<Inventaire_Script>();
        savedGame = FindObjectOfType<SavedGame>();

        /*saveSlots.Clear();

        saveSlots.AddRange(GameObject.FindGameObjectsWithTag("Save"));
        saveSlots.AddRange(GameObject.FindGameObjectsWithTag("Load"));

        StartCoroutine(affichageSaveLoad());*/
    }

    public IEnumerator affichageSaveLoad()
    {
        yield return new WaitForSeconds(1);
        GestionSlots();
    }

    public void ShowSavedFile(SavedGame savedGame)
    {
        if(File.Exists(Application.persistentDataPath + "/" + savedGame.MySaveName + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + savedGame.MySaveName + ".dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);

            file.Close();
            savedGame.ShowInfo(data);
        }
    }

    public void GestionSlots()
    {
        foreach (GameObject saveGame in saveSlots)
        {
            ShowSavedFile(saveGame.GetComponent<SavedGame>());
        }
    }

    public void Save(SavedGame savedGame)
    {
        try
        {
            if (savedGame.transform.GetChild(0).GetComponent<Text>().text == "Nouvelle partie")
            {
                GameManager.instance.choixNomSauvegarde();
            }
            else if (File.Exists(Application.persistentDataPath + "/" + savedGame.MySaveName + ".dat") && GameManager.instance.popUpActif == false) GameManager.instance.ecraserSauvegarde();
            else
            {
                BinaryFormatter bf = new BinaryFormatter();

                FileStream file = File.Open(Application.persistentDataPath + "/" + savedGame.MySaveName + ".dat", FileMode.Create);

                SaveData data = new SaveData();

                //SaveName(data);
                SavePlayer(data);
                SaveScene(data);

                bf.Serialize(file, data);

                file.Close();

                GestionSlots();
            }
        }
        catch (Exception)
        {

        }
    }

    private void SavePlayer(SaveData data)
    {
        data.MyPlayerData = new PlayerData(RessourcesVitalesWilliam_Scrip.instance.vieWilliam, 
            RessourcesVitalesWilliam_Scrip.instance.maxVie, 
            RessourcesVitalesWilliam_Scrip.instance.manaWilliam,
            RessourcesVitalesWilliam_Scrip.instance.maxMana,
            William_Script.instance.transform.position);
    }

    private void SaveScene(SaveData data)
    {
        data.MySceneData = new SceneData(SceneManager.GetActiveScene().buildIndex, SceneManager.GetActiveScene().name);
    }

    private void SaveName(SaveData data)
    {
        data.MyDataSave = new DataSave(GameManager.instance.popUp.GetComponentInChildren<InputField>().GetComponentInChildren<Text>().text);
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

    public void NewSave(SavedGame savedGame)
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Open(Application.persistentDataPath + "/" + savedGame.MySaveName + ".dat", FileMode.Create);

            SaveData data = new SaveData();

            NewSavePlayerData(data);

            bf.Serialize(file, data);

            file.Close();

            GameManager.instance.nomSauvegarde = savedGame.MySaveName;

            GestionSlots();

            LoadPlayer(data);

            if (data.MySceneData.IdScene != SceneManager.GetActiveScene().buildIndex) fondTransition.DOFade(1, 1.5f).OnComplete(()=>LoadScene(data));

            GameManager.instance.menuPause();
        }
        catch (Exception)
        {

        }
    }

    private void NewSavePlayerData(SaveData data)
    {
        data.MySceneData = new SceneData(2, "maxence_SceneTestPersonnage");
        data.MyPlayerData = new PlayerData(100, 100, 100, 100, new Vector3(0,5,0));
    }

    public void LoadInGame(string nomSave)
    {
        try
        {
            UnityEngine.Debug.Log(Application.persistentDataPath + "/" + nomSave + ".dat");
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Open(Application.persistentDataPath + "/" + nomSave + ".dat", FileMode.Open);

            SaveData data = (SaveData)bf.Deserialize(file);

            file.Close();

            LoadPlayer(data);

            if (data.MySceneData.IdScene != SceneManager.GetActiveScene().buildIndex) fondTransition.DOFade(1, 1.5f).OnComplete(() => LoadScene(data));
            else if (data.MySceneData.IdScene == SceneManager.GetActiveScene().buildIndex) HUD_Script.instance.setInfoWilliam();

            GameManager.instance.menuPause();
        }
        catch (Exception)
        {

        }
    }

    public void Load(SavedGame savedGame)
    {
        try
        {
            if (savedGame.transform.GetChild(0).GetComponent<Text>().text == "Nouvelle partie") GameManager.instance.choixNomSauvegarde();
            else
            {
                BinaryFormatter bf = new BinaryFormatter();

                FileStream file = File.Open(Application.persistentDataPath + "/" + savedGame.MySaveName + ".dat", FileMode.Open);

                SaveData data = (SaveData)bf.Deserialize(file);

                file.Close();

                GameManager.instance.nomSauvegarde = savedGame.MySaveName;

                LoadPlayer(data);

                if (data.MySceneData.IdScene != SceneManager.GetActiveScene().buildIndex) fondTransition.DOFade(1, 1.5f).OnComplete(() => LoadScene(data));

                GameManager.instance.menuPause();
            }
        }
        catch (Exception)
        {

        }
    }

    private void LoadPlayer(SaveData data)
    {
        InformationsPlayer.instance.williamVie = data.MyPlayerData.MyLife;
        InformationsPlayer.instance.maxWilliamVie = data.MyPlayerData.MyMaxLife;
        //RessourcesVitalesWilliam_Scrip.instance.SetVie(RessourcesVitalesWilliam_Scrip.instance.vieWilliam);

        InformationsPlayer.instance.williamMana = data.MyPlayerData.MyMana;
        InformationsPlayer.instance.maxWilliamMana = data.MyPlayerData.MyMaxMana;
        //RessourcesVitalesWilliam_Scrip.instance.SetMana(RessourcesVitalesWilliam_Scrip.instance.manaWilliam);

        InformationsPlayer.instance.williamPosition = new Vector3(data.MyPlayerData.MyX, data.MyPlayerData.MyY, data.MyPlayerData.MyZ);
    }
    
    private void LoadScene(SaveData data)
    {
        savedGame.idSceneActuelle = data.MySceneData.IdScene;
        savedGame.nomSceneActuelle = data.MySceneData.NameScene;

        SceneManager.LoadSceneAsync(savedGame.idSceneActuelle);
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
