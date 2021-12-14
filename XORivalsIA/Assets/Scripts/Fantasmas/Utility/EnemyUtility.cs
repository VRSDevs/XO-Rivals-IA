using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUtility : MonoBehaviour
{
    Action mejorAccion;

    public EnemyNavMesh navMesh;
    public DetectPlayer detect;
    public GameObject luciernaga;

    float followPlayerAct;
    float patrolAct;
    float followAdviseAct;
    float huirAct;

    private bool newTarget = true; //CAMBIAMOS DE OBJETIVO
    private Transform taskTarget;
    private Transform adelante = null;
    private Transform atras = null;
    private Transform derecha = null;
    private Transform izquierda = null;

    public enum Action
    {
        FollowPlayer,
        Patrol,
        FollowAdvise,
        Huir
    }


    public void Start()
    {
        navMesh = GetComponent<EnemyNavMesh>();

    }

    public void Update()
    {

        ajustarActions();

        mejorAccion = elegirMejorAccion();
        Debug.Log(mejorAccion);
        ejecutarMejorAccion(mejorAccion);

    }


    public float Umbral(float valor, float umbral)
    {
        if (valor < umbral)
        {
            return 0;
        }
        else
        {
            return 1;
        }
    }


    public float Inverse(float valor)
    {

        return 1 - valor;
    }


    public float Exponencial(float valor, float max)
    {

        return (valor*valor)/ (max*max);

    }

    public float directNormalize(float valor, float max)
    {
        return valor / max;
    }

    public void ajustarActions()
    {
        followPlayerAct = Inverse(        Exponencial(   ( Vector3.Distance(navMesh.transform.position, navMesh.player.transform.position) )  , 10f ) );
        Debug.Log((Vector3.Distance(navMesh.transform.position, navMesh.player.transform.position) + "DISTANCIAAAAA")); 
        Debug.Log(followPlayerAct + "followPlayerAct");
        huirAct = Inverse(Exponencial((Vector3.Distance(luciernaga.transform.position, navMesh.transform.position)), 10f));
        Debug.Log(huirAct + "huirAct");
        patrolAct = 0.5f;
        followAdviseAct = 0;
    }

    public Action elegirMejorAccion()
    {
        if (huirAct == Mathf.Max(followPlayerAct, huirAct, patrolAct, followAdviseAct)) {

            return Action.Huir;

        }
        else if (followPlayerAct == Mathf.Max(followPlayerAct, huirAct, patrolAct, followAdviseAct))
        {
            return Action.FollowPlayer;

        }
        else if (patrolAct == Mathf.Max(followPlayerAct, huirAct, patrolAct, followAdviseAct))
        {
            return Action.Patrol;
        }
        else if (followAdviseAct == Mathf.Max(followPlayerAct, huirAct, patrolAct, followAdviseAct))
        {
            return Action.FollowAdvise;
        }


        return Action.Patrol;



    }

    public void ejecutarMejorAccion(Action mejorAccion)
    {

        if (mejorAccion == Action.Huir)
        {
            //CAMBIO ANIMACION
            foreach (Transform t in navMesh.transform.GetComponentsInChildren<Transform>())
            {
                if (t.name == "Enemy2D")
                {
                    //Cosas que hacer

                    t.GetComponent<Animator>().SetBool("Normal", true);
                    t.GetComponent<Animator>().SetBool("Ask", false);
                    t.GetComponent<Animator>().SetBool("Alert", false);

                }
            }


            //BUSCAMOS EJES DE MOVIMIENTO (GAMEOBJECTS)
            foreach (Transform t in navMesh.transform.GetComponentsInChildren<Transform>())
            {
                if (t.name == "TargetAlante")
                {
                    adelante = t;
                }
                if (t.name == "TargetAtras")
                {
                    atras = t;
                }
                if (t.name == "TargetDerecha")
                {
                    derecha = t;
                }
                if (t.name == "TargetIzquierda")
                {
                    izquierda = t;
                }

            }



            //DISTANCIAS
            float distAdelante = Vector3.Distance(luciernaga.transform.position, adelante.position);
            float distAtras = Vector3.Distance(luciernaga.transform.position, atras.position);
            float distDerecha = Vector3.Distance(luciernaga.transform.position, derecha.position);
            float distIzquierda = Vector3.Distance(luciernaga.transform.position, izquierda.position);



            //COGEMOS CUAL ES LA DIRECCION MAS CERCANA A LA LUCIERNAGA
            Transform posCercana = adelante;
            float distanciaCercana = distAdelante;
            if (distAdelante > distAtras) //ES ATRAS MAS CERCANO'
            {
                posCercana = atras;
                distanciaCercana = distAtras;
            }
            if (distanciaCercana > distDerecha) //ES DERECHA MAS CERCANO'
            {
                posCercana = derecha;
                distanciaCercana = distDerecha;
            }
            if (distanciaCercana > distIzquierda) //ES IZQUIERDA MAS CERCANO'
            {
                posCercana = izquierda;
                distanciaCercana = distIzquierda;
            }



            //SE DIRIGE A LA DIRECCION CONTRARIA A LA MAS CERCANA A LA LUCIERNAGA
            if (posCercana == derecha)
            {
                navMesh.target.position = izquierda.position;
            }
            if (posCercana == izquierda)
            {
                navMesh.target.position = derecha.position;
            }
            if (posCercana == adelante)
            {
                navMesh.target.position = atras.position;
            }
            if (posCercana == atras)
            {
                navMesh.target.position = adelante.position;
            }



        }
        if (mejorAccion == Action.FollowPlayer)
        {

            //CAMBIO ANIMACION
            foreach (Transform t in navMesh.transform.GetComponentsInChildren<Transform>())  
            {
                if (t.name == "Enemy2D")
                {
                    //Cosas que hacer

                    t.GetComponent<Animator>().SetBool("Alert", true);
                    t.GetComponent<Animator>().SetBool("Normal", false);
                    t.GetComponent<Animator>().SetBool("Ask", false);

                }
            }

            //MUEVO
            navMesh.target.position = navMesh.player.position;


        }
        if (mejorAccion == Action.FollowAdvise)
        {



        }
        if (mejorAccion == Action.Patrol)
        {

            foreach (Transform t in navMesh.transform.GetComponentsInChildren<Transform>())
            {
                if (t.name == "Enemy2D")
                {

                    t.GetComponent<Animator>().SetBool("Normal", true);
                    t.GetComponent<Animator>().SetBool("Alert", false);
                    t.GetComponent<Animator>().SetBool("Ask", false);

                }
            }

            if (newTarget)
            {
                newTarget = false;
                //SELECCIONAMOS UN TARGET AL AZAR
                System.Random rnd = new System.Random();
                int random = rnd.Next(0, navMesh.movePositionTransform.Count);
                taskTarget = navMesh.movePositionTransform[random];

            }
            navMesh.target.position = taskTarget.position;


            //SI LLEGAMOS A UN PUNTO DE PATRULLA DEBERIAMOS IR AL SIGUIENTE
            if (Vector3.Distance(navMesh.transform.position, taskTarget.position) < 1f)
            {
                newTarget = true; //CAMBIAMOS DE TARGET
            }


        }


    }


}
