using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [Range(0f, 1f)]
    public float parallaxFactor;

    private Vector3 startPos;
    private float length;
    private Transform cam;
    private float cameraStartX;

    void Start()
    {
        startPos = transform.position;
        length = GetComponent<SpriteRenderer>().bounds.size.x;

        cam = PlayerMovement.Instance.cam.transform;
        cameraStartX = cam.position.x;
    }

    void LateUpdate()
    {
        float cameraMovement = cam.position.x - cameraStartX;

        transform.position = new Vector3(
            startPos.x + cameraMovement * parallaxFactor,
            transform.position.y,
            transform.position.z
        );
    }
}