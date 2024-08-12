using UnityEngine;

public class DebugClick : MonoBehaviour
{
    public void OnClick()
    {
        Debug.Log("Button Clicked");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse Clicked");
        }
    }
}
