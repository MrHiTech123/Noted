
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;


public class AudioUIHelper : MonoBehaviour
{
	[SerializeField] Button StartStopButton;
	[SerializeField] TMP_Text StartStopText;
	
	[SerializeField] TMP_Dropdown SourcePicker;
	[SerializeField] TMP_Dropdown SampleRate;
	
	static readonly int SAMPLE_RATE = 44100;
	HashSet<String> knownDevices;
	
	
	[SerializeField] AudioRecorder LinkedRecorder;

	void Start()
	{
		Debug.Log(Microphone.devices.Length);
		
		foreach (string device in Microphone.devices)
		{
			Debug.Log(device);
		}
		
	}


}