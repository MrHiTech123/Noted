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
	[SerializeField] MoveWithMicLoudness playerFollowPointAudio;
    [SerializeField] Transform planeTransform;


    [Header("Vars")]
    [SerializeField] float playerVelocity = 7;

    [SerializeField] float scrollTimerMax = 1.5f;
    [SerializeField] float playerForce = 750;
    [SerializeField] float vertFollowSpeed = 3f;
    
    float scrollTimer = 1.5f;
    Bezier bezier;
    bool isResting = false;
    bool isFirstUpdate = true;

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
        DontDestroyOnLoad(gameObject);
        bezier = new Bezier();
        GameInput.Instance.EnablePlayerActions();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isFirstUpdate)
        {
            isFirstUpdate = false;
            playerFollowPointRB.gravityScale = 1;
            GameInput.Instance.EnablePlayerActions();
        }
        HandleMovement();
    }
	
	private void HandleMovement()
	{
		HandleHorizontalMovement();
		Follow(playerRB, playerFollowPoint);
		switch (CurrentInputMode.value)
		{
			case InputMode.SCROLL_WHEEL:
				HandleMovementScrollWheel();
				HandleMovementWhistling();
				break;
			case InputMode.AUDIO:
				HandleMovementWhistling();
				break;
		}
	}
	
	private void HandleHorizontalMovement()
	{
		playerFollowPointRB.linearVelocityX = isResting ? 10f : playerVelocity;
		
		
	}
	
	private void Follow(Rigidbody2D follower, Transform toFollow)
	{
		Vector2 direction = toFollow.position - (Vector3)follower.position;
        float verticalVelocity = direction.y * vertFollowSpeed;
        follower.linearVelocity = new Vector2(playerFollowPointRB.linearVelocityX,verticalVelocity);
		planeTransform.rotation = bezier.Rotate(-direction, planeTransform);
	}
	private void HandleMovementWhistling()
	{
		// float yOffset = MoveWithMicLoudness.DesiredYCoordinate();
		
		// playerFollowPointAudio.transform.localPosition = new Vector2(10, yOffset);
		
		
		// Vector2 direction = playerFollowPointAudio.transform.position - (Vector3)playerRB.position;
		// float verticalVelocity = direction.y * vertFollowSpeed;
		// playerRB.linearVelocity = new Vector2(playerFollowPointRB.linearVelocityX, verticalVelocity);
		
		Follow(playerFollowPointRB, playerFollowPointAudio.transform);
	}
	
    private void HandleMovementScrollWheel()
    {
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

    public Transform GetFollowPoint()
    {
        return playerFollowPoint;
    }

    public void ChangeResting()
    {
        isResting = !isResting;
    }

   
}
