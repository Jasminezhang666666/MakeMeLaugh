using UnityEngine;

public class Van : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Transform leftBoundaryTransform;
    [SerializeField] private Transform rightStartTransform;
    private float posY;

    private void Start()
    {
        posY = transform.position.y;
    }

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (transform.position.x <= leftBoundaryTransform.position.x)
        {
            // Teleport the van to the right
            transform.position = new Vector3(rightStartTransform.position.x, posY, transform.position.z);
        }
    }
}

