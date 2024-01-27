using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MafiaBullet : MonoBehaviour
{
    public Rigidbody2D rb;
    public int speed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = new Vector2 (speed, Random.Range(-1f,1f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
