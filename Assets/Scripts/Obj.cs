using UnityEngine;

public class Obj : MonoBehaviour
{
    public bool isLifted = false;
    public bool isFalling = false;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        UpdateRigidbodyType();
    }

    private void Update()
    {
        // Update the Rigidbody type based on the parent's status
        UpdateRigidbodyType();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            cancelParent(); // Remove parent relationship
            return;
        }

        if (isFalling && !collision.gameObject.CompareTag("Feet"))
        {
            isFalling = false; // Reset isFalling when colliding with other objects
        }
    }

    private void UpdateRigidbodyType()
    {
        if (transform.parent != null)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    public void cancelParent()
    {
        transform.parent = null;
    }
}
