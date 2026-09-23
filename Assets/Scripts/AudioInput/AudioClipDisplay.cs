using System.Collections.Generic;
using System.Linq;
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
	
	public static float average(float[] nums)
	{
		float toReturn = 0;
		foreach (float num in nums)
		{
			toReturn += num;
		}
		
		toReturn /= nums.Length;
		
		return toReturn;
	}
	
	public static string stringifyArrOfFloats(float[] floats)
	{
		string toReturn = "";
		foreach (float f in floats)
		{
			toReturn += f;
			toReturn += ",";
		}
		return toReturn;
	}
	
	public static float Max(float[] data)
	{
		float toReturn = 0;
		foreach (float item in data)
		{
			if (item > toReturn)
			{
				toReturn = item;
			}
		}
		
		return toReturn;
	}
	public static void OnSetNewAudioClip(AudioClip clip)
	{
		Debug.Log("Frequency: " + clip.frequency);
		Debug.Log("Channels: " + clip.channels);
		
		float[] data = new float[clip.samples * clip.channels];
		
		clip.GetData(data, 0);
		
		Debug.Log("First: " + data[0] + " max: " + Max(data));
		// Debug.Log(stringifyArrOfFloats(data));
		
	}
	
}
