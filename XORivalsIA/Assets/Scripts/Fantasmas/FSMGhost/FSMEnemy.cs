using Assets.RobustFSM.Mono;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMEnemy : MonoFSM
{
    public EnemyNavMesh Owner { get; set; }

    public override void AddStates()
    {
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

}
