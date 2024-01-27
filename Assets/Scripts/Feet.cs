using UnityEngine;

public class Feet : MonoBehaviour
{
    private GameObject attachedObject = null;
    private Rigidbody2D objectRigidbody;
    private Vector3 originalObjectWorldScale;
    private BoxCollider2D feetCollider;
    public bool HasCollidedWithObject { get; private set; } = false;


    private void Start()
    {
        feetCollider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Object") && attachedObject == null)
        {
            HasCollidedWithObject = true;
            AttachObject(collision.gameObject);
        }
    }

    void FixedUpdate()
    {
        if (attachedObject != null)
        {
            Vector3 inverseScale = new Vector3(1 / transform.lossyScale.x, 1 / transform.lossyScale.y, 1 / transform.lossyScale.z);
            attachedObject.transform.localScale = Vector3.Scale(originalObjectWorldScale, inverseScale);
            PositionObjectAtFeetBottom();
        }
    }

    public void AttachObject(GameObject obj)
    {
        attachedObject = obj;
        attachedObject.transform.SetParent(transform);
        originalObjectWorldScale = attachedObject.transform.lossyScale;

        objectRigidbody = attachedObject.GetComponent<Rigidbody2D>();
        if (objectRigidbody != null)
        {
            objectRigidbody.isKinematic = true;
        }

        Collider2D objectCollider = attachedObject.GetComponent<Collider2D>();
        if (objectCollider != null)
        {
            objectCollider.enabled = false;
        }

        PositionObjectAtFeetBottom();
    }

    public void ReleaseObject()
    {
        if (attachedObject != null)
        {
            attachedObject.transform.SetParent(null);
            attachedObject.transform.localScale = originalObjectWorldScale;

            if (objectRigidbody != null)
            {
                objectRigidbody.isKinematic = false;
            }

            Collider2D objectCollider = attachedObject.GetComponent<Collider2D>();
            if (objectCollider != null)
            {
                objectCollider.enabled = true;
            }

            attachedObject = null;
            objectRigidbody = null;
        }
    }

    private void PositionObjectAtFeetBottom()
    {
        if (feetCollider != null && attachedObject != null)
        {
            float bottomY = transform.position.y - feetCollider.bounds.extents.y;
            float objectHeight = attachedObject.GetComponent<Collider2D>().bounds.size.y;
            Vector3 bottomPosition = new Vector3(transform.position.x,
                                                 bottomY - objectHeight / 2,
                                                 transform.position.z);

            if (objectRigidbody != null)
            {
                objectRigidbody.MovePosition(bottomPosition);
            }
            else
            {
                attachedObject.transform.position = bottomPosition;
            }
        }
    }

    public bool IsObjectAttached()
    {
        return attachedObject != null;
    }

    public void ResetCollisionFlag()
    {
        HasCollidedWithObject = false;
    }
}
