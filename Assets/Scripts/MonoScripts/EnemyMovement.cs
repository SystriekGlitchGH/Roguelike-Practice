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

    private void FixedUpdate()
    {
        if(target != null)
        {
            distance = PlayerDistance(target.transform.position);
            if(distance > 2)
            {
                rb2d.AddForce(PlayerDirection(target.transform.position) * acceleration);
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
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            target = collision.GetComponent<PlayerMovement>();
        }
    }
    public void Die()
    {
        Destroy(gameObject);
    }

    //movement help methods
    private float PlayerDistance(Vector2 playerPos)
    {
        return Vector2.Distance((Vector2)transform.position, playerPos);
    }
    private Vector2 PlayerDirection(Vector2 playerPos)
    {
        return playerPos - (Vector2)transform.position;
    }
}
