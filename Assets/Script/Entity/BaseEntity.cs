
using UnityEngine;



public enum Elements
{
    None,//0
    Fire,//1
    Water,//2
    Earth,
    Air
}

public abstract class BaseEntity : MonoBehaviour
{
    [SerializeField] protected int entityID;
    [SerializeField] protected string entityName;
    [SerializeField] protected string enetityDescription;

    [SerializeField] protected Elements element;

    [SerializeField] protected BaseStats stats;

    protected virtual void Awake()
    {
        stats = new(100, 10, 10, 10, 10);
    }
    private void Start()
    {
        
    }

    public abstract void TakeDamage(BaseEntity damager);

    public BaseStats Stats => stats;
    public Elements Element => element;
}
