using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ballPosition : MonoBehaviour
{
    public Image football;
    public GameObject gameField; //Planen

    //Delar upp planen
    public GameObject gameFieldBottomRight;
    public GameObject gameFieldBottomLeft;
    public GameObject gameFieldTopRight;
    public GameObject gameFieldTopLeft;

    public GameObject ballPositionBottomRight;
    public GameObject ballPositionBottomLeft;
    public GameObject ballPositionTopRight;
    public GameObject ballPositionTopLeft;

    public Vector2Int ballPositionQudrant;

    // Start is called before the first frame update
    void Awake()
    {
        /*
        Instantiate(football, gameFieldBottomRight.transform);
        Instantiate(football, gameFieldBottomLeft.transform);
        Instantiate(football, gameFieldTopRight.transform);
        Instantiate(football, gameFieldTopLeft.transform);
        */
        //ballPositionQudrant = new Vector2Int(2, 2);
        ballPositionOnField(ballPositionQudrant);
    }

    public void ballPositionOnField(Vector2 newPosition)
    {
        
        if (newPosition.x == 2 && newPosition.y == 1)
        {
            ballPositionQudrant = new Vector2Int(2, 1);
            //DestroyObject(GameObject.Find("BallPrefab(Clone)"));
            //Instantiate(football, gameFieldTopRight.transform);

            ballPositionBottomRight.SetActive(false);
            ballPositionBottomLeft.SetActive(false);
            ballPositionTopRight.SetActive(true);
            ballPositionTopLeft.SetActive(false);

            return;
        }

        if (newPosition.x == 2 && newPosition.y == 2)
        {
            ballPositionQudrant = new Vector2Int(2, 2);
            //DestroyObject(GameObject.Find("BallPrefab(Clone)"));
            //Instantiate(football, gameFieldBottomRight.transform);

            ballPositionBottomRight.SetActive(true);
            ballPositionBottomLeft.SetActive(false);
            ballPositionTopRight.SetActive(false);
            ballPositionTopLeft.SetActive(false);

            return;
        }

        if (newPosition.x == 1 && newPosition.y == 2)
        {
            ballPositionQudrant = new Vector2Int(1, 2);
            //DestroyObject(GameObject.Find("BallPrefab(Clone)"));
            //Instantiate(football, gameFieldBottomLeft.transform);

            ballPositionBottomRight.SetActive(false);
            ballPositionBottomLeft.SetActive(true);
            ballPositionTopRight.SetActive(false);
            ballPositionTopLeft.SetActive(false);

            return;
        }

        if (newPosition.x == 1 && newPosition.y == 1)
        {
            ballPositionQudrant = new Vector2Int(1, 1);
            //DestroyObject(GameObject.Find("BallPrefab(Clone)"));
            //Instantiate(football, gameFieldTopLeft.transform);

            ballPositionBottomRight.SetActive(false);
            ballPositionBottomLeft.SetActive(false);
            ballPositionTopRight.SetActive(false);
            ballPositionTopLeft.SetActive(true);

            return;
        }
    }


}

