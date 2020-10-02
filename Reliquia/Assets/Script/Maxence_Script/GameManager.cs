﻿using clavier;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEditor;

public class GameManager : MonoBehaviour
{

    RaccourciClavier_Script raccourciClavier;

    public GameObject MenuPause;
    public Transform MenuInventaire;
    public bool voirMenu;

    public bool menuPauseOuvert;
    public bool menuInventaireOuvert;
    public bool menuSlots;

    public GameObject MessageInteraction;
    public Text TexteMessageInteraction;

    public Transform ParentBarresVieMana;
    public GameObject ParentCompas;

    public Transform ParentSlotSave;
    public Transform ParentSlotLoad;
    public Transform ParentBoutonMenu;

    public static GameManager instance;

    public GameObject popUpNomSauvegarde;
    public GameObject popUpEcraserSauvegarde;

    public GameObject popUp;
    public GameObject SlotSaveSelect;

    public bool popUpActif;

    public int chapitreEnCours;

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
    }

    // Start is called before the first frame update
    void Start()
    {
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

        if ((EventSystem.current.currentSelectedGameObject.CompareTag("Load") || EventSystem.current.currentSelectedGameObject.CompareTag("Save")) && menuInventaireOuvert == false) SlotSaveSelect = EventSystem.current.currentSelectedGameObject;
        else return;
    }

    public void choixNomSauvegarde()
    {
        popUp = Instantiate(popUpNomSauvegarde, GameObject.FindGameObjectWithTag("HUD").transform);
        popUpActif = true;
    }

    public void ecraserSauvegarde()
    {
        popUp = Instantiate(popUpEcraserSauvegarde, GameObject.FindGameObjectWithTag("HUD").transform);
        popUpActif = true;
    }

    public void menuSauvegarde()
    {
        menuSlots = !menuSlots;
        ParentSlotSave.DOLocalMoveX((menuSlots == true ? 0f : 2000f), 0.25f);
        ParentBoutonMenu.DOMoveX((menuSlots == true ? -780f : 0f), 0.25f);
    }

    public void menuCharger()
    {
        menuSlots = !menuSlots;
        ParentSlotLoad.DOLocalMoveX((menuSlots == true ? 0f : 2000f), 0.25f);
        ParentBoutonMenu.DOMoveX((menuSlots == true ? -780f : 0f), 0.25f);
    }

    public void menuPause()
    {
        voirMenu = !voirMenu;
        if(voirMenu == true) MenuPause.SetActive(voirMenu);
        DeplacerUIMenu();
        menuPauseOuvert = !menuPauseOuvert;
        //ParentBoutonPause.localPosition = new Vector3(53, -170, 0);
        ParentSlotLoad.localPosition = new Vector3(2000f, 0, 0);
        ParentSlotSave.localPosition = new Vector3(2000f, 0, 0);
        ParentBoutonMenu.DOMoveX((voirMenu == true ? 0f : -780f), 0.25f).OnComplete(() => MenuPause.SetActive(voirMenu));

        //MenuPause.SetActive(voirMenu);
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
        ParentCompas.SetActive(!voirMenu);
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
