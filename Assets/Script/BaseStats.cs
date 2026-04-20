using UnityEngine;



public class BaseStats 
{
    // Relación de Composición
    private int health;
    private int power;
    private int speed;
    private int knockback;
    private int xp;


    public BaseStats(int health , int power, int speed, int knockback , int xp)
    {
        SetHealth(health);
        SetPower(power);
        SetSpeed(speed);
        SetKnockback(knockback);
        SetXP(xp);
    }
    public void SetPower(int power)
    {
        this.power = power;
    }
    public void SetHealth(int health)
    {
        if(health <= 0)
            health = 0;

        this.health = health;
    }
  
    public void SetSpeed(int speed)
    {
        this.speed = speed;
    }
    public void SetKnockback(int knockback)
    {
        this.knockback = knockback;
    }
    public void SetXP(int xp)
    {
        this.xp = xp;
    }
    public void TakeDamage(int value)
    {
        health -= value;
        if (health <= 0)
            health = 0;

    }
    public int Health => health;
    public int Power => power;
    public int Speed => speed;
    public int Knockback => knockback;
    public int XP => xp;

    ~BaseStats()
    {
        Debug.Log("Eliminado por el garbage collector");
    }
}
