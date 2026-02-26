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

    [Header("Debugging Tools")]
    [SerializeField] Transform anchorTransform;

    private float attackAngle;
    private enum Direction
    {
        North, South, East, West, NorthEast, NorthWest, SouthEast, SouthWest
    }
    private Direction playerDirection;

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
        if(directionX == 0 && directionY == 0) rb2d.linearDamping = friction;
        else rb2d.linearDamping = 0;
    }
    private void Update()
    {
        FindDirection();
        FindAngle();
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
            //RaycastHit2D hit = Physics2D.BoxCast(new Vector2(transform.position.x,transform.position.y+attackDistance), attackSize, attackAngle, Vector2.zero);
            RaycastHit2D hit = MakeBoxCastAttack();
            if (hit && hit.collider.TryGetComponent(out EnemyMovement enemy))
            {
                Debug.Log("attack succesful");
                //enemy.Die();
            }
        }
    }

    private void FindAngle()
    {
        if(playerDirection == Direction.North) attackAngle = 0;
        if(playerDirection == Direction.South) attackAngle = 180;
        if(playerDirection == Direction.East) attackAngle = -90;
        if(playerDirection == Direction.West) attackAngle = 90;
        if(playerDirection == Direction.NorthEast) attackAngle = -45;
        if(playerDirection == Direction.NorthWest) attackAngle = 45;
        if(playerDirection == Direction.SouthEast) attackAngle = -135;
        if(playerDirection == Direction.SouthWest) attackAngle = 135;
    }
    private void FindDirection()
    {
        //north
        if(directionX == 0 && directionY > 0) playerDirection = Direction.North;
        //south
        if(directionX == 0 && directionY < 0) playerDirection = Direction.South;
        //east
        if(directionX > 0 && directionY == 0) playerDirection = Direction.East;
        //west
        if(directionX < 0 && directionY == 0) playerDirection = Direction.West;
        //northeast
        if(directionX > 0 && directionY > 0) playerDirection = Direction.NorthEast;
        //northwest
        if(directionX < 0 && directionY > 0) playerDirection = Direction.NorthWest;
        //southeast
        if(directionX > 0 && directionY < 0) playerDirection = Direction.SouthEast;
        //southwest
        if(directionX < 0 && directionY < 0) playerDirection = Direction.SouthWest;
    }
    private RaycastHit2D MakeBoxCastAttack()
    {
        if(playerDirection == Direction.North)
            return Physics2D.BoxCast(new Vector2(transform.position.x,transform.position.y+attackDistance), attackSize, attackAngle, Vector2.zero);
        if(playerDirection == Direction.South)
            return Physics2D.BoxCast(new Vector2(transform.position.x,transform.position.y-attackDistance), attackSize, attackAngle, Vector2.zero);
        if(playerDirection == Direction.East)
            return Physics2D.BoxCast(new Vector2(transform.position.x+attackDistance,transform.position.y), attackSize, attackAngle, Vector2.zero);
        if(playerDirection == Direction.West)
            return Physics2D.BoxCast(new Vector2(transform.position.x-attackDistance,transform.position.y), attackSize, attackAngle, Vector2.zero);
        if(playerDirection == Direction.NorthEast)
            return Physics2D.BoxCast(new Vector2(transform.position.x+Mathf.Sqrt(attackDistance),transform.position.y+Mathf.Sqrt(attackDistance)), attackSize, attackAngle, Vector2.zero);
        if(playerDirection == Direction.NorthWest)
            return Physics2D.BoxCast(new Vector2(transform.position.x-Mathf.Sqrt(attackDistance),transform.position.y+Mathf.Sqrt(attackDistance)), attackSize, attackAngle, Vector2.zero);
        if(playerDirection == Direction.SouthEast)
            return Physics2D.BoxCast(new Vector2(transform.position.x+Mathf.Sqrt(attackDistance),transform.position.y-Mathf.Sqrt(attackDistance)), attackSize, attackAngle, Vector2.zero);
        if(playerDirection == Direction.SouthWest)
            return Physics2D.BoxCast(new Vector2(transform.position.x-Mathf.Sqrt(attackDistance),transform.position.y-Mathf.Sqrt(attackDistance)), attackSize, attackAngle, Vector2.zero);
        return Physics2D.BoxCast(new Vector2(transform.position.x,transform.position.y), attackSize, attackAngle, Vector2.zero);
    }
    private void OnDrawGizmos()
    {   
        Gizmos.matrix = anchorTransform.localToWorldMatrix;
        anchorTransform.eulerAngles = new Vector3(0,0,attackAngle);
        Gizmos.DrawWireCube(new Vector2(0,attackDistance), attackSize);
        
    }
}