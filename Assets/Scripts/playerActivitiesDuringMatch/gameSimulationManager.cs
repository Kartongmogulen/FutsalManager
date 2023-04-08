using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameSimulationManager : MonoBehaviour
{
    //Hanterar vad som händer i matchen

    public float timeBetweenActions;
    public int numberOfUpdatesCommentary; //Antal fördröjningar ("timeBetweenActions") som krävs för att synka kommentarer med script för funktion

    public playerActionManager PlayerActionManager;
    public gameCommentry GameCommentry;
    public GameObject gameCommentryPanel;

    public GameObject clockGamePanelGO;
    public clockGameUpdate ClockGameUpdate;

    public matchStatisticManager MatchStatisticManager;

    public GameObject teamTwoGO;
    public GameObject teamOneGO;

    public int iStartSim;
    private void Start()
    {
        PlayerActionManager = GetComponent<playerActionManager>();
        GameCommentry = gameCommentryPanel.GetComponent<gameCommentry>();
        ClockGameUpdate = clockGamePanelGO.GetComponent<clockGameUpdate>();
    }

    public void startSimulation()
    {
        //Debug.Log("StartSim Script: " + iStartSim);
        //iStartSim++;
        StartCoroutine(gameSimulation());
        //PlayerActionManager.counterPossibility = false;
    }

    public void counterAttack()
    {
        //Debug.Log("CounterAttack");
      
        if (MatchStatisticManager.teamWithBall == 2)
        {

            teamTwoGO.GetComponentInChildren<tacticsTeam>().winsBall();

        }

        else if (MatchStatisticManager.teamWithBall == 1)
        {
                      
            
            teamOneGO.GetComponentInChildren<tacticsTeam>().winsBall();
        }
        
        PlayerActionManager.counterPossibility = false;
    }

    public void gameSimulationRound()
    {
        //Debug.Log(MatchStatisticManager.teamWithBall);
        if (GameCommentry.commentryFinishedRound == true)
        {
            Debug.Log("StartSim Script: " + iStartSim);
            iStartSim++;

            //Debug.Log(MatchStatisticManager.teamWithBall);
            if (PlayerActionManager.counterPossibility == true)
            {
                GameCommentry.counterPosibilty = true;
                //Debug.Log("Kontring möjlig");
                counterAttack();
            }
            
            PlayerActionManager.whichPlayerHasTheBall();
            PlayerActionManager.whatActionWillThePlayerDo();
           
           
            //Debug.Log(PlayerActionManager.ballNewPosition);
            ClockGameUpdate.updateClock();
            GameCommentry.updateCommentryStart();
            //Debug.Log(MatchStatisticManager.teamWithBall);
        }
    }

    
    IEnumerator gameSimulation()
    {
        gameSimulationRound();
        yield return new WaitForSeconds(1f);
        startSimulation();
    }
    /*
        //numberOfUpdatesCommentary = 0;
        //Debug.Log(MatchStatisticManager.teamWithBall + " - " + PlayerActionManager.counterPossibility);
        if (PlayerActionManager.counterPossibility == true)
        {
            counterAttack();
            yield return new WaitForSeconds(timeBetweenActions);
            //PlayerActionManager.shootAttempt(0, PlayerActionManager.PlayerAttributesWithBall);
            //PlayerActionManager.whatActionWillThePlayerDo();


        }
        /* TESTA OM DETTA FUNKAR UTAN PROBLEM: KOMMENTERADE I BRIST PÅ TID
        else if(MatchStatisticManager.teamWithBall == 1 && PlayerActionManager.counterPossibility == true)
        {
            teamOneGO.GetComponent<tacticsTeam>().winsBall();

            PlayerActionManager.counterPossibility = false;
        }
        
        //else
      
        PlayerActionManager.whichPlayerHasTheBall();
        GameCommentry.updateCommentryPlayerWithBallName();
        PlayerActionManager.whatActionWillThePlayerDo();

        GameCommentry.updateCommentryStart();
        //Debug.Log("Antal fördröjningar: " + numberOfUpdatesCommentary);
        yield return new WaitForSeconds(numberOfUpdatesCommentary*timeBetweenActions);
        PlayerActionManager.ballPositionNow(PlayerActionManager.ballNewPosition);
        ClockGameUpdate.updateClock();
        GetComponent<fieldUIManager>().teamWithBallUIUpdate();
        startSimulation();
            
    }
    */
    

}
