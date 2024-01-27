using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class transform : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public MafiaGun Gun;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Base")
        {
            spriteRenderer.color = Color.red;
            Gun.canFire = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Base")
        {
            spriteRenderer.color = Color.red;
            Gun.canFire = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Base")
        {
            spriteRenderer.color = Color.white;
            Gun.canFire = false;
        }
    }
}
