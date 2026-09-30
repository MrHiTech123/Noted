using UnityEngine;

public class Coin : MonoBehaviour
{
	public static int TotalCollected {get; private set;}
	
	[SerializeField] private Sprite STICKY_NOTE_SPRITE;
	[SerializeField] private Sprite PAPER_CLIP_SPRITE;
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
		bool collidedWithPlayer = collision.gameObject.GetComponent<PlayerMovement>() != null;
		Debug.Log("Collided with player? " + collidedWithPlayer);
		
		if (collidedWithPlayer)
		{
			BeCollected();
		}
		
	}



}
