
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;


public class AudioUIHelper : MonoBehaviour
{
	[SerializeField] Button StartStopButton;
	[SerializeField] TMP_Text StartStopText;
	
	[SerializeField] TMP_Dropdown SourcePicker;
	[SerializeField] TMP_Dropdown SampleRate;
	
	static int SAMPLE_RATE = 48000;
	
	public string selectedRecorder;
	HashSet<String> knownDevices;
	
	
	[SerializeField] AudioRecorder LinkedRecorder;
	
	void DisableAllButtons()
	{
		StartStopButton.enabled = false;
		SourcePicker.enabled = false;
		SampleRate.enabled = false;
		
	}
	
	void SetupUI()
	{
		knownDevices.AddRange(Microphone.devices);
		
		if (knownDevices.Count == 0)
		{
			DisableAllButtons();
			return;
		}
		
		foreach (string device in knownDevices)
		{
			int minFrequency, maxFrequency;
			Microphone.GetDeviceCaps(device, out minFrequency, out maxFrequency);
			
			List<int> sampleRates = new List<int>();
			
			sampleRates.Add(minFrequency);
			
			
			
		}
		
		
	}
	
	void Start()
	{
		Debug.Log(Microphone.devices.Length);
		
		if (Microphone.devices.Length < 1)
		{
			return;
		}
		
		selectedRecorder = Microphone.devices[0];
		Microphone.GetDeviceCaps(selectedRecorder, out _, out SAMPLE_RATE);
		
		
		foreach (string device in Microphone.devices)
		{
			Debug.Log(device);
			
			int minFrequency, maxFrequency;
			Microphone.GetDeviceCaps(device, out minFrequency, out maxFrequency);
			
			Debug.Log(minFrequency);
			Debug.Log(maxFrequency);
			
		}
		
		
		
		
	}
	
	bool jumpKeyDown;
	
	void Update()
	{
		if (GameInput.Instance.JumpKeyDown() != 0 && !jumpKeyDown)
		{
			OnStartStopButtonPressed();
			jumpKeyDown = true;
			Debug.Log("Pressed");
		}
		else if (jumpKeyDown && GameInput.Instance.JumpKeyDown() == 0)
		{
			jumpKeyDown = false;
		}
	}
	
	void StopRecording()
	{
		LinkedRecorder.StopRecording();
	}
	
	void StartRecording()
	{
		LinkedRecorder.StartRecording(selectedRecorder, SAMPLE_RATE);
	}


	public void OnStartStopButtonPressed()
	{
		if (LinkedRecorder.recording)
		{
			Debug.Log("Stopping Recording");
			StopRecording();
		}
		else
		{
			Debug.Log("Starting recording");
			StartRecording();
		}
	}

}