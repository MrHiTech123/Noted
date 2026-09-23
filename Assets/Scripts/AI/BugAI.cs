using UnityEngine;

public class BugAI : MonoBehaviour, IAI
{
    [SerializeField] float chaseSpeed = 8f;
    [SerializeField] float shpereSight = 10f;
    [SerializeField] LayerMask playerLayer;
    Vector2 startPos;
    void Start()
    {
        startPos = transform.position;
    }
    public void Patrol()
    {
        Vector2 offset = new Vector2(2,0);
        transform.RotateAround(startPos - offset, new Vector3(0,0,1) ,1f);
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
        if(collider != null) return true;
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
