using UnityEngine;

public class PinBehaviour : MonoBehaviour
{
    public float speed = 10.0f;
    public Vector2 newPosition;
    public Vector3 mousePosG;
    Camera cam;

    void Update(){
        cam = Camera.main;
        mousePosG = cam.ScreenToWorldPoint(Input.mousePosition);
        newPosition = Vector2.MoveTowards(transform.position, mousePosG, speed * Time.fixedDeltaTime);

        transform.position = newPosition;
    }
}
