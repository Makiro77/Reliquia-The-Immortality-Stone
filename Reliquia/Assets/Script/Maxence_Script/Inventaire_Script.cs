using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventaire_Script : MonoBehaviour
{
    private const int Slots = 12;

    public List<IInventaireItem> mItems = new List<IInventaireItem>();

    public event EventHandler<InventaireEventArgs> ItemAdded;

    public event EventHandler<InventaireEventArgs> ItemRemoved;

    public void AddItem(IInventaireItem item)
    {
        if(mItems.Count < Slots)
        {
            Collider collider = (item as MonoBehaviour).GetComponent<Collider>();
            if (collider.enabled)
            {
                collider.enabled = false;

                mItems.Add(item);

                item.OnPickup();

                if(ItemAdded != null)
                {
                    ItemAdded(this, new InventaireEventArgs(item));
                }
            }
        }
    }

    public void RemoveItem(IInventaireItem item)
    {
        if (mItems.Contains(item))
        {
            mItems.Remove(item);

            item.OnDrop();

            Collider collider = (item as MonoBehaviour).GetComponent<Collider>();
            if(collider != null)
            {
                collider.enabled = true;
            }

            if(ItemRemoved != null)
            {
                ItemRemoved(this, new InventaireEventArgs(item));
            }
        }
    }
}
