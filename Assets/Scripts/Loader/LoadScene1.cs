using UnityEngine;

public class LoadScene1 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Loader.Load(Loader.Scene.Level_1);
    }
}
