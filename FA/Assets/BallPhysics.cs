using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPhysics : MonoBehaviour
{
    #region General Variables
    [Header("General")]
    [SerializeField] private GameObject myGameObject;
    [SerializeField] private Rigidbody2D myRigidbody;

    [SerializeField] private int randomXForce;
    public int xForceMin;
    public int xForceMax;

    [SerializeField] private int randomYForce;
    public int yForceMin;
    public int yForceMax;

    public GameObject shadow;

    public bool player1Kick;
    public bool player2Kick;
    #endregion

    private GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        myGameObject = gameObject;
        myRigidbody = myGameObject.GetComponent<Rigidbody2D>();
        shadow = GameObject.Find("Shadow");

        randomXForce = Random.Range(xForceMin, xForceMax);
        randomYForce = Random.Range(yForceMin, yForceMax);
        myRigidbody.AddForce(new Vector2(randomXForce, randomYForce));
    }

    // Update is called once per frame
    void Update()
    {
        shadow.transform.position = new Vector2 (gameObject.transform.position.x, shadow.transform.position.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Boot" && collision.collider.gameObject.GetComponent<Boot>().shoot == true)
        {
            Debug.Log("kick");
            Vector3 direction = (collision.transform.position - transform.position).normalized;
            Debug.Log(direction);
            myRigidbody.AddForce(Vector3.Scale(-direction, new Vector3 (2,2,0))* collision.gameObject.GetComponent<PlayerController>().kickForce , ForceMode2D.Impulse);

            if (collision.gameObject.GetComponent<PlayerController>().player1)
            {
                player1Kick = true;
                player2Kick = false;
            }

            else if (collision.gameObject.GetComponent<PlayerController>().player2)
            {
                player1Kick = false;
                player2Kick = true;
            }
        }
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("head");
            Vector3 direction = (collision.transform.position - transform.position).normalized;
            myRigidbody.AddForce(-direction * collision.gameObject.GetComponent<PlayerController>().headForce, ForceMode2D.Impulse);

            if (collision.gameObject.GetComponent<PlayerController>().player1)
            {
                player1Kick = true;
                player2Kick = false;
            }

            else if (collision.gameObject.GetComponent<PlayerController>().player2)
            {
                player1Kick = false;
                player2Kick = true;
            }

        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        float step = 1f * Time.deltaTime;
        if (collision.GetComponent<Collider>().gameObject.name == "Crossbar")
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0f, transform.position.y, transform.position.z), step);
        }
    }
}
