using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feet : MonoBehaviour
{
    private GameObject attachedObject = null;
    private Vector3 originalObjectWorldScale;

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
            // Apply the inverse scale of the feet to maintain the original world scale of the object
            Vector3 inverseScale = new Vector3(1 / transform.lossyScale.x, 1 / transform.lossyScale.y, 1 / transform.lossyScale.z);
            attachedObject.transform.localScale = Vector3.Scale(originalObjectWorldScale, inverseScale);
        }
    }

    public void AttachObject(GameObject obj)
    {
        attachedObject = obj;
        attachedObject.transform.SetParent(transform); // Set the object as a child of the feet
        originalObjectWorldScale = attachedObject.transform.lossyScale; // Store the original world scale
    }

    public void ReleaseObject()
    {
        if (attachedObject != null)
        {
            attachedObject.transform.SetParent(null); // Detach the object
            attachedObject.transform.localScale = originalObjectWorldScale; // Reset the scale to original world scale
            attachedObject = null;
        }
    }
}


