using UnityEngine;

class MoveWithMicLoudness : MonoBehaviour
{
	void Start()
	{
		
	}

	void Update()
	{
		float volumeYCoordinate = MicInput.MicLoudness * 5.0f;
		
		Debug.Log(MicInput.MicLoudness + " -> " + volumeYCoordinate);
		Debug.Log(MicInput.MicFrequency + " -> ");
		
		transform.SetPositionAndRotation(new Vector3(transform.position.x, volumeYCoordinate, transform.position.z), transform.rotation);
		
	}
}