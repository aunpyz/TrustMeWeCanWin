using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Helper
{
    public static T RandomEnumValue<T>()
    {
        var v = Enum.GetValues(typeof(T));
        return (T)v.GetValue(new System.Random().Next(v.Length));
    }
}

public class ItemGenerator : MonoBehaviour
{
    public enum ItemType
    {
        swapHp,
        heal
    }

    public Item item;

    private static ItemGenerator _instance;
    public static ItemGenerator Instance
    {
        get
        {
            return _instance;
        }
    }
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public Item GenerateItem()
    {
        var itemType = Helper.RandomEnumValue<ItemType>();
        return Instantiate(item).Init(
            (PlayerController self, PlayerController friend) => { },
            itemType.ToString());
    }
}
