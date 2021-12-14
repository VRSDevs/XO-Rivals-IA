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
        followPlayerAct = Inverse(        Exponencial(   ( Vector3.Distance(navMesh.transform.position, navMesh.player.transform.position) )  , 1000f ) );
        huirAct = Inverse(Exponencial((Vector3.Distance(luciernaga.transform.position, navMesh.player.transform.position)), 1000f));
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



        }
        if (mejorAccion == Action.FollowPlayer)
        {



        }
        if (mejorAccion == Action.FollowAdvise)
        {



        }
        if (mejorAccion == Action.Patrol)
        {



        }


    }


}
