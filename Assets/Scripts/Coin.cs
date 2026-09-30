using UnityEngine;

public class Coin : MonoBehaviour
{
	public static int TotalCollected {get; private set;}
	
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	
	private void BeCollected()
	{
		++TotalCollected;
		Debug.Log("Coins collected: " + TotalCollected);
		Destroy(gameObject);
	}
	
	void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log("Coin hit by " + collision.gameObject.name);
		bool collidedWithPlayer = collision.gameObject.GetComponent<PlayerMovement>() != null;
		
		if (collidedWithPlayer)
		{
			BeCollected();
		}
		
	}



}
