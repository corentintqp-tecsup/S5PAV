using UnityEngine;

public class Enemy : BaseEntity
{
    public XpSphere xpSpherePrefab;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

    public override void TakeDamage(BaseEntity damager)
    {
        int damage = damager.Stats.Power;

        switch (damager.Element)
        {
            case Elements.None:
                //damage = damage;
                break;
            case Elements.Fire:
                damage *= 2;
                break;
            case Elements.Water:
                damage /= 2;
                break;
            case Elements.Earth:
                damage *= 3;
                break;
            case Elements.Air:
                damage = 0;
                break;
            default:
                break;
        }
        print("ENEMY " + entityName + " TOOK " + damage + " DAMAGE!");
        stats.TakeDamage(damage);
        if (stats.Health <= 0)
        {
            if (xpSpherePrefab)
                Instantiate(xpSpherePrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void FollowPlayer()
    {
        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
        if (player != null)
        {
            GameObject closestPlayer = null;
            float closestDistance = Mathf.Infinity;
            foreach (GameObject p in player)
            {
                float distance = Vector3.Distance(transform.position, p.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestPlayer = p;
                }
            }
            if (closestPlayer != null)
            {
                transform.position = Vector2.MoveTowards(transform.position, closestPlayer.transform.position, Time.deltaTime);
            }
        }
    }
}
