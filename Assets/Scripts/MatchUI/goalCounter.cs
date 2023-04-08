using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class goalCounter : MonoBehaviour
{
    
    public int hometeamGoals;
    public int awayteamsGoals;

    public Text goalsText;

    public fieldUIManager FieldUIManager;
    public matchStatisticManager MatchStatisticManager;


    public void addGoalHometeam()
    {
        hometeamGoals++;
        goalsText.text = "H: " + hometeamGoals + " A: " + awayteamsGoals;
        MatchStatisticManager.teamWithBall = 2;
        FieldUIManager.kickOffAwayTeam();
    }

    public void addGoalAwayteam()
    {
        awayteamsGoals++;
        goalsText.text = "H: " + hometeamGoals + " A: " + awayteamsGoals;
        MatchStatisticManager.teamWithBall = 1;
        FieldUIManager.kickOffHomeTeam();
    }
}
