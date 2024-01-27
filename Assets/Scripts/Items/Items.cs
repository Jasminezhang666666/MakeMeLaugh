using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Items : MonoBehaviour
{
    public enum status
    {
        Before,
        Air,
        EnterBase
    }
    public status itemStatus = status.Before;
    private int point;
    public abstract void Launch();
    public abstract void Destroy();
    public int getPoint()
    {
        return point;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBase")
        {
            itemStatus = status.EnterBase;
        }
        else if (collision.gameObject.tag == "EnemyBase")
        {
            Destroy();
        }
    }


}
