using UnityEngine;

public class Head : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Rotate with mouse
        Vector3 MouseScreenToCameraSpace = new Vector3(Input.mousePosition.x, 0f, Input.mousePosition.y);
        Vector3 PlayerScreenToCameraScreen = new Vector3(Camera.main.WorldToScreenPoint(transform.position).x, 0f, Camera.main.WorldToScreenPoint(transform.position).y);

        Vector3 PlayerToMouse = MouseScreenToCameraSpace - PlayerScreenToCameraScreen;
        transform.LookAt(PlayerToMouse);
    }
}
