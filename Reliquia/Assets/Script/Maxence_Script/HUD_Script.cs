using clavier;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class HUD_Script : MonoBehaviour
{
    public GameObject MenuPause;
    public Transform MenuInventaire;

    public GameObject MessageInteraction;
    public Text TexteMessageInteraction;

    public Transform ParentBarresVieMana;
    public GameObject ParentCompas;

    public Transform ParentSlotSave;
    public Transform ParentSlotLoad;
    public Transform ParentBoutonMenu;
    public Transform ParentOptions;

    //[SerializeField] private Button boutonMenuSave;
    //[SerializeField] private Button boutonMenuLoad;
    [SerializeField] private Button boutonMenuOptions;
    [SerializeField] private Button boutonRetourOption;

    public GameObject[] SloatsLoadSave;
    public GameObject prefabMenuOptions;

    [SerializeField] private GameObject prefab;

    public Image imageTransition;
    public GameObject fondPause;

    [SerializeField] private CanvasGroup GrimoireInventaire;

    public static HUD_Script instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        prefab = Instantiate(prefabMenuOptions, GameObject.FindGameObjectWithTag("Options").transform);
        boutonRetourOption =  prefab.transform.GetChild(1).GetComponent<Button>();
        imageTransition.DOFade(0, 1.5f);
        fondPause.SetActive(false);

        ManageGame();

        //boutonMenuSave.onClick.AddListener(GameManager.instance.menuSauvegarde);
        boutonMenuOptions.onClick.AddListener(GameManager.instance.menuOptions);
        boutonRetourOption.onClick.AddListener(GameManager.instance.menuOptions);
    }

    void Start()
    {

        SaveManager.instance.saveSlots.Clear();

        foreach (GameObject slots in SloatsLoadSave)
        {
            SaveManager.instance.saveSlots.Add(slots);
        }

        StartCoroutine(SaveManager.instance.affichageSaveLoad());

        setInfoWilliam();
    }

    public void setInfoWilliam()
    {
        RessourcesVitalesWilliam_Scrip.instance.manaWilliam = InformationsPlayer.instance.williamMana;
        RessourcesVitalesWilliam_Scrip.instance.vieWilliam = InformationsPlayer.instance.williamVie;
        RessourcesVitalesWilliam_Scrip.instance.maxMana = InformationsPlayer.instance.maxWilliamMana;
        RessourcesVitalesWilliam_Scrip.instance.maxVie = InformationsPlayer.instance.maxWilliamVie;

        RessourcesVitalesWilliam_Scrip.instance.SetVie(InformationsPlayer.instance.williamVie);
        RessourcesVitalesWilliam_Scrip.instance.SetMana(InformationsPlayer.instance.williamMana);

        William_Script.instance.transform.position = InformationsPlayer.instance.williamPosition;
    }

    public void ManageGame()
    {
        GameManager.instance.MenuPause = MenuPause;
        GameManager.instance.MenuInventaire = MenuInventaire;

        GameManager.instance.MessageInteraction = MessageInteraction;
        GameManager.instance.TexteMessageInteraction = TexteMessageInteraction;

        GameManager.instance.ParentBarresVieMana = ParentBarresVieMana;
        GameManager.instance.ParentCompas = ParentCompas;

        GameManager.instance.ParentSlotSave = ParentSlotSave;
        GameManager.instance.ParentSlotLoad = ParentSlotLoad;
        GameManager.instance.ParentBoutonMenu = ParentBoutonMenu;
        GameManager.instance.ParentOptions = ParentOptions;

        GameManager.instance.GrimoireInventaire = GrimoireInventaire;

        GameManager.instance.voirMenu = false;
        GameManager.instance.menuSlots = false;
        GameManager.instance.menuPauseOuvert = false;
        GameManager.instance.menuInventaireOuvert = false;
        GrimoireInventaire.alpha = Convert.ToInt32(GameManager.instance.voirMenu);
        GameManager.instance.ParentBoutonMenu.DOMoveX(-780f, 0.01f);
        GameManager.instance.FermerMessageInteraction();
    }

    public void ContinuerJeu()
    {
        GameManager.instance.menuPause();
    }

    public void QuitterPartie()
    {
        GameManager.instance.retourMenu();
    }

    public void LoadCheckpoint()
    {
        string nom = GameManager.instance.nomSauvegarde;
        SaveManager.instance.LoadInGame(nom);
    }
}
