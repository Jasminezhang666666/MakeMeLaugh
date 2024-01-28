using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VGun : MonoBehaviour
{
    public int coolDown;
    public int coolCounter;
    public GameObject bullet;
    public bool canFire;
    public Rigidbody2D rb;
    public float shootRate;
    public AudioSource shootSound;
    public Animator animator;
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
        animator.SetBool("isAttack", true);
        Instantiate(bullet, rb.position, Quaternion.identity);
        shootSound.Play();
        Instantiate(bullet, rb.position, Quaternion.identity);
        yield return new WaitForSeconds(shootRate);
        animator.SetBool("isAttack", false);




    }
}
