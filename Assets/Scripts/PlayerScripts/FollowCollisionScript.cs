using UnityEngine;

public class FollowCollisionScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D followRB;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        
        if(collider.gameObject.layer == 7)
        {
            followRB.linearVelocityY = 0;

            Vector2 closestPoint = collider.ClosestPoint(transform.position);

            if (closestPoint.y > transform.position.y)
            {
                followRB.AddForceY(-200);
            }
            else
            {
                followRB.AddForceY(200);
            }
        }
        
    }
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
