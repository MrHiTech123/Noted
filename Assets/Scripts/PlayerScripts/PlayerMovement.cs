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

        if(GameInput.Instance.GetScrollDir() > 0)
        {
            playerRB.AddForceY(100);
        }
        else if(GameInput.Instance.GetScrollDir() < 0)
        {
            playerRB.AddForceY(-100);
        }
    }

    public Vector2 GetVelocity()
    {
        return playerRB.linearVelocity;
    }
}
