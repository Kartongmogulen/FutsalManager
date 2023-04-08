using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class clockGameUpdate : MonoBehaviour
{
    public Text clockTimeText;
    public int timePerActionOnField;//Tiden varje aktion tar
    public int timeMinNow;
    public int timeSecNow;

    public void updateClock()
    {
        timeSecNow += timePerActionOnField;

        if(timeSecNow == 60)
        {
            timeMinNow++;
            timeSecNow = 0;
        }

        clockTimeText.text = timeMinNow + ":" + timeSecNow;
    }

}
