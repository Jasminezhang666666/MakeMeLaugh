using UnityEngine;

public class Feet : MonoBehaviour
{
    private GameObject attachedObject = null;
    private Rigidbody2D objectRigidbody;
    private BoxCollider2D feetCollider;
    public bool HasCollidedWithObject { get; private set; } = false;

    private void Start()
    {
        feetCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (attachedObject != null)
        {
            // Keep the object at the bottom of the feet
            attachedObject.transform.position = transform.position - new Vector3(0, feetCollider.size.y, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Object") && attachedObject == null)
        {
            if(collision.gameObject.GetComponent<Obj>().isFalling)
            {
                Debug.Log("The Object is Falling. Can't be grabbed");
                return;
            }

            HasCollidedWithObject = true;
            attachedObject = collision.gameObject;

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

            attachedObject.transform.SetParent(transform);

            Obj objectScript = attachedObject.GetComponent<Obj>();
            if (objectScript != null)
            {
                objectScript.isLifted = true;
            }
        }
    }

    public void ReleaseObject()
    {
        print("Releasing Object");
        if (attachedObject != null)
        {
            // Re-enable physics, if necessary
            if (objectRigidbody != null)
            {
                objectRigidbody.isKinematic = false;
            }

            // Re-enable collider, if necessary
            Collider2D objectCollider = attachedObject.GetComponent<Collider2D>();
            if (objectCollider != null)
            {
                objectCollider.enabled = true;
            }

            // Detach the object from the feet
            attachedObject.transform.SetParent(null);



            Obj objectScript = attachedObject.GetComponent<Obj>();
            if (objectScript != null)
            {
                objectScript.isLifted = false;
                objectScript.isFalling = true;
            }

            // Reset the attached object and its Rigidbody
            attachedObject = null;
            objectRigidbody = null;

        }
    }

    public bool IsObjectAttached()
    {
        return attachedObject != null;
    }
}