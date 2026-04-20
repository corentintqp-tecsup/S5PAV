using UnityEngine;

public class XpSphere : BaseCollectable
{
    public override void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
            collider.gameObject.GetComponent<Player>().XP += 10;
        print("Player +10 XP !");
        base.OnTriggerEnter2D(collider);
    }
}
