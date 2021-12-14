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

    //Distance to shooter
    private float distanceToParent = 0f;
    private const float MAXDISTTOPARENT = 30f;
    private const float SPEED = 3f;

    private void Start() {

        thisMatch = FindObjectOfType<MatchAI>();
        localPlayer = FindObjectOfType<PlayerInfo>();

            characterPlaying = characterO;

    }

    void Update(){

        //Move
        this.transform.position = Vector3.MoveTowards(this.transform.position, characterPlaying.transform.position, Time.deltaTime * SPEED);

        //Update distance to parent
        distanceToParent = Vector3.Distance(this.transform.position, parent.transform.position);

        //If distance is greater than max, hide
        if(distanceToParent >= MAXDISTTOPARENT){
            Destroy(this);
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Player")){
            Destroy(this);
        }
    }
}