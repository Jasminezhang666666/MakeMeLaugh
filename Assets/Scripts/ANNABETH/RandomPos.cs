using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPos : MonoBehaviour
{
    public float speed = 1.0f; 
    public float targetRadius = 1.0f;
    public float noiseScale = 5.0f;

    public GameObject target;
    private Vector3 targetPosition;
    private Vector3 initialPosition;
    private Vector3 perlinMotion;
    private Vector3 targetDirection;
    private Vector3 changePosition;

    //private Vector3 velocity = Vector3.zero;

    private int count;
    private bool change;
    //float smooth;

    void Start()
    {
        targetPosition = target.transform.position;
        initialPosition = transform.position;
        changePosition = targetDirection + perlinMotion;
        count = 0;
        //smooth = 0.3f;
    }

    void Update()
    {

        count++;
        SetPerlinNoise();

        targetDirection = (targetPosition - transform.position).normalized;
 
        if (count == 100)
        {
            change = ChangeDirection();
            count = 0;
        }

        if (change)
        {
            changePosition = targetDirection - perlinMotion;
        }
        else
        {
            changePosition = targetDirection + perlinMotion;
        }

        if (Vector3.Distance(transform.position, targetPosition) > targetRadius)
        {
            //transform.position = Vector3.SmoothDamp(transform.position, changePosition, ref velocity, smooth);
            transform.Translate((changePosition) * speed * Time.deltaTime);
        }
        
        else
        {
            transform.position = initialPosition;
        }
    }

    void SetPerlinNoise()
    {
        perlinMotion = new Vector3(
        Mathf.PerlinNoise(Time.time, 0) * noiseScale - noiseScale / 2.0f,
        Mathf.PerlinNoise(0, Time.time) * noiseScale - noiseScale / 2.0f,
        Mathf.PerlinNoise(Time.time, Time.time) * noiseScale - noiseScale / 2.0f
        );
    }

    //use to reduce biase
    private bool ChangeDirection()
    {
        return (Random.value > 0.5);
    }

}
