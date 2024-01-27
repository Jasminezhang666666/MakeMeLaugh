using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantScaler : MonoBehaviour
{
    private Vector3 original;
    private Transform parentTransform;
    void Start()
    {
        //original = transform.localScale;
        parentTransform = transform.parent;
        original = new Vector3(
            transform.localScale.x / parentTransform.localScale.x,
            transform.localScale.y / parentTransform.localScale.y,
            transform.localScale.z / parentTransform.localScale.z
        );
    }

    void Update()
    {
        transform.localScale = new Vector3(
                original.x * parentTransform.localScale.x,
                original.y * parentTransform.localScale.y,
                original.z * parentTransform.localScale.z
                );
        
    }
}
