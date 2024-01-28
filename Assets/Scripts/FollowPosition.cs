using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPosition : MonoBehaviour
{
    [SerializeField]
    private bool extend = false;
    public Transform parent;

    private Vector3 originalScale;
    private void Start()
    {
        originalScale = transform.localScale;
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
            transform.position = parent.position - new Vector3(0, parent.GetComponent<cat>().lengthChanged + parent.GetComponent<cat>().originalLength, 0);
        }
        transform.localScale = originalScale;
    }

    public void StartMoving()
    {
        extend = false;
    }
}
