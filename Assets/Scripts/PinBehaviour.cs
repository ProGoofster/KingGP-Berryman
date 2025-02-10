using UnityEngine;

public class PinBehaviour : MonoBehaviour
{
    public float baseSpeed = 5.0f;
    public float dashSpeed = 15.0f;
    public float speed;
    public float dashDuration;
    public float timeDashStart;
    public float timeLastDashEnd;
    public static float cooldownRate = 2;
    public static float cooldown;


    Rigidbody2D body;
    public Vector2 newPosition;
    public Vector3 mousePosG;
    Camera cam;

    void Start(){
        body = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    void Update() {
        CheckDash();
    }

    void FixedUpdate(){
        
       
        mousePosG = cam.ScreenToWorldPoint(Input.mousePosition);
        newPosition = Vector2.MoveTowards(body.position, mousePosG, speed * Time.fixedDeltaTime);

        body.MovePosition(newPosition);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        string collided = collision.gameObject.tag;
        
        if(collided == "Ball" || collided == "Wall") Debug.Log("Game Over");
    }

    private void CheckDash(){
        if (speed == dashSpeed){
            float howLong = Time.time - timeDashStart;
            if (howLong > dashDuration){
                speed = baseSpeed;
                timeLastDashEnd = Time.time;
                cooldown = cooldownRate;
            }
        } else {
            cooldown -= Time.deltaTime;

            if(cooldown < 0.0){
                cooldown = 0.0f;
            }

            if(Input.GetMouseButtonDown(0) && cooldown == 0.0){
                speed = dashSpeed;
                timeDashStart = Time.time;
            }
        }
    }
}
