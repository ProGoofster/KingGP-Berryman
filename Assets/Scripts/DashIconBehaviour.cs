using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class DashIconBehaviour : MonoBehaviour {
    TextMeshProUGUI label;
    Image overlay;
    float cooldown;
    float cooldownRate;

    void Start() {
        label = GetComponentInChildren<TextMeshProUGUI>();
        Image[] images = GetComponentsInChildren<Image>();
        foreach(Image image in images){
            if(image.tag == "Overlay"){
                overlay = image;
            }
        }
        cooldownRate = PinBehaviour.cooldownRate;
        overlay.fillAmount = 0.0f;
    }

    void Update() {
        cooldown = PinBehaviour.cooldown;
        string message = "";
        if (cooldown > 0.0){
            float fill = cooldown / cooldownRate;
            message = string.Format("{0:0.0}", cooldown);
            overlay.fillAmount = fill;
        }
        label.SetText(message);
    }
}
