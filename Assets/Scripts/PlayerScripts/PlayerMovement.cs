using UnityEditor.Timeline.Actions;
using UnityEngine;
// using NAudio.Wave


public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance {get; private set;}

    [Header("StuffToGrab")]
    [SerializeField] Rigidbody2D playerRB;

    [Header("Vars")]
    [SerializeField] float playerVelocity = 7;

    [SerializeField] float scrollTimerMax = 1.5f;
    [SerializeField] float playerForce = 750;
    
    float scrollTimer = 1.5f;

    void Awake()
    {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovment();
    }

    private void HandleMovment()
    {
        if(playerRB.linearVelocityX != playerVelocity)
        {
            playerRB.linearVelocityX = playerVelocity;
        }
        scrollTimer += Time.deltaTime;
        if(scrollTimer >= scrollTimerMax){
            if(GameInput.Instance.GetScrollDir() > 0)
            {
                playerRB.linearVelocityY = 0;
                playerRB.AddForceY(playerForce);
                scrollTimer = 0;
            }
            else if(GameInput.Instance.GetScrollDir() < 0)
            {
                playerRB.linearVelocityY = 0;
                playerRB.AddForceY(-playerForce/2);
                scrollTimer = 0;
            }
        }
    }

    public Vector2 GetVelocity()
    {
        return playerRB.linearVelocity;
    }
}
