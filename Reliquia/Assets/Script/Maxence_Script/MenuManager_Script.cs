using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MenuManager_Script : MonoBehaviour
{
    [SerializeField] private Image fondTransition;

    public GameObject[] pagesMenuPrincipal;
    public Transform[] boutonsMenuPrincipal;

    public Image backgroundImageMenu;
    public Sprite[] ImagesBackground;

    Menu_Script menu_Script;
    public static MenuManager_Script instance;

    [SerializeField] private GameObject popUpQuitter;
    [SerializeField] private GameObject prefabMenuOptions;

    private int pageMenuActive;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        fondTransition.DOFade(0, 1f);

        Instantiate(prefabMenuOptions, GameObject.FindGameObjectWithTag("Options").transform);

        pagesMenuPrincipal[2].transform.GetChild(1).GetComponent<Options_Script>().valeurAuLancement();

        pagesMenuPrincipal[2].SetActive(false);

        backgroundImageMenu.sprite = ImagesBackground[0];
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

    public void retourMenu()
    {
        if (pageMenuActive != 2) fondTransition.DOFade(1, 0.5f).OnComplete(() => afficherMenuPrincipal());
        else afficherMenuPrincipal();
    }

    public void quitterJeu()
    {
        Instantiate(popUpQuitter, new Vector2(1520, 0), Quaternion.identity, GameObject.Find("Canvas").transform);
    }

    public void afficherEcranSauvegarde()
    {
        MenuManager_Script.instance.pagesMenuPrincipal[pageMenuActive].SetActive(false);
        MenuManager_Script.instance.pagesMenuPrincipal[1].SetActive(true);
        fondTransition.DOFade(0, 0.5f);
        pageMenuActive = 1;
    }

    public void afficherMenuPrincipal()
    {
        MenuManager_Script.instance.pagesMenuPrincipal[pageMenuActive].SetActive(false);
        MenuManager_Script.instance.pagesMenuPrincipal[0].SetActive(true);
        if (pageMenuActive != 2) fondTransition.DOFade(0, 0.5f);
        else
        {
            for (int i = 0; i < 4; i++)
            {
                MenuManager_Script.instance.boutonsMenuPrincipal[i].DOLocalMoveX(-822f, 0.5f);
            }
            MenuManager_Script.instance.backgroundImageMenu.sprite = MenuManager_Script.instance.ImagesBackground[0];
        }

        pageMenuActive = 0;
    }

    public void afficherEcranOption()
    {
        for (int i = 0; i < 4; i++)
        {
            MenuManager_Script.instance.boutonsMenuPrincipal[i].DOLocalMoveX(-1260f, 0.5f);
        }

        MenuManager_Script.instance.pagesMenuPrincipal[2].SetActive(true);
        MenuManager_Script.instance.backgroundImageMenu.sprite = MenuManager_Script.instance.ImagesBackground[1];

        pageMenuActive = 2;
    }
}
