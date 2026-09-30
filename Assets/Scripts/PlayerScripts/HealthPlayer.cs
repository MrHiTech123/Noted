using System;
using UnityEngine;
using UnityEngine.SceneManagement;   

public class HealthPlayer : MonoBehaviour
{
    public static HealthPlayer Instance {get; private set;}
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite[] sprites;
    [SerializeField] LayerMask floorLayer;
    [SerializeField] Rigidbody2D playerRB;
    [SerializeField] float iFrameMax = .5f;
    [SerializeField] float respawnTimeMax = .75f;

    int health = 3;
    float iFrameTimer = 0;
    public EventHandler OnDie;
    bool didDie = false;
    float respawnTimer;

    void Awake()
    {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(this);
        }
        respawnTimer = respawnTimeMax;
    }
    private void OnCollisionEnter2D(Collision2D collider)
    {
        if(health > 0 && iFrameTimer > iFrameMax){
            health--;
            spriteRenderer.sprite = sprites[health];
            if(collider.gameObject.layer == 7)
            {
                playerRB.linearVelocityY = 0;
                playerRB.AddForceY(350);
            }
            iFrameTimer = 0;
        }
        if(health <= 0)
        {
            didDie = true;
            GameInput.Instance.DisablePlayerActions();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 9){
            if(health > 0 && iFrameTimer > iFrameMax){
                health--;
                spriteRenderer.sprite = sprites[health];
                iFrameTimer = 0;
            }
            if(health <= 0)
            {
                didDie = true;
                GameInput.Instance.DisablePlayerActions();
            }
        }
    }
    void Update()
    {
        if (iFrameTimer <= iFrameMax)
        {
            iFrameTimer += Time.deltaTime;
        }
        if (didDie)
        {
            respawnTimer -= Time.deltaTime;
            if(respawnTimer <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                health = 3;
                spriteRenderer.sprite = sprites[health];
                didDie = false;
                respawnTimer = respawnTimeMax;
                GameInput.Instance.EnablePlayerActions();
            }
        }
    }
}
