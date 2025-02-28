using System.Collections;
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
    private bool gameActive;
    public AudioSource[] audioSources;


    Rigidbody2D body;
    public Vector2 newPosition;
    public Vector3 mousePosG;
    Camera cam;

    void Start(){
        this.gameObject.SetActive(true);
        body = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        gameActive = true;
        audioSources = GetComponents<AudioSource>();
    }

    void Update() {
        CheckDash();
        CheckInvincible();
    }

    void FixedUpdate(){
       if(!gameActive) return;
        mousePosG = cam.ScreenToWorldPoint(Input.mousePosition);
        newPosition = Vector2.MoveTowards(body.position, mousePosG, speed * Time.fixedDeltaTime);

        body.MovePosition(newPosition);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        string collided = collision.gameObject.tag;
        
        if(collided == "Ball" || collided == "Wall") {
            gameActive = false;
            StartCoroutine(WaitForSoundAndTransition("GameOver"));
        }
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
                if(audioSources[1].isPlaying) audioSources[1].Stop();
                audioSources[1].Play();
            }
        }
    }

    private void CheckInvincible(){

    }

    private IEnumerator WaitForSoundAndTransition(string sceneName){
        audioSources[0].Play();
        yield return new WaitForSeconds(audioSources[0].clip.length);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
