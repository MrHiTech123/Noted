using UnityEngine;

public class DespawnDistance : MonoBehaviour
{
    [SerializeField] private int despawnDistance = 30;

    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (transform.position.x < PlayerMovement.Instance.transform.position.x - despawnDistance)
        {
            audioSource.Play();
            Destroy(gameObject);
        }
    }
}
