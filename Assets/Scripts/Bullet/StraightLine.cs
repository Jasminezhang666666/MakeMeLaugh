using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightLine : MonoBehaviour
{
    public float speed = 0;
    public int damage = 0;

    void Update()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Enemy Base" || collision.gameObject.name == "Player")
        {
            Point.DecreaseEnemyLife(damage);
            Destroy(this.gameObject);
        }

    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Destroy(this.gameObject);
    //}
}
