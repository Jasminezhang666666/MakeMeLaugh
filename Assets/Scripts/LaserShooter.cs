using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShooter : MonoBehaviour
{

    [SerializeField] private float defDistanceRay = 100;
    public Transform laserPoint;
    public LineRenderer lineRenderer;
    Transform m_transform;

    private void Awake()
    {
        m_transform = GetComponent<Transform>();
    }

    private void Update()
    {
        ShootLaser();
    }

    void ShootLaser()
    {
        if (Physics2D.Raycast(m_transform.position, transform.right))
        {
            RaycastHit2D _hit = Physics2D.Raycast(laserPoint.position, transform.right);
            Draw2DRay(laserPoint.position, _hit.point);
        }
        else
        {
            //Draw2DRay(laserPoint.position, laserPoint.transform.right * defDistanceRay);
            Draw2DRay(laserPoint.position, laserPoint.position + laserPoint.transform.right * defDistanceRay);
        }

    }

    void Draw2DRay(Vector2 startPos, Vector2 endPos)
    {
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }
}
