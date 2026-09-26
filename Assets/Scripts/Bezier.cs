using UnityEngine;

public class Bezier
{
    public Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;
    
        return (uuu * p0) + (3 * uu * t * p1) + (3 * u * tt * p2) + (ttt * p3);
    }   

    public void GenerateBezierLoop(Vector3 control, Vector3[] bezPoints)
    {
        Vector3 center = control;

        float width = Random.Range(5f, 7f);
        float height = Random.Range(4f, 6f);

        float rotationAngle = Random.Range(-20f, 20f);

        Quaternion rotation = Quaternion.Euler(0, 0, rotationAngle);

        Vector3 leftControl1 = center + (rotation * new Vector3(-width, -height, 0));

        Vector3 leftControl2 = center + (rotation * new Vector3(-width, height, 0));

        Vector3 rightControl1 = center +(rotation * new Vector3(width, -height, 0));

        Vector3 rightControl2 = center +(rotation * new Vector3(width, height, 0));

        bezPoints[0] = center;
        bezPoints[1] = leftControl1;
        bezPoints[2] = leftControl2;
        bezPoints[3] = center;

        bezPoints[4] = rightControl1;
        bezPoints[5] = rightControl2;
        bezPoints[6] = center;
    }
    public Vector3 CalculateLoopPoint(float t, bool loop, Vector3[] bezPoints)
    {
        if (!loop)
        {
            return CalculateBezierPoint(t, bezPoints[0], bezPoints[1], bezPoints[2], bezPoints[3]);
        }
        else
        {
            return CalculateBezierPoint(t, bezPoints[3], bezPoints[4], bezPoints[5], bezPoints[6]);
        }
    }

    public Vector2 CalculateLoopDirection(float t, bool loop, Vector3[] bezPoints)
    {
        Vector3 p0;
        Vector3 p1;
        Vector3 p2;
        Vector3 p3;

        if (!loop)
        {
            p0 = bezPoints[0];
            p1 = bezPoints[1];
            p2 = bezPoints[2];
            p3 = bezPoints[3];
        }
        else
        {
            p0 = bezPoints[3];
            p1 = bezPoints[4];
            p2 = bezPoints[5];
            p3 = bezPoints[6];
        }

        float u = 1 - t;
        Vector2 direction = 3 * u * u * (p1 - p0) + 6 * u * t * (p2 - p1) + 3 * t * t * (p3 - p2);

        return direction.normalized;
    }



    public Quaternion Rotate(Vector2 dir, Transform transform)
    {
        float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg - 180;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        float rotateSpeed = 500f;
        return Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }
}
