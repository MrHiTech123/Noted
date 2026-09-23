using UnityEditor.Timeline.Actions;
using UnityEngine;
// using NAudio.Wave


public class PlayerMovement : MonoBehaviour
{
    [Header("StuffToGrab")]
    [SerializeField] Rigidbody2D playerRB;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovment();
    }

    private void HandleMovment()
    {
        Debug.Log(GameInput.Instance.GetScrollDir());
        if(GameInput.Instance.GetScrollDir() > 0)
        {
            playerRB.AddForceY(100);
        }
        else if(GameInput.Instance.GetScrollDir() < 0)
        {
            playerRB.AddForceY(-100);
        }
    }
}
