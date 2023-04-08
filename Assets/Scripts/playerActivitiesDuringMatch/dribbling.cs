using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dribbling : MonoBehaviour
{
    public float basicProbActionCompletion;
    [SerializeField] private float techniqueModifier;
    [SerializeField] private float positionModifier;

    public float actionCompletionProbFinishSim;//Slutresultatet för sannolikheten att aktionen lyckas

    public bool actionCompletionBool;

    public float randomNumber;

    public GameObject teamOneTactics;
    public GameObject teamTwoTactics;
    public matchStatisticManager MatchStatisticManager;
    public tacticsStatsEffect TacticsStatsEffect;

    public bool actionCompletion(int playerAttackTechnique, int playerAttackMovement, int playerDefendTacklingStat, int playerDefendPositionStat)
    {
        //Påverkan om försvarande lag använder Aggresiv press
        //Om laget använder aggresiv tackling ökar minskar deras förmåga att ta bollen.
        if (MatchStatisticManager.teamWithBall == 1 && teamTwoTactics.GetComponent<tacticsDefence>().DefencePress == defencePress.aggresive)
        {

            playerDefendTacklingStat -= TacticsStatsEffect.aggresivePressTacklingDecrease;

        }

        else if (MatchStatisticManager.teamWithBall == 2 && teamOneTactics.GetComponent<tacticsDefence>().DefencePress == defencePress.aggresive)
        {
            playerDefendTacklingStat -= TacticsStatsEffect.aggresivePressTacklingDecrease;
        }

        //Formel för att aktionen ska lyckas
        actionCompletionProbFinishSim = basicProbActionCompletion + ((playerAttackTechnique*techniqueModifier + playerAttackMovement) - (playerDefendTacklingStat + playerDefendPositionStat*positionModifier));

        randomNumber = Random.Range(0, 99);

        //Slumpar om dribblingen lyckas
        if (randomNumber <= actionCompletionProbFinishSim)
        {
            actionCompletionBool = true;
        }
        else
        {
            actionCompletionBool = false;
           
        }

        return actionCompletionBool;
    }
}
