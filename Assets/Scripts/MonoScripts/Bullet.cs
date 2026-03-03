using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public PlayerMovement pm;
    public Vector2 direction;
    private int enemiesHit;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyMovement enemy = collision.GetComponent<EnemyMovement>();
            enemy.Hit(this, pm.weapon.baseKnockback);
            enemiesHit++;
            if(enemiesHit == pm.weapon.pierce)
            {
                Destroy(gameObject);
            }
        }
    }
}
