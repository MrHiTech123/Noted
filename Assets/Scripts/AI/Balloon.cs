
using UnityEngine;

public class Balloon : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite pop;

    Vector2 startPos;
    Vector2 endPos;
    float moveTimer = 0;
    bool poped = false;
    float popedTimer = 0;
    float randomStartTimer = 0;
    float randomStartTime;
    void Start()
    {
        randomStartTime  = Random.Range(0f,2f);
        startPos = transform.position;
        endPos = new Vector2(startPos.x, startPos.y + Random.Range(3,6));
    }

    void Update()
    {
        randomStartTimer += Time.deltaTime;
        if(randomStartTimer <= randomStartTime) return;
        if (poped)
        {
            popedTimer += Time.deltaTime;
            if(popedTimer >= .5f)
            {
                Destroy(gameObject);
            }
            return;
        }
        moveTimer += Time.deltaTime;
        transform.position = Vector2.Lerp(startPos, endPos, moveTimer);
        if(moveTimer >= 1)
        {
            Vector2 temp = startPos;
            startPos = endPos;
            endPos = temp;
            moveTimer = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            spriteRenderer.sprite = pop;
            poped = true;    
        }
    }
}
