using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControllerAI : MonoBehaviour
{
    public void GoToGame(){
        SceneManager.LoadScene("TicTac_AI");
    }
}
