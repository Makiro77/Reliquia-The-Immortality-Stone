using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaccourciClavier_Script : MonoBehaviour
{

    private Dictionary<string, KeyCode> toucheClavier = new Dictionary<string, KeyCode>();

    public Text avancer, gauche, reculer, droite, action, saut, pouvoirSpecial, pouvoir1, pouvoir2, pouvoir3, pouvoir4, courir, attaque, garde;
    [SerializeField] private Text[] texteAssignationTouche;

    int Alpha;

    private GameObject toucheSelectionne;

    private Color32 normal = new Color32(115, 115, 115, 255);
    private Color32 selection = new Color32(247, 247, 247, 255);

    // Start is called before the first frame update
    void Start()
    {
        AssignationTouche();
    }

    public void AssignationTouche()
    {
        toucheClavier.Add("Avancer", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Avancer", "Z")));
        toucheClavier.Add("Gauche", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Gauche", "Q")));
        toucheClavier.Add("Reculer", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Reculer", "S")));
        toucheClavier.Add("Droite", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Droite", "D")));

        toucheClavier.Add("Action", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Action", "E")));
        toucheClavier.Add("Saut", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Saut", "Space")));

        toucheClavier.Add("PouvoirSpecial", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("PouvoirSpecial", "Tab")));
        toucheClavier.Add("Pouvoir1", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Pouvoir1", "Alpha1")));
        toucheClavier.Add("Pouvoir2", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Pouvoir2", "Alpha2")));
        toucheClavier.Add("Pouvoir3", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Pouvoir3", "Alpha3")));
        toucheClavier.Add("Pouvoir4", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Pouvoir4", "Alpha4")));

        toucheClavier.Add("Courir", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Pouvoir4", "LeftShift")));
        toucheClavier.Add("Attaque", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Attaque", "Mouse0")));
        toucheClavier.Add("Garde", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Garde", "Mouse1")));

        avancer.text = toucheClavier["Avancer"].ToString();
        gauche.text = toucheClavier["Gauche"].ToString();
        reculer.text = toucheClavier["Reculer"].ToString();
        droite.text = toucheClavier["Droite"].ToString();

        action.text = toucheClavier["Action"].ToString();
        saut.text = toucheClavier["Saut"].ToString();

        pouvoirSpecial.text = toucheClavier["PouvoirSpecial"].ToString();
        pouvoir1.text = toucheClavier["Pouvoir1"].ToString();
        pouvoir2.text = toucheClavier["Pouvoir2"].ToString();
        pouvoir3.text = toucheClavier["Pouvoir3"].ToString();
        pouvoir4.text = toucheClavier["Pouvoir4"].ToString();

        courir.text = toucheClavier["Courir"].ToString();
        attaque.text = toucheClavier["Attaque"].ToString();
        garde.text = toucheClavier["Garde"].ToString();

        for (int i = 0; i < 14; i++)
        {
            Alpha = i;

            switch (texteAssignationTouche[i].text)
            {
                case "Alpha0":
                    texteAssignationTouche[i].text = "0";
                    break;

                case "Alpha1":
                    texteAssignationTouche[i].text = "1";
                    break;

                case "Alpha2":
                    texteAssignationTouche[i].text = "2";
                    break;

                case "Alpha3":
                    texteAssignationTouche[i].text = "3";
                    break;

                case "Alpha4":
                    texteAssignationTouche[i].text = "4";
                    break;

                case "Alpha5":
                    texteAssignationTouche[i].text = "5";
                    break;

                case "Alpha6":
                    texteAssignationTouche[i].text = "6";
                    break;

                case "Alpha7":
                    texteAssignationTouche[i].text = "7";
                    break;

                case "Alpha8":
                    texteAssignationTouche[i].text = "8";
                    break;

                case "Alpha9":
                    texteAssignationTouche[i].text = "9";
                    break;

                case "Mouse0":
                    texteAssignationTouche[i].text = "CLIC-G";
                    break;

                case "Mouse1":
                    texteAssignationTouche[i].text = "CLIC-D";
                    break;

                case "LeftShift":
                    texteAssignationTouche[i].text = "MAJ-G";
                    break;
            }
        }
    }

    void OnGUI()
    {
        if(toucheSelectionne != null)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                toucheClavier[toucheSelectionne.name] = e.keyCode;
                toucheSelectionne.GetComponent<Text>().text = e.keyCode.ToString();
                toucheSelectionne.GetComponent<Text>().color = normal;

                switch (e.keyCode)
                {
                    case KeyCode.Alpha0:
                        toucheSelectionne.GetComponent<Text>().text = "0";
                        break;

                    case KeyCode.Alpha1:
                        toucheSelectionne.GetComponent<Text>().text = "1";
                        break;

                    case KeyCode.Alpha2:
                        toucheSelectionne.GetComponent<Text>().text = "2";
                        break;

                    case KeyCode.Alpha3:
                        toucheSelectionne.GetComponent<Text>().text = "3";
                        break;

                    case KeyCode.Alpha4:
                        toucheSelectionne.GetComponent<Text>().text = "4";
                        break;

                    case KeyCode.Alpha5:
                        toucheSelectionne.GetComponent<Text>().text = "5";
                        break;

                    case KeyCode.Alpha6:
                        toucheSelectionne.GetComponent<Text>().text = "6";
                        break;

                    case KeyCode.Alpha7:
                        toucheSelectionne.GetComponent<Text>().text = "7";
                        break;

                    case KeyCode.Alpha8:
                        toucheSelectionne.GetComponent<Text>().text = "8";
                        break;

                    case KeyCode.Alpha9:
                        toucheSelectionne.GetComponent<Text>().text = "9";
                        break;

                    case KeyCode.Mouse0:
                        toucheSelectionne.GetComponent<Text>().text = "CLIC-G";
                        break;

                    case KeyCode.Mouse1:
                        toucheSelectionne.GetComponent<Text>().text = "CLIC-D";
                        break;

                    case KeyCode.LeftShift:
                        toucheSelectionne.GetComponent<Text>().text = "MAJ-G";
                        break;
                }

                SauvegardeToucheClavier();
                toucheSelectionne = null;
            }
        }
    }

    public void ChangerTouche(GameObject clique)
    {
        if(toucheSelectionne != null)
        {
            toucheSelectionne.GetComponent<Text>().color = normal;
        }

        toucheSelectionne = clique;
        toucheSelectionne.GetComponent<Text>().color = selection;
    }

    public void SauvegardeToucheClavier()
    {
        foreach (var touche in toucheClavier)
        {
            PlayerPrefs.SetString(touche.Key, touche.Value.ToString());
        }

        PlayerPrefs.Save();
    }

    private void Update()
    {
        
    }
}
