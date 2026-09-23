using System;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;

public class AudioRecorder : MonoBehaviour
{
    int maxDuration = 1;
	// [SerializeField] UnityEvent<AudioClip> onRecordingReady = new UnityEvent<AudioClip>();
	
	public bool recording {get; private set;} = false;
	
	AudioClip currentRecording;
	string currentDevice;
	
	private DateTime recordingStartTime;

	private string InDevice;
	
	public static int getSampleRate(string device)
	{
		int minFrequency;
		Microphone.GetDeviceCaps(device, out minFrequency, out _);
		return minFrequency;
	}
	private int SampleRate;

	void Awake()
	{
		InDevice = Microphone.devices[0];
		SampleRate = getSampleRate(InDevice);
	}

	public readonly static double RECORDING_SAMPLE_TIME = 0.25;
	public bool StartRecording()
	{
		if (recording)
		{
			return false;
		}
		
		currentRecording = Microphone.Start(InDevice, false, maxDuration, SampleRate);
		recordingStartTime = DateTime.Now;
		
		if (currentRecording == null)
		{
			return false;
		}
		
		currentDevice = InDevice;
		recording = true;
		
		return true;
		
	}
	
	public bool StopRecording()
	{
		if (!recording) return false;
		
		if (currentRecording == null || currentDevice == null) return false;
		
		Microphone.End(currentDevice);
		
		
		
		// onRecordingReady.Invoke(currentRecording);
		
		AudioClipDisplay.OnSetNewAudioClip(currentRecording);
		
		currentRecording = null;
		recording = false;
		
		return true;
		
	}

	void UpdateRecording()
	{
		TimeSpan timeRecordedSoFar = System.DateTime.Now.Subtract(recordingStartTime);
		
		// Debug.Log(timeRecordedSoFar);
		
		if (recording && timeRecordedSoFar > TimeSpan.FromSeconds(RECORDING_SAMPLE_TIME))
		{
			StopRecording();
		}
		else if (!recording)
		{
			StartRecording();
		}
	}
	void Update()
	{
		UpdateRecording();
	}

}
