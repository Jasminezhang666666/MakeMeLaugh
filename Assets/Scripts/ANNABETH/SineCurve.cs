using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineCurve : MonoBehaviour
{
    private Transform initialPosition;
    public GameObject target;
    private Transform targetPosition;

    public float speed = 5;
    public float amplitude = 5;
    public float offset = 0.5f;

    private Vector2 n;
    private int loopTime = -1;
    private int currentLoop = 0;

    void Start()
    {
        initialPosition.position = transform.position;
        offset = transform.position.z;
        float xPos = targetPosition.position.x - initialPosition.position.x;
        float yPos = targetPosition.position.y - initialPosition.position.y;
        n = new Vector2(xPos, yPos).normalized * 0.02f;

        loopTime = (int)Mathf.Ceil(Mathf.Abs(xPos) / Mathf.Abs(n.x));
    }

    void Update()
    {
        float zPos = offset + Mathf.Sin(Time.time * speed) * amplitude;
        transform.position = new Vector3(transform.position.x + n.x, transform.position.y + n.y, zPos);

        if (currentLoop > loopTime)
        {
            transform.position = initialPosition.position;
            currentLoop = 0;
        }

       
        currentLoop++;

        //if(transform.position.x <= end.position.x)
        //{
        //    transform.position = start.position;
        //}
    }
}
