using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketCone : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(speed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (speed < 10)
        {
            speed += 0.01f;
        }
        rb.velocity = new Vector2 (speed, 0);
    }
}
