using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameCommentry : MonoBehaviour
{
    public Text commentryText;

    public playerActionManager PlayerActionManager;
    public gameSimulationManager GameSimulationManager;
    public matchStatisticManager MatchStatisticManager;
    public fieldUIManager FieldUIManager;

    public goalCounter GoalCounter;
    public GameObject fieldGO;

    public float timeBetweenActions;

    public bool commentryFinishedRound; //När all kommentering är färdig = sant

    public string playerWithBall;
    public string playerWithBallAction;
    public bool actionCompletedOrNotBool;
    public bool counterPosibilty; //Kollar i början. Fördröjning mot realtid för att kommentarer ska funka.

    public string playerReceiverName;

    public int iTest;
    public int iWhoHasTheBall;
    public int iPlayerAction;

    private void Start()
    {
        //PlayerActionManager = fieldGO.GetComponent<playerActionManager>();
        //GameSimulationManager = fieldGO.GetComponent<gameSimulationManager>();
        timeBetweenActions = GameSimulationManager.timeBetweenActions;
    }

    public void updateCommentryStart()
    {
        commentryFinishedRound = false;
        StartCoroutine(updateCommentryDelay());
    }

    IEnumerator updateCommentryDelay()
    {
        //yield return new WaitForSeconds(timeBetweenActions);

        //counterPosibilty = PlayerActionManager.counterPossibility;
        //Debug.Log(counterPosibilty);

        updateCommentryPlayerWithBallName();
        yield return new WaitForSeconds(timeBetweenActions);
        updateCommentryPlayerWithBallAction();
        yield return new WaitForSeconds(timeBetweenActions);
        if (counterPosibilty == true)
        {
            //Debug.Log("CounterPos: " + PlayerActionManager.counterPossibility);
            counterAttackUpdateCommentry();
            counterPosibilty = false;
            //yield return new WaitForSeconds(timeBetweenActions);
            //actionCompletedOrNot();
        }
        else
        actionCompletedOrNot();
        yield return new WaitForSeconds(timeBetweenActions);

        //if (MatchStatisticManager.teamWithBall == 2 && PlayerActionManager.counterPossibility == true)
        
        /*else
        {
            yield break;
        }*/
        //yield return new WaitForSeconds(timeBetweenActions);
        iTest++;
        FieldUIManager.afterRoundUpdate();
        commentryFinishedRound = true;
        //Debug.Log("commentryFinishedRound "+ iTest);
    }

    public void updateCommentryPlayerWithBallName()
    {
        //Debug.Log("updateCommentryPlayer: " + iWhoHasTheBall);
        //GameSimulationManager.numberOfUpdatesCommentary++; //Synka kommentarfält med script för funktioner
        playerWithBall = PlayerActionManager.PlayerAttributesWithBall.namePlayer;
        //Debug.Log("Name Player: " + playerWithBall);
        commentryText.text = playerWithBall + " has the ball.";
        iWhoHasTheBall++;
    }

    public void updateCommentryPlayerWithBallAction()
    {
        Debug.Log("updateCommentryAction: " + iPlayerAction + " " + PlayerActionManager.playerWithBallAction);
        iPlayerAction++;
        //GameSimulationManager.numberOfUpdatesCommentary++; //Synka kommentarfält med script för funktioner
        //Debug.Log("updateCommentryPlayerWithBallAction");
        if (PlayerActionManager.playerWithBallAction == "pass")
        {
            playerReceiverName = PlayerActionManager.PlayerAttributesReceiverBall.namePlayer;
            commentryText.text = playerWithBall + " has the ball." + "Tries to " + PlayerActionManager.playerWithBallAction + " to " + playerReceiverName;
        }

        if (PlayerActionManager.playerWithBallAction == "Dribble")
        {
            commentryText.text = playerWithBall + " has the ball." + "Tries to " + PlayerActionManager.playerWithBallAction + " and get past";
            //GameSimulationManager.numberOfUpdatesCommentary += 2; //Synka kommentarfält med script för funktioner
        }

        if (PlayerActionManager.playerWithBallAction == "shoot")
        {
            commentryText.text = "Takes a shoot";
            //Debug.Log("Takes a shot!");
        }

        
    }

    public void counterAttackUpdateCommentry()
    {
        Debug.Log(PlayerActionManager.playerWithBallAction);
        //PlayerActionManager.whatActionWillThePlayerDo();
        //Debug.Log(PlayerActionManager.playerWithBallAction);
        if (PlayerActionManager.playerWithBallAction == "CounterAttack")
        {
            Debug.Log("CounterAttackUpdateCommentry");
            commentryText.text = "Runs down the pitch!";
            //PlayerActionManager.playerWithBallAction = "shoot";

        }

        if (PlayerActionManager.playerWithBallAction == "CounterAttackThroughBall")
        {
            commentryText.text = "Througball attempt!";
            //PlayerActionManager.playerWithBallAction = "shoot";
        }
    }

    public void actionCompletedOrNot()
    {
        Debug.Log("ActionCompletedOrNot");
        if (PlayerActionManager.playerWithBallAction == "pass")
        {

            actionCompletedOrNotBool = PlayerActionManager.passCompletionBool;

            if (actionCompletedOrNotBool == true)
                commentryText.text = playerReceiverName + " recieves the pass";

            if (actionCompletedOrNotBool == false)
                commentryText.text = playerReceiverName + " drops the ball";

            return;
        }

        if (PlayerActionManager.playerWithBallAction == "Dribble")
        {
            actionCompletedOrNotBool = PlayerActionManager.dribbleCompletionBool;

            if (actionCompletedOrNotBool == true)
            {
                commentryText.text = "Gets past his opponent!";
                //PlayerActionManager.shootAttempt(0, PlayerActionManager.PlayerAttributesWithBall);
                PlayerActionManager.playerWithBallAction = "dribbleSucceded";
            }

            if (actionCompletedOrNotBool == false)
                commentryText.text = "Drops the ball!";

            return;
        }

        if (PlayerActionManager.playerWithBallAction == "shoot" || PlayerActionManager.playerWithBallAction == "CounterAttackThroughBall")
        {
            actionCompletedOrNotBool = PlayerActionManager.shootCompletionBool;
            if (actionCompletedOrNotBool == true)
            {
                //GameSimulationManager.numberOfUpdatesCommentary++; //Synka kommentarfält med script för funktioner
                goalScored();
            }
            else
            {
                
                commentryText.text = "Goalkeeper blocks the shot!";
                PlayerActionManager.shootBlockedByGoalkeeper();
                //StartCoroutine(shotAttemptWhereWillTheBallEndUp());
            }
            return;
        }      

        if (PlayerActionManager.playerWithBallAction == "CounterAttackThroughBall")
        {
            if (PlayerActionManager.passCompletionBool == true)
            {
                commentryText.text = PlayerActionManager.PlayerAttributesReceiverBall.namePlayer + " is alone with the goalkeeper!";
            }

            

        }

    }

    public void goalScored()
    {
        StartCoroutine(goalScoredCommentryEffect());
        if (MatchStatisticManager.teamWithBall == 1)
        {
            GoalCounter.addGoalHometeam();
        }

        else if (MatchStatisticManager.teamWithBall == 2)
        {
            GoalCounter.addGoalAwayteam();
        }
    }

    IEnumerator goalScoredCommentryEffect()
    {
       
            commentryText.text = "GOAL!!!";
            yield return new WaitForSeconds(timeBetweenActions/4);
            commentryText.text = "";
            yield return new WaitForSeconds(timeBetweenActions / 4);
            commentryText.text = "GOAL!!!";
            yield return new WaitForSeconds(timeBetweenActions / 4);
            commentryText.text = "";
            yield return new WaitForSeconds(timeBetweenActions / 4);
            commentryText.text = "GOAL!!!";

    }

    IEnumerator shotAttemptWhereWillTheBallEndUp()
    {
        //Debug.Log("WhereWillBallEndUp");
        //GameSimulationManager.numberOfUpdatesCommentary++; //Synka kommentarfält med script för funktioner
        yield return new WaitForSeconds(timeBetweenActions);
        GameSimulationManager.numberOfUpdatesCommentary++; //Synka kommentarfält med script för funktioner
        commentryText.text = "Ball deflects. Duel between " + PlayerActionManager.PlayerAttributesWithBall.namePlayer + " and " + PlayerActionManager.PlayerAttributesDefending.namePlayer;
        PlayerActionManager.whichPlayerHasTheBall(); //Uppdaterar vem som har bollen efter duell.
        yield return new WaitForSeconds(timeBetweenActions);
        //GameSimulationManager.numberOfUpdatesCommentary++; //Synka kommentarfält med script för funktioner
        commentryText.text = PlayerActionManager.PlayerAttributesWithBall.namePlayer + " wins the rebound!";
    }

}

