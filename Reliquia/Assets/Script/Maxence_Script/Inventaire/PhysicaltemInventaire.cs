using UnityEngine;

public class PhysicaltemInventaire : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private ItemInventaire thisItem;
    [SerializeField] private InventaireManager thisManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !other.isTrigger)
        {
            //GameManager.instance.AfficherMessageInteraction("");
            if(gameObject.CompareTag("Quetes") && thisManager.maxItemQuete < 6) AddItemToQuetes();
            else if(gameObject.CompareTag("Consommable") && thisManager.maxItemConsommable < 12) AddItemToConsommable();
            else if(gameObject.CompareTag("Puzzle") && thisManager.maxItemPuzzles < 6) AddItemToPuzzles();

            Destroy(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //GameManager.instance.FermerMessageInteraction();
    }

    void AddItemToConsommable()
    {
        if (playerInventory && thisItem)
        {
            thisManager.ClearConsommableSlots();

            if (playerInventory.consommablesInventory.Contains(thisItem))
            {
                thisItem.numberHeld++;
            }
            else
            {
                thisManager.maxItemConsommable++;
                thisItem.numberHeld++;
                playerInventory.consommablesInventory.Add(thisItem);
            }

            thisManager.MakeConsommableSlot();
        }
    }

    void AddItemToQuetes()
    {
        if (playerInventory && thisItem)
        {
            thisManager.maxItemQuete++;

            thisManager.ClearObjetQuetesSlots();
            if (!playerInventory.objetsQuetesInventory.Contains(thisItem))
            {
                thisItem.numberHeld++;
            }

            playerInventory.objetsQuetesInventory.Add(thisItem);

            thisManager.MakeObjetQueteSlot();
        }
    }

    void AddItemToPuzzles()
    {
        if (playerInventory && thisItem)
        {
            thisManager.maxItemPuzzles++;

            thisManager.ClearPuzzlesSlots();
            if (!playerInventory.puzzlesInventory.Contains(thisItem))
            {
                thisItem.numberHeld++;
            }

            playerInventory.puzzlesInventory.Add(thisItem);

            thisManager.MakePuzzlesSlot();
        }
    }
}
