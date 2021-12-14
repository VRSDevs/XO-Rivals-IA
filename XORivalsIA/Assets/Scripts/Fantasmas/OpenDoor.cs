using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{

    public List<EnemyBT> enemyTree;
    public List<FSMEnemy> enemyFSM;
    public float distanciaAviso = 100f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {



        if (collision.gameObject.name == "ColliderPuerta")
        {

            collision.gameObject.transform.GetChild(0).gameObject.SetActive(false);

            //Avisamos a los fantasmas cercanos
            foreach (EnemyBT tree in enemyTree)
            {
                if (Vector3.Distance(collision.gameObject.transform.position, tree.gameObject.transform.position) < distanciaAviso)
                {

                    if (tree.medium || tree.clever)//SOLO AVISA A MEIDO E INTELIGENTE
                    {
                        tree.setAdvise(collision.gameObject.transform.position);
                    }
                   
                }


            }
            //Avisamos a los fantasmas cercanos
            foreach (FSMEnemy fsm in enemyFSM)
            {
                if (Vector3.Distance(collision.gameObject.transform.position, fsm.gameObject.transform.position) < distanciaAviso)
                {

                    if (fsm.Medium || fsm.Clever)//SOLO AVISA A MEIDO E INTELIGENTE
                    {
                        fsm.setAdvise(collision.gameObject.transform.position);
                    }

                }


            }



        }

    }
    private void OnTriggerExit(Collider collision)
    {



        if (collision.gameObject.name == "ColliderPuerta")
        {
            //EL muro se abre
            collision.gameObject.transform.GetChild(0).gameObject.SetActive(true);

                
        }

    }


}
