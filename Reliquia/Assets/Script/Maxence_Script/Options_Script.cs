using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Options_Script : MonoBehaviour
{

    /// <summary>
    /// Gameobjet généraux
    /// </summary>
    [SerializeField] Image[] ImageBoutonOptions;

    [SerializeField] private Transform ContenuOptions;

    [SerializeField] private Text TitreMenuActif;

    /// <summary>
    /// Gameobjet menu réglages
    /// </summary>
    [SerializeField] private Text ValeurMusique;
    [SerializeField] private Text ValeurDialogues;

    [SerializeField] private Slider VolumeMusiques;
    [SerializeField] private Slider VolumeDialogues;

    [SerializeField] private Text ValeurSousTitres;
    [SerializeField] private Text ValeurAffichage;

    [SerializeField] private bool SousTitresActive;

    /// <summary>
    /// Gameobjet menu paramètres généraux
    /// </summary>
    [SerializeField] private Text ValeurSouris;
    [SerializeField] private Text ValeurResolution;
    [SerializeField] private Text ValeurQualiteEffets;

    [SerializeField] private Slider SensibiliteSouris;

    [SerializeField] private bool InversionSourisActive;

    // Start is called before the first frame update
    void Start()
    {
        SousTitresActive = true;
        InversionSourisActive = false;

        ValeurSousTitres.text = SousTitresActive ? "OUI" : "NON";
        ValeurSouris.text = !InversionSourisActive ? "NON" : "OUI";

        ValeurDialogues.text = (VolumeDialogues.value * 100).ToString("N0");
        ValeurMusique.text = (VolumeMusiques.value * 100).ToString("N0");

        //Set l'affichage au maximum
        //Screen.SetResolution(1920, 1080, true);
        ValeurAffichage.text = "FULL";

        //Set la qualité des effets
        QualitySettings.SetQualityLevel(3);
        ValeurQualiteEffets.text = "ULTRA";

        //Set la résolution
        ValeurResolution.text = "NORMALE";

        AfficherReglages();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AfficherReglages()
    {
        TitreMenuActif.text = "Réglages";
        ContenuOptions.DOLocalMoveX(0, 0.35f).SetEase(Ease.OutQuint);
        for(int i = 0; i < 3; i++)
        {
            ImageBoutonOptions[i].DOColor(new Color32(115, 115, 155, 255), 0.2f);
        }

        ImageBoutonOptions[0].DOColor(new Color32(247, 247, 247, 255), 0.2f);
    }

    public void AfficherParamGeneraux()
    {
        TitreMenuActif.text = "Paramètres Généraux";
        ContenuOptions.DOLocalMoveX(-2000, 0.35f).SetEase(Ease.OutQuint);
        for (int i = 0; i < 3; i++)
        {
            ImageBoutonOptions[i].DOColor(new Color32(115, 115, 155, 255), 0.2f);
        }

        ImageBoutonOptions[1].DOColor(new Color32(247, 247, 247, 255), 0.2f);
    }

    public void AfficherControles()
    {
        TitreMenuActif.text = "Contrôles";
        ContenuOptions.DOLocalMoveX(-4000, 0.35f).SetEase(Ease.OutQuint);
        for (int i = 0; i < 3; i++)
        {
            ImageBoutonOptions[i].DOColor(new Color32(115, 115, 155, 255), 0.2f);
        }

        ImageBoutonOptions[2].DOColor(new Color32(247, 247, 247, 255), 0.2f);
    }

    public void ChangerVolumeMusique()
    {
        ValeurMusique.text = (VolumeMusiques.value * 100).ToString("N0");
    }

    public void ChangerVolumeDialogues()
    {
        ValeurDialogues.text = (VolumeDialogues.value * 100).ToString("N0");
    }

    public void ChangerValeurSousTitre()
    {
        SousTitresActive = !SousTitresActive;
        ValeurSousTitres.text = SousTitresActive ? "OUI" : "NON";
    }

    public void ChangerValeurAffichages()
    {
        //Avoir le nom de la valeur actuelle de l'affichage
        string index = ValeurAffichage.text;

        switch (index) //Change l'affichage en fonction de la valeur actuelle
        {
            case "FULL":
                Screen.SetResolution(1920, 1080, false);
                ValeurAffichage.text = "FENÊTRÉ";
                break;

            case "FENÊTRÉ":
                Screen.SetResolution(1920, 1080, true);
                ValeurAffichage.text = "FULL";
                break;
        }
    }

    public void ChangerValeurSouris()
    {
        InversionSourisActive = !InversionSourisActive;
        ValeurSouris.text = !InversionSourisActive ? "NON" : "OUI";
    }

    public void ChangerResolution()
    {
        //Avoir le nom de la valeur actuelle de la résolution 
        string index = ValeurResolution.text;

        switch (index) //Change la résolution en fonction de la valeur actuelle
        {
            case "FAIBLE":
                //QualitySettings.SetQualityLevel(1);
                ValeurResolution.text = "NORMALE";
                break;

            case "NORMALE":
                //QualitySettings.SetQualityLevel(2);
                ValeurResolution.text = "ÉLEVÉE";
                break;

            case "ÉLEVÉE":
                //QualitySettings.SetQualityLevel(3);
                ValeurResolution.text = "FAIBLE";
                break;
        }
    }

    public void ChangerQualiteEffets()
    {
        //Avoir le nom de la valeur actuelle de la qualité 
        string index = ValeurQualiteEffets.text;

        switch (index) //Change la qualité en fonction de la valeur actuelle
        {
            case "FAIBLE":
                QualitySettings.SetQualityLevel(1);
                ValeurQualiteEffets.text = "NORMALE";
                break;

            case "NORMALE":
                QualitySettings.SetQualityLevel(2);
                ValeurQualiteEffets.text = "ÉLEVÉE";
                break;

            case "ÉLEVÉE":
                QualitySettings.SetQualityLevel(3);
                ValeurQualiteEffets.text = "ULTRA";
                break;

            case "ULTRA":
                QualitySettings.SetQualityLevel(0);
                ValeurQualiteEffets.text = "FAIBLE";
                break;
        }
    }
}
