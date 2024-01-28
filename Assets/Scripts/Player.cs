using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //TEST ONLY
    public bool WASD = false;

    public int time;
    public Slider slider;

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


    [Header("Move")]
    public float speed;
    public Animator animator;
    public float feetHeight;
    public AudioSource hurt;

    [SerializeField] private GameObject poop;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalFeetPosition = feet.localPosition; // Store original position at start
    }

    private void OnMovement(InputValue value)
    {
        //set the movement by input
        movement = value.Get<Vector2>();


    }

    void Update()
    {
        time--;

        slider.value = time;

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



        //TEST ONLY
        if(WASD)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            movement.Normalize();
        }
        // Movement and sprite flipping logic
        //movement.x = Input.GetAxisRaw("Horizontal");
        //movement.y = Input.GetAxisRaw("Vertical");
        //movement.Normalize();
        //FlipSprite();

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
        time--;
        if (movement.x != 0  && isMovingAllowed || movement.y != 0 && isMovingAllowed)
        {
            //change the movement when player move
            rb.velocity = new Vector2(movement.x * speed, movement.y * speed);
        }

        if (rb.velocity.x > 5)
        {
            animator.SetBool("right", true);
            animator.SetBool("left", false);
            animator.SetBool("stay", false);
        }
        else if (rb.velocity.x < -5)
        {
            animator.SetBool("right", false);
            animator.SetBool("left", true);
            animator.SetBool("stay", false);
        }
        else
        {
            animator.SetBool("right", false);
            animator.SetBool("left", false);
            animator.SetBool("stay", true);
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
        yield return new WaitForSeconds(1f);
        isMovingAllowed = true;
        feetExtended = false; // Reset the feetExtended state
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "nut")
        {
            hurt.Play();
            time -= 20;
            Feet feetScript = feet.GetComponent<Feet>();
            if (feetScript.IsObjectAttached())
            {
                feetScript.ReleaseObject();
            }
            Destroy(collision.gameObject);
        }
    }

}