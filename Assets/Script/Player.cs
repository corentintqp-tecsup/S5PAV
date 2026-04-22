
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerController
{
    None,
    Player1,
    Player2
}

public class Player : BaseEntity
{

    public InputSystem_Actions inputs;
    public PlayerController playerController;
    public Vector2 MoveInput;

    public CircleCollider2D coll;
    public float range;
    public float MoveSpeed = 5f;

    public List<GameObject> Enemys = new();
    public BaseWeapon[] Weapons = new BaseWeapon[2];

    public int XP = 0;

    protected override void Awake()
    {
        base.Awake();
        coll = GetComponentInChildren<CircleCollider2D>();
        coll.radius = range;

        inputs = new();
    }

    private void OnEnable()
    {
        inputs.Enable();

        switch (playerController)
        {
            case PlayerController.None:
                break;
            case PlayerController.Player1:
                inputs.Player.Move.performed += OnPlayerMove;
                inputs.Player.Move.canceled += OnPlayerMoveCanceled;
                inputs.Player.Attack1.performed += OnAttack1;
                inputs.Player.Attack2.performed += OnAttack2;
                break;
            case PlayerController.Player2:
                inputs.Player2.Move.performed += OnPlayerMove;
                inputs.Player2.Move.canceled += OnPlayerMoveCanceled;
                inputs.Player2.Attack1.performed += OnAttack1;
                inputs.Player2.Attack2.performed += OnAttack2;
                break;
            default:
                break;
        }
    }

    void Start()
    {
        // InvokeRepeating("AutoAttackEnemies", 1f, 1f);
    }

    void Update()
    {
        OnMove();
    }

    public void OnMove()
    {
        if (MoveInput != Vector2.zero)
        {
            transform.position += (Vector3)MoveInput * MoveSpeed * Time.deltaTime;
        }
    }

        private void OnPlayerMove(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
    }

    private void OnPlayerMoveCanceled(InputAction.CallbackContext context)
    {
        MoveInput = Vector2.zero;
    }

    private void OnAttack1(InputAction.CallbackContext context)
    {
        Instantiate(Weapons[0], transform.position, Quaternion.identity, transform);
    }

    private void OnAttack2(InputAction.CallbackContext context)
    {
        Instantiate(Weapons[1], transform.position, Quaternion.identity);
    }

    // public void AutoAttackEnemies()
    // {
    //     for (int i = Enemys.Count - 1; i >= 0; i--)
    //     {
    //         GameObject enemy = Enemys[i];

    //         if (enemy == null)
    //         {
    //             Enemys.RemoveAt(i);
    //             continue;
    //         }

    //         float distance = Vector3.Distance(enemy.transform.position, transform.position);
    //         if (distance <= range && enemy.GetComponent<Enemy>() != null)
    //             enemy.GetComponent<Enemy>().TakeDamage(this);
    //     }

    // }

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

