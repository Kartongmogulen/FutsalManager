using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootBlocked : MonoBehaviour
{
    public int probReturnQudrantRowOne; //Slh att bollen styrs mot raden närmast mål

    public int randomNumber;

    public ballPosition BallPosition;
    public Vector2Int ballPositionQudrant;

    public void whichQuadrantBallEndsUp()
    {
        randomNumber = Random.Range(0, 99);

        //Någon av kvadranterna närmast mål
        if (randomNumber < probReturnQudrantRowOne)
        {
            Debug.Log("Ball ends up at RowOne");

            //Vilken sida av planen hamnar bollen på
            randomNumber = Random.Range(0, 99);
            if (randomNumber <= 50)
            {
                Debug.Log(randomNumber + " 1");
                ballPositionQudrant = new Vector2Int(1, 1);
                BallPosition.ballPositionOnField(ballPositionQudrant);
                GetComponent<playerActionManager>().ballNewPosition = new Vector2Int(1, 1);
            }
            else
            {
                Debug.Log(randomNumber + " 2");
                ballPositionQudrant = new Vector2Int(2, 1);
                BallPosition.ballPositionOnField(ballPositionQudrant);
                GetComponent<playerActionManager>().ballNewPosition = new Vector2Int(2, 1);
            }
        }

        //Annars någon av de bakre kvadranterna
        else
        {
            randomNumber = Random.Range(0, 99);
            if (randomNumber <= 100)
            {
                ballPositionQudrant = new Vector2Int(1, 2);
                BallPosition.ballPositionOnField(ballPositionQudrant);
                GetComponent<playerActionManager>().ballNewPosition = new Vector2Int(1, 2);
            }
            else
            {
                ballPositionQudrant = new Vector2Int(2, 2);
                BallPosition.ballPositionOnField(ballPositionQudrant);
                GetComponent<playerActionManager>().ballNewPosition = new Vector2Int(2, 2);
            }

        }
    }

    //Vem av spelarna i kvadranten som vinner bollen
    public int playerThatGetsTheReturn(int attackingTeamTacklingPlayer, int defendingTeamTacklingPlayer)
    {

        //Debug.Log("Anfallare tackling: " + attackingTeamTacklingPlayer);
        //Debug.Log("Försvare tackling: " + defendingTeamTacklingPlayer);

        int randomNumberAttackingTeam = Random.Range(0, attackingTeamTacklingPlayer);
        int randomNumberDefendingTeam = Random.Range(0, defendingTeamTacklingPlayer);

        //Debug.Log(randomNumberAttackingTeam + " - " + randomNumberDefendingTeam);

        if (randomNumberAttackingTeam > randomNumberDefendingTeam)
        {
            return 1;
        }
        else
        {
            return 2;
        }
    } 
}
