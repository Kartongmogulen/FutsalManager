using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerPosition : MonoBehaviour
{
    public GameObject teamOneFieldHalf;
    public Transform teamOneFieldHalfTopLeft;
    public Transform teamOneFieldHalfBottomLeft;
    public Transform teamOneFieldHalfTopRight;
    public Transform teamOneFieldHalfBottomRight;

    //public GameObject playingFieldBottomRight;
    public Transform playerPosAttackBottomRight;
    public Transform playerPosAttackBottomLeft;
    public Transform playerPosAttackTopRight;
    public Transform playerPosAttackTopLeft;

    public Transform playerPosDefenceBottomRight;
    public Transform playerPosDefenceTopRight;
    public Transform playerPosDefenceBottomLeft;
    public Transform playerPosDefenceTopLeft;

    public GameObject playerListHolderTeamOne;
    public List<playerAttributes> PlayerAttributesListTeamOne;

    public GameObject playerListHolderTeamTwo;
    public List<playerAttributes> PlayerAttributesListTeamTwo;

    private void Start()
    {
        PlayerAttributesListTeamOne = playerListHolderTeamOne.GetComponent<playerHolderList>().playerList;
        PlayerAttributesListTeamTwo = playerListHolderTeamTwo.GetComponent<playerHolderList>().playerList;

        //POSITIONERING
        //Anfallande lag ETT
        
        Instantiate(PlayerAttributesListTeamOne[0], playerPosAttackBottomRight);
        Instantiate(PlayerAttributesListTeamOne[1], playerPosAttackTopRight);
        Instantiate(PlayerAttributesListTeamOne[2], playerPosAttackBottomLeft);
        Instantiate(PlayerAttributesListTeamOne[3], playerPosAttackTopLeft);

        //Försvarande lag TVÅ
        Instantiate(PlayerAttributesListTeamTwo[0], playerPosDefenceBottomRight);
        Instantiate(PlayerAttributesListTeamTwo[1], playerPosDefenceTopRight);
        Instantiate(PlayerAttributesListTeamTwo[2], playerPosDefenceBottomLeft);
        Instantiate(PlayerAttributesListTeamTwo[3], playerPosDefenceTopLeft);

        /*
        teamOneFieldHalfTopLeft = teamOneFieldHalf.gameObject.transform.GetChild(0);
        teamOneFieldHalfTopRight = teamOneFieldHalf.gameObject.transform.GetChild(1);
        teamOneFieldHalfBottomLeft = teamOneFieldHalf.gameObject.transform.GetChild(2);
        teamOneFieldHalfBottomRight = teamOneFieldHalf.gameObject.transform.GetChild(3);

        //Anfallande lag TVÅ
        Instantiate(PlayerAttributesListTeamTwo[0], teamOneFieldHalfBottomRight.GetChild(1));
        Instantiate(PlayerAttributesListTeamTwo[1], teamOneFieldHalfTopRight.GetChild(1));
        */
    }

    public void teamTwoAttacks()
    {
        //teamOneFieldHalfTopRight = teamOneFieldHalf.gameObject.transform.GetChild(1);
        //teamOneFieldHalfBottomRight = teamOneFieldHalf.gameObject.transform.GetChild(3);

        //Lag Två
        //Instantiate(PlayerAttributesListTeamTwo[0], teamOneFieldHalfBottomRight.GetChild(1));
    }
}
