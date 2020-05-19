using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Video;

public class Menu_Script : MonoBehaviour
{
    private Image fondTransition;
    private Image backgroundImageMenu;

    private VideoPlayer IntroCinematique;

    [SerializeField] private GameObject[] PagesMenuPrincipal;
    [SerializeField] private Transform[] BoutonsMenuPrincipal;
    [SerializeField] private Sprite[] ImagesBackground;

    private int pageMenuActive;

    [SerializeField] private GameObject popUpQuitter;

    // Start is called before the first frame update
    void Awake()
    {
        Scene scene = SceneManager.GetActiveScene();

        fondTransition = GameObject.Find("Canvas/FondNoir").GetComponent<Image>();
        if (scene.name == "Menu_01") 
{
            backgroundImageMenu = GameObject.Find("Canvas/ZoneMenuPrincipal/Background").GetComponent<Image>();
            backgroundImageMenu.sprite = ImagesBackground[0];

            fondTransition.DOFade(0, 0.5f);
        }
        else if(scene.name == "Menu_00")
        {
            IntroCinematique = GameObject.Find("Canvas/CinematiqueIntro").GetComponent<VideoPlayer>();

            StartCoroutine(JouerCinematiqueIntro());
        }
    }

    IEnumerator JouerCinematiqueIntro()
    {
        IntroCinematique.Play();
        yield return new WaitForSeconds(7);

        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.05f);
            IntroCinematique.targetCameraAlpha -= 0.1f;

            if (IntroCinematique.targetCameraAlpha <= 0)
            {
                fondTransition.DOFade(0, 0.5f).SetDelay(0.5f);
            }
        }
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
        Instantiate(popUpQuitter, new Vector2(1520, 0), Quaternion.identity, GameObject.Find("Canvas").transform);
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
                BoutonsMenuPrincipal[i].DOLocalMoveX(-822f, 0.5f);
            }
            backgroundImageMenu.sprite = ImagesBackground[0];
        }

        pageMenuActive = 0;
    }

    public void afficherEcranOption()
    {
        for (int i = 0; i < 4; i++)
        {
            BoutonsMenuPrincipal[i].DOLocalMoveX(-1260f, 0.5f);
        }

        PagesMenuPrincipal[2].SetActive(true);
        backgroundImageMenu.sprite = ImagesBackground[1];

        pageMenuActive = 2;
    }
}
