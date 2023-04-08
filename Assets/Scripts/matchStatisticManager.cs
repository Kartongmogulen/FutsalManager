using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class matchStatisticManager : MonoBehaviour
{
    //Hanterar statistiken under match

    public int teamWithBall;

    //Lag 1
    [Header("Home Team:")]
    [SerializeField] float passAttemptTeamOne;    
    public Text passAttemptTeamOneUI;
    [SerializeField] float passCompletedTeamOne;    
    public Text passCompletedTeamOneUI;

    //Lag 2
    [Header("Away Team:")]
    [SerializeField] float passAttemptTeamTwo;
    public Text passAttemptTeamTwoUI;
    [SerializeField] float passCompletedTeamTwo;
    public Text passCompletedTeamTwoUI;

    public void passAttempt(bool passCompleted)
    {
        //Debug.Log("PassAttempt");
        if (teamWithBall == 1)
        {
            passAttemptTeamOne++;

            if(passCompleted == true)
            {
                passCompletedTeamOne++;
            }
        }

        if (teamWithBall == 2)
        {
            passAttemptTeamTwo++;

            if (passCompleted == true)
            {
                passCompletedTeamTwo++;
            }
        }

        updatePassUI();
    }

    public void updatePassUI()
    {
        //Lag 1
        passAttemptTeamOneUI.text = "" + passAttemptTeamOne;
        passCompletedTeamOneUI.text = "" + Mathf.RoundToInt((passCompletedTeamOne / passAttemptTeamOne) * 100) + "%";

        //Lag 2
        passAttemptTeamTwoUI.text = "" + passAttemptTeamTwo;
        passCompletedTeamTwoUI.text = "" + Mathf.RoundToInt((passCompletedTeamTwo / passAttemptTeamTwo) * 100) + "%";
    }

}
