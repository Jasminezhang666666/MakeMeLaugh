using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Boxgun Gun;
    public Obj obj;
    public Animator animator;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Base" && obj.isLifted == false)
        {
            animator.SetBool("isRaise", true);
            Gun.canFire = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Base" && obj.isLifted == false)
        {
            animator.SetBool("isRaise", true);
            Gun.canFire = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Base")
        {
            animator.SetBool("isRaise", false);
            Gun.canFire = false;
        }
    }

    //private void Update()
    //{
    //    if (obj.isLifted == true)
    //    {
    //        animator.SetBool("isRaise", true);
    //    }
    //    else
    //    {
    //        animator.SetBool("isRaise", false);
    //    }
    //}
}
