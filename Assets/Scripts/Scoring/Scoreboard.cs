using System;
using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
	private static ScoreBoard INSTANCE = null;
	private static readonly String FORMATTING_TEMPLATE = "Score: {0:00}";
	
	private TMP_Text textField = null;
	void Awake()
	{
		if (INSTANCE == null) INSTANCE = this;
		
		// DontDestroyOnLoad(this.gameObject);
		
		textField = GetComponent<TMP_Text>();
	}
	
	public static void Update(int score)
	{
		String toWrite = String.Format(FORMATTING_TEMPLATE, score);
		
		INSTANCE.textField.text = toWrite;
		
	}
	
}