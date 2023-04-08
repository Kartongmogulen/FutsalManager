using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class findReceivingPlayer : MonoBehaviour
{
    //Vilken spelare är mottagare

    public playerActionManager PlayerActionManager;

    public void getPlayer(int teamWithBall, Vector2 positionReceivingPlayer)
    {
        if (teamWithBall == 2)
        {
            if (positionReceivingPlayer.x == 2 && positionReceivingPlayer.y == 1)
            {
                PlayerActionManager.PlayerAttributesReceiverBall = PlayerActionManager.gameFieldBottomRightDefencePlayer.GetComponentInChildren<playerAttributes>();
            }

            else if (positionReceivingPlayer.x == 1 && positionReceivingPlayer.y == 1)
            {
                PlayerActionManager.PlayerAttributesReceiverBall = PlayerActionManager.gameFieldBottomLeftDefencePlayer.GetComponentInChildren<playerAttributes>();
            }
        }

        if (teamWithBall == 1)
        {
            if (positionReceivingPlayer.x == 2 && positionReceivingPlayer.y == 2)
            {
                PlayerActionManager.PlayerAttributesReceiverBall = PlayerActionManager.gameFieldTopRightAttackPlayer.GetComponentInChildren<playerAttributes>();
            }

            else if (positionReceivingPlayer.x == 1 && positionReceivingPlayer.y == 2)
            {
                PlayerActionManager.PlayerAttributesReceiverBall = PlayerActionManager.gameFieldTopLeftAttackPlayer.GetComponentInChildren<playerAttributes>();
            }
        }
    }
}
