using UnityEngine;

public interface IConsumable
{
    string Name { get; }
    string Description { get; }
    void Consume();
    void Add();
}

public class Item : MonoBehaviour, IConsumable
{
    [SerializeField] string name;
    [SerializeField] string description;

    public string Name => name;
    public string Description => description;

    public void Add()
    {
    }

    public void Consume()
    {
    }
}