using Unity.VisualScripting;
using UnityEngine;

public class HealthPlayer : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite[] sprites;
    [SerializeField] LayerMask floorLayer;
    [SerializeField] Rigidbody2D playerRB;
    [SerializeField] float iFrameMax = .5f;

    int health = 3;
    float iFrameTimer = 0;

    
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
    }
    void Update()
    {
        if (iFrameTimer <= iFrameMax)
        {
            iFrameTimer += Time.deltaTime;
        }
    }
}
