using UnityEngine;
using UnityEngine.Animations;

public class BallBehaviour : MonoBehaviour{
    Rigidbody2D body;
    public bool rerouting;

    public float minX = -8.048f;
    public float minY = -4.16f;
    public float maxX = 7.8f;
    public float maxY = 4.34f;
    public float minSpeed;
    public float maxSpeed;
    public Vector2 targetPosition;

    public GameObject target;
    public bool launching;
    public float timeLastLaunch;
    public float launchDuration;
    public float timeLaunchStart;
    public float minLaunchSpeed;
    public float maxLaunchSpeed;
    public float cooldown;

    public int secondsToMaxSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        initialPosition();

        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate(){
        if(onCooldown() == false) {
            if (launching == true) {
                float currentLaunchTime = Time.time - timeLaunchStart;
                if (currentLaunchTime > launchDuration) {
                    startCooldown();
                }
            } else {
                Debug.Log("unim");
                launch();
            }
        }
        body = GetComponent<Rigidbody2D>();
        Vector2 currentPos = body.position;
        float distance = Vector2.Distance(currentPos, targetPosition);
        if(distance > 0.1) {
            float difficulty = getDifficultyPercentage();
            float currentSpeed;
            if (launching == true) {
                float launchingForHowLong = Time.time - timeLaunchStart;
                if(launchingForHowLong > launchDuration) {
                    startCooldown();
                }
               currentSpeed = Mathf.Lerp(minLaunchSpeed, maxLaunchSpeed, difficulty);
            } else {
               currentSpeed = Mathf.Lerp(minSpeed, maxSpeed, difficulty);
            }
            currentSpeed = currentSpeed * Time.deltaTime;
            Vector2 newPosition = Vector2.MoveTowards(currentPos, targetPosition, currentSpeed);
            body.MovePosition(newPosition);
        } else { // You are at target
            if(launching == true) {
                startCooldown();
            }
            targetPosition = getRandomPosition();
        }
        

    }

    private void OnCollisionEnter2D(Collision2D collision){
        Debug.Log(this.gameObject.name + " Collided with: " + collision.gameObject.name);

        if(collision.gameObject.tag == "Wall") targetPosition = getRandomPosition();
    }

    Vector2 getRandomPosition() {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        Vector2 v = new Vector2(randomX, randomY);
        return v;
    }

    public float getDifficultyPercentage() {
        return Mathf.Clamp01(Time.timeSinceLevelLoad / secondsToMaxSpeed);
    }

    public void launch() {
        Rigidbody2D targetBody = target.GetComponent<Rigidbody2D>();
        targetPosition = targetBody.position;
        if (launching == false) {
            timeLaunchStart = Time.time;
            launching = true;
        }
    }

    public bool onCooldown() {
        bool result = false;
        float timeSinceLastLaunch = Time.time - timeLastLaunch;

        if(timeSinceLastLaunch < cooldown) {
            result = true;
        }

        return result;
    }

    public void startCooldown() {
        timeLastLaunch = Time.time;
        launching = false;
    }

    public void setBounds(float miX, float maX, float miY, float maY){
        minX = miX;
        maxX = maX;
        minY = miY;
        maxY = maY;
    }

    public void initialPosition() {
        transform.position = getRandomPosition();
        targetPosition = getRandomPosition();
        launching = false;
    }

    public void setTarget(GameObject pin) {
        target = pin;
    }

}
