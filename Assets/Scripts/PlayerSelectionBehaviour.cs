using TMPro;
using UnityEngine;

public class PlayerSelectionBehaviour : MonoBehaviour
{
    public Pins pinsDB;

    public static int selection = 0;
    public SpriteRenderer sprite;
    public TMP_Text nameLabel;
    private AudioSource audioSource;

    private void Start(){
        UpdateCharacter();
        audioSource = GetComponentInChildren<AudioSource>();
    }
    
    void UpdateCharacter() {
        Pin current = pinsDB.getPin(selection);
        sprite.sprite = current.prefab.GetComponent<SpriteRenderer>().sprite;
        nameLabel.SetText(current.name);
    }

    public void next(){
        int numberPins = pinsDB.getCount();
        if (selection < numberPins-1) {
            selection = selection + 1;
        } else {
            selection = 0;
        }
        UpdateCharacter();
        audioSource.Play();
    }

    public void previous(){
        if(selection > 0){
            selection = selection - 1;
        } else {
            selection = pinsDB.getCount() - 1;
        }
        UpdateCharacter();
        audioSource.Play();
    }
}
