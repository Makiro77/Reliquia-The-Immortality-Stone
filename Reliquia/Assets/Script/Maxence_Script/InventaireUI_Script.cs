using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventaireUI_Script : MonoBehaviour
{
    public Inventaire_Script inventaire;

    // Start is called before the first frame update
    void Start()
    {
        inventaire.ItemAdded += InventaireScript_ItemAdded;
    }

    private void InventaireScript_ItemAdded(object sender, InventaireEventArgs e)
    {
        Transform inventairePanel = gameObject.transform;
        foreach(Transform slot in inventairePanel)
        {
            Image image = slot.GetChild(0).GetChild(0).GetComponent<Image>();

            if (!image.enabled)
            {
                Debug.Log("enable");
                image.enabled = true;
                image.sprite = e.Item.Image;

                break;
            }
        }
    }
}
