using TMPro;
using UnityEngine;

public class SceneBehaviour : MonoBehaviour {
    private float timer = 0.0f;
    private TextMeshProUGUI m_text;

    private void Start(){
        m_text = GetComponent<TextMeshProUGUI>();
        Component[] cmps = GetComponents<Component>();

        if(m_text == null) Debug.Log("No TextMeshProUGUI found.");
    }

    void Update(){
        timer = Time.time;

        if(m_text != null){
            int mins = Mathf.FloorToInt(timer / 60);
            int secs = Mathf.FloorToInt(timer % 60);
            string timeLabel = string.Format("<color=black>Time: <color=#0080ff>{0:00}<color=black>:<color=#0080ff>{1:00}", mins, secs);
            m_text.SetText(timeLabel);
        }
    }
}
