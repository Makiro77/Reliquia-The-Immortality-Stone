using clavier;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{

    RaccourciClavier_Script raccourciClavier;

    [SerializeField] private GameObject MenuPause;
    [SerializeField] private Transform MenuInventaire;
    public bool voirMenu;

    bool menuPauseOuvert;
    bool menuInventaireOuvert;
    bool menuSlots;

    [SerializeField] private GameObject MessageInteraction;
    [SerializeField] private Text TexteMessageInteraction;

    [SerializeField] private Transform ParentBarresVieMana;
    [SerializeField] private Transform ParentCompas;

    [SerializeField] private Transform ParentSlotSave;
    [SerializeField] private Transform ParentSlotLoad;
    [SerializeField] private Transform ParentBoutonPause;

    // Start is called before the first frame update
    void Start()
    {
        voirMenu = false;
        menuSlots = false;
        menuPauseOuvert = false;
        menuInventaireOuvert = false;
        MenuPause.SetActive(voirMenu);
        FermerMessageInteraction();
        raccourciClavier = FindObjectOfType<RaccourciClavier_Script>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(raccourciClavier.toucheClavier["MenuPause"]) && menuInventaireOuvert == false)
        {
            menuPause();
        }

        if (Input.GetKeyUp(raccourciClavier.toucheClavier["MenuInventaire"]) && menuPauseOuvert == false)
        {
            menuInventaire();
        }
    }

    public void menuSauvegarde()
    {
        menuSlots = !menuSlots;
        ParentSlotSave.DOLocalMoveX((menuSlots == true ? 0f : 2000f), 0.25f);
        ParentBoutonPause.DOLocalMoveX((menuSlots == true ? -2000f : 0f), 0.25f);
    }

    public void menuCharger()
    {
        menuSlots = !menuSlots;
        ParentSlotLoad.DOLocalMoveX((menuSlots == true ? 0f : 2000f), 0.25f);
        ParentBoutonPause.DOLocalMoveX((menuSlots == true ? -2000f : 0f), 0.25f);
    }

    public void menuPause()
    {
        voirMenu = !voirMenu;
        DeplacerUIMenu();
        menuPauseOuvert = !menuPauseOuvert;
        ParentBoutonPause.localPosition = new Vector3(0, -225f, 0);
        ParentSlotLoad.localPosition = new Vector3(2000f, 0, 0);
        ParentSlotSave.localPosition = new Vector3(2000f, 0, 0);
        MenuPause.SetActive(voirMenu);
    }

    public void menuInventaire()
    {
        voirMenu = !voirMenu;
        DeplacerUIMenu();
        menuInventaireOuvert = !menuInventaireOuvert;
        MenuInventaire.localPosition = new Vector3((voirMenu == true ? 0 : -2000f), 0, 0);
    }

    public void DeplacerUIMenu()
    {
        ParentBarresVieMana.DOMoveX((voirMenu == true ? - 632f : 0f), 0.25f);
    }

    public void AfficherMessageInteraction(string text)
    {
        MessageInteraction.SetActive(true);
        TexteMessageInteraction.text = "Appuyer sur " + raccourciClavier.toucheClavier["Action"].ToString() + " pour intéragir";
    }

    public void FermerMessageInteraction()
    {
        MessageInteraction.SetActive(false);
    }
}
