using UnityEngine;

public interface IConsumable
{
    string Name { get; }
    string ShortName { get; }
    string Description { get; }
    string Url { get; }
    void Consume(PlayerController self, PlayerController other);
    void Destroy();
}

[System.Serializable]
public sealed class Item : MonoBehaviour, IConsumable
{
    [SerializeField] private string itemName;
    [SerializeField] private string itemShortName;
    [SerializeField] private string itemDescription;
    [SerializeField] private string imageUrl;
    public delegate void ItemEffect(PlayerController self, PlayerController other);
    public ItemEffect itemEffect;

    public string Name => itemName;
    public string Description => itemDescription;
    public string Url => imageUrl;
    public string ShortName => itemShortName;

    public void Consume(PlayerController self, PlayerController other)
    {
        itemEffect(self, other);
        Destroy(gameObject);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public Item Init(string name = "default", string shortName = "default", string description = "description", string url = "")
    {
        itemName = name;
        itemShortName = shortName;
        itemDescription = description;
        imageUrl = url;
        return this;
    }

    public Item SetEffect(ItemEffect itemEffect)
    {
        this.itemEffect = itemEffect;
        return this;
    }
}