using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chair : Items
{
    [SerializeField]
    private bool triggered;
    public GameObject bullet;
    private GameObject person;

    [SerializeField] private float bulletSpeed = 2f;
    [SerializeField] private float launchInterval  = 2f;
    void Start()
    {
        person = transform.Find("Person").gameObject;
        bullet.GetComponent<StraightLine>().speed = bulletSpeed;

    }

    public void Update()
    {
        if (itemStatus == status.EnterBase) Launch();
    }
    public override void Launch()
    {
        person.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.name == "Person")
        {
            triggered = true;
            StartCoroutine(BulletShot(launchInterval));
        }
        

    }

    IEnumerator BulletShot(float timeInterval)
    {
        while (triggered)
        {
            Instantiate(bullet, this.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(timeInterval);
        }

    }

    public override void Destroy()
    {
    }


}
