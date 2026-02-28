using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public PlayerMovement target;
    [Header("Components")]
    [SerializeField] Rigidbody2D rb2d;
    public Enemy1 enemy;

    [Header("Movement Stats")]
    public float acceleration;
    public float topSpeed;
    public float friction;
    // variables to help with movement
    private float distance;
    private bool hasKnockback;
    public float knockbackTime;

    private void Start()
    {
        rb2d.linearDamping = friction;
    }
    private void FixedUpdate()
    {
        if(target != null)
        {
            Debug.Log(PlayerDirection(target.transform.position));
            if (hasKnockback)
            {
                return;
            }
            distance = PlayerDistance(target.transform.position);
            if(distance > 2)
            {
                rb2d.linearDamping = 0;
                Vector2 newVelocity = PlayerDirection(target.transform.position)*acceleration;
                rb2d.AddForce(newVelocity);
                Vector2 velocity = Vector2.ClampMagnitude(new(rb2d.linearVelocity.x, rb2d.linearVelocity.y), topSpeed);
                rb2d.linearVelocity = velocity;
            }
            if(distance < 2)
            {
                rb2d.linearDamping = friction;
            }
            if(distance > 20)
            {
                target = null;
                rb2d.linearDamping = friction;
            }
        }
    }
    public void Die()
    {
        Destroy(gameObject);
    }
    public void Hit(Vector2 playerPos, float knockback)
    {
        StartCoroutine(Knockback());
        rb2d.AddForce(-PlayerDirection(playerPos)*knockback,ForceMode2D.Impulse);
    }

    private IEnumerator Knockback()
    {
        hasKnockback = true;
        yield return new WaitForSeconds(knockbackTime);
        hasKnockback = false;
    }

    //movement help methods
    private float PlayerDistance(Vector2 playerPos)
    {
        return Vector2.Distance((Vector2)transform.position, playerPos);
    }
    private Vector2 PlayerDirection(Vector2 playerPos)
    {
        return (playerPos - (Vector2)transform.position).normalized;
    }
}
