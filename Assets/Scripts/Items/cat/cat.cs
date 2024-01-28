using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cat : Items
{
    private bool inAir;
    [SerializeField]
    private bool triggered;
    private float catLength;
    public float originalLength;
    private float originalScale;
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
    public float lengthChanged = 0;
    public GameObject ownHead;
    public GameObject ownFeet;
    private Animator anim;


    void Start()
    {
        point = 50;
        originalLength = GetComponent<Collider2D>().bounds.size.y;
        originalScale = transform.localScale.y;
        catLength = transform.localScale.y;

        feet = GameObject.Find("Feet").gameObject;
        feetHeight = GameObject.Find("Feet").gameObject.GetComponent<Feet>().feetHeight;

        target = GameObject.Find("Enemy Base").transform;
        
        rb = GetComponent<Rigidbody2D>();
        timeToHit = false;

        anim = ownFeet.GetComponent<Animator>();

    }

    void Update()
    {
        feetHeight = GameObject.Find("Feet").gameObject.GetComponent<Feet>().feetHeight;
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
            
            catLength = (float)(Mathf.Abs(feetHeight - feet.transform.position.y) + originalLength);
            float newScaleY = catLength / originalLength * originalScale;
            foreach (Transform child in transform)
            {
                child.localScale = new Vector3(child.localScale.x, 1 / newScaleY, child.localScale.z);
            }
            lengthChanged = Mathf.Abs(feetHeight - feet.transform.position.y);
            transform.localScale = new Vector3(transform.localScale.x, newScaleY, transform.localScale.z);
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

        anim.SetBool("turn", true);
        //rotation
        float currentRotation = 0;
        float rotationStep;

        while (currentRotation < rotationDegree)
        {
            rotationStep = -rotationSpeed * Time.deltaTime;
            //transform.Rotate(Vector3.forward, rotationStep, Space.Self);
            ownHead.transform.Rotate(Vector3.forward, rotationStep, Space.Self);

            currentRotation -= rotationStep;
            yield return null;

        }

        Vector2 direction = target.position - transform.position;
        direction.Normalize();
        rb.AddForce(direction * force, ForceMode2D.Impulse);
    


    }
}
