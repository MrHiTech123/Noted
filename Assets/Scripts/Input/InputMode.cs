public enum InputMode
{
	SCROLL_WHEEL,
	AUDIO
}

public class CurrentInputMode
{
	public static InputMode value = InputMode.SCROLL_WHEEL;
	public static void SetValue(int i)
	{
		if(i == 0)
		{
			value = InputMode.AUDIO;
		}
		else
		{
			value = InputMode.SCROLL_WHEEL;
		}
	}
}




