using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;

public class AudioRecorder : MonoBehaviour
{
    int maxDuration = 1;
	[SerializeField] UnityEvent<AudioClip> onRecordingReady = new UnityEvent<AudioClip>();
	
	public bool recording {get; private set;} = false;
	
	AudioClip currentRecording;
	string currentDevice;
	
	public bool StartRecording(string inDevice, int inSampleRate)
	{
		if (recording)
		{
			return false;
		}
		
		currentRecording = Microphone.Start(inDevice, false, maxDuration, inSampleRate);
		
		if (currentRecording == null)
		{
			return false;
		}
		
		currentDevice = inDevice;
		recording = true;
		
		return true;
		
	}
	
	public bool StopRecording()
	{
		if (!recording) return false;
		
		if (currentRecording == null || currentDevice == null) return false;
		
		Microphone.End(currentDevice);
		
		currentRecording = null;
		recording = false;
		
		onRecordingReady.Invoke(currentRecording);
		
		return true;
		
	}
	
}
