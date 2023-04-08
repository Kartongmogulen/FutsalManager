using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerActionManager : MonoBehaviour
{
    public int i; //FÖR TEST

    //Hanterar centralt hur en spelare agerar

    public passing Passing;
    public dribbling Dribbling;
    public ballPosition BallPosition;
    public shoot Shoot;
    public shootBlocked ShootBlocked;
    public matchStatisticManager MatchStatisticManager;
    public gameSimulationManager GameSimulationManager;
    public tacticsStatsEffect TacticsStatsEffect;

    [Header("Pass")]
    //public float passCompletionProb;//Sannolikheten att passning genomförs
    public float passActionProb;//Slh att spelaren passar
    public float passProbY;
    public float passProbX;
    public float passProbDiagonally;
    public bool passCompletionBool;

    [Header("Dribbla")]
    public float dribbleActionProb;//Slh att spelaren passar
    public bool dribbleCompletionBool;

    [Header("Skott")]
    public bool shootCompletionBool;

    [Header("Other")]
    public GameObject gameFieldGO;

    public GameObject gameFieldGoalkeeper;

    public GameObject gameFieldTopLeftAttackPlayer;
    public GameObject gameFieldTopLeftDefencePlayer;

    public GameObject gameFieldBottomRightAttackPlayer;
    public GameObject gameFieldBottomRightDefencePlayer;

    public GameObject gameFieldBottomLeft;
    public GameObject gameFieldBottomLeftAttackPlayer;
    public GameObject gameFieldBottomLeftDefencePlayer;

    public GameObject gameFieldTopRightAttackPlayer;
    public GameObject gameFieldTopRightDefencePlayer;
    public GameObject gameFieldTopLeft;

    public playerAttributes PlayerAttributesGoalkeeper;
    public playerAttributes PlayerAttributesWithBall;
    public playerAttributes PlayerAttributesReceiverBall;
    public playerAttributes PlayerAttributesDefenceReceiverBall; //Spelaren i försvarande laget i samma kvadrat som mottagaren
    public playerAttributes PlayerAttributesAttacking;
    public playerAttributes PlayerAttributesDefending;

    public string playerWithBallAction;//Vad  gör spelaren med bollen. Pass, Skott, Dribbling

    public Vector2Int ballNewPosition;
    public Vector2Int ballNowPosition;

    public int newPositionX;
    public int newPositionY;

    public int oldPositionX;
    public int oldPositionY;

    public float randomNumber;

    public int teamWithTheBall;
    public bool counterPossibility; //Om laget med bollen förlorar den till andra laget

    private void Start()
    {
        Passing = GetComponent<passing>();
        Dribbling = GetComponent<dribbling>();
        Shoot = GetComponent<shoot>();
        MatchStatisticManager = GetComponent<matchStatisticManager>();
        BallPosition = GetComponent<ballPosition>();
        ballNowPosition = BallPosition.ballPositionQudrant;
    }

    public void ballPositionNow(Vector2Int ballNewPosition)
    {
        BallPosition.ballPositionOnField(ballNewPosition);
    }


    public void whichPlayerHasTheBall()
    {
        //Debug.Log("TeamWithBall: " + MatchStatisticManager.teamWithBall);
        ballNowPosition = BallPosition.ballPositionQudrant; //Hämtar bollens position
        if (MatchStatisticManager.teamWithBall == 1)
        {
            if (ballNowPosition.x == 2 && ballNowPosition.y == 2)
            {
                PlayerAttributesWithBall = gameFieldBottomRightAttackPlayer.GetComponentInChildren<playerAttributes>();
            }

            if (ballNowPosition.x == 2 && ballNowPosition.y == 1)
            {
                PlayerAttributesWithBall = gameFieldTopRightAttackPlayer.GetComponentInChildren<playerAttributes>();
            }

            if (ballNowPosition.x == 1 && ballNowPosition.y == 2)
            {
                PlayerAttributesWithBall = gameFieldBottomLeftAttackPlayer.GetComponentInChildren<playerAttributes>();
            }

            if (ballNowPosition.x == 1 && ballNowPosition.y == 1)
            {
                PlayerAttributesWithBall = gameFieldTopLeftAttackPlayer.GetComponentInChildren<playerAttributes>();
            }
        }

        else
        {
            if (ballNowPosition.x == 2 && ballNowPosition.y == 2)
            {
                PlayerAttributesWithBall = gameFieldBottomRightDefencePlayer.GetComponentInChildren<playerAttributes>();
            }

            if (ballNowPosition.x == 2 && ballNowPosition.y == 1)
            {
                PlayerAttributesWithBall = gameFieldTopRightDefencePlayer.GetComponentInChildren<playerAttributes>();
            }

            if (ballNowPosition.x == 1 && ballNowPosition.y == 2)
            {
                PlayerAttributesWithBall = gameFieldBottomLeftDefencePlayer.GetComponentInChildren<playerAttributes>();
            }

            if (ballNowPosition.x == 1 && ballNowPosition.y == 1)
            {
                PlayerAttributesWithBall = gameFieldTopLeftDefencePlayer.GetComponentInChildren<playerAttributes>();
            }
        }
        //Debug.Log("Player With Ball: " + PlayerAttributesWithBall.namePlayer);
    }

    public void whichPlayerIsDefendingInQudrant()
    {
        if (ballNowPosition.x == 1 && ballNowPosition.y == 1)
        {
            PlayerAttributesDefending = gameFieldTopLeftDefencePlayer.GetComponentInChildren<playerAttributes>();
        }

        if (ballNowPosition.x == 1 && ballNowPosition.y == 2)
        {
            PlayerAttributesDefending = gameFieldBottomLeftDefencePlayer.GetComponentInChildren<playerAttributes>();
        }

        if (ballNowPosition.x == 2 && ballNowPosition.y == 1)
        {
            PlayerAttributesDefending = gameFieldTopRightDefencePlayer.GetComponentInChildren<playerAttributes>();
        }

        if (ballNowPosition.x == 2 && ballNowPosition.y == 2)
        {
            PlayerAttributesDefending = gameFieldBottomRightDefencePlayer.GetComponentInChildren<playerAttributes>();
        }
    }

    //KOMMER SPELAREN PASSA, SKJUTA ELLER DRIBBLA
    public void whatActionWillThePlayerDo()
    {
        i++;
        Debug.Log("whatActionWillThePlayerDo: " + i +" " + playerWithBallAction);
        randomNumber = Random.Range(0, 99); //Kommer spelaren passa, skjuta eller dribbla
        //randomNumber = 0;//TEST

        ballNowPosition = BallPosition.ballPositionQudrant; //Bollens position

        if (playerWithBallAction == "BuildUpAttack")
        {
            //Debug.Log("BuildUpAttack PAM");
            passTheBall();
            return;
        }

        if (playerWithBallAction == "CounterAttack")
        {
            //Debug.Log("BuildUpAttack PAM");
            GetComponent<counterAttack>().counterAttackAction(MatchStatisticManager.teamWithBall, ballNowPosition);
            return;
        }

        if (playerWithBallAction == "CounterAttackThroughBall")
        {
            Debug.Log("SHOT");
            shootAttempt(TacticsStatsEffect.counterShootModifier, PlayerAttributesWithBall);
            return;
        }

        if (playerWithBallAction == "dribbleSucceded")
        {
            shootAttempt(0, PlayerAttributesWithBall);
            return;
            
        }
        else
        {

            //PASSA
            if (randomNumber <= (passActionProb))
            {
                passTheBall();
                return;
            }

            //DRIBBLA
            else if (randomNumber <= passActionProb + dribbleActionProb)
            {
                dribbleAttempt();
                return;
            }

            else
            {
                Debug.Log("PlayerActionManager Else");
                shootAttempt(0, PlayerAttributesWithBall);
            }
        }
        //Debug.Log(MatchStatisticManager.teamWithBall);
       
    }

    public void dribbleAttempt()
    {
        
        GameSimulationManager.numberOfUpdatesCommentary = 3;
        if (MatchStatisticManager.teamWithBall == 1) {
            if (ballNowPosition.x == 1 && ballNowPosition.y == 1)
            {
                PlayerAttributesWithBall = gameFieldTopLeftAttackPlayer.GetComponentInChildren<playerAttributes>();
                PlayerAttributesDefending = gameFieldTopLeftDefencePlayer.GetComponentInChildren<playerAttributes>();
            }

            if (ballNowPosition.x == 1 && ballNowPosition.y == 2)
            {
                PlayerAttributesWithBall = gameFieldBottomLeftAttackPlayer.GetComponentInChildren<playerAttributes>();
                PlayerAttributesDefending = gameFieldBottomLeftDefencePlayer.GetComponentInChildren<playerAttributes>();
            }

            if (ballNowPosition.x == 2 && ballNowPosition.y == 1)
            {
                PlayerAttributesWithBall = gameFieldTopRightAttackPlayer.GetComponentInChildren<playerAttributes>();
                PlayerAttributesDefending = gameFieldTopRightDefencePlayer.GetComponentInChildren<playerAttributes>();
            }

            if (ballNowPosition.x == 2 && ballNowPosition.y == 2)
            {
                PlayerAttributesWithBall = gameFieldBottomRightAttackPlayer.GetComponentInChildren<playerAttributes>();
                PlayerAttributesDefending = gameFieldBottomRightDefencePlayer.GetComponentInChildren<playerAttributes>();
            }
            //Debug.Log("PlayerName: " + PlayerAttributesWithBall.namePlayer);

        }
        else
        {
            if (ballNowPosition.x == 1 && ballNowPosition.y == 1)
            {
                PlayerAttributesWithBall = gameFieldTopLeftDefencePlayer.GetComponentInChildren<playerAttributes>();
                PlayerAttributesDefending = gameFieldTopLeftAttackPlayer.GetComponentInChildren<playerAttributes>();
            }

            if (ballNowPosition.x == 1 && ballNowPosition.y == 2)
            {
                PlayerAttributesWithBall = gameFieldBottomLeftDefencePlayer.GetComponentInChildren<playerAttributes>();
                PlayerAttributesDefending = gameFieldBottomLeftAttackPlayer.GetComponentInChildren<playerAttributes>();
            }

            if (ballNowPosition.x == 2 && ballNowPosition.y == 1)
            {
                PlayerAttributesWithBall = gameFieldTopRightDefencePlayer.GetComponentInChildren<playerAttributes>();
                PlayerAttributesDefending = gameFieldTopRightAttackPlayer.GetComponentInChildren<playerAttributes>();

            }

            if (ballNowPosition.x == 2 && ballNowPosition.y == 2)
            {
                PlayerAttributesWithBall = gameFieldBottomRightDefencePlayer.GetComponentInChildren<playerAttributes>();
                PlayerAttributesDefending = gameFieldBottomRightAttackPlayer.GetComponentInChildren<playerAttributes>();

            }
           
        }
        playerWithBallAction = "Dribble";
        dribbleCompletionBool = Dribbling.actionCompletion(PlayerAttributesWithBall.technique, PlayerAttributesWithBall.movement, PlayerAttributesDefending.tackling, PlayerAttributesDefending.positioning);

        if (dribbleCompletionBool == true)
        {
            //Debug.Log("Dribble succed");
            //shootAttempt(0, PlayerAttributesWithBall);
            //playerWithBallAction = "shoot";
        }

        else
            GetComponent<teamLosesBall>().whichTeamHasTheBall(MatchStatisticManager.teamWithBall);

        return;
    }

    public void shootAttempt(int shotModifier, playerAttributes Player)
    {
        //Debug.Log("ShotAttempt");
        /*if (ballNowPosition.x == 2 && ballNowPosition.y == 1)
        {
            PlayerAttributesWithBall = gameFieldTopRightAttackPlayer.GetComponentInChildren<playerAttributes>();
        }


        if (ballNowPosition.x == 1 && ballNowPosition.y == 1)
        {
            PlayerAttributesWithBall = gameFieldTopLeftAttackPlayer.GetComponentInChildren<playerAttributes>();
        }*/
        
        PlayerAttributesWithBall = Player;
        PlayerAttributesGoalkeeper = gameFieldGoalkeeper.GetComponentInChildren<playerAttributes>();

        //Debug.Log("Shoot attempt!");
        playerWithBallAction = "shoot";
        shootCompletionBool = Shoot.actionCompletion(PlayerAttributesWithBall.shoot + shotModifier, PlayerAttributesGoalkeeper.goalkeeping);
        Debug.Log("ShotAttempt: " + shootCompletionBool);
        /*
        if (shootCompletionBool == true)
        {
            GameSimulationManager.numberOfUpdatesCommentary = 4;
        }
        else
            GameSimulationManager.numberOfUpdatesCommentary = 5;
            */
    }

    public void shootBlockedByGoalkeeper()
    {
        //playerWithBallAction = "shootBlocked";
        //GameSimulationManager.numberOfUpdatesCommentary = 5;
        ShootBlocked.whichQuadrantBallEndsUp();
        whichPlayerHasTheBall(); //Vilken spelare i kvadranten bollen landar i laget som har bollen innan duellen
        whichPlayerIsDefendingInQudrant();
        teamWithTheBall = ShootBlocked.playerThatGetsTheReturn(PlayerAttributesWithBall.tackling, PlayerAttributesDefending.tackling);

        //Om laget med bollen förlorar den till andra laget
        if (teamWithTheBall != MatchStatisticManager.teamWithBall)
        {
            counterPossibility = true;
        }
        MatchStatisticManager.teamWithBall = teamWithTheBall;

    }

    public void passTheBall()
    {
        randomNumber = Random.Range(0, 99); //Var kommer spelaren passa          
        ballNowPosition = BallPosition.ballPositionQudrant; //Bollens position
        GameSimulationManager.numberOfUpdatesCommentary = 3;
        //Vilket lag har bollen?
        //I vilken led vill laget passa?
        //Y-led
        //X-led
        //Diagonalt
        //Lyckas laget med passningen?
        //Vem passar?
        //Vem ska ta emot passning?
        //Vem försvarar?

        if (MatchStatisticManager.teamWithBall == 1)
        {
            if (randomNumber <= passProbY && ballNowPosition.y == 2) //Lag 1 testar passa framåt
            {
                
                //Vilka spelare är involverade i situationen
                if (ballNowPosition.x == 2)
                {
                    PlayerAttributesReceiverBall = gameFieldTopRightAttackPlayer.GetComponentInChildren<playerAttributes>();
                    PlayerAttributesDefenceReceiverBall = gameFieldTopRightDefencePlayer.GetComponentInChildren<playerAttributes>();
                }

                if (ballNowPosition.x == 1)
                {
                    PlayerAttributesReceiverBall = gameFieldTopLeftAttackPlayer.GetComponentInChildren<playerAttributes>();
                    PlayerAttributesDefenceReceiverBall = gameFieldBottomLeftDefencePlayer.GetComponentInChildren<playerAttributes>();
                }

                //Kontrollerar om passing lyckas eller ej
                playerWithBallAction = "pass";
                passCompletionBool = Passing.passCompletion(PlayerAttributesWithBall.passing, PlayerAttributesReceiverBall.technique, PlayerAttributesDefenceReceiverBall.tackling, PlayerAttributesDefenceReceiverBall.positioning);

                if (passCompletionBool == true)
                {
                    passTheBallForward(MatchStatisticManager.teamWithBall);
                    
                    MatchStatisticManager.passAttempt(true);
                    //Debug.Log("Pass Succed");
                }

                else
                {
                    //Debug.Log("Pass fail");
                    passTheBallForward(MatchStatisticManager.teamWithBall);
                    MatchStatisticManager.passAttempt(false);

                    GetComponent<teamLosesBall>().whichTeamHasTheBall(MatchStatisticManager.teamWithBall);
                }
                //i++;
                
                return;

            }

            if (randomNumber <= passProbY && ballNowPosition.y == 1) //Lag 1 testar passa bakåt
            {
                //Vilka spelare är involverade i situationen
                if (ballNowPosition.x == 2)
                {
                    PlayerAttributesReceiverBall = gameFieldBottomRightAttackPlayer.GetComponentInChildren<playerAttributes>();
                    PlayerAttributesDefenceReceiverBall = gameFieldBottomRightDefencePlayer.GetComponentInChildren<playerAttributes>();
                }

                if (ballNowPosition.x == 1)
                {
                    PlayerAttributesReceiverBall = gameFieldBottomLeftAttackPlayer.GetComponentInChildren<playerAttributes>();
                    PlayerAttributesDefenceReceiverBall = gameFieldBottomLeftDefencePlayer.GetComponentInChildren<playerAttributes>();
                }

                //Kontrollerar om passing lyckas eller ej
                playerWithBallAction = "pass";
                passCompletionBool = Passing.passCompletion(PlayerAttributesWithBall.passing, PlayerAttributesReceiverBall.technique, PlayerAttributesDefenceReceiverBall.tackling, PlayerAttributesDefenceReceiverBall.positioning);

                if (passCompletionBool == true)
                {
                    passTheBallBack(MatchStatisticManager.teamWithBall);

                    MatchStatisticManager.passAttempt(true);
                    //Debug.Log("Pass Succed");
                }

                else
                {
                    //Debug.Log("Pass fail");
                    passTheBallBack(MatchStatisticManager.teamWithBall);
                    MatchStatisticManager.passAttempt(false);
                    GetComponent<teamLosesBall>().whichTeamHasTheBall(MatchStatisticManager.teamWithBall);
                }

                return;
            }

            //Passar Sidled
            if (randomNumber <= passProbY + passProbX && ballNowPosition.y == 2)
            {
                if (ballNowPosition.x == 2)
                {
                    PlayerAttributesReceiverBall = gameFieldBottomLeftAttackPlayer.GetComponentInChildren<playerAttributes>();
                    PlayerAttributesDefenceReceiverBall = gameFieldBottomLeftDefencePlayer.GetComponentInChildren<playerAttributes>();
                }

                if (ballNowPosition.x == 1)
                {
                    PlayerAttributesReceiverBall = gameFieldBottomRightAttackPlayer.GetComponentInChildren<playerAttributes>();
                    PlayerAttributesDefenceReceiverBall = gameFieldBottomRightDefencePlayer.GetComponentInChildren<playerAttributes>();
                }

                passCompletionBool = Passing.passCompletion(PlayerAttributesWithBall.passing, PlayerAttributesReceiverBall.technique, PlayerAttributesDefenceReceiverBall.tackling, PlayerAttributesDefenceReceiverBall.positioning);
                playerWithBallAction = "pass";

                if (passCompletionBool == true)
                {
                    passTheBallSideways();
                    MatchStatisticManager.passAttempt(true);
                }

                else
                {
                    //Debug.Log("Pass fail");
                    passTheBallSideways();
                    MatchStatisticManager.passAttempt(false);
                    GetComponent<teamLosesBall>().whichTeamHasTheBall(MatchStatisticManager.teamWithBall);

                }

                return;
            }

            if (randomNumber <= passProbY + passProbX && ballNowPosition.y == 1)
            {
                if (ballNowPosition.x == 2)
                {
                    PlayerAttributesReceiverBall = gameFieldTopLeftAttackPlayer.GetComponentInChildren<playerAttributes>();
                    PlayerAttributesDefenceReceiverBall = gameFieldTopLeftDefencePlayer.GetComponentInChildren<playerAttributes>();
                }

                if (ballNowPosition.x == 1)
                {
                    PlayerAttributesReceiverBall = gameFieldTopRightAttackPlayer.GetComponentInChildren<playerAttributes>();
                    PlayerAttributesDefenceReceiverBall = gameFieldTopRightDefencePlayer.GetComponentInChildren<playerAttributes>();
                }

                passCompletionBool = Passing.passCompletion(PlayerAttributesWithBall.passing, PlayerAttributesReceiverBall.technique, PlayerAttributesDefenceReceiverBall.tackling, PlayerAttributesDefenceReceiverBall.positioning);
                playerWithBallAction = "pass";

                if (passCompletionBool == true)
                {
                    passTheBallSideways();
                    MatchStatisticManager.passAttempt(true);
                }

                else
                {
                    Debug.Log("Pass fail");
                    passTheBallSideways();
                    MatchStatisticManager.passAttempt(false);
                    GetComponent<teamLosesBall>().whichTeamHasTheBall(MatchStatisticManager.teamWithBall);

                }

                return;
            }
        }

        //LAG 2
        if (MatchStatisticManager.teamWithBall == 2)
        {
            if (randomNumber <= passProbY && ballNowPosition.y == 1) //Lag 2 testar passa framåt
            {
                //Vilka spelare är involverade i situationen
                if (ballNowPosition.x == 2)
                {
                    PlayerAttributesReceiverBall = gameFieldBottomRightDefencePlayer.GetComponentInChildren<playerAttributes>();
                    PlayerAttributesDefenceReceiverBall = gameFieldBottomRightAttackPlayer.GetComponentInChildren<playerAttributes>();

                }

                if (ballNowPosition.x == 1)
                {
                    PlayerAttributesReceiverBall = gameFieldBottomLeftDefencePlayer.GetComponentInChildren<playerAttributes>();
                    PlayerAttributesDefenceReceiverBall = gameFieldBottomLeftAttackPlayer.GetComponentInChildren<playerAttributes>();

                }

                //Kontrollerar om passing lyckas eller ej
                playerWithBallAction = "pass";
                passCompletionBool = Passing.passCompletion(PlayerAttributesWithBall.passing, PlayerAttributesReceiverBall.technique, PlayerAttributesDefenceReceiverBall.tackling, PlayerAttributesDefenceReceiverBall.positioning);

                if (passCompletionBool == true)
                {
                    passTheBallForward(MatchStatisticManager.teamWithBall);
                    MatchStatisticManager.passAttempt(true);
                    Debug.Log("Pass Succed");
                }

                else
                {
                    Debug.Log("Pass fail");
                    passTheBallForward(MatchStatisticManager.teamWithBall);
                    MatchStatisticManager.passAttempt(false);
                    GetComponent<teamLosesBall>().whichTeamHasTheBall(MatchStatisticManager.teamWithBall);

                }
                //i++;
                return;

            }


            if (randomNumber <= passProbY && ballNowPosition.y == 2) //Lag 2 testar passa bakåt
            {
                //Vilka spelare är involverade i situationen
                if (ballNowPosition.x == 2)
                {
                    PlayerAttributesReceiverBall = gameFieldTopRightDefencePlayer.GetComponentInChildren<playerAttributes>();
                    PlayerAttributesDefenceReceiverBall = gameFieldTopRightAttackPlayer.GetComponentInChildren<playerAttributes>();

                }
                
                if (ballNowPosition.x == 1)
                {
                    PlayerAttributesReceiverBall = gameFieldTopLeftDefencePlayer.GetComponentInChildren<playerAttributes>();
                    PlayerAttributesDefenceReceiverBall = gameFieldTopLeftAttackPlayer.GetComponentInChildren<playerAttributes>();
                    
                }
                
                //Kontrollerar om passing lyckas eller ej
                playerWithBallAction = "pass";
                passCompletionBool = Passing.passCompletion(PlayerAttributesWithBall.passing, PlayerAttributesReceiverBall.technique, PlayerAttributesDefenceReceiverBall.tackling, PlayerAttributesDefenceReceiverBall.positioning);

                if (passCompletionBool == true)
                {
                    passTheBallBack(MatchStatisticManager.teamWithBall);
                    MatchStatisticManager.passAttempt(true);
                    Debug.Log("Pass Succed");
                }

                else
                {
                    Debug.Log("Pass fail");
                    passTheBallBack(MatchStatisticManager.teamWithBall);
                    MatchStatisticManager.passAttempt(false);
                    GetComponent<teamLosesBall>().whichTeamHasTheBall(MatchStatisticManager.teamWithBall);
                }

                //i++;
                return;

            }

            //Passar Sidled
            if (randomNumber <= passProbY + passProbX && ballNowPosition.y == 2)
            {
                if (ballNowPosition.x == 2)
                {
                    PlayerAttributesReceiverBall = gameFieldBottomLeftDefencePlayer.GetComponentInChildren<playerAttributes>();
                    PlayerAttributesDefenceReceiverBall = gameFieldBottomLeftAttackPlayer.GetComponentInChildren<playerAttributes>();

                }

                if (ballNowPosition.x == 1)
                {
                    PlayerAttributesReceiverBall = gameFieldBottomRightDefencePlayer.GetComponentInChildren<playerAttributes>();
                    PlayerAttributesDefenceReceiverBall = gameFieldBottomRightAttackPlayer.GetComponentInChildren<playerAttributes>();

                }

                passCompletionBool = Passing.passCompletion(PlayerAttributesWithBall.passing, PlayerAttributesReceiverBall.technique, PlayerAttributesDefenceReceiverBall.tackling, PlayerAttributesDefenceReceiverBall.positioning);
                playerWithBallAction = "pass";

                if (passCompletionBool == true)
                {
                    passTheBallSideways();
                    MatchStatisticManager.passAttempt(true);
                }

                else
                {
                    Debug.Log("Pass fail");
                    passTheBallSideways();
                    MatchStatisticManager.passAttempt(false);
                    GetComponent<teamLosesBall>().whichTeamHasTheBall(MatchStatisticManager.teamWithBall);
                }

                return;
            }

            if (randomNumber <= passProbY + passProbX && ballNowPosition.y == 1)
            {
                if (ballNowPosition.x == 2)
                {
                    PlayerAttributesReceiverBall = gameFieldTopLeftDefencePlayer.GetComponentInChildren<playerAttributes>();
                    PlayerAttributesDefenceReceiverBall = gameFieldTopLeftAttackPlayer.GetComponentInChildren<playerAttributes>();
                }

                if (ballNowPosition.x == 1)
                {
                    PlayerAttributesReceiverBall = gameFieldTopRightDefencePlayer.GetComponentInChildren<playerAttributes>();
                    PlayerAttributesDefenceReceiverBall = gameFieldTopRightAttackPlayer.GetComponentInChildren<playerAttributes>();
                }

                passCompletionBool = Passing.passCompletion(PlayerAttributesWithBall.passing, PlayerAttributesReceiverBall.technique, PlayerAttributesDefenceReceiverBall.tackling, PlayerAttributesDefenceReceiverBall.positioning);
                playerWithBallAction = "pass";

                if (passCompletionBool == true)
                {
                    passTheBallSideways();
                    MatchStatisticManager.passAttempt(true);
                }

                else
                {
                    Debug.Log("Pass fail");
                    passTheBallSideways();
                    MatchStatisticManager.passAttempt(false);
                    GetComponent<teamLosesBall>().whichTeamHasTheBall(MatchStatisticManager.teamWithBall);
                }

                return;
            }
        }

    
    }

        //PASSNING FRAMÅT
        public void passTheBallForward(int teamWithBall)
        {
            //Debug.Log("Pass The Ball Forward");
            ballNowPosition = BallPosition.ballPositionQudrant; //Hämtar bollens position innan passning
            oldPositionY = ballNowPosition.y;

            if (teamWithBall == 1)
            {
                //Debug.Log("Team One has ball");
                newPositionY = Passing.forwardPass(oldPositionY, teamWithBall); //Bollens position efter passning

            }
            else
            {
                //Debug.Log("Team Two has ball");
                newPositionY = Passing.forwardPass(oldPositionY, teamWithBall); //Bollens position efter passning
            }
            ballNewPosition = new Vector2Int(ballNowPosition.x, newPositionY);
            //MÅSTE AVKOMMENTERAS OM JAG VILL ANVÄNDA EGEN KNAPP
            //ballPositionNow(ballNewPosition); 
            //BallPosition.ballPositionOnField(ballNewPosition); //Ändrar bollens position efter passning

        }

        //PASSNING BAKÅT
        public void passTheBallBack(int teamWithBall)
        {
            ballNowPosition = BallPosition.ballPositionQudrant;//Hämtar bollens position innan passning
            oldPositionY = ballNowPosition.y;

            if (teamWithBall == 1)
            {
                //Debug.Log("oldPositionY: " + oldPositionY);
                newPositionY = Passing.backwardPass(oldPositionY,1);//Bollens position efter passning
            }
            if (teamWithBall == 2)
            {
                //Debug.Log("Team Two has ball");
                newPositionY = Passing.backwardPass(oldPositionY, 2);//Bollens position efter passning
            }
            //Debug.Log("newPositionY " + newPositionY);

            ballNewPosition = new Vector2Int(ballNowPosition.x, newPositionY);

            //MÅSTE AVKOMMENTERAS OM JAG VILL ANVÄNDA EGEN KNAPP
            //ballPositionNow(ballNewPosition); 
            //BallPosition.ballPositionOnField(ballNewPosition);//Ändrar bollens position efter passning
        }

        //PASSNING SIDLED
        public void passTheBallSideways()
        {
            ballNowPosition = BallPosition.ballPositionQudrant;//Hämtar bollens position innan passning
            oldPositionX = ballNowPosition.x;

            if (oldPositionX == 2) { 
            newPositionX = Passing.sidewaysPassLeft(oldPositionX);
            }

            if (oldPositionX == 1)
            {
                newPositionX = Passing.sidewaysPassRight(oldPositionX);
            }

            ballNewPosition = new Vector2Int(newPositionX, ballNowPosition.y);

            //MÅSTE AVKOMMENTERAS OM JAG VILL ANVÄNDA EGEN KNAPP
            //ballPositionNow(ballNewPosition); 
            //BallPosition.ballPositionOnField(ballNewPosition);
        }

    //Genomskärare
    public void throughBall()
    {
        passCompletionBool = Passing.passCompletion(PlayerAttributesWithBall.passing+100, PlayerAttributesReceiverBall.technique, PlayerAttributesDefenceReceiverBall.tackling, PlayerAttributesDefenceReceiverBall.positioning);
        playerWithBallAction = "CounterAttackThroughBall";
        GetComponent<findReceivingPlayer>().getPlayer(MatchStatisticManager.teamWithBall, ballNowPosition);
       
        //Om genomskäraren lyckas
        if (passCompletionBool == true)
        {
            //Debug.Log(MatchStatisticManager.teamWithBall);
            passTheBallForward(MatchStatisticManager.teamWithBall);
            MatchStatisticManager.passAttempt(true);
            Debug.Log("ThrougBall Succed");
        }

        else
        {
            Debug.Log("ThrougBall fail");
            passTheBallBack(MatchStatisticManager.teamWithBall);
            MatchStatisticManager.passAttempt(false);
            GetComponent<teamLosesBall>().whichTeamHasTheBall(MatchStatisticManager.teamWithBall);
        }

        whichPlayerHasTheBall();
    }

        /*
        public void passTheBallDiagonally()
        {
            ballNowPosition = BallPosition.ballPositionQudrant;//Hämtar bollens position innan passning
            oldPositionX = ballNowPosition.x;
            oldPositionY = ballNowPosition.y;

            if (oldPositionX == 1 && oldPositionY == 1)
            {
                newPositionY = Passing.backwardPass(oldPositionY, );
                newPositionX = Passing.sidewaysPassRight(oldPositionX);
            }

            if (oldPositionX == 1 && oldPositionY == 2)
            {
                newPositionY = Passing.forwardPass(oldPositionY);
                newPositionX = Passing.sidewaysPassRight(oldPositionX);
            }

            if (oldPositionX == 2 && oldPositionY == 1)
            {
                newPositionY = Passing.backwardPass(oldPositionY);
                newPositionX = Passing.sidewaysPassLeft(oldPositionX);
            }

            if (oldPositionX == 2 && oldPositionY == 2)
            {
                newPositionY = Passing.forwardPass(oldPositionY);
                newPositionX = Passing.sidewaysPassLeft(oldPositionX);

            }

            ballNewPosition = new Vector2Int(newPositionX, newPositionY);
            //ballPositionNow(ballNewPosition);
        }
        */
        }
