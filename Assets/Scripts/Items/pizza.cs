using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pizza : Items
{
    public bool triggered = false;
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
        point = 10;
        target = GameObject.FindGameObjectWithTag("EnemyBase");
        initialPosition = transform.position;
        offset = transform.position.y;
        float xPos = target.transform.position.x - initialPosition.x;
        triggered = true;
        n = new Vector2(speed * Time.fixedDeltaTime, 0);
        loopTime = Mathf.CeilToInt(Mathf.Abs(xPos) / n.x);
    }

    void Update()
    {
        if (itemStatus == status.EnterBase) Launch();
        if (triggered)
        {
            float yPos = offset + Mathf.Sin(Time.time * speed) * amplitude;
            transform.position = new Vector2(transform.position.x + n.x, yPos);

            if (currentLoop > loopTime)
            {
                DestroyThis();
            }
            currentLoop++;
        }

    }  
    public override void Launch()
    {
        triggered = true;
    }

    public override void DestroyThis()
    {
        print(getPoint());
        doDamage();
        Destroy(gameObject);
    }


}
