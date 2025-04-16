using UnityEngine;

public class InputBroadcaster : MonoBehaviour
{
    public bool IsTapPressed { get; private set; }

    private void Update()
    {
        // Mobile touch
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            IsTapPressed = touch.phase switch
            {
                TouchPhase.Began or TouchPhase.Moved or TouchPhase.Stationary => true,
                _ => false
            };
        }
        // Mouse input
        else
        {
            IsTapPressed = Input.GetMouseButton(0);
        }
    }
}