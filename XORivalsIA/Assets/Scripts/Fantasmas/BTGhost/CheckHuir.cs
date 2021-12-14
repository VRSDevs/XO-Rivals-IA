using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class CheckHuir : Node
{

    private GameObject _luciernaga;
    private EnemyBT _tree;

    public CheckHuir(GameObject luciernaga, EnemyBT tree)
    {
        _luciernaga = luciernaga;
        _tree = tree;

    }



    public override NodeState Evaluate()
    {
        float distLuciernaga = Vector3.Distance(_luciernaga.transform.position, _tree.transform.position);


        if ((distLuciernaga < _tree.distanciaMiedo) || _tree.asustado  ) //ESTAMOS YA ASUSTADOS O NOS EMPEZAMOS A ASUSTAR
        {
            _tree.asustado = true;
            _tree.dejarMiedoEnTiempo();
            state = NodeState.SUCCESS;
            return state;
        }
        else //NO HUYE
        {
            state = NodeState.FAILURE;
            return state;
        }


    }

}
