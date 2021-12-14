using UnityEngine;

public class Bullet : MonoBehaviour {

    //GameObject that shoots
    [SerializeField] private GameObject parent;
    //Character GameObject
    [SerializeField] private GameObject characterO;
    [SerializeField] private GameObject characterX;
    public GameObject characterPlaying;
    MatchAI thisMatch;
    PlayerInfo localPlayer;
    public Vector3 posPlayer;

    //Distance to shooter
    private float distanceToParent = 0f;
    private const float MAXDISTTOPARENT = 300000f;
    private const float SPEED = 3f;

    private void Start() {

        thisMatch = FindObjectOfType<MatchAI>();
        localPlayer = FindObjectOfType<PlayerInfo>();

            characterPlaying = characterO;
        posPlayer = characterPlaying.transform.position;
    }

    void Update(){

        //Move
        this.transform.position = Vector3.MoveTowards(this.transform.position, posPlayer, Time.deltaTime * SPEED);

        //Update distance to parent
        distanceToParent = Vector3.Distance(this.transform.position, posPlayer);

        Debug.Log(distanceToParent);
        //If distance is greater than max, hide
        if(distanceToParent <= 0.01f){
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision other) {
        
        if(other.gameObject.CompareTag("Player")){
            Destroy(this);
        }
        
    }
}