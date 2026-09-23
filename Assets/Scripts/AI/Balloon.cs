
using UnityEngine;

public class Balloon : MonoBehaviour
{
    Vector2 startPos;
    Vector2 endPos;
    float moveTimer = 0;
    void Start()
    {
        startPos = transform.position;
        endPos = new Vector2(startPos.x, startPos.y + Random.Range(3,6));
    }

    void Update()
    {
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
}
