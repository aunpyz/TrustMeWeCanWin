using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemContainer : MonoBehaviour
{
    [SerializeField] private Animator LeftDialogAnimation;
    public List<Item> items = new List<Item>(3);
    public ItemSlot[] slots = new ItemSlot[3];
    public Text textDialog;

    private int currentItemIndex = 0;
    private float itemCooldown = 1f;

    public bool Empty
    {
        get { return items.Count <= 0; }
    }

    private void Start()
    {
        AssignItemsData();
    }

    public void MoveSelected()
    {
        currentItemIndex = currentItemIndex + 1 >= 3 ? 0 : currentItemIndex + 1;
        for (int i = 0; i < slots.Length; ++i)
        {
            slots[i].ChangeActive(currentItemIndex == i);
        }
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
        try
        {
            var item = items[currentItemIndex];
            RemoveItem(item);
            return item;
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public IEnumerator ShowItemDialog(Item item)
    {
        yield return ShowItemDialog($"Got {item.Name}");
    }

    public IEnumerator ShowItemDialog(string content)
    {
        textDialog.text = content;
        LeftDialogAnimation.SetTrigger("Show");
        yield return new WaitForSeconds(3f);
        LeftDialogAnimation.SetTrigger("Hide");
        yield return new WaitForSeconds(2f);
    }

    private void AssignItemsData()
    {
        for (int i = 0; i < slots.Length; ++i)
        {
            var selected = currentItemIndex == i;
            try
            {
                slots[i].Assign(items[i], selected);
            }
            catch
            {
                slots[i].Assign(null, selected);
            }
        }
    }
}
