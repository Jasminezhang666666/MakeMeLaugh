using UnityEngine;

public class Obj : MonoBehaviour
{
    public bool isLifted = false;
    public bool isFalling = false;

    private void Start()
    {
        UpdateCollisionSettings();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            cancelParent();
            return;
        }

        if (isFalling && !collision.gameObject.CompareTag("Feet"))
        {
            isFalling = false; // Reset isFalling when colliding with other objects
        }
    }

    private void UpdateCollisionSettings()
    {
        // If the Obj is on a Van, ignore collisions with the default layer
        if (transform.parent != null && transform.parent.gameObject.layer == LayerMask.NameToLayer("Van"))
        {
            Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Default"), true);
        }
        else
        {
            print("Not ignoring default layer anymore.");
            // Reset collision settings to default when not a child of Van
            Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Default"), false);
        }
    }

    public void cancelParent()
    {
        transform.parent = null;
        UpdateCollisionSettings(); 
    }
}
