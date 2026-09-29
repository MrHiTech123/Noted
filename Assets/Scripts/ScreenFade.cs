using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFade : MonoBehaviour
{
    public static ScreenFade Instance {get;private set;}
    [SerializeField] private Image image;

    float fadeSpeed = 1f;
    void Awake()
    {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(this);
        }
    }
    void Start()
    {
        FadeIn();
    }
    public void FadeOut()
    {
        StartCoroutine(FadeO());
    }

    public void FadeIn()
    {
        StartCoroutine(FadeI());        
    }

    IEnumerator FadeO()
    {
        Color color = image.color;

        while (color.a < 1f)
        {
            color.a += fadeSpeed * Time.deltaTime;
            image.color = color;
            yield return null;
        }
    }

    IEnumerator FadeI()
    {
        Color color = image.color;

        while (color.a > 0f)
        {
            color.a -= fadeSpeed * Time.deltaTime;
            image.color = color;
            yield return null;
        }
    }
}
