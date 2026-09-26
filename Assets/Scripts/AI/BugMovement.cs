using UnityEngine;

public class BugMovement : MonoBehaviour
{
    [SerializeField] float beeRange = .7f;
    Bezier bezier = new Bezier();
    Vector3[] bezPoints = new Vector3[4];
    Vector3 startPos;
    float bezTimer = 0;

    void Start()
    {
        startPos = transform.localPosition;
        bezPoints[0] = startPos;
        bezPoints[1] = new Vector3(Random.Range(-beeRange,beeRange) + bezPoints[0].x,Random.Range(-beeRange,beeRange) + bezPoints[0].y,0);   
        bezPoints[2] = new Vector3(Random.Range(-beeRange,beeRange) + bezPoints[1].x,Random.Range(-beeRange,beeRange) + bezPoints[1].y,0);   
        bezPoints[3] = new Vector3(Random.Range(-beeRange,beeRange) + bezPoints[2].x,Random.Range(-beeRange,beeRange) + bezPoints[2].y,0);   

    }


    void Update()
    {
        bezTimer += Time.deltaTime;
        transform.localPosition = bezier.CalculateBezierPoint(bezTimer, bezPoints[0],bezPoints[1],bezPoints[2],bezPoints[3]);
        if(bezTimer >= 1)
        {
            bezTimer = 0;
        bezPoints[0] = bezPoints[3];
        bezPoints[1] = new Vector3(Random.Range(-beeRange,beeRange) + bezPoints[0].x,Random.Range(-beeRange,beeRange) + bezPoints[0].y,0);   
        bezPoints[2] = new Vector3(Random.Range(-beeRange,beeRange) + bezPoints[1].x,Random.Range(-beeRange,beeRange) + bezPoints[1].y,0);   
        bezPoints[3] = startPos;   
        }
    }
}
