using System;
using UnityEngine;

public class RestZone : MonoBehaviour
{
    [SerializeField] float restTime = 5f;
    public EventHandler OnTrigger;
    Vector2 targetPos;
    bool didTrigger = false;
    float timer = 0;
    Rigidbody2D followPoint;
    PlayerMovement player;
    void Start()
    {
        targetPos = new Vector2(transform.position.x + 100f, transform.position.y - 13f);
        HealthPlayer.Instance.OnDie += HealthPlayer_OnDie;
    }

    private void HealthPlayer_OnDie(object sender, System.EventArgs e)
    {
        StopResting();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other != null){
            if(other.gameObject.layer == 6)
            {
                didTrigger = true;
                player = other.gameObject.GetComponent<PlayerMovement>();
                player.ChangeResting();
                followPoint = player.GetFollowPoint().gameObject.GetComponent<Rigidbody2D>();
                GameInput.Instance.DisablePlayerActions();
                followPoint.gravityScale = 0;
                followPoint.linearVelocityY = 0;
                OnTrigger?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    void FixedUpdate()
    {
        if(didTrigger){
            timer += Time.deltaTime;
            float newY = Mathf.MoveTowards(followPoint.position.y, targetPos.y, 5f * Time.deltaTime);
            followPoint.position = new Vector2(followPoint.position.x, newY);
            if(timer >= restTime)
            {
                StopResting();
            }
        }
    }

    public void StopResting()
    {
        player.ChangeResting();
        didTrigger = false;
        followPoint.gravityScale = 1;
        GameInput.Instance.EnablePlayerActions();
    }


}
