using System.Collections;
using UnityEngine;

public class MenuBehaviour : MonoBehaviour
{
    public void gotoGame() {
        StartCoroutine(WaitForSoundAndTransition("MainGame"));
    }

    public void goToMainMenu(){
        StartCoroutine(WaitForSoundAndTransition("MainMenu"));
    }

    public void goToCharacterSelect(){
        StartCoroutine(WaitForSoundAndTransition("CharacterSelection"));
    }

    private IEnumerator WaitForSoundAndTransition(string sceneName){
        AudioSource audioSource = GetComponentInChildren<AudioSource>();
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
