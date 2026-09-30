using UnityEngine;

public class KeepBackground : MonoBehaviour
{
    [Range(0f, 1f)]
    public float parallaxFactor;

    [Range(0f, 1f)]
    public float verticalParallaxFactor = 1f;

    private float length;
    private Transform cam;

    private float cameraStartX;
    private float startX;

    private float verticalOffset;

    void Start()
    {
        length = GetComponent<SpriteRenderer>().bounds.size.x;

        cam = PlayerMovement.Instance.cam.transform;

        cameraStartX = cam.position.x;
        startX = transform.position.x;

        // Remember the original distance between camera and background
        verticalOffset = transform.position.y - cam.position.y;
    }

    void LateUpdate()
    {
        float cameraMovementX = cam.position.x - cameraStartX;

        transform.position = new Vector3(
            startX + cameraMovementX * parallaxFactor,
            cam.position.y * verticalParallaxFactor
                + verticalOffset * (1f - verticalParallaxFactor),
            transform.position.z
        );

        // Horizontal looping
        if (cam.position.x - transform.position.x > length)
        {
            startX += length * 2f;
        }
        else if (transform.position.x - cam.position.x > length)
        {
            startX -= length * 2f;
        }
    }
}