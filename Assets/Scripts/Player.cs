using System.Collections;
using UnityEditor;
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

    public float feetHeight;

    [SerializeField] private GameObject poop;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalFeetPosition = feet.localPosition; // Store original position at start
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            {
                
                Feet feetScript = feet.GetComponent<Feet>();
                
                if (feetScript.IsObjectAttached())
                {
                    feetScript.ReleaseObject();
                }
                else if (!feetExtended && isMovingAllowed)
                {
                    // Extend feet only if they are not already extended and the player is allowed to move
                    StartCoroutine(ExtendFeet());
                }
            }

        if (!isMovingAllowed || feetExtended) return;

        // Movement and sprite flipping logic
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();
        FlipSprite();

        if (Input.GetKeyDown(KeyCode.E))
        {
            Vector3 offset = new Vector3(-0.5f, -0.7f, 0); // Adjust as needed

            // Check the direction the player is facing
            if (GetComponent<SpriteRenderer>().flipX)
            {
                offset.x *= -1; // Flip the offset if the player is flipped
            }
            Vector3 spawnPosition = gameObject.transform.position + offset;

            Instantiate(poop, spawnPosition, Quaternion.identity);
        }

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
        if (feetExtended)
        {
            yield break; // Exit if feet are already extended
        }

        isMovingAllowed = false;
        feetExtended = true;

        // Set the target local Y position based on the original local position and max extension distance
        float targetLocalYPosition = originalFeetPosition.y - maxExtendDistance;

        while (feet.localPosition.y > targetLocalYPosition)
        {
            Vector3 newLocalPosition = feet.localPosition;
            newLocalPosition.y = Mathf.MoveTowards(feet.localPosition.y, targetLocalYPosition, extendSpeed * Time.deltaTime);
            feet.localPosition = newLocalPosition;

            if (feet.GetComponent<Feet>().HasCollidedWithObject)
            {
                feet.GetComponent<Feet>().ResetCollisionFlag(); // Ensure Feet class has this method to reset the flag
                break;
            }

            yield return null;
        }

        StartCoroutine(RetractFeet()); // Always retract after extending
    }

    IEnumerator RetractFeet()
    {
        while (feet.localPosition.y < originalFeetPosition.y)
        {
            Vector3 newLocalPosition = feet.localPosition;
            newLocalPosition.y = Mathf.MoveTowards(feet.localPosition.y, originalFeetPosition.y, extendSpeed * Time.deltaTime);
            feet.localPosition = newLocalPosition;
            yield return null;
        }

        isMovingAllowed = true;
        feetExtended = false; // Reset the feetExtended state
    }


}