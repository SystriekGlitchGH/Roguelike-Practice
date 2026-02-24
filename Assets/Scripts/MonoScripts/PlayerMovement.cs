using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Rigidbody2D rb2d;
    [SerializeField] SpriteRenderer spriteRend;

    [Header("Movement stats")]
    // actual stats for how the player moves
    public float acceleration;
    public float topSpeed;
    public float friction;
    
    // variables for direction when moving
    private float directionX, directionY;

    [Header("Attack stats")]
    //variables for attack size
    public Vector2 attackSize;
    public float attackDistance;

    private float attackDirectionX, attackDirectionY;

    private void Awake()
    {
        
    }
    private void FixedUpdate()
    {
        // adding acceleration to the directions
        Vector2 newVelocity = new Vector2(directionX * acceleration, directionY * acceleration);
        // adding the force to the rigidbody2d
        rb2d.AddForce(newVelocity);
        // maxing the velocity of the player to the topSpeed variable
        Vector2 velocity = Vector2.ClampMagnitude(new(rb2d.linearVelocity.x, rb2d.linearVelocity.y), topSpeed);
        // applying clamped velocity to rigidbody2d
        rb2d.linearVelocity = velocity;
        // when both direction inputs are 0, add linear damping to slow down player quickly
        if(directionX == 0 && directionY == 0)
            rb2d.linearDamping = friction;
        else
            rb2d.linearDamping = 0;
    }
    private void Update()
    {
        attackDirectionX = directionX;
        attackDirectionY = directionY;
    }
    public void Move(InputAction.CallbackContext ctx)
    {
        // direction x takes left and right, direction y takes up and down
        directionX = ctx.ReadValue<Vector2>().x;
        directionY = ctx.ReadValue<Vector2>().y;
    }

    public void Attack(InputAction.CallbackContext ctx)
    {
        if(ctx.ReadValue<float>() == 0)
            return;
        if(ctx.performed)
        {
            RaycastHit2D hit = Physics2D.BoxCast(new Vector2(transform.position.x,transform.position.y+attackDistance), attackSize, 0, Vector2.zero);
            if (hit && hit.collider.TryGetComponent(out EnemyMovement enemy))
            {
                Debug.Log("attack succesful");
                enemy.Die();
            }
        }
    }

    private void OnDrawGizmos()
    {   
        //up
        if(attackDirectionX == 0 && attackDirectionY > 0)
        {
            Gizmos.DrawLine(new Vector2(transform.position.x-(attackSize.x/2),transform.position.y+(attackDistance/2)),new Vector2(transform.position.x+(attackSize.x/2),transform.position.y+(attackDistance/2)));
            Gizmos.DrawLine(new Vector2(transform.position.x-(attackSize.x/2),transform.position.y+(attackDistance/2)),new Vector2(transform.position.x-(attackSize.x/2),transform.position.y+(attackDistance/2)+attackSize.y));
        }
        //right
        if(attackDirectionX > 0 && attackDirectionY == 0)
        {
            Gizmos.DrawLine(new Vector2(transform.position.x+(attackDistance/2),transform.position.y+(attackSize.x/2)),new Vector2(transform.position.x+(attackDistance/2),transform.position.y-(attackSize.x/2)));
            Gizmos.DrawLine(new Vector2(transform.position.x+(attackDistance/2),transform.position.y+(attackSize.x/2)),new Vector2(transform.position.x+(attackDistance/2)+attackSize.y,transform.position.y+(attackSize.x/2)));
        }
        //down
        if(attackDirectionX == 0 && attackDirectionY < 0)
        {
            Gizmos.DrawLine(new Vector2(transform.position.x-(attackSize.x/2),transform.position.y-(attackDistance/2)),new Vector2(transform.position.x+(attackSize.x/2),transform.position.y-(attackDistance/2)));
            Gizmos.DrawLine(new Vector2(transform.position.x+(attackSize.x/2),transform.position.y-(attackDistance/2)),new Vector2(transform.position.x+(attackSize.x/2),transform.position.y-(attackDistance/2)-attackSize.y));
        }
        //left
        if(attackDirectionX < 0 && attackDirectionY == 0)
        {
            Gizmos.DrawLine(new Vector2(transform.position.x-(attackDistance/2),transform.position.y+(attackSize.x/2)),new Vector2(transform.position.x-(attackDistance/2),transform.position.y-(attackSize.x/2)));
            Gizmos.DrawLine(new Vector2(transform.position.x-(attackDistance/2),transform.position.y-(attackSize.x/2)),new Vector2(transform.position.x-(attackDistance/2)-attackSize.y,transform.position.y-(attackSize.x/2)));
        }
        //upright
        if(attackDirectionX > 0 && attackDirectionY > 0)
        {
            Gizmos.DrawLine(new Vector2(transform.position.x+(attackDistance/4),transform.position.y+(attackDistance/2f)),new Vector2(transform.position.x,transform.position.y));
        
        }
    }
}