using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RessourcesVitalesWilliam_Scrip : MonoBehaviour
{
    [SerializeField] private int manaWilliam;
    [SerializeField] private int vieWilliam;

    [SerializeField] private int valeurMana;
    [SerializeField] private int valeurVie;

    [SerializeField] private float pourcentageMana;
    [SerializeField] private float pourcentageVie;

    [SerializeField] private int maxVie = 120;
    [SerializeField] private int minVie = 0;

    [SerializeField] private int maxMana = 120;
    [SerializeField] private int minMana = 0;

    [SerializeField] private Text texteMana;
    [SerializeField] private Text texteVie;
    
    [SerializeField] private Image barreMana;
    [SerializeField] private Image barreVie;

    // Start is called before the first frame update
    void Start()
    {
        vieWilliam = maxVie;
        manaWilliam = maxMana;

        SetMana(manaWilliam);
        SetVie(vieWilliam);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A)) EnleverVie();
        if (Input.GetKeyUp(KeyCode.B)) EnleverMana();

        if (Input.GetKeyUp(KeyCode.E)) RajouterVie();
        if (Input.GetKeyUp(KeyCode.C)) RajouterMana();
    }

    public void EnleverVie()
    {
        if (valeurVie > minVie)
        {
            vieWilliam -= 10;
            SetVie(vieWilliam);
        }
    }

    public void EnleverMana()
    {
        if (valeurMana > minMana)
        {
            manaWilliam -= 10;
            SetMana(manaWilliam);
        }
    }

    public void RajouterVie()
    {
        if (valeurVie < maxVie)
        {
            vieWilliam += 10;
            SetVie(vieWilliam);
        }
    }

    public void RajouterMana()
    {
        if (valeurMana < maxMana)
        {
            manaWilliam += 10;
            SetMana(manaWilliam);
        }
    }

    public void SetVie(int Vie)
    {
        if(Vie != valeurVie)
        {
            if (maxVie - minVie == 0)
            {
                valeurVie = 0;
                pourcentageVie = 0;
            }
            else
            {
                valeurVie = Vie;

                pourcentageVie = (float)valeurVie / (float)(maxVie - minVie);
            }

            texteVie.text = string.Format("{0} %", Mathf.RoundToInt(pourcentageVie * 100));
            barreVie.DOFillAmount(pourcentageVie, 0.5f);
        }
    }

    public float pourcentageVieActuel
    {
        get { return pourcentageVie; }
    }

    public int valeurVieActuel
    {
        get { return valeurVie; }
    }

    public void SetMana(int Mana)
    {
        if (Mana != valeurMana)
        {
            if (maxMana - minMana == 0)
            {
                valeurMana = 0;
                pourcentageMana = 0;
            }
            else
            {
                valeurMana = Mana;

                pourcentageMana = (float)valeurMana / (float)(maxMana - minMana);
            }

            texteMana.text = string.Format("{0} %", Mathf.RoundToInt(pourcentageMana * 100));
            barreMana.DOFillAmount(pourcentageMana, 0.5f);
        }
    }

    public float pourcentageManaActuel
    {
        get { return pourcentageMana; }
    }

    public int valeurManaActuel
    {
        get { return valeurMana; }
    }
}
