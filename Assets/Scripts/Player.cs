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

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (feetExtended && feet.GetComponent<Feet>().IsObjectAttached())
            {
                // Release the object if feet are extended and holding an object
                feet.GetComponent<Feet>().ReleaseObject();
                feetExtended = false;
            }
            else if (isMovingAllowed)
            {
                // Extend feet if not currently extended
                StartCoroutine(ExtendFeet());
            }
            return;
        }

        if (!isMovingAllowed) return; // Skip movement if not allowed

        // Input for movement
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();

        // Flip sprite based on movement direction
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
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void FlipSprite()
    {
        if (movement.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (movement.x > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    IEnumerator ExtendFeet()
    {
        isMovingAllowed = false;
        feetExtended = true;

        Vector3 originalScale = feet.localScale;
        Vector3 extendedScale = new Vector3(feet.localScale.x, feet.localScale.y + maxExtendDistance, feet.localScale.z);

        // Extend
        while (feet.localScale.y < extendedScale.y)
        {
            if (feet.GetComponent<Feet>().HasCollidedWithObject)
            {
                break; // Stop extending if the feet have collided with an object
            }

            feet.localScale = Vector3.MoveTowards(feet.localScale, extendedScale, extendSpeed * Time.deltaTime);
            yield return null;
        }

        // Delay before retracting
        yield return new WaitForSeconds(1f);

        // Retract
        float retractSpeed = extendSpeed / 2;
        while (feet.localScale.y > originalScale.y)
        {
            feet.localScale = Vector3.MoveTowards(feet.localScale, originalScale, retractSpeed * Time.deltaTime);
            yield return null;
        }

        feet.GetComponent<Feet>().ReleaseObject();
        feet.GetComponent<Feet>().ResetCollisionFlag();
        isMovingAllowed = true;
        feetExtended = false;
    }
}
