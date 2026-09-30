using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ButtonController : MonoBehaviour {
    public Button myButton;
    public TMP_Dropdown myDropdown;

    void Start() {
        myButton.onClick.AddListener(OnButtonClicked);
    }

    void OnButtonClicked() {
        CurrentInputMode.SetValue(myDropdown.value);
        Loader.Load(Loader.Scene.StartScene);
    }
    void OnDestroy()
    {
        if (myButton != null)
            myButton.onClick.RemoveListener(OnButtonClicked);
    }
}   