using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements.Experimental;

public class ButtonController : MonoBehaviour {
    public Button myButton;
    public int value;

    void Start() {
        myButton.onClick.AddListener(OnButtonClicked);
    }

    void OnButtonClicked() {
        CurrentInputMode.SetValue(value);
        Loader.Load(Loader.Scene.StartScene);
    }
    void OnDestroy()
    {
        if (myButton != null)
            myButton.onClick.RemoveListener(OnButtonClicked);
    }
}   