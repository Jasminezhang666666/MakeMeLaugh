using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Van : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    private Vector2 movementDirection = Vector2.right;

    void Update()
    {
        transform.Translate(movementDirection * speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with an object on the "Van" layer
        if (collision.gameObject.layer == LayerMask.NameToLayer("Van"))
        {
            // Reverse the direction
            movementDirection = -movementDirection;
        }
    }
}
