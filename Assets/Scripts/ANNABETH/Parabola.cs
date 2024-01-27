using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Parabola : MonoBehaviour
{
    protected float anim;
    public GameObject target;
    private Vector3 targetPosition;

    private float fixedRotation = 0;
    private Vector3 eulerAngles;



    void Start()
    {
        targetPosition = gameObject.transform.position;
        eulerAngles = transform.eulerAngles;
    }

    void Update()
    {
        anim += Time.deltaTime;
        anim = anim % 5f;

        transform.position = BrachistochroneCalculate(targetPosition, target.transform.position, (anim/5));
        //transform.eulerAngles = new Vector3(fixedRotation, fixedRotation, eulerAngles.z);
    }

    public static Vector3 BrachistochroneCalculate(Vector3 start, Vector3 end, float t)
    {
        var h = Mathf.Abs(end.y - start.y);
        var d = Mathf.Abs(end.x - start.x);
        Func<float, float> f = x => ( h/2 *(1 - Mathf.Cos((Mathf.PI * x) / d)));

        var mid = Vector3.Lerp(start, end, t);

        return new Vector3(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
    }

    //public static Vector3 ParabolaCalculate(Vector3 start, Vector3 end, float height, float t, float a, float b)
    //{
    //    Func<float, float> f = x => (a * x * x - b * x);

    //    var mid = Vector3.Lerp(start, end, t);

    //    return new Vector3(mid.x, f(5 * t) + Mathf.Lerp(start.y, end.y, t), mid.z);



    //    //public static Vector3 ParabolaCalculate(Vector3 start, Vector3 end, float height, float t)
    //    //{
    //    //    Func<float, float> f = x => (-4 * height * x * x + 4 * height * x);

    //    //    var mid = Vector3.Lerp(start, end, t);

    //    //    return new Vector3(mid.x, -f(t) - Mathf.Lerp(start.y, end.y, t), mid.z);
    //    //}

    //}
}
