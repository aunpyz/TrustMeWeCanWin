using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainer : MonoBehaviour
{
    public List<Item> items = new List<Item>(3);
    public ItemSlot[] slots = new ItemSlot[3];

    private int currentItemIndex = 0;
    private float itemCooldown = 1f;

    public bool Empty
    {
        get { return items.Count <= 0; }
    }

    public void MoveSelecte()
    {
        currentItemIndex = currentItemIndex + 1 >= 3 ? 0 : currentItemIndex + 1;
    }

    public void AddItem(Item item)
    {
        if (items.Count < 3)
        {
            items.Add(item);
        }
        else
        {
            items[0].Destroy();
            items.RemoveAt(0);
            items.Add(item);
        }

        AssignItemsData();
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
        AssignItemsData();
    }

    // remove and return selected item
    public Item PullSelected()
    {
        var item = items[currentItemIndex];
        RemoveItem(item);
        return item;
    }

    private void AssignItemsData()
    {
        for (int i = 0; i < slots.Length; ++i)
        {
            try
            {
                slots[i].Assign(items[i]);
            }
            catch
            {
                slots[i].Assign(null);
            }
        }
    }
}
