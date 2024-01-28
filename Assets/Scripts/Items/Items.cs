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
    protected int point;
    public abstract void Launch();
    public abstract void DestroyThis();
    public int getPoint()
    {
        return point;
    }

    public void doDamage()
    {
        Point.DecreaseEnemyLife(getPoint());
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Base")
        {
            itemStatus = status.EnterBase;
        }
        else if (collision.gameObject.tag == "EnemyBase")
        {
            DestroyThis();
        }
    }


}
