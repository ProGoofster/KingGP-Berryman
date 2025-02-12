using UnityEngine;

public class MenuBehaviour : MonoBehaviour
{
    public void gotoGame() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");
    }

    public void goToMainMenu(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void goToCharacterSelect(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("CharacterSelect");
    }
}
