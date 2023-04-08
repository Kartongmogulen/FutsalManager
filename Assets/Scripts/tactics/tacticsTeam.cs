using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class tacticsTeam : MonoBehaviour
{
    public GameObject matchScriptsGO;

    public tacticsStatsEffect TacticsStatsEffect;
    [SerializeField] private int randomNumb;

    //public defencePress DefencePress;

    [Header("Attack")]
    public int counterAttack; //Hur stor vikt laget vid kontringar
    public int passAttack; //Hur stor vikt laget vid att bygga upp ett passningspel vid erövring av bollen
    
    public void winsBall()
    {
        //Debug.Log("winsBall Script");

        randomNumb = Random.Range(0, 100);

        if (randomNumb < counterAttack)
        {
            attackCounter();
        }
        else
        attackPass();
    }

    public void attackCounter()
    {
        //Debug.Log("attackCounter");
        matchScriptsGO.GetComponent<playerActionManager>().playerWithBallAction = "CounterAttack";
        //matchScriptsGO.GetComponent<playerActionManager>().shootAttempt(0); //LÄGG TILL INT I FUNKTION OM MAN SKA FÅ EXTRA CHANS FÖR MÅL VID KONTRING
    }

    //Laget vill bygga upp ett passningsspel vid erövring av bollen
    public void attackPass()
    {
        matchScriptsGO.GetComponent<playerActionManager>().playerWithBallAction = "BuildUpAttack";
       
        //Positioner nollställs och backen börjar med bollen
        matchScriptsGO.GetComponent<playerActionManager>().ballNewPosition = (new Vector2Int(1, 1));
        //matchScriptsGO.GetComponent<playerActionManager>().playerWithBallAction = "pass";
        //matchScriptsGO.GetComponent<playerActionManager>().whichPlayerHasTheBall();
    }

    public void counterProb(int numb)
    {
        counterAttack = TacticsStatsEffect.counterProb[numb];
    }
}
