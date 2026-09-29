using Unity.VectorGraphics;
using UnityEngine;

public class LevelFinish : MonoBehaviour
{
    [SerializeField] private RestZone restZone;
    [SerializeField] int targetScene;

    private Loader.Scene scene;
    float timer = 5f;
    bool levelEnd = false;

    void Start()
    {
        restZone.OnTrigger += RestZone_OnTrigger;
        scene = (Loader.Scene)targetScene;
    }

    private void RestZone_OnTrigger(object sender, System.EventArgs e)
    {
        ScreenFade.Instance.FadeOut();
        levelEnd = true;
    }

    void Update()
    {
        if (levelEnd)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                restZone.StopResting();
                Loader.Load(scene);
            }
        }
    }
}
