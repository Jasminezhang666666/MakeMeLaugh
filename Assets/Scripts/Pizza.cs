using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizza : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Pizzabox Gun;
    public Obj obj;
    public Animator animator;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Base" && obj.isLifted == false)
        {
            animator.SetBool("isTrigger", true);
            Gun.canFire = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Base" && obj.isLifted == false)
        {
            animator.SetBool("isTrigger", true);
            Gun.canFire = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Base")
        {
            animator.SetBool("isTrigger", false);
            Gun.canFire = false;
        }
    }
}
