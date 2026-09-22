using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class AudioClipDisplay : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public static void OnSetNewAudioClip(AudioClip clip)
	{
		Debug.Log(clip.frequency);
		
	}
	
}
