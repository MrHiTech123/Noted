using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
	public static int TotalCollected {get; private set;}
	
	private static readonly System.Random random = new();
	private static readonly double ONE_HALF = (double)1 / 2;
	
	[SerializeField] private SpriteRenderer renderer;
	[SerializeField] private Sprite STICKY_NOTE_SPRITE;
	[SerializeField] private Sprite PAPER_CLIP_SPRITE;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		if (random.NextDouble() < ONE_HALF)
		{
			renderer.sprite = STICKY_NOTE_SPRITE;
		}
		else
		{
			renderer.sprite = PAPER_CLIP_SPRITE;
		}
		
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	
	private void BeCollected()
	{
		++TotalCollected;
		Debug.Log("Coins collected: " + TotalCollected);
		ScoreBoard.Update(TotalCollected);
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
