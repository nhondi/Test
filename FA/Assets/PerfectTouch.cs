using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerfectTouch : MonoBehaviour
{
    private PlayerController playerControls;

    // Start is called before the first frame update
    void Start()
    {
        playerControls = GetComponentInParent<PlayerController>();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            //collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            Debug.Log("Chance");

            if (playerControls.player1)
            {
                if (Input.GetKeyDown(KeyCode.C))
                {
                    Debug.Log("PT");
                    collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                }
            }

            if (playerControls.player2)
            {
                if (Input.GetKeyDown(KeyCode.O))
                {
                    Debug.Log("PT");
                    collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                }
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
