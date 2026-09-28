using UnityEngine;

public class BugAI : MonoBehaviour, IAI
{
    [SerializeField] float chaseSpeed = 8f;
    [SerializeField] float shpereSight = 10f;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] Transform[] bugs;
    bool locked = false;
    float returnTimer = 0f;
    Vector2 startPos;
    Vector2 currPos;
    float distanceFromStart = 0;
    bool destroy = false;
    Bezier bezier = new Bezier();
    void Start()
    {
        startPos = transform.position;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            foreach (Transform bug in bugs)
            {
                bug.SetParent(null,true);
                
                Rigidbody2D bugRB = bug.GetComponent<Rigidbody2D>();
                bug.GetComponent<BugMovement>().flyOff = true;
                Vector2 direction = Random.insideUnitCircle.normalized;
                bugRB.linearVelocity = direction * 10f;

            }
            destroy = true;
        }
    }

    void Update()
    {
        if (destroy) Destroy(gameObject);
        
    }

    public void Patrol()
    {
        if(!locked){
            Vector2 offset = new Vector2(2,0);
            Quaternion rotation = transform.rotation;
            transform.RotateAround(startPos - offset, new Vector3(0,0,1) ,1f);
            transform.rotation = rotation;
        }
        else
        {
            returnTimer += Time.deltaTime/distanceFromStart;
            transform.position = Vector2.MoveTowards(transform.position, startPos, 4f * Time.deltaTime);
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
