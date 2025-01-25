using UnityEngine;

public class Inputs : MonoBehaviour
{
    public static Inputs Instance;

    public InputActions Actions;

    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        Actions = new InputActions();
    }

    public void OnEnable()
    {
        Actions.Enable();
    }
    public void OnDisable()
    {
        Actions.Disable();
    }
}
