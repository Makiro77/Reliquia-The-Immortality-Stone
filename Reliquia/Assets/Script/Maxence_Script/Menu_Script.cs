using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class Menu_Script : MonoBehaviour
{
    private Animator logoApparition;
    private Image fondTransition;
    private Image backgroundImageMenu;

    [SerializeField] private GameObject[] PagesMenuPrincipal;
    [SerializeField] private Transform[] BoutonsMenuPrincipal;
    [SerializeField] private Sprite[] ImagesBackground;

    private int pageMenuActive;

    // Start is called before the first frame update
    void Awake()
    {
        Scene scene = SceneManager.GetActiveScene();

        Debug.Log(scene.name);

        fondTransition = GameObject.Find("Canvas/FondNoir").GetComponent<Image>();
        if (scene.name == "Menu_01") 
{
            backgroundImageMenu = GameObject.Find("Canvas/ZoneMenuPrincipal/Background").GetComponent<Image>();
            backgroundImageMenu.sprite = ImagesBackground[0];
        }
        if (scene.name == "Menu_00") logoApparition = GameObject.Find("Canvas/ZoneBandeau/Bandeau/ParentLogo").GetComponent<Animator>();

        fondTransition.DOFade(0, 0.5f).OnComplete(() => logoApparition.Play("ApparitionLogo"));//Lance l'animation d'apparition du logo quand le fade est fini
    }

    public void menuPrincipale()
    {
        fondTransition.DOFade(1, 0.5f).OnComplete(() => SceneManager.LoadScene("Menu_01")); //Lance la scène Menu_01 lorsque le fondu au noir est fini
    }

    public void ecranSauvegarde()
    {
        fondTransition.DOFade(1, 0.5f).OnComplete(() => afficherEcranSauvegarde());
    }                                 
                                      
    public void ecranOption()         
    {
        afficherEcranOption();
    }                                 
                                      
    public void ecranBonus()          
    {                                 
        fondTransition.DOFade(1, 0.5f);
        pageMenuActive = 3;
    }

    public void quitterJeu()
    {
        Application.Quit(); //Quitte le jeu
    }

    public void retourMenu()
    {
        if (pageMenuActive != 2) fondTransition.DOFade(1, 0.5f).OnComplete(() => afficherMenuPrincipal());
        else afficherMenuPrincipal();
    }

    public void afficherEcranSauvegarde()
    {
        PagesMenuPrincipal[pageMenuActive].SetActive(false);
        PagesMenuPrincipal[1].SetActive(true);
        fondTransition.DOFade(0, 0.5f);
        pageMenuActive = 1;
    }

    public void afficherMenuPrincipal()
    {
        PagesMenuPrincipal[pageMenuActive].SetActive(false);
        PagesMenuPrincipal[0].SetActive(true);
        if(pageMenuActive != 2) fondTransition.DOFade(0, 0.5f);
        else {
            for (int i = 0; i < 4; i++)
            {
                BoutonsMenuPrincipal[i].DOMoveX(138f, 0.5f);
            }
            backgroundImageMenu.sprite = ImagesBackground[0];
        }

        pageMenuActive = 0;
    }

    public void afficherEcranOption()
    {
        for (int i = 0; i < 4; i++)
        {
            BoutonsMenuPrincipal[i].DOMoveX(-300f, 0.5f);
        }

        PagesMenuPrincipal[2].SetActive(true);
        backgroundImageMenu.sprite = ImagesBackground[1];

        pageMenuActive = 2;
    }
}
