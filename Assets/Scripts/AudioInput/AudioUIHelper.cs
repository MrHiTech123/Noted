
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
	
	static readonly int SAMPLE_RATE = 44100;
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
		
		foreach (string device in Microphone.devices)
		{
			Debug.Log(device);
			
			int minFrequency, maxFrequency;
			Microphone.GetDeviceCaps(device, out minFrequency, out maxFrequency);
			
			Debug.Log(minFrequency);
			Debug.Log(maxFrequency);
			
		}
		
		
	}


}