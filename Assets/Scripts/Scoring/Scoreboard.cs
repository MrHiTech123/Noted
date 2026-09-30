using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
	void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
	}
}