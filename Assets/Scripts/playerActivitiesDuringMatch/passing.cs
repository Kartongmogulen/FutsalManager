using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class passing : MonoBehaviour
{
    public int oldPositionBall;
    public int newPositionBallY;
    public int newPositionBallX;

    public float basicProbPassCompletion;
    public float passCompletionProbFinishSim;

    public bool passCompletionBool;

    public float randomNumber;

    public GameObject teamOneTactics;
    public GameObject teamTwoTactics;
    public matchStatisticManager MatchStatisticManager;

    public int forwardPass(int oldPositionY, int teamWithBall)
    {
        if (oldPositionY == 2 && teamWithBall == 1)
            newPositionBallY = 1;
        if(oldPositionY == 1 && teamWithBall == 2)
            newPositionBallY = 2;

        return newPositionBallY;
    }

    public int backwardPass(int oldPositionY, int teamWithBall)
    {
        if (oldPositionY == 1 && teamWithBall == 1)
            newPositionBallY = 2;
        if (oldPositionY == 2 && teamWithBall == 2)
            newPositionBallY = 1;

        //Debug.Log("newPositionBallY: " + newPositionBallY);

        return newPositionBallY;
    }

    public int sidewaysPassLeft(int oldPositionX)
    {
        newPositionBallX = oldPositionX - 1;

        return newPositionBallX;
    }

    public int sidewaysPassRight(int oldPositionX)
    {
        newPositionBallX = oldPositionX + 1;

        return newPositionBallX;
    }
    
    //Sannolikheten att passning går bra
    public bool passCompletion(int playerPassingStat, int playerRecieveStat, int playerDefendTacklingStat, int playerDefendPositionStat)
    {
        //Debug.Log("Team Two press: " + teamTwoTactics.GetComponent<tacticsDefence>().DefencePress);
        
        //Vilken försvaradnde lag använder Aggresiv press
        if (MatchStatisticManager.teamWithBall == 1 && teamTwoTactics.GetComponent<tacticsDefence>().DefencePress == defencePress.aggresive)
        {
            
            playerDefendTacklingStat += teamTwoTactics.GetComponent<tacticsDefence>().getIncreasedTacklingWhenAggresivePress();
       
        }

        if (MatchStatisticManager.teamWithBall == 2 && teamOneTactics.GetComponent<tacticsDefence>().DefencePress == defencePress.aggresive)
        {
            playerDefendTacklingStat += teamOneTactics.GetComponent<tacticsDefence>().getIncreasedTacklingWhenAggresivePress();
        }

        passCompletionProbFinishSim = basicProbPassCompletion + ((playerPassingStat + playerRecieveStat) - (playerDefendTacklingStat + playerDefendPositionStat));

        randomNumber = Random.Range(0, 99);

        //Debug.Log("Pass prob: " + passCompletionProbFinishSim);
        //Debug.Log("Pass randomNumber: " + randomNumber);

        if (randomNumber <= passCompletionProbFinishSim)
        {
            passCompletionBool = true;
        }

        else 
            passCompletionBool = false;

        return passCompletionBool;
    }








}
