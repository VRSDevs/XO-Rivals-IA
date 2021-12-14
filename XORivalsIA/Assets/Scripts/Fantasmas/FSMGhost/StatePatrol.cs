using RobustFSM.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePatrol : BState
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


    public override void Enter()
    {
        base.Enter();

        //set the previous manual execute time
        _prevManualExecuteTime = DateTime.Now;

        foreach (Transform t in Owner.transform.GetComponentsInChildren<Transform>())
        {
            if (t.name == "Enemy2D")
            {

                t.GetComponent<Animator>().SetBool("Normal", true);
                t.GetComponent<Animator>().SetBool("Alert", false);
                t.GetComponent<Animator>().SetBool("Ask", false);

            }
        }

        //Owner.following = false;


        //set the custom update frequency
        ((FSMEnemy)SuperMachine).SetUpdateFrequency(1f);
        _manualExecuteRate = ((FSMEnemy)SuperMachine).ManualUpdateFrequency;
    }

    public override void Execute()
    {
        base.Execute();



        if (newTarget)
        {
            newTarget = false;
            //SELECCIONAMOS UN TARGET AL AZAR
            System.Random rnd = new System.Random();
            int random = rnd.Next(0, Owner.movePositionTransform.Count);  
            taskTarget = Owner.movePositionTransform[random];
            
        }
        Owner.target.position = taskTarget.position;


        //SI LLEGAMOS A UN PUNTO DE PATRULLA DEBERIAMOS IR AL SIGUIENTE
        if (Vector3.Distance(Owner.transform.position, taskTarget.position) < 1f)
        {
            newTarget = true; //CAMBIAMOS DE TARGET
        }



        float distLuciernaga = Vector3.Distance(Luciernaga.transform.position, Owner.transform.position);
        if ((distLuciernaga < 10f)) //HUYO
        {
            SuperMachine.ChangeState<StateHuir>();
        }
        else if (DetectPlayer_.veoPlayerDistance && DetectPlayer_.linernaDetect) //PERSIGO JUGADOR
        {
            SuperMachine.ChangeState<StateFollowPlayer>();

        }
        else if (SuperFSM.getAdvise()) //PERSIGO AVISO
        {
            SuperMachine.ChangeState<StateFollowAdvise>();

        }
        else if (PerseguirHuella) //SI ESTA DADO AVISO NO PARA HASTA LLEGAR AL AVISO
        {
            if (SuperFSM.Clever)
            {
                SuperMachine.ChangeState<StateFollowHuella>();
            }
           
        }



    }

    public override void ManualExecute()
    {
        base.ManualExecute();

        //find the time difference
        double timeDiff = DateTime.Now.Subtract(_prevManualExecuteTime).TotalSeconds;

        //prepare the message
        string message = string.Format("{0}::{1}::{2}", "<color=green>Invoke patrol main state manual execute.</color>", _manualExecuteRate, timeDiff);
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

