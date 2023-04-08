using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{
    public float basicProbActionCompletion;
    public float actionCompletionProbFinishSim;//Slutresultatet för sannolikheten att aktionen lyckas

    public bool actionCompletionBool;

    public float randomNumber;

    public bool actionCompletion(int playerAttackShoot, int defenderGoalkeeperBlock)
    {
        //Formel för att aktionen ska lyckas
        actionCompletionProbFinishSim = basicProbActionCompletion + (playerAttackShoot - defenderGoalkeeperBlock)*2;
        //Debug.Log("Shoot stat: " + playerAttackShoot);
        //Debug.Log("Goal prob: " + actionCompletionProbFinishSim);

        randomNumber = Random.Range(0, 99);

        //Slumpar om dribblingen lyckas
        if (randomNumber <= actionCompletionProbFinishSim)
        {
            actionCompletionBool = true;
        }
        else
            actionCompletionBool = false;

        return actionCompletionBool;
    }
}

