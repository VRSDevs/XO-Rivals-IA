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
        SceneManager.LoadScene("PlarformMinigame_Off");
    }

    public void GoToFantasmas(){
        SceneManager.LoadScene("Fantasmas3D_Off");
    }

    public void GoToCarnival(){
        SceneManager.LoadScene("CarnivalMinigame_Off");
    }

}
