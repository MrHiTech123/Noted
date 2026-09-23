

using UnityEngine;

public class BirdAI : MonoBehaviour, IAI
{
    Vector3[] bezPoints = new Vector3[4];
    float bezTimer = 0.0f;

    // Vector3 attackTarget;
    // Vector3 attackStart;
    // bool locked;
    // float attackLerp = 0f;
    [Header("Settings")]
    [SerializeField] float turnSpeed = 5f;
    [SerializeField] float attackSpeed = 8f;

    private bool attackOver = false;
    private Vector2 direction;
    void Start()
    {
        bezPoints[0] = transform.position;
        bezPoints[1] = new Vector3(RandPoint(bezPoints[0].x),RandPoint(bezPoints[0].y),0);
        bezPoints[2] = new Vector3(RandPoint(bezPoints[1].x),RandPoint(bezPoints[1].y),0);
        bezPoints[3] = new Vector3(RandPoint(bezPoints[2].x),RandPoint(bezPoints[2].y),0);
    }
    public void Patrol()
    {
        if(bezTimer < 1.0f)
        {
            bezTimer += Time.deltaTime/1.4f;
            transform.position = CalculateBezierPoint(bezTimer,bezPoints[0],bezPoints[1],bezPoints[2],bezPoints[3]);
        }
        else
        {
            bezPoints[3] = bezPoints[0];
            bezPoints[0] = transform.position;
            bezPoints[1] = new Vector3(RandPoint(bezPoints[0].x),RandPoint(bezPoints[0].y),0);
            bezPoints[2] = new Vector3(RandPoint(bezPoints[1].x),RandPoint(bezPoints[1].y),0);
            bezTimer = 0;
        }
    }

    public void Attack()
    {
        // if (locked)
        // {
        //     attackLerp += Time.deltaTime;
        //     transform.position = Vector3.Lerp(attackStart,attackTarget, attackLerp);
        // }
        // else
        // {
        //     locked = true;
        //     attackStart = transform.position;
        //     attackTarget = PlayerMovement.Instance.transform.position;
        //     Vector3 lead = PlayerMovement.Instance.GetVelocity().normalized * 3;
        //     attackTarget += lead;
        // }
        if (!attackOver)
        {
            Vector2 pos = (Vector2)transform.position;
            Vector2 playerPos = (Vector2)PlayerMovement.Instance.transform.position;
            Vector2 targetDirection = (playerPos - pos).normalized;
            direction = Vector2.Lerp(direction, targetDirection, turnSpeed * Time.deltaTime).normalized;
            transform.position += (Vector3)(direction * attackSpeed * Time.deltaTime);
            if (Mathf.Abs(pos.x) + .7f < Mathf.Abs(playerPos.x))
            {
                attackOver = true;
                bezPoints[0] = transform.position;
                bezPoints[1] = new Vector3(RandPoint(bezPoints[0].x),RandPoint(bezPoints[0].y),0);
                bezPoints[2] = new Vector3(RandPoint(bezPoints[1].x),RandPoint(bezPoints[1].y),0);
                bezPoints[3] = new Vector3(RandPoint(bezPoints[2].x),RandPoint(bezPoints[2].y),0);
                bezTimer = 0;
            }
        }

        
    }

    public bool CanAttack()
    {
        return !attackOver;
    }

    public void Chase()
    {
        Vector2 direction = PlayerMovement.Instance.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.x,direction.y) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0,0,angle);
    }

    public bool IsInAttackArea()
    {
        if (attackOver) return false;
        float distance = Vector2.Distance(PlayerMovement.Instance.transform.position, transform.position);
        return distance <= 13;
    }

    public bool IsInSearchArea()
    {
        if (attackOver) return false;
        float distance = Vector2.Distance(PlayerMovement.Instance.transform.position, transform.position);
        return distance <= 15;
    }

    Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;
    
        return (uuu * p0) + (3 * uu * t * p1) + (3 * u * tt * p2) + (ttt * p3);
    }   

    float RandPoint(float x)
    {
        float y = (Random.value > .5) ? Random.Range(-2.0f,-1.0f) : Random.Range(1.0f,2.0f);
        return x + y;
    }

    
}
