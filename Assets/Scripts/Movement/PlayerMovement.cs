using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Animator animator;
    private new Rigidbody2D rigidbody;

    private Vector2 movingDirection;
    private Direction facingDirection;
    [SerializeField] private bool canMove = true;
    [SerializeField] private float speed = 0.25f;

    private enum Direction
    {
        None,
        Up,
        Down,
        Right,
        Left
    }

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        Vector2 previousMovingDirection = rigidbody.velocity.normalized;
        Direction previousFacingDirection = facingDirection;

        rigidbody.velocity = movingDirection * speed;

        SetFacingDirection(movingDirection);
        SetAnimation(movingDirection, previousMovingDirection, facingDirection, previousFacingDirection);
    }

    private void OnEnable()
    {
        Controller.OnMove += SetMovingDirection;
    }

    private void OnDisable()
    {
        Controller.OnMove -= SetMovingDirection;
    }

    void SetMovingDirection(Vector2 input)
    {
        if(!canMove)
        {
            movingDirection = Vector2.zero;
        }
        else
        {
            movingDirection = input;
        }
    }

    void SetFacingDirection(Vector2 movingDirection)
    {
        facingDirection = Direction.None;

        if (movingDirection.x > 0)
        {
            facingDirection = Direction.Right;
        }
        if (movingDirection.x < 0)
        {
            facingDirection = Direction.Left;
        }
        if (movingDirection.y > 0)
        {
            facingDirection = Direction.Up;
        }
        if (movingDirection.y < 0)
        {
            facingDirection = Direction.Down;
        }
    }

    void SetAnimation(Vector2 currentMovingDirection, Vector2 previousMovingDirection,
        Direction currentFacingDirection, Direction previousFacingDirection)
    {
        // Use the == operator to test two vectors for approximate equality
        if (currentMovingDirection != previousMovingDirection || currentFacingDirection != previousFacingDirection)
        {
            SetAnimationFromDirection(currentMovingDirection, currentFacingDirection);
        }
    }

    void SetAnimationFromDirection(Vector2 movingDirection, Direction facingDirection)
    {
        string animationTriggerKey = "";

        // Check if moving
        if (movingDirection == Vector2.zero)
        {
            animationTriggerKey += "Idling";
        }
        else
        {
            animationTriggerKey += "Walking";
        }

        // Check the direction
        string directionString = facingDirection.ToString();

        if (facingDirection == Direction.None)
        {
            directionString = "Down";
        }

        animationTriggerKey += directionString;

        animator.SetTrigger(animationTriggerKey);
    }
}
