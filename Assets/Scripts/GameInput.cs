using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance {get; private set;}
    private InputSystem_Actions inputActions;

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
        inputActions = new InputSystem_Actions();
        inputActions.Player.Enable();
    }

    public float GetScrollDir()
    {
        return inputActions.Player.Scroll.ReadValue<Vector2>().y;
    }
	
	public float JumpKeyDown()
	{
		return inputActions.Player.Jump.ReadValue<Vector2>().y;
	}


}
