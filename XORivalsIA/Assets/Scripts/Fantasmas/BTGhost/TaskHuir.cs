using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskHuir : Node
{

    private GameObject _luciernaga;
    private EnemyBT _tree;
    private EnemyNavMesh _enemyNav;



    public TaskHuir(EnemyBT tree, EnemyNavMesh enemyNav, GameObject luciernaga)
    {
        _tree = tree;
        _enemyNav = enemyNav;
        _luciernaga = luciernaga;
    }




    public override NodeState Evaluate()
    {
        Debug.Log("EXECUTE TASKHUIR");



        //CAMBIO ANIMACION
        foreach (Transform t in _tree.transform.GetComponentsInChildren<Transform>())
        {
            if (t.name == "Enemy2D")
            {
                //Cosas que hacer
                Debug.Log(t.name);
                t.GetComponent<Animator>().SetBool("Normal", true);
                t.GetComponent<Animator>().SetBool("Ask", false);
                t.GetComponent<Animator>().SetBool("Alert", false);

            }
        }
        Transform adelante = null;
        Transform atras = null;
        Transform derecha = null;
        Transform izquierda = null;

        //BUSCAMOS EJES DE MOVIMIENTO (GAMEOBJECTS)
        foreach (Transform t in _tree.transform.GetComponentsInChildren<Transform>())
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


        _tree.following = false;
        _tree.perseguirHuella = false;


        //DISTANCIAS
        float distAdelante = Vector3.Distance(_luciernaga.transform.position, adelante.position);
        float distAtras = Vector3.Distance(_luciernaga.transform.position, atras.position);
        float distDerecha = Vector3.Distance(_luciernaga.transform.position, derecha.position);
        float distIzquierda = Vector3.Distance(_luciernaga.transform.position, izquierda.position);



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
            _enemyNav.target.position = izquierda.position;
        }
        if (posCercana == izquierda)
        {
            _enemyNav.target.position = derecha.position;
        }
        if (posCercana == adelante)
        {
            _enemyNav.target.position = atras.position;
        }
        if (posCercana == atras)
        {
            _enemyNav.target.position = adelante.position;
        }


        state = NodeState.RUNNING;
        return state;

    }
}
