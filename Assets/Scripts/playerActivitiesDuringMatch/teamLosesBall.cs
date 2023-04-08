using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teamLosesBall : MonoBehaviour
{

    public matchStatisticManager MatchStatisticManager;
    //Bollen hamnar hos motståndarlaget

    public void whichTeamHasTheBall(int teamWithBall)
    {
        //Debug.Log("TeamLosesBall: " + teamWithBall);
        if (teamWithBall == 1)
        {
            homeTeamDropsBall();
        }
        else
        {
            awayTeamDropsBall();
        }

        GetComponent<playerActionManager>().counterPossibility = true;
    }

    public void homeTeamDropsBall()
    {
        //Debug.Log("HomeTeamDropsBall");
        MatchStatisticManager.teamWithBall = 2;
        
    }

    public void awayTeamDropsBall() 
    {
       //Debug.Log("AwayTeamDropsBall");
        MatchStatisticManager.teamWithBall = 1;
    }
}
