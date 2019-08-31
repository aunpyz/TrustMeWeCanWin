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

    public GameObject item;

    private ItemGenerator _instance;
    public ItemGenerator Instance
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

    public void GenerateItem()
    {
        var itemType = Helper.RandomEnumValue<ItemType>();
    }
}
