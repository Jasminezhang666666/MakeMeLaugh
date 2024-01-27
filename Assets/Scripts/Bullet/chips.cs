using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chips : MonoBehaviour
{
    public Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //rb.rotation = -35f; 
        rb.velocity = new Vector2(Random.Range(20,30), Random.Range(20,30));
        
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.rotation > -180)
        {
            rb.rotation = -60 + (rb.velocity.y * 7);
        }
        
    }
}
