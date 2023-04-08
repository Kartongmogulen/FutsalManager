using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "tacticsStatsEffect", menuName = "Scriptable Objects")]
public class tacticsStatsEffect : ScriptableObject
{
    //Hur taktiken p�verkar stats i olika situationer
    public int aggresivePressTacklingIncrease;
    public int aggresivePressTacklingDecrease; //Om anfallande lag lyckas ta emot bollen minskar f�rsvarande lags f�rm�ga att f�rsvara vid dribbling

    [Header("Attack")]
    public int[] counterProb;

    public int counterShootModifier;

}
