using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public Text itemName;
    public Image itemImage;

    public void Assign(Item item, bool selected)
    {
        itemName.text = item?.Name ?? "";
        ChangeActive(selected);
    }

    public void ChangeActive(bool selected)
    {
        if (selected)
        {
            itemImage.color = Color.green;
        }
        else
        {
            itemImage.color = Color.white;
        }
    }

    public void Remove()
    {
        itemName.text = "";
    }
}
