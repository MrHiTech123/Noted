using System;
using UnityEngine;

class MoveWithMicLoudness : MonoBehaviour
{
	
	public static readonly float POSITION_OFFSET = -30;
	void Start()
	{
		
	}

	void Update()
	{
		float scaledFrequency = MicInput.MicFrequency;
		
		float frequencyYCoordinate;
		
		
		if (scaledFrequency < 1)
		{
			frequencyYCoordinate = 0;
		}
		else {
			frequencyYCoordinate = 4 * (float)(Math.Log(scaledFrequency) / Math.Log(2)) + POSITION_OFFSET;
		}
		
		// frequencyYCoordinate += POSITION_OFFSET;
		
		Debug.Log(MicInput.MicLoudness + " -> ");
		Debug.Log(MicInput.MicFrequency + " -> " + frequencyYCoordinate);
		
		transform.SetPositionAndRotation(new Vector3(transform.position.x, frequencyYCoordinate, transform.position.z), transform.rotation);
		
	}
}