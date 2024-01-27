using UnityEngine;

public class Obj : MonoBehaviour
{
    public bool isLifted = false;
    public bool isFalling = false;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (isFalling && !collision.gameObject.CompareTag("Feet"))
        {
            print("isFalling set to false.");
            isFalling = false; // Reset isFalling when colliding with other objects

        }
    }

}
