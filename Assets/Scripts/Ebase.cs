using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ebase : MonoBehaviour
{
    public int Health;
    public int MaxHealth;
    public float score;
    public float mult;
    public GameObject nut;
    public int nutCooldown;
    public int nutTimeMult;

    public int nutNumber;
    public GameObject shooter1;
    public GameObject shooter2;
    public GameObject shooter3;
    public GameObject shooter4;
    public GameObject shooter5;

    public TMP_Text Score;
    public TMP_Text Mult;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (nutCooldown > 0)
        {
            nutCooldown -= nutTimeMult;
        }
        
        if (Health <= 0)
        {
            mult += 0.5f;
            Health = 1000;
            nutTimeMult += 1;
        }

        if (nutCooldown <= 0)
        {
            nutNumber = Random.Range(0, 4);
            if (nutNumber == 0)
            {
                Instantiate(nut, shooter1.transform.position, Quaternion.identity);
            }
            if (nutNumber == 1)
            {
                Instantiate(nut, shooter2.transform.position, Quaternion.identity);
            }
            if (nutNumber == 2)
            {
                Instantiate(nut, shooter3.transform.position, Quaternion.identity);
            }
            if (nutNumber == 3)
            {
                Instantiate(nut, shooter4.transform.position, Quaternion.identity);
            }
            if (nutNumber == 4)
            {
                Instantiate(nut, shooter5.transform.position, Quaternion.identity);
            }
            nutCooldown = 500;
        }
        Score.text = "Score:" + score.ToString();
        Mult.text = "Mult:" + mult.ToString() + "X";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            score += (collision.gameObject.GetComponent<BulletDamage>().damage * mult) /10;
            Health -= collision.gameObject.GetComponent<BulletDamage>().damage;
            Instantiate(collision.gameObject.GetComponent<BulletDamage>().deathEffect, collision.gameObject.GetComponent<Rigidbody2D>().position, Quaternion.identity);
            Destroy(collision.gameObject);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            score += (collision.gameObject.GetComponent<BulletDamage>().damage * mult )/10;
            Health -= collision.gameObject.GetComponent<BulletDamage>().damage;
            Instantiate(collision.gameObject.GetComponent<BulletDamage>().deathEffect, collision.gameObject.GetComponent<Rigidbody2D>().position, Quaternion.identity);
            Destroy(collision.gameObject);
        }
    }
}
