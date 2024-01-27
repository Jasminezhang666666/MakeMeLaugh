using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Items : MonoBehaviour
{
    public bool collected = false;
    private int point;

    public abstract void Launch();
    public int getPoint()
    {
        return point;
    }
}
