using UnityEngine;

public interface IConsumable
{
    string Name { get; }
    string Description { get; }
    void Consume(PlayerController self, PlayerController other);
    void Add();
}

public sealed class Item : MonoBehaviour, IConsumable
{
    [SerializeField] private string name;
    [SerializeField] private string description;
    public delegate void ItemEffect(PlayerController self, PlayerController other);
    public ItemEffect itemEffect;

    public string Name => name;
    public string Description => description;

    public void Add()
    {

    }

    public void Consume(PlayerController self, PlayerController other)
    {
        itemEffect(self, other);
        Destroy(gameObject);
    }

    public Item Init(string name = "default", string description = "description")
    {
        this.name = name;
        this.description = description;
        return this;
    }
}