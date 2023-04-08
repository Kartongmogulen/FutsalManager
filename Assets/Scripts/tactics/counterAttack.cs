using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class counterAttack : MonoBehaviour
{
    [SerializeField] bool defenderWithBall;
    [SerializeField] bool runWithBall;

    public playerActionManager PlayerActionManager;

    //Genomskärare
    public void counterAttackAction(int teamWithBall, Vector2 ballPosition)
    {
        //Debug.Log("CounterAttackAction");
        defenderOrAttackWithBall(teamWithBall, ballPosition);
        if (runWithBall == false)
        {
            PlayerActionManager.throughBall();
        }

        else if (runWithBall == true)
        {
            PlayerActionManager.playerWithBallAction = "CounterAttack";
        }
    }

    public bool defenderOrAttackWithBall(int teamWithBall, Vector2 ballPosition)
    {
        if (teamWithBall == 2)
        {
            if (ballPosition.y == 1)

                runWithBall = false;
                
        }

        return runWithBall;
    }

    

}
