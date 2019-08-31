using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public Text itemName;
    public Image itemImage;

    private void Start()
    {
        itemName.text = "";
    }

    public void Assign(Item item)
    {
        itemName.text = item?.Name ?? "";
    }

    public void Remove()
    {
        itemName.text = "";
    }
}
