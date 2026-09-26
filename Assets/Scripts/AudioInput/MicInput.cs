using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Rendering;
using UnityEngine;

public class MicInput : MonoBehaviour {
  
    public static float MicLoudness;
	
	public static readonly float RECORDING_LENGTH = 0.2f;
	public static float MicFrequency
	{
		get
		{
			// if (peaks.Count() <= 1)
			// {
			// 	return -1;
			// }
			
			// TimeSpan timeOfMeasurement = peaks.Last().Subtract(peaks.First());
			
			
			
			// Debug.Log(peaks.Count() + " / " + timeOfMeasurement.TotalSeconds);
			
			// if (timeOfMeasurement.TotalSeconds == 0)
			// {
			// 	return -1;
			// }
			
			// return peaks.Count() / (float)timeOfMeasurement.TotalSeconds;
			
			return peaks.Count();
		}
	}
	private WaveState currentWaveState;
	private static readonly float PEAK_THRESHOLD = 0.0001f;
	
	private static List<DateTime> peaks = new List<DateTime>();
	
    private string _device;
  
    //mic initialization
    void InitMic(){
        // if(_device == null) _device = Microphone.devices[0];
		_device = "Headset (GROOVZ)";
		Debug.Log("Hi " + Microphone.devices[0]);
		Debug.Log("Hi " + _device);
        _clipRecord = Microphone.Start(_device, true, 999, 44100);
		peaks = new List<DateTime>();
    }
  
    void StopMicrophone()
    {
        Microphone.End(_device);
    }
  

    AudioClip _clipRecord;
    int _sampleWindow = 128;
  
    //get data from microphone into audioclip
    float  LevelMax()
    {
        float levelMax = 0;
        float[] waveData = new float[_sampleWindow];
        int micPosition = Microphone.GetPosition(null)-(_sampleWindow+1); // null means the first microphone
        if (micPosition < 0) return 0;
        _clipRecord.GetData(waveData, micPosition);
        // Getting a peak on the last 128 samples
        for (int i = 0; i < _sampleWindow; i++) {
            float wavePeak = waveData[i] * waveData[i];
			
			TrackCurrentFrequencyState(wavePeak);
			
            if (levelMax < wavePeak) {
                levelMax = wavePeak;
            }
        }
        return levelMax;
    }
  
  
	void TrackCurrentFrequencyState(float loudness)
	{
		Debug.Log("Tracking " + loudness);
		try {
		Debug.Log(peaks.First().Subtract(DateTime.Now));
		}
		catch {}
		while (peaks.Count > 0 && DateTime.Now.Subtract(peaks.First()) > TimeSpan.FromSeconds(RECORDING_LENGTH))
		{
			peaks.Remove(peaks.First());
			Debug.Log("Removing");
		}
		switch (currentWaveState)
		{
			case WaveState.AT_POSITIVE_PEAK:
				if (loudness < PEAK_THRESHOLD / 2)
				{
					currentWaveState = WaveState.BELOW_ZERO;
				}
				break;
			case WaveState.BELOW_ZERO:
				if (loudness <= 1e-5)
				{
					currentWaveState = WaveState.AT_NEGATIVE_PEAK;
				}
				break;
			case WaveState.AT_NEGATIVE_PEAK:
				if (loudness > PEAK_THRESHOLD / 2)
				{
					currentWaveState = WaveState.ABOVE_ZERO;
				}
				break;
			case WaveState.ABOVE_ZERO:
				if (loudness > PEAK_THRESHOLD)
				{
					currentWaveState = WaveState.AT_POSITIVE_PEAK;
					peaks.Add(DateTime.Now);
				}
				break;
		}
		
	}
    void Update()
    {
        // levelMax equals to the highest normalized value power 2, a small number because < 1
        // pass the value to a static var so we can access it from anywhere
        MicLoudness = LevelMax ();
    }
  
    bool _isInitialized;
    // start mic when scene starts
    void OnEnable()
    {
        InitMic();
        _isInitialized=true;
    }
  
    //stop mic when loading a new level or quit application
    void OnDisable()
    {
        StopMicrophone();
    }
  
    void OnDestroy()
    {
        StopMicrophone();
    }
  
  
    // make sure the mic gets started & stopped when application gets focused
    void OnApplicationFocus(bool focus) {
        if (focus)
        {
            //Debug.Log("Focus");
          
            if(!_isInitialized){
                //Debug.Log("Init Mic");
                InitMic();
                _isInitialized=true;
            }
        }      
        if (!focus)
        {
            //Debug.Log("Pause");
            StopMicrophone();
            //Debug.Log("Stop Mic");
            _isInitialized=false;
          
        }
    }
}

enum WaveState
{
	AT_POSITIVE_PEAK,
	BELOW_ZERO,
	AT_NEGATIVE_PEAK,
	ABOVE_ZERO
}