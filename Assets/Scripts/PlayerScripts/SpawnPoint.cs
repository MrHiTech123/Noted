using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HealthPlayer.Instance.OnDie += HealthPlayer_OnDie;
        PlayerMovement.Instance.transform.position = transform.position;
        PlayerMovement.Instance.GetFollowPoint().position = transform.position + new Vector3(8f,0,0);
    }

    private void HealthPlayer_OnDie(object sender, System.EventArgs e)
    {
        PlayerMovement.Instance.transform.position = transform.position;
        PlayerMovement.Instance.GetFollowPoint().position = transform.position + new Vector3(8f,0,0);
    }

    void OnDestroy()
    {
        HealthPlayer.Instance.OnDie -= HealthPlayer_OnDie;
    }
}
