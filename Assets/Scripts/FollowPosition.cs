using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPosition : MonoBehaviour
{
    public Transform parent;
    void Update()
    {
        transform.position = parent.position;
    }
}
