using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerAttributes : MonoBehaviour
{
    public string namePlayer;
    public Text playerNameText;

    [Header("Attack")]
    public int shoot;
    public int passing;
    public int technique;
    public int movement;

    [Header("Defence")]
    public int tackling;
    public int positioning;
    //Målvakt
    public int goalkeeping;

    private void Start()
    {
        playerNameText = GetComponentInChildren<Text>();
        playerNameText.text = namePlayer;
    }

}
