using UnityEngine;

public class DespawnDistance : MonoBehaviour
{
    [SerializeField] private int despawnDistance = 30;
    void Update()
    {
        if (transform.position.x < PlayerMovement.Instance.transform.position.x - despawnDistance)
        {
            Destroy(gameObject);
        }
    }
}
