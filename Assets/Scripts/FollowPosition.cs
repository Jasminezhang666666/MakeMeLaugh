using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPosition : MonoBehaviour
{
    [SerializeField]
    private bool extend = false;
    public Transform parent;
    private void Start()
    {
        if(gameObject.name == "Head")
        {
            extend = false;
        }
        else
        {
            extend = true;
        }
    }
    void Update()
    {
        if (!extend)
        {
            transform.position = parent.position;
        }
        else
        {
            transform.position = parent.position - new Vector3(0, parent.GetComponent<cat>().lengthChanged, 0);
        }
        
    }

    public void StartMoving()
    {
        extend = false;
    }
}
