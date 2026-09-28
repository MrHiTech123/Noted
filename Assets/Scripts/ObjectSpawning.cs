using UnityEngine;

public class ObjectSpawning : MonoBehaviour
{
    [SerializeField] private Transform[] pointTransforms = new Transform[4];
    [SerializeField] private Transform[] spawningObjects;
    [SerializeField] private int amountToSpawn1;
    [SerializeField] private int amountToSpawn2;
    [SerializeField] private float distanceToSpawn;
    
    private bool spawn;
    private Vector2[] points = new Vector2[4];
    void Update()
    {
        if(Vector2.Distance(PlayerMovement.Instance.transform.position, transform.position) < distanceToSpawn && !spawn)
        {
            SpawnStuff();
        }
        for (int i = 0; i < 4; i++)
        {
            points[i] = pointTransforms[i].localPosition;
        }
    }

    private void SpawnStuff()
    {
        spawn = true;
        for(int i = 0; i < amountToSpawn1; i++)
            SpawnInTriangle(points[0], points[1], points[2]);
       
        for(int i = 0; i < amountToSpawn2; i++)
            SpawnInTriangle(points[1], points[2], points[3]);

    }

    private void SpawnInTriangle(Vector2 p1, Vector2 p2, Vector2 p3)
    {
        float p = (float) Mathf.Sqrt(Random.Range(0f,1f));
        float q = Random.Range(0f,1f);

        Vector3 randomPoint = (1f - p) * p1 + ( p * (1 - q)) * p2 + (q * p) * p3; 
        Transform obj = Instantiate(spawningObjects[Random.Range(0,spawningObjects.Length)],transform, false);
        obj.localPosition = randomPoint;
    }


}
