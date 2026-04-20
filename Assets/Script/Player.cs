
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : BaseEntity
{
    public CircleCollider2D coll;
    public float range;
    public float moveSpeed = 5f;

    private Vector2 moveInput;

    public List<GameObject> Enemys = new();

    public int XP = 0;

    protected override void Awake()
    {
        base.Awake();
        coll = GetComponentInChildren<CircleCollider2D>();
        coll.radius = range;
    }
    void Start()
    {
        InvokeRepeating("AutoAttackEnemies", 1f, 1f);
    }

    void Update()
    {
        transform.Translate(moveInput * moveSpeed * Time.deltaTime);
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void AutoAttackEnemies()
    {
        for (int i = Enemys.Count - 1; i >= 0; i--)
        {
            GameObject enemy = Enemys[i];

            if (enemy == null)
            {
                Enemys.RemoveAt(i);
                continue;
            }

            float distance = Vector3.Distance(enemy.transform.position, transform.position);
            if (distance <= range && enemy.GetComponent<Enemy>() != null)
                enemy.GetComponent<Enemy>().TakeDamage(this);
        }

    }

    private void OnDestroy()
    {
        Debug.Log("oh no me cancelaron");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
            Enemys.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //if(Enemys.Find(collision.gameObject))
        Enemys.Remove(collision.gameObject);
    }

    public override void TakeDamage(BaseEntity damager)
    {
        // base.TakeDamage(damager);

        Debug.Log(damager.Element);

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
        stats.TakeDamage(damage);
        if (stats.Health <= 0)
            Destroy(gameObject);
    }
}

