using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public Text itemName;
    public Image itemImage;
    public Image selectIndicator;

    public void Assign(Item item, bool selected)
    {
        itemName.text = item?.Name ?? "";
        if (item == null)
        {
            itemImage.gameObject.SetActive(false);
        }
        else
        {
            itemImage.gameObject.SetActive(true);
            itemImage.sprite = Resources.Load<Sprite>(item.Url);
        }
        ChangeActive(selected);
    }

    public void ChangeActive(bool selected)
    {
        selectIndicator.gameObject.SetActive(selected);
    }

    public void Remove()
    {
        itemName.text = "";
        itemImage.gameObject.SetActive(false);
    }
}
