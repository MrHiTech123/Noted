using UnityEngine;

public class BugAI : MonoBehaviour, IAI
{
    [SerializeField] float chaseSpeed = 8f;
    [SerializeField] float shpereSight = 10f;
    [SerializeField] LayerMask playerLayer;
    bool locked = false;
    float returnTimer = 0f;
    Vector2 startPos;
    Vector2 currPos;
    float distanceFromStart = 0;
    void Start()
    {
        startPos = transform.position;
    }
    public void Patrol()
    {
        if(!locked){
            Vector2 offset = new Vector2(2,0);
            transform.RotateAround(startPos - offset, new Vector3(0,0,1) ,1f);
        }
        else
        {
            returnTimer += Time.deltaTime/distanceFromStart;
            transform.position = Vector2.Lerp(transform.position, startPos, 4f * Time.deltaTime);
            if(returnTimer >= 1)
            {
                locked = false;
                returnTimer = 0;
            }
        }
    }
    public void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, PlayerMovement.Instance.transform.position, chaseSpeed * Time.deltaTime);
        // float distance = Vector2.Distance(transform.position,PlayerMovement.Instance.transform.position);
    }
    public void Attack()
    {
        
    }
    public bool IsInSearchArea()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, shpereSight, playerLayer);
        if(collider != null) {
            locked = true;
            currPos = transform.position;
            distanceFromStart = Vector2.Distance(currPos, startPos);
            return true;
        }
        return false;
    }
    public bool IsInAttackArea()
    {
        return false;
    }
    public bool CanAttack()
    {
        return false;
    }
}
