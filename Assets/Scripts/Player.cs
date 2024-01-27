using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform feet; // Assign the Feet child object in the Inspector
    public float extendSpeed = 5f; // Speed at which feet extends
    public float maxExtendDistance = 5f; // Maximum extension distance, increased for longer extension
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Vector2 movement;
    private bool isMovingAllowed = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isMovingAllowed)
        {
            StartCoroutine(ExtendFeet());
            return; // Skip reading movement input when extending feet
        }

        if (!isMovingAllowed) return; // Skip movement if not allowed

        // Input
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
        isMovingAllowed = false; // Stop player movement
        Vector3 originalScale = feet.localScale;
        Vector3 extendedScale = new Vector3(feet.localScale.x, feet.localScale.y + maxExtendDistance, feet.localScale.z);

        Vector3 originalPosition = feet.localPosition;
        Vector3 newPosition = new Vector3(feet.localPosition.x, feet.localPosition.y - maxExtendDistance / 2, feet.localPosition.z);

        // Extend
        while (feet.localScale.y < extendedScale.y)
        {
            feet.localScale = Vector3.MoveTowards(feet.localScale, extendedScale, extendSpeed * Time.deltaTime);
            feet.localPosition = Vector3.MoveTowards(feet.localPosition, newPosition, extendSpeed * Time.deltaTime / 2);
            yield return null;
        }

        //Delay before retracting
        yield return new WaitForSeconds(1f);

        // Retract
        float retractSpeed = extendSpeed / 2;
        while (feet.localScale.y > originalScale.y)
        {
            feet.localScale = Vector3.MoveTowards(feet.localScale, originalScale, retractSpeed * Time.deltaTime);
            feet.localPosition = Vector3.MoveTowards(feet.localPosition, originalPosition, retractSpeed * Time.deltaTime / 2);
            yield return null;
        }

        feet.GetComponent<Feet>().ReleaseObject(); // Release the object when feet retract

        isMovingAllowed = true; // Player can move again
    }
}
