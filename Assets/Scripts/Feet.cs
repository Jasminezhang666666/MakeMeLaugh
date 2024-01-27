using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feet : MonoBehaviour
{
    private GameObject attachedObject = null;
    private Vector3 originalObjectWorldScale;
    private BoxCollider2D feetCollider;

    private void Start()
    {
        feetCollider = GetComponent<BoxCollider2D>();
    }

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

            // Keep the object positioned at the bottom of the feet
            PositionObjectAtFeetBottom();
        }
    }

    public void AttachObject(GameObject obj)
    {
        attachedObject = obj;
        attachedObject.transform.SetParent(transform); // Set the object as a child of the feet
        originalObjectWorldScale = attachedObject.transform.lossyScale; // Store the original world scale

        // Initial positioning of the object at the bottom of the feet
        PositionObjectAtFeetBottom();
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

    private void PositionObjectAtFeetBottom()
    {
        if (feetCollider != null && attachedObject != null)
        {
            // Calculate the bottom position of the feet collider
            float bottomY = transform.position.y - feetCollider.bounds.extents.y;
            float objectHeight = attachedObject.GetComponent<Collider2D>().bounds.size.y;
            Vector3 bottomPosition = new Vector3(transform.position.x,
                                                 bottomY - objectHeight / 2,
                                                 transform.position.z);

            // Position the object at the bottom of the feet collider
            attachedObject.transform.position = bottomPosition;
        }
    }
}