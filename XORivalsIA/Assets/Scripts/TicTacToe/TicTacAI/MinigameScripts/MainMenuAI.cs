using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuAI : MonoBehaviour
{
    public void GoToGame(){
        SceneManager.LoadScene("Pruebas");
    }
}
