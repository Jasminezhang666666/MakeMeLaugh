using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightLine : MonoBehaviour
{
    [SerializeField]
    public float speed = 0;

    void Update()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name =="Enemy Base" || collision.gameObject.name == "Player") Destroy(this.gameObject);
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Destroy(this.gameObject);
    //}
}
