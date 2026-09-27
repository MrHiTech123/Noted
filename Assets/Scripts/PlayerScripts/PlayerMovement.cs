using UnityEditor.Timeline.Actions;
using UnityEngine;
// using NAudio.Wave


public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance {get; private set;}

    [Header("StuffToGrab")]
    [SerializeField] Rigidbody2D playerRB;
    [SerializeField] Transform playerFollowPoint;
    [SerializeField] Rigidbody2D playerFollowPointRB;
    [SerializeField] Transform planeTransform;


    [Header("Vars")]
    [SerializeField] float playerVelocity = 7;

    [SerializeField] float scrollTimerMax = 1.5f;
    [SerializeField] float playerForce = 750;
    [SerializeField] float vertFollowSpeed = 3f;
    
    float scrollTimer = 1.5f;

    Bezier bezier;

    void Awake()
    {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(this);
        }
    }
    void Start()
    {
        bezier = new Bezier();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovment();
    }

    private void HandleMovment()
    {
        if(playerFollowPointRB.linearVelocityX != playerVelocity)
        {
            playerFollowPointRB.linearVelocityX = playerVelocity;
        }
        Vector2 direction = playerFollowPoint.position - (Vector3)playerRB.position;
        float verticalVelocity = direction.y * vertFollowSpeed;
        playerRB.linearVelocity = new Vector2(playerVelocity,verticalVelocity);
        planeTransform.rotation = bezier.Rotate(-direction, planeTransform);
        scrollTimer += Time.deltaTime;
        if(scrollTimer >= scrollTimerMax){
            if(GameInput.Instance.GetScrollDir() > 0)
            {
                playerFollowPointRB.linearVelocityY = 0;
                playerFollowPointRB.AddForceY(playerForce);
                scrollTimer = 0;
            }
            else if(GameInput.Instance.GetScrollDir() < 0)
            {
                playerFollowPointRB.linearVelocityY = 0;
                playerFollowPointRB.AddForceY(-playerForce/1.5f);
                scrollTimer = 0;
            }
        }
    }

    public Vector2 GetVelocity()
    {
        return playerRB.linearVelocity;
    }
}
