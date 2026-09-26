using UnityEngine;

public class FollowCollisionScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D followRB;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        
            if(collider.gameObject.layer == 7)
            {
                followRB.linearVelocityY = 0;
                followRB.AddForceY(200);
            }
       
        
    }
}
