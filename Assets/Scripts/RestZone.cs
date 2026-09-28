using UnityEngine;

public class RestZone : MonoBehaviour
{
    [SerializeField] float restTime = 5f;
    Vector2 targetPos;
    bool didTrigger = false;
    float timer = 0;
    Rigidbody2D followPoint;
    PlayerMovement player;
    void Start()
    {
        targetPos = new Vector2(transform.position.x + 100f, transform.position.y - 10f);
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
                player.ChangeResting();
                didTrigger = false;
                followPoint.gravityScale = 1;
                GameInput.Instance.EnablePlayerActions();
                
            }
        }
    }
}
