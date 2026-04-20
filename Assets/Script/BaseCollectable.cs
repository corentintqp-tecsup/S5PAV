using UnityEngine;

public abstract class BaseCollectable: MonoBehaviour
{
    private void Start()
    {
        gameObject.AddComponent<BoxCollider2D>().isTrigger = true;
    }

    public virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
            Destroy(gameObject);
    }
}
