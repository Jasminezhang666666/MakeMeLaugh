using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feet : MonoBehaviour
{
    private GameObject attachedObject = null;
    private Vector3 offset; // Offset between the feet and the object

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Object") && attachedObject == null)
        {
            attachedObject = collision.gameObject;
            // Calculate and store the offset between the feet and the object
            offset = attachedObject.transform.position - transform.position;
        }
    }

    void Update()
    {
        if (attachedObject != null)
        {
            // Update the object's position to follow the feet, using the stored offset
            attachedObject.transform.position = transform.position + offset;
        }
    }

    public void ReleaseObject()
    {
        // Release the reference to the object
        attachedObject = null;
    }
}
