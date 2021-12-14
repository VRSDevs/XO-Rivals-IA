using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectHuella : MonoBehaviour
{

    private EnemyBT enemyBT;
    private FSMEnemy enemyFSM;

    // Start is called before the first frame update
    void Start()
    {
        enemyBT = this.GetComponent<EnemyBT>();
        enemyFSM = this.GetComponent<FSMEnemy>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider collision)
    {
        
      

        if (collision.gameObject.name == "Huella(Clone)")
        {
            enemyBT.lastHuella = collision.gameObject;
            enemyBT.perseguirHuella = true;
            enemyFSM.LastHuella = collision.gameObject;
            enemyFSM.PerseguirHuella = true;
        }

    }


}
