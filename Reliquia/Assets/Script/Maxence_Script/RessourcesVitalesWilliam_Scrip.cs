using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RessourcesVitalesWilliam_Scrip : MonoBehaviour
{
    [SerializeField] private float valeurMana;
    [SerializeField] private float valeurVie;

    [SerializeField] private Text texteMana;
    [SerializeField] private Text texteVie;
    
    [SerializeField] private Image barreMana;
    [SerializeField] private Image barreVie;

    // Start is called before the first frame update
    void Start()
    {
        valeurMana = 100f;
        valeurVie = 100f;

        barreMana.fillAmount = valeurMana/100;
        barreVie.fillAmount = valeurVie/100;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A)) EnleverVie();
        if (Input.GetKeyUp(KeyCode.B)) EnleverMana();

        if (Input.GetKeyUp(KeyCode.E)) RajouterVie();
        if (Input.GetKeyUp(KeyCode.C)) RajouterMana();

        valeurVie = Mathf.Clamp(valeurVie, 0f, 100f);
        valeurMana = Mathf.Clamp(valeurMana, 0f, 100f);
    }

    public void EnleverVie()
    {
        if (valeurVie > 0)
        {
            valeurVie -= 10;
            texteVie.text = valeurVie.ToString("N0") + " %";
            barreVie.DOFillAmount(valeurVie / 100, 0.5f);
        }
    }

    public void EnleverMana()
    {
        if (valeurMana > 0)
        {
            valeurMana -= 10;
            texteMana.text = valeurMana.ToString("N0") + " %";
            barreMana.DOFillAmount(valeurMana / 100, 0.5f);
        }
    }

    public void RajouterVie()
    {
        if (valeurVie < 100)
        {
            valeurVie += 10;
            texteVie.text = valeurVie.ToString("N0") + " %";
            barreVie.DOFillAmount(valeurVie / 100, 0.5f);
        }
    }

    public void RajouterMana()
    {
        if (valeurMana < 100)
        {
            valeurMana += 10;
            texteMana.text = valeurMana.ToString("N0") + " %";
            barreMana.DOFillAmount(valeurMana / 100, 0.5f);
        }
    }
}
