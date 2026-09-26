using UnityEngine;

class MoveWithMicLoudness : MonoBehaviour
{
	void Start()
	{
		
	}

	void Update()
	{
		float frequencyYCoordinate = MicInput.MicFrequency * 0.01f;
		
		Debug.Log(MicInput.MicLoudness + " -> ");
		Debug.Log(MicInput.MicFrequency + " -> " + frequencyYCoordinate);
		
		transform.SetPositionAndRotation(new Vector3(transform.position.x, frequencyYCoordinate, transform.position.z), transform.rotation);
		
	}
}