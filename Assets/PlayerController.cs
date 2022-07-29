using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float Health
    {
        set
        {
            health = value;
            if (health <= 0)
            {
                Defeated();
            }
        }
        get
        {
            return health;
        }
    }

    public float health = 1;
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;

    Vector2 movementInput;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    Animator animator;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    bool canMove = true;
    public bool dead = false;
    public SwordAttack swordAttack;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        if (canMove && !dead)
        {
            //if movement input is not 0, try to move
            if (movementInput != Vector2.zero)
            {
                bool success = TryMove(movementInput);

                if (!success)
                {
                    success = TryMove(new Vector2(movementInput.x, 0));
                }
                if (!success)
                {
                    success = TryMove(new Vector2(0, movementInput.y));
                }
                animator.SetBool("IsMoving", success);
            }
            else
            {
 
                animator.SetBool("IsMoving", false);
            }

            // Set direction of sprite to movement direction
            if (movementInput.x < 0)
            {
                spriteRenderer.flipX = true;
                
            }
            else if (movementInput.x > 0)
            {
                spriteRenderer.flipX = false;
                
            }
        }
        
        
    }

    public void Defeated()
    {
        if (!dead)
        {
            dead = true;
            animator.SetBool("IsMoving", false);
            animator.SetTrigger("dead");
        }
        
    }

    private bool TryMove(Vector2 direction)
    {
        if(direction != Vector2.zero)
        {
            int count = rb.Cast(
                direction, //x and y values between -1 and 1 that represent the direction from the body to look for collisions
                movementFilter, //The settings that determine where a collision can occur on such as layers to collide with
                castCollisions, //List of collisions to store the found collision into after the Cast is finished
                moveSpeed * Time.fixedDeltaTime + collisionOffset); //The amount to cast equal to the movement plus an offset
            if (count == 0)
            {
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            }
            else
            {
                return false;
            }
        } else
        {
            return false;
        }
        //check for potential collisions
        
    }

    void OnMove(InputValue movementValue) {
        movementInput = movementValue.Get<Vector2>();
    }

    void OnFire()
    {
        if (!dead)
        {
            animator.SetTrigger("swordAttack");
        }
        
    }

    public void SwordAttack() {
        LockMovement();
        if (spriteRenderer.flipX == true)
        {
            swordAttack.AttackLeft();
        } else {
            swordAttack.AttackRight();
        }
            
    }
    public void EndSwordAttack()
    {
        UnlockMovement();
        swordAttack.StopAttack();
    }
    public void LockMovement ()
    {
        canMove = false;
    }
    public void UnlockMovement()
    {
        canMove = true;
    }
}
