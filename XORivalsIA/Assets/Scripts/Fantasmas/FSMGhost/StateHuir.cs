using RobustFSM.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateHuir : BState
{


    /// <summary>
    /// A reference to the manual execute frequency
    /// </summary>
    float _manualExecuteRate = 0f;

    /// <summary>
    /// A reference to the previous manual execute time
    /// </summary>
    DateTime _prevManualExecuteTime;
    private bool newTarget = true; //CAMBIAMOS DE OBJETIVO
    private Transform taskTarget;
    private Transform adelante = null;
    private Transform atras = null;
    private Transform derecha = null;
    private Transform izquierda = null;

    public override void Enter()
    {
        base.Enter();


        //DEJA DE TENER MIEDO EN 5 S
        SuperFSM.dejarMiedoEnTiempo();


        //set the previous manual execute time
        _prevManualExecuteTime = DateTime.Now;

        //CAMBIO ANIMACION
        foreach (Transform t in Owner.transform.GetComponentsInChildren<Transform>())
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


        //BUSCAMOS EJES DE MOVIMIENTO (GAMEOBJECTS)
        foreach (Transform t in Owner.transform.GetComponentsInChildren<Transform>())
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

        //set the custom update frequency
        ((FSMEnemy)SuperMachine).SetUpdateFrequency(1f);
        _manualExecuteRate = ((FSMEnemy)SuperMachine).ManualUpdateFrequency;
    }

    public override void Execute()
    {
        base.Execute();





        //DISTANCIAS
        float distAdelante = Vector3.Distance(Luciernaga.transform.position, adelante.position);
        float distAtras = Vector3.Distance(Luciernaga.transform.position, atras.position);
        float distDerecha = Vector3.Distance(Luciernaga.transform.position, derecha.position);
        float distIzquierda = Vector3.Distance(Luciernaga.transform.position, izquierda.position);



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
            Owner.target.position = izquierda.position;
        }
        if (posCercana == izquierda)
        {
            Owner.target.position = derecha.position;
        }
        if (posCercana == adelante)
        {
            Owner.target.position = atras.position;
        }
        if (posCercana == atras)
        {
            Owner.target.position = adelante.position;
        }




        float distLuciernaga = Vector3.Distance(Luciernaga.transform.position, Owner.transform.position);
        if (SuperFSM.StopMiedo) //PATROL
        {
            SuperFSM.StopMiedo = false ;
            SuperMachine.ChangeState<StatePatrol>();
        }




    }

    public override void ManualExecute()
    {
        base.ManualExecute();

        //find the time difference
        double timeDiff = DateTime.Now.Subtract(_prevManualExecuteTime).TotalSeconds;

        //prepare the message
        string message = string.Format("{0}::{1}::{2}", "<color=green>Invoke Huir state manual execute.</color>", _manualExecuteRate, timeDiff);
        Debug.Log(message);

        //set the previous manual execute time
        _prevManualExecuteTime = DateTime.Now;
    }

    /// <summary>
    /// Retries the FSM that owns this state
    /// </summary>
    public FSMEnemy SuperFSM
    {
        get
        {
            return (FSMEnemy)SuperMachine;
        }
    }

    /// <summary>
    /// A little extra stuff. Accessing info inside the OwnerFSM
    /// </summary>
    public EnemyNavMesh Owner
    {
        get
        {
            return SuperFSM.Owner;
        }
    }

    public DetectPlayer DetectPlayer_
    {
        get
        {
            return SuperFSM.DetectPlayer_;
        }
    }
    public GameObject Luciernaga
    {
        get
        {
            return SuperFSM.Luciernaga;
        }
    }

    public bool PerseguirHuella
    {
        get
        {
            return SuperFSM.PerseguirHuella;
        }
    }

    public GameObject LastHuella
    {
        get
        {
            return SuperFSM.LastHuella;
        }
    }

}
