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

    [SerializeField] private GameObject MessageInteraction;
    [SerializeField] private Text TexteMessageInteraction;

    [SerializeField] private Transform ParentBarresVieMana;
    [SerializeField] private Transform ParentCompas;

    // Start is called before the first frame update
    void Start()
    {
        voirMenu = false;
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

    public void menuPause()
    {
        voirMenu = !voirMenu;
        DeplacerUIMenu();
        menuPauseOuvert = !menuPauseOuvert;
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
        TexteMessageInteraction.text = "Appuyer sur " + raccourciClavier.action + " pour intéragir";
    }

    public void FermerMessageInteraction()
    {
        MessageInteraction.SetActive(false);
    }
}
