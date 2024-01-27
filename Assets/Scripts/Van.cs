using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Van : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    private Vector2 movementDirection = Vector2.right;

    void Start()
    {
        // Flip the sprite at the start
        Vector3 startScale = transform.localScale;
        startScale.x *= -1;
        transform.localScale = startScale;
    }

    void Update()
    {
        // Move the van
        transform.Translate(movementDirection * speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with an object on the "Van" layer
        if (collision.gameObject.layer == LayerMask.NameToLayer("Van"))
        {
            // Reverse the direction
            movementDirection = -movementDirection;

            // Flip the sprite by scaling on x-axis
            Vector3 newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;
        }
    }
}

