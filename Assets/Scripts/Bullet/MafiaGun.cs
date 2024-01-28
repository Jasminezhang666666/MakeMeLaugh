using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MafiaGun : MonoBehaviour
{
    public int coolDown;
    public int coolCounter;
    public GameObject bullet;
    public bool canFire;
    public Rigidbody2D rb;
    public float shootRate;
    public AudioSource shootSound;
    public Animator animator;
    public GameObject gunpoint;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        coolCounter = coolDown;

    }

    // Update is called once per frame
    void Update()
    {
        if (canFire)
        {
            coolCounter--;
        }
        if (coolCounter == 0)
        {
            
            StartCoroutine(MafiaFireGun());
            coolCounter = coolDown;
        }
    }

    IEnumerator MafiaFireGun()
    {
        animator.SetBool("isAttacking",true);
        Instantiate(bullet, new Vector2(gunpoint.transform.position.x, gunpoint.transform.position.y), Quaternion.identity);
        shootSound.Play();
        yield return new WaitForSeconds(shootRate);
        Instantiate(bullet, new Vector2 (gunpoint.transform.position.x, gunpoint.transform.position.y), Quaternion.identity);
        shootSound.Play();
        yield return new WaitForSeconds(shootRate);
        Instantiate(bullet, new Vector2(gunpoint.transform.position.x, gunpoint.transform.position.y), Quaternion.identity);
        shootSound.Play();
        yield return new WaitForSeconds(shootRate);
        Instantiate(bullet, new Vector2(gunpoint.transform.position.x, gunpoint.transform.position.y), Quaternion.identity);
        shootSound.Play();
        yield return new WaitForSeconds(shootRate);
        Instantiate(bullet, new Vector2(gunpoint.transform.position.x, gunpoint.transform.position.y), Quaternion.identity);
        shootSound.Play();

        yield return new WaitForSeconds(shootRate);
        animator.SetBool("isAttacking", false);


    }
}
