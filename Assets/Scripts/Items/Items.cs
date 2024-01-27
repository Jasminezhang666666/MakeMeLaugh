using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Items : MonoBehaviour
{
    public enum status
    {
        Before,
        EnterBase
    }
    public status itemStatus = status.Before;
    private int point;

    public abstract void Launch();
    public int getPoint()
    {
        return point;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerBase")
        {
            Launch();
        }
    }
}
