using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region General Variables
    [Header("General")]
    [SerializeField] private GameObject myGameObject;
    [SerializeField] public Rigidbody2D myRigidbody;

    public float player1MoveInput;
    public float player2MoveInput;

    public bool canMove;

    public float gravityScale = 1.0f;
    public float maxSpeed = 5f;

    public bool player1;
    public bool player2;
    public bool CPU;
    #endregion

    #region Jump Variables
    [Header("Jump")]
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private GameObject groundChecker;
    public bool isOnGround;

    private bool jump;
    public float jumpForce = 10.0f;
    public float jumpTime;
    private float jumpTimeCounter;
    public bool canDoubleJump;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    #endregion

    #region Shooting
    [Header("Boot")]
    [SerializeField] private Boot bootController;
    [SerializeField] public float headForce = 2f;
    [SerializeField] public float kickForce = 30f;
    #endregion

    #region Adv
    [Header("Extras")]
    [SerializeField] private PerfectTouch PTZ;
    #endregion

    private void Awake()
    {
      
    }
    // Start is called before the first frame update
    void Start()
    {
        myGameObject = gameObject;
        myRigidbody = myGameObject.GetComponent<Rigidbody2D>();

        bootController = myGameObject.GetComponentInChildren<Boot>();

        PTZ = myGameObject.GetComponentInChildren<PerfectTouch>();

        gravityScale = myRigidbody.gravityScale;

        headForce = 5f;
        kickForce = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        isOnGround = Physics2D.OverlapCircle(groundChecker.transform.position, 0.05f, whatIsGround);
        if (canMove)
        {
            if (player1)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    {
                        if (isOnGround)
                        {
                            jump = true;
                            canDoubleJump = true;

                        }
                        else
                        {
                            if (canDoubleJump)
                            {
                                jump = true;
                                canDoubleJump = false;
                            }
                        }
                    }
                }
                if (Input.GetKey(KeyCode.Space))
                {
                    bootController.Shoot();
                }
                else
                {
                    bootController.ResetBoot();
                }
            }
            if (player2)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    {
                        if (isOnGround)
                        {
                            jump = true;
                            canDoubleJump = true;

                        }
                        else
                        {
                            if (canDoubleJump)
                            {
                                jump = true;
                                canDoubleJump = false;
                            }
                        }
                    }
                }
                if (Input.GetKey(KeyCode.P))
                {
                    bootController.Shoot();
                }
                else
                {
                    bootController.ResetBoot();
                }
            }
            if (myRigidbody.velocity.y < 0)
            {
                myRigidbody.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
        }
    }
    private void FixedUpdate()
    {
        checkInputs();
        
    }
    public void checkInputs()
    {
        if (canMove)
        {
            if (player1)
            {
                player1MoveInput = Input.GetAxis("Player1 Horizontal");

                //sfloat movementValueX = 1.0f;
                myRigidbody.velocity = new Vector2(player1MoveInput * maxSpeed, myRigidbody.velocity.y);

                if (jump)
                {
                    myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, 0);
                    myRigidbody.AddForce(new Vector3(0, jumpForce));
                    jump = false;
                }

                if (myRigidbody.velocity.y < 0)
                {
                    myRigidbody.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
                }
            }
            if (player2)
            {
                player2MoveInput = Input.GetAxis("Player2 Horizontal");

                //sfloat movementValueX = 1.0f;
                myRigidbody.velocity = new Vector2(player2MoveInput * maxSpeed, myRigidbody.velocity.y);

                if (jump)
                {
                    myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, 0);
                    myRigidbody.AddForce(new Vector3(0, jumpForce));
                    jump = false;
                }

                if (myRigidbody.velocity.y < 0)
                {
                    myRigidbody.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
                }
            }
        }
    }
    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Boot" && collision.collider.gameObject.GetComponent<Boot>().shoot == true)
        {
            Debug.Log("kick player");
            Vector3 direction = (collision.gameObject.GetComponentInParent<PlayerController>().transform.position - gameObject.GetComponentInParent<PlayerController>(myGameObject).transform.position).normalized;
            myRigidbody.AddForce(-direction  * collision.gameObject.GetComponent<PlayerController>().kickForce * 200, ForceMode2D.Impulse);
        }
    }
    */
}
