using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cat : Items
{
    private bool inAir;
    [SerializeField]
    private bool triggered;
    private float catLength;
    private float originalLength;
    private GameObject feet;
    private float feetHeight;
    private Transform target;
    private Rigidbody2D rb;
    private bool timeToHit;

    private Vector3 originalPosition;

    [SerializeField] float RiseHeight = 3f;
    [SerializeField] float speed = 3f;
    [SerializeField] private float rotationDegree = 150.0f;
    [SerializeField] private float rotationSpeed = 45.0f;
    [SerializeField] private float force = 10.0f;


    void Start()
    {
        point = 50;
        originalLength = transform.localScale.y;
        catLength = transform.localScale.y;
        feet = GameObject.Find("Feet").gameObject;
        feetHeight = GameObject.Find("Player").gameObject.GetComponent<Player>().feetHeight;
        target = GameObject.Find("Enemy Base").transform;
        
        rb = GetComponent<Rigidbody2D>();
        timeToHit = false;
}

    void Update()
    {
        if (itemStatus == status.EnterBase)
        {
            if (!triggered)
            {
                originalPosition = transform.position;
                Launch();

            }
            
        }
        if (this.GetComponent<Obj>().isLifted)
        {
            itemStatus = status.Air;
            catLength = (Mathf.Abs(feet.transform.position.y - feetHeight) + originalLength);
            transform.localScale = new Vector3(transform.localScale.x, catLength, transform.localScale.z);
        }
        
    }

    public override void Launch()
    {

        StartCoroutine(Attack());
        triggered = true;
    }

    public override void DestroyThis()
    {
        doDamage();
        Destroy(gameObject);
        Destroy(GameObject.Find("Head").gameObject);

    }
    IEnumerator Attack()
    {
        yield return new WaitForSeconds(2f);
        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition + new Vector3(0, RiseHeight, 0);

        float elapsedTime = 0;
        while (elapsedTime < RiseHeight / speed)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, (elapsedTime * speed) / RiseHeight);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = endPosition;


        //rotation
        float currentRotation = 0;
        float rotationStep;

        while (currentRotation < rotationDegree)
        {
            rotationStep = -rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.forward, rotationStep, Space.Self);
            currentRotation -= rotationStep;
            yield return null;

        }

        Vector2 direction = target.position - transform.position;
        direction.Normalize();
        rb.AddForce(direction * force, ForceMode2D.Impulse);
    


    }
}
