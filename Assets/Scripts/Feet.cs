using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feet : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Object"))
        {
            collision.gameObject.transform.SetParent(transform);
            collision.gameObject.transform.localPosition = Vector3.zero; // Adjust as necessary
        }
    }
}
