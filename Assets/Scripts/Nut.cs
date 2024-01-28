using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Nut : MonoBehaviour
{
    public Rigidbody2D rb;
    public int PlifeTime;
    public int lifeTime;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = new Vector2 (Random.Range(-40,-45), Random.Range(-30,30));
    }


    private void Update()
    {
        PlifeTime--;
        lifeTime--;

        if (lifeTime < 0)
        { 
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet" && PlifeTime < 0)
        {
            
            Instantiate(collision.gameObject.GetComponent<BulletDamage>().deathEffect, collision.gameObject.GetComponent<Rigidbody2D>().position, Quaternion.identity);
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet" && PlifeTime < 0)
        {

            Instantiate(collision.gameObject.GetComponent<BulletDamage>().deathEffect, collision.gameObject.GetComponent<Rigidbody2D>().position, Quaternion.identity);
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
