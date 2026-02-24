using UnityEngine;

public class Enemy1
{
    public float health;

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}
