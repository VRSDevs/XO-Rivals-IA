using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameFantasmasController : MonoBehaviour
{

    // Controles de movil
    [SerializeField]
    public GameObject leftButton;
    [SerializeField]
    public GameObject rightButton;
    [SerializeField]
    public GameObject upButton;
    [SerializeField]
    public GameObject downButton;

    // Gamemanager

    //Musica
    public AudioClip MusicaBosque;

    public List<GameObject> enemigos;
    

    // Start is called before the first frame update
    void Start()
    {
        //Los enemigos comienzan desactivados
        foreach (GameObject enem in enemigos)
        {
            enem.SetActive(false);
        }


       
            leftButton.SetActive(false);
            rightButton.SetActive(false);
            upButton.SetActive(false);
            downButton.SetActive(false);
        
        
        FindObjectOfType<AudioManager>().StopAllSongs();
        FindObjectOfType<AudioManager>().ChangeMusic(MusicaBosque,"Tic-Tac-Toe");


    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
