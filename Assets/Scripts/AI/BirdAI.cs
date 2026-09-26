

using UnityEngine;
using UnityEngine.InputSystem.Composites;

public class BirdAI : MonoBehaviour, IAI
{
    Vector3[] bezPoints = new Vector3[7];
    float bezTimer = 0.0f;

    // Vector3 attackTarget;
    // Vector3 attackStart;
    // bool locked;
    // float attackLerp = 0f;
    [Header("Settings")]
    [SerializeField] float turnSpeed = 5f;
    [SerializeField] float attackSpeed = 8f;
    [SerializeField] float bouceForce = 300f;


    private bool attackOver = false;
    private Vector2 direction;
    Vector2 control;
    bool loop = false;
    private Rigidbody2D birdRB;
    int playerLayer = 6;
    bool hitPlayer = false;
    Vector2 contactNormal;
    private Bezier bezier = new Bezier();
    void Start()
    {
        control = transform.position;
        birdRB = GetComponent<Rigidbody2D>();
        bezier.GenerateBezierLoop(control, bezPoints);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == playerLayer)
        {
            attackOver = true;
            hitPlayer = true;
            contactNormal = collision.contacts[0].normal;
            birdRB.AddForce(contactNormal*bouceForce);

        }
    }
    public void Patrol()
    {
        if(attackOver && hitPlayer)
        {
            birdRB.gravityScale = 1;
            return;
        }
        else if(attackOver && !hitPlayer)
        {
            Vector2 dir = new Vector2(-1,0);
            transform.rotation = bezier.Rotate(dir,transform);
            transform.position = Vector2.MoveTowards(transform.position, transform.position + (Vector3) dir, .2f);
            return;
        }
        if(bezTimer < 1.0f)
        {
            bezTimer += Time.deltaTime/1.5f;
            transform.position = bezier.CalculateLoopPoint(bezTimer, loop, bezPoints);

            Vector2 dir = bezier.CalculateLoopDirection(bezTimer, loop, bezPoints);
            transform.rotation = bezier.Rotate(dir,transform);
        }
        else
        {
            loop = !loop;
            bezier.GenerateBezierLoop(control, bezPoints);
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
            transform.rotation = bezier.Rotate(direction,transform);
            if (Mathf.Abs(pos.x) + .7f < Mathf.Abs(playerPos.x))
            {
                attackOver = true;
                control = transform.position;
                // GenerateBezierLoop();
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
        transform.rotation = bezier.Rotate(direction,transform);
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

    
    
}
