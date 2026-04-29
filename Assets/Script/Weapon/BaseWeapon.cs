using UnityEngine;

public enum ProjectileType
{
    None,
    Spin,
    Throw,
    Falling
}

public class BaseWeapon : MonoBehaviour
{

    public int Duration;
    public ProjectileType Type;
    public float Speed;
    public float RotationSpeed;
    private Vector2 dir;

    void Start()
    {
        Destroy(gameObject, Duration);
        dir = randomDirection();
    }

    void Update()
    {
        switch (Type)
        {
            case ProjectileType.None:
                break;
            case ProjectileType.Spin:
                Spin();
                break;
            case ProjectileType.Throw:
                transform.position += (Vector3)(dir * Speed * Time.deltaTime);
                break;
            case ProjectileType.Falling:
                transform.position += (Vector3)(Vector2.down * Speed * Time.deltaTime);
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().TakeDamage(GetComponentInParent<Player>());
        }
    }

    private void Spin()
    {
        transform.RotateAround(transform.parent.position, Vector3.forward, RotationSpeed * Time.deltaTime);
    }

    public Vector2 randomDirection()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
