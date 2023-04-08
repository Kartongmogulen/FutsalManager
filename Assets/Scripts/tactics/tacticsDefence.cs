using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tacticsDefence : MonoBehaviour
{

    public defencePress DefencePress;
    public tacticsStatsEffect TacticsStatsEffect;
    [SerializeField] private int aggresivePressTacklingIncrease; 

    private void Start()
    {
        aggresivePressTacklingIncrease = TacticsStatsEffect.aggresivePressTacklingIncrease;
    }

    public void pressNormalActive()
    {
        DefencePress = defencePress.normal;
    }

    public void pressAggresiveActive()
    {
        DefencePress = defencePress.aggresive;
    }

    public int getIncreasedTacklingWhenAggresivePress()
    {
        return aggresivePressTacklingIncrease;
    }
    
}
