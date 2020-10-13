using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventaireSlot : MonoBehaviour
{
    [Header("Affichage infos Item")]
    [SerializeField] private Text itemNumberText;
    [SerializeField] private Image itemImage;

    [Header("Variable infos Item")]
    /*public Sprite itemSprite;
    public int numberHeld;
    public string itemDescription;*/
    public ItemInventaire thisItem;
    public InventaireManager thisManager;

    public string TypeItemBase;
    public string TypeItem;

    public void Setup(ItemInventaire newItem, InventaireManager newManager)
    {
        thisItem = newItem;
        thisManager = newManager;
        if (thisItem)
        {
            itemImage.sprite = thisItem.itemImage;
            itemNumberText.text = thisItem.numberHeld.ToString();
            TypeItemBase = thisItem.typeItem;
            TypeItem = thisItem.typeItem;
        }
    }

    public void ClickedOn() //Quand on clique sur le slot d'inventaire
    {
        if (thisItem)
        {
            if (thisItem.usable)
            {
                thisManager.SetupDescriptionAndButton(thisItem.itemDescription, thisItem.usable, thisItem);
                thisItem.Use();
                thisManager.ClearConsommableSlots();
                thisManager.MakeConsommableSlot();
                thisManager.SetTextAndButton("", false);

                if (thisItem.numberHeld <= 0)
                {
                    thisManager.playerInventory.consommablesInventory.Remove(thisItem);
                }
            }
        }
    }
}
