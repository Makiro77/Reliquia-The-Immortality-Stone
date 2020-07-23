using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoItem_Script : MonoBehaviour, IInventaireItem
{
    public string _NomItem = null;

    public string NomItem
    {
        get
        {
            return _NomItem;
        }
    }

    public Sprite _Image = null;

    public Sprite Image
    {
        get
        {
            return _Image;
        }
    }

    public void OnPickup()
    {
        gameObject.SetActive(false);
    }
}
