using Assets.RobustFSM.Mono;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMEnemy : MonoFSM
{
    public EnemyNavMesh Owner { get; set; }
    public EnemyNavMesh owner;
    public DetectPlayer DetectPlayer_ { get; set; }
    public DetectPlayer detectPlayer;
    public GameObject Luciernaga { get; set; }
    public GameObject luciernaga;

    public GameObject LastHuella { get; set; }

    public bool PerseguirHuella { get; set; }

    public bool StopMiedo { get; set; }

    public bool Advised { get; set; }
    public Vector3 AdviseVector { get; set; }

    public bool Clever { get; set; }
    public bool clever;

    public bool Medium { get; set; }
    public bool medium;

    public bool Silly { get; set; }
    public bool silly;

    public List<FSMEnemy> otherEnemy;


    public override void AddStates()
    {
        Owner = owner;
        DetectPlayer_ = detectPlayer;
        Luciernaga = luciernaga;
        PerseguirHuella = false;
        StopMiedo = false;
        Clever = clever;
        Medium = medium;
        Silly = silly;

        //set the custom update frequenct
        SetUpdateFrequency(0.1f);

        //add the states
        AddState<StatePatrol>();
        AddState<StateHuir>();
        AddState<StateFollowPlayer>();
        AddState<StateFollowAdvise>();
        AddState<StateAdvise>();
        AddState<StateFollowHuella>();
        

        //set the initial state
        SetInitialState<StatePatrol>();
    }
    IEnumerator dejarDeTenerMiedo()
    {

        yield return new WaitForSeconds(5);

        StopMiedo = true;

    }

    public void dejarMiedoEnTiempo()
    {
        StartCoroutine(dejarDeTenerMiedo());

    }
    public void setAdvise(Vector3 vectorA)
    {
        AdviseVector = vectorA;
        Advised = true;
    }
    public void setAdviseFalse()
    {
        Advised = false;
    }
    public bool getAdvise()
    {
        return Advised;
    }

}
