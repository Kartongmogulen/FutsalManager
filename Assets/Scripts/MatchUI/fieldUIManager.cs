using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fieldUIManager : MonoBehaviour
{
    public playerActionManager PlayerActionManager;

    //Hanterar UI på planen

    public GameObject goalkeeperTeamOne;
    public GameObject goalkeeperTeamTwo;

    public GameObject teamOneFieldHalf;
    public GameObject teamTwoFieldHalf;

    public Vector2Int homeTeamKickOffPoss;
    public Vector2Int awayTeamKickOffPoss;




    private void Start()
    {
        if (GetComponent<matchStatisticManager>().teamWithBall == 1)
        {
            kickOffHomeTeam();
        }
        else
        {
            kickOffAwayTeam();
        }
        
    }

    public void afterRoundUpdate()
    {
        PlayerActionManager.ballPositionNow(PlayerActionManager.ballNewPosition);
        teamWithBallUIUpdate();
    }

    public void teamWithBallUIUpdate()
    {
        if (GetComponent<matchStatisticManager>().teamWithBall == 1)
        {
            setUpPitchTeamOneAttacks();
        }

        else
        {
            setUpPitchTeamTwoAttacks();
        }
    }

    //Om Lag två väljer att bygga upp ett anfallspel
    public void setUpPitchTeamTwoAttacks()
    {
        goalkeeperTeamOne.SetActive(true);
        goalkeeperTeamTwo.SetActive(false);

        //teamOneFieldHalf.SetActive(true);
        //teamTwoFieldHalf.SetActive(false);

    }

    public void setUpPitchTeamOneAttacks()
    {
        goalkeeperTeamOne.SetActive(false);
        goalkeeperTeamTwo.SetActive(true);

        //teamOneFieldHalf.SetActive(true);
        //teamTwoFieldHalf.SetActive(false);

    }

    public void kickOffHomeTeam()
    {
        setUpPitchTeamOneAttacks();
        GetComponent<playerActionManager>().ballPositionNow(homeTeamKickOffPoss);
    }

    public void kickOffAwayTeam()
    {
        setUpPitchTeamTwoAttacks();
        GetComponent<playerActionManager>().ballPositionNow(awayTeamKickOffPoss);
    }
    }
