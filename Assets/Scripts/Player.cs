using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform feet; // Assign the Feet child object in the Inspector
    public float extendSpeed = 5f; // Speed at which feet extends
    public float maxExtendDistance = 15f; // Maximum extension distance
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Vector2 movement;
    private bool isMovingAllowed = true;
    private bool feetExtended = false;
    private Vector3 originalFeetPosition; // To store original position of the feet

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalFeetPosition = feet.position; // Store original position at start
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (feet.GetComponent<Feet>().IsObjectAttached())
            {
                feet.GetComponent<Feet>().ReleaseObject();
            }

            if (feetExtended)
            {
                StartCoroutine(RetractFeet());
            }
            else if (isMovingAllowed)
            {
                // Extend feet
                StartCoroutine(ExtendFeet());
            }
        }

        if (!isMovingAllowed || feetExtended) return;

        // Movement and sprite flipping logic
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();
        FlipSprite();
    }

    void FixedUpdate()
    {
        if (isMovingAllowed)
        {
            MovePlayer();
        }
    }

    private void MovePlayer()
    {
        // Move the player based on the movement vector
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void FlipSprite()
    {
        // Flip the sprite based on the horizontal movement direction
        if (movement.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (movement.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }
    IEnumerator ExtendFeet()
    {
        isMovingAllowed = false;
        originalFeetPosition = feet.position;

        float targetYPosition = originalFeetPosition.y - maxExtendDistance;

        while (feet.position.y > targetYPosition)
        {
            Vector3 newPosition = feet.position;
            newPosition.y = Mathf.MoveTowards(feet.position.y, targetYPosition, extendSpeed * Time.deltaTime);
            feet.position = newPosition;

            // Check if the feet collided with something
            if (feet.GetComponent<Feet>().HasCollidedWithObject)
            {
                break;
            }

            yield return null;
        }

        feetExtended = true;

        // Always retract the feet after extending
        StartCoroutine(RetractFeet());
    }


    IEnumerator RetractFeet()
    {
        feetExtended = false;

        while (feet.position.y < originalFeetPosition.y)
        {
            Vector3 newPosition = feet.position;
            newPosition.y = Mathf.MoveTowards(feet.position.y, originalFeetPosition.y, extendSpeed * Time.deltaTime);
            feet.position = newPosition;
            yield return null;
        }

        // Re-enable movement after the feet have fully retracted
        isMovingAllowed = true;
        movement = Vector2.zero; // Reset movement vector
    }

}