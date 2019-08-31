using UnityEngine;

public interface IConsumable
{
    string Name { get; }
    string Description { get; }
    void Consume(PlayerController self, PlayerController other);
    void Destroy();
}

public sealed class Item : MonoBehaviour, IConsumable
{
    [SerializeField] private string itemName;
    [SerializeField] private string itemDescription;
    public delegate void ItemEffect(PlayerController self, PlayerController other);
    public ItemEffect itemEffect;

    public string Name => itemName;
    public string Description => itemDescription;

    public void Consume(PlayerController self, PlayerController other)
    {
        itemEffect(self, other);
        Destroy(gameObject);
    }

    public void Destroy() 
    {
        Destroy(gameObject);
    }

    public Item Init(string name = "default", string description = "description")
    {
        itemName = name;
        itemDescription = description;
        return this;
    }

    public Item SetEffect(ItemEffect itemEffect)
    {
        this.itemEffect = itemEffect;
        return this;
    }
}