using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feet : MonoBehaviour
{
    private GameObject attachedObject = null;
    private Vector3 originalObjectScale; // Store the original scale of the object

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Object") && attachedObject == null)
        {
            AttachObject(collision.gameObject);
        }
    }

    void Update()
    {
        if (attachedObject != null)
        {
            // Continuously reset the local scale of the object to its original scale
            attachedObject.transform.localScale = originalObjectScale;
        }
    }

    public void AttachObject(GameObject obj)
    {
        attachedObject = obj;
        attachedObject.transform.SetParent(transform);
        originalObjectScale = obj.transform.localScale; // Store the original scale
    }

    public void ReleaseObject()
    {
        if (attachedObject != null)
        {
            attachedObject.transform.SetParent(null);
            attachedObject = null;
        }
    }
}

