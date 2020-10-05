using DG.Tweening;
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

    [SerializeField] private Button boutonMenuSave;
    [SerializeField] private Button boutonMenuLoad;

    public GameObject[] SloatsLoadSave;

    public Image imageTransition;

    // Start is called before the first frame update
    void Awake()
    {
        imageTransition.DOFade(0, 1.5f);
    }

    void Start()
    {
        ManageGame();

        boutonMenuSave.onClick.AddListener(GameManager.instance.menuSauvegarde);
        boutonMenuLoad.onClick.AddListener(GameManager.instance.menuCharger);

        SaveManager.instance.saveSlots.Clear();

        foreach (GameObject slots in SloatsLoadSave)
        {
            SaveManager.instance.saveSlots.Add(slots);
        }

        StartCoroutine(SaveManager.instance.affichageSaveLoad());

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

        GameManager.instance.voirMenu = false;
        GameManager.instance.menuSlots = false;
        GameManager.instance.menuPauseOuvert = false;
        GameManager.instance.menuInventaireOuvert = false;
        GameManager.instance.MenuPause.SetActive(GameManager.instance.voirMenu);
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
}
