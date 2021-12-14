using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuAI : MonoBehaviour
{
    public void GoToGame(){
        SceneManager.LoadScene("TicTac_AI");
    }

    public void GoToPistolero(){
        SceneManager.LoadScene("Pistolero_Off");
    }

    public void GoToPlatform(){
        SceneManager.LoadScene("PlatformMinigame_Off");
    }

    public void GoToFantasmas(){
        SceneManager.LoadScene("Fanstasmas3D");
    }

    public void GoToCarnival(){
        SceneManager.LoadScene("CarnivalMinigame_Off");
    }

    public void GoToCocinitas()
    {
        SceneManager.LoadScene("MinijuegoComida_Off");
    }

}
