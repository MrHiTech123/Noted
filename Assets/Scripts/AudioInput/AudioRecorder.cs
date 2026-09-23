using System;
using System.Collections.Generic;
using System.Linq;
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

	public readonly static double RECORDING_SAMPLE_TIME = 1;
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
		
		OnSetNewAudioClip(currentRecording);
		
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
		for (int i = 40000; i < 43000; ++i)
		{
			float f = floats[i];
			// Debug.Log(f);
			toReturn += f;
			toReturn += ",";
		}
		return toReturn;
	}
	
	public static float MaxOfFloatArray(float[] data)
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
	
	
	static int Find(float needle, float[] haystack)
	{
		for (int i = 0; i < haystack.Length; ++i)
		{
			if (haystack[i] == needle)
			{
				return i;
			}
		}
		return -1;
	}
	
	public static readonly int LOCAL_MAXIMUM_CHECKING_POINTS = 10;
	public static bool LocalMaximumAt(float[] data, int index)
	{
		int lowerBound = Math.Max(0, index - LOCAL_MAXIMUM_CHECKING_POINTS);
		int upperBound = Math.Min(data.Length, index + LOCAL_MAXIMUM_CHECKING_POINTS + 1);
		
		for (int i = lowerBound; i < upperBound; ++i)
		{
			if (data[i] > data[index])
			{
				return false;
			}
		}
		
		return true;
	}
	
	public static List<int> PositivePeaks(float[] data)
	{
		List<int> toReturn = new List<int>();
		
		float biggest = MaxOfFloatArray(data);
		float threshold = biggest * 2;
		
		bool crossedThresholdSinceLastPeak = false;
		
		for (int i = 0; i < data.Length; ++i)
		{
			float num = data[i];
			if (num < threshold)
			{
				crossedThresholdSinceLastPeak = true;
			}
			if (crossedThresholdSinceLastPeak && LocalMaximumAt(data, i))
			{
				toReturn.Add(i);
				crossedThresholdSinceLastPeak = false;
			}
			
		}
		
		return toReturn;
	}
	
	public static List<int> waveLengths(List<int> wavePeaks)
	{
		List<int> toReturn = new List<int>();
		for (int i = 1; i < wavePeaks.Count; ++i)
		{
			toReturn.Add(wavePeaks[i] - wavePeaks[i - 1]);
		}
		return toReturn;
	}
	
	public static float AverageOfIntList(List<int> nums)
	{
		return (float)nums.Sum() / nums.Count;
	}
	
	public static float AverageWaveLengthInSamples(float[] data)
	{
		List<int> peaks = PositivePeaks(data);
		
		List<int> lengths = waveLengths(peaks);
		
		return AverageOfIntList(lengths);
	}
	public void OnSetNewAudioClip(AudioClip clip)
	{
		Debug.Log("Frequency: " + clip.frequency);
		Debug.Log("Channels: " + clip.channels);
		
		float[] data = new float[clip.samples * clip.channels];
		
		clip.GetData(data, 0);
		
		Debug.Log("First: " + data[0] + " max: " + MaxOfFloatArray(data));
		Debug.Log(stringifyArrOfFloats(data));
		Debug.Log(data[2]);
		Debug.Log(MaxOfFloatArray(data) + ": " + Find(MaxOfFloatArray(data), data));
		
		float waveLengthInSamples = AverageWaveLengthInSamples(data);
		float waveLengthInSeconds = waveLengthInSamples / SampleRate;
		float frequency = 1 / waveLengthInSeconds;
		
		Debug.Log("Wavelength: " + waveLengthInSamples + " = " + waveLengthInSeconds + "/sec = " + frequency + "Hz");
		
		// AudioSource source;
		
		
	}
	
	
	
}
