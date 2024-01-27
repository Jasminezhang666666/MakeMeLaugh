using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pizza : Items
{
    private Vector2 initialPosition;
    public GameObject target;

    [SerializeField]
    private float speed = 2;
    [SerializeField]
    private float amplitude = 5;
    private float offset;

    private Vector2 n;
    private int loopTime = 1;
    private int currentLoop = 0;

    void Start()
    {
        initialPosition = transform.position;
        offset = transform.position.y;
        float xPos = target.transform.position.x - initialPosition.x;

        n = new Vector2(speed * Time.fixedDeltaTime, 0);
        loopTime = Mathf.CeilToInt(Mathf.Abs(xPos) / n.x);
    }

    void Update()
    {
        if (collected)
        {
            float yPos = offset + Mathf.Sin(Time.time * speed) * amplitude;
            transform.position = new Vector2(transform.position.x + n.x, yPos);

            if (currentLoop > loopTime)
            {
                Destroy(this.gameObject);
            }
            currentLoop++;
        }

    }  
    public override void Launch()
    {
        collected = true;
    }
}