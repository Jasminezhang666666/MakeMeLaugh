using UnityEngine;
using UnityEngine.U2D;

public class CameraZoomTrigger : MonoBehaviour
{
    [SerializeField] private int zoomInValue = 2;

    private int zoomedInPixelsPerUnit;
    private int zoomedOutPixelsPerUnit;

    PixelPerfectCamera pixelPerfectCamera;

    private void Start()
    {
        pixelPerfectCamera = Camera.main.GetComponent<PixelPerfectCamera>();
        zoomedOutPixelsPerUnit = pixelPerfectCamera.assetsPPU;
        zoomedInPixelsPerUnit = zoomedOutPixelsPerUnit + zoomInValue;
        if (pixelPerfectCamera == null)
        {
            Debug.LogError("Pixel Perfect Camera component not found on the main camera.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player") && pixelPerfectCamera != null)
        {
            print("Player inside cam");
            pixelPerfectCamera.assetsPPU = zoomedInPixelsPerUnit;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && pixelPerfectCamera != null)
        {
            pixelPerfectCamera.assetsPPU = zoomedOutPixelsPerUnit;
        }
    }
}
