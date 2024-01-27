using UnityEngine;

public class Van : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Transform leftBoundaryTransform;
    [SerializeField] private Transform rightStartTransform;  

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (transform.position.x <= leftBoundaryTransform.position.x)
        {
            // Teleport the van to the right start position
            transform.position = new Vector3(rightStartTransform.position.x, rightStartTransform.position.y, transform.position.z);
        }
    }

}
