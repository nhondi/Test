using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalDetect : MonoBehaviour
{
    public bool p1Goal;
    public bool p2Goal;

    private GameManager gm;

    private void Start()
    {
        gm = GameObject.Find("GM").GetComponent<GameManager>();
        if(gameObject.name == "Goal P1")
        {
            p1Goal = true;
        }
        else
        {
            p2Goal = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            if (p1Goal)
            {
                gm.P2Scored = true;
            }
            else if (p2Goal)
            {
                gm.P1Scored = true;
            }
            else
            {
                Debug.Log("error");
            }
        }
    }
}
