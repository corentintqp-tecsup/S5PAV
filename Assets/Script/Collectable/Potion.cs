using UnityEngine;

public class Potion : BaseCollectable
{
    public override void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            BaseStats playerStats = collider.gameObject.GetComponent<Player>().Stats;
            playerStats.SetHealth(playerStats.Health + 20);
        }
        print("Player +20 HP !");
        base.OnTriggerEnter2D(collider);
    }
}
