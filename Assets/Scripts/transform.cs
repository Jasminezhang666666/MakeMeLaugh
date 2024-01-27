using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transform : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public MafiaGun mafiaGun;
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
            mafiaGun.canFire = true;
        }
        else
        {
            spriteRenderer.color = Color.white;
            mafiaGun.canFire = false;
        }
        
        
    }
}
