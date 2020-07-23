using clavier;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    RaccourciClavier_Script raccourciClavier;

    [SerializeField] private GameObject MenuPause;
    [SerializeField] private Transform MenuInventaire;
    public bool voirMenu;

    [SerializeField] private GameObject MessageInteraction;
    [SerializeField] private Text TexteMessageInteraction;

    // Start is called before the first frame update
    void Start()
    {
        voirMenu = false;
        MenuPause.SetActive(voirMenu);
        //TexteMessageInteraction.text = "Appuyer sur " + raccourciClavier.action + " pour intéragir";
        FermerMessageInteraction();
        raccourciClavier = FindObjectOfType<RaccourciClavier_Script>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(raccourciClavier.toucheClavier["MenuPause"]))
        {
            voirMenu = !voirMenu;
            MenuPause.SetActive(voirMenu);
        }

        if (Input.GetKeyUp(raccourciClavier.toucheClavier["MenuInventaire"]))
        {
            voirMenu = !voirMenu;
            if(voirMenu == true) MenuInventaire.localPosition = Vector3.zero;
            else MenuInventaire.localPosition = new Vector3 (-2000f, 0 ,0);
        }
    }

    public void AfficherMessageInteraction(string text)
    {
        MessageInteraction.SetActive(true);
    }

    public void FermerMessageInteraction()
    {
        MessageInteraction.SetActive(false);
    }
}
