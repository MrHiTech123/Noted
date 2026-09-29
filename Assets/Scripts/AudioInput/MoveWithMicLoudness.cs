using System;
using UnityEngine;

class MoveWithMicLoudness : MonoBehaviour
{
	
	public static readonly float POSITION_OFFSET = -30;
	void Start()
	{
		
	}
	
	public static float DesiredYCoordinate()
	{
		float scaledFrequency = MicInput.MicFrequency;
		
		
		
		
		if (scaledFrequency < 1)
		{
			return 0;
		}
		else {
			return 4 * (float)(Math.Log(scaledFrequency) / Math.Log(2)) + POSITION_OFFSET;
		}
		
	}

	void Update()
	{
		
		float frequencyYCoordinate = DesiredYCoordinate();
		
		// frequencyYCoordinate += POSITION_OFFSET;
		
		Debug.Log(MicInput.MicLoudness + " -> ");
		Debug.Log(MicInput.MicFrequency + " -> " + frequencyYCoordinate);
		
		transform.SetPositionAndRotation(new Vector3(transform.position.x, frequencyYCoordinate, transform.position.z), transform.rotation);
		
	}
}