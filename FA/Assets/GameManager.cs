using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum states
{setup, start, postGoal, end}

public enum powerUps
{ smallBall, bigBall, freezeBall, freezeOp, speedSelf, jumpUpSelf, jumpDownOpp, pitchInvader, slowOp }

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    #region
    public states gameState;
    #endregion

    #region Players
    [Header("Players")]
    public GameObject Player1;
    public Vector3 Player1Startpos;
    private PlayerController player1Control;
    [SerializeField] private bool player1Ready;
    [SerializeField] private bool player1Win;


    public GameObject Player2;
    public Vector3 Player2Startpos;
    private PlayerController player2Control;
    [SerializeField] private bool player2Ready;
    [SerializeField] private bool player2Win;
    #endregion

    #region Score
    [Header("Score System")]

    [SerializeField] private GameObject ball;

    private GameObject particle1Prefab;
    private GameObject particle2Prefab;

    private GameObject particleObject1;
    private GameObject particleObject2;
    
    public ParticleSystem P1Goal;
    public ParticleSystem P2Goal;

    public bool P1Scored;
    public bool P2Scored;

    [SerializeField] public static int homeScore;
    [SerializeField] public static int awayScore;

    [SerializeField] private Text scoreText;

    #endregion

    #region Setup
    [Header("GameStart")]
    public GameObject ballPrefab;
    public GameObject ballObject;
    public GameObject shadow;
    #endregion

    #region UI
    [Header("Text/Buttons")]

    [SerializeField] private GameObject P1Control;
    [SerializeField] private GameObject P2Control;
    #endregion

    #region Abilities
    [Header("PowerUps")]
    public powerUps ability;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GameObject.Find("ScoreT").GetComponent<Text>();
        scoreText.text = homeScore.ToString() + " : " + awayScore.ToString();

        P1Control = GameObject.Find("SpaceToStart");
        P2Control = GameObject.Find("PToStart");

        P1Control.SetActive(true);
        P2Control.SetActive(true);

        particle1Prefab = (GameObject)Resources.Load("Player1 Explosion", typeof(GameObject));
        particle2Prefab = (GameObject)Resources.Load("Player2 Explosion", typeof(GameObject));


        ballPrefab = (GameObject)Resources.Load("Ball", typeof(GameObject));
        shadow = GameObject.Find("Shadow");

        shadow.SetActive(false);

        Player1 = GameObject.Find("Player 1");
        player1Control = Player1.GetComponent<PlayerController>();
        player1Control.player1 = true;

        Player2 = GameObject.Find("Player 2");
        player2Control = Player2.GetComponent<PlayerController>();
        player2Control.player2 = true;

        Player1Startpos = Player1.transform.position;
        Player2Startpos = Player2.transform.position;

        StartCoroutine(GameStart());
        
    }
    IEnumerator GameStart()
    {
        ResetVariables();
        gameState = states.setup;

        Debug.Log("start coroutine");
        player1Control.canMove = false;
        player2Control.canMove = false;
        //Look for two player inputs

        yield return new WaitUntil(() => player1Ready && player2Ready);

        //Time.timeScale = 1;
        Instantiate(ballPrefab, new Vector2(0f, 0f), Quaternion.identity);
        
        shadow.SetActive(true);
        ballObject = GameObject.FindGameObjectWithTag("Ball");

        player1Control.canMove = true;
        player2Control.canMove = true;

        gameState = states.start;
        StartCoroutine(GameLoop());
        yield return null;

    }
    IEnumerator GameLoop()
    {
        while (homeScore < 5 || awayScore < 5)
        {
            yield return new WaitUntil(() => P1Scored || P2Scored);
            if (P1Scored || P2Scored)
            {
                if (homeScore < 5 || awayScore < 5)
                {
                    gameState = states.postGoal;

                    Instantiate(particle1Prefab, ballObject.transform.position + new Vector3(1, 0, 0), Quaternion.identity);
                    Instantiate(particle2Prefab, ballObject.transform.position - new Vector3(1, 0, 0), Quaternion.identity);

                    particleObject1 = GameObject.FindGameObjectWithTag("PS");
                    particleObject2 = GameObject.FindGameObjectWithTag("PS2");

                    P1Goal = particleObject1.GetComponent<ParticleSystem>();
                    P2Goal = particleObject2.GetComponent<ParticleSystem>();

                    shadow.SetActive(false);
                    if (P1Scored)
                    {
                        Player1Scored();
                    }

                    if (P2Scored)
                    {
                        Player2Scored();
                    }

                    StartCoroutine(StartNewRound());
                }
            }
            if (homeScore == 5 || awayScore == 5)
            {
                if (homeScore == 5)
                {
                    Debug.Log("p1 win");
                    player1Win = true;
                    homeScore = 0;
                    awayScore = 0;
                    SceneManager.LoadScene(0);
                }
                else if (awayScore == 5)
                {
                    Debug.Log("p2 win");
                    player2Win = true;
                    homeScore = 0;
                    awayScore = 0;
                    SceneManager.LoadScene(0);
                }
            }
        }

        yield return null;
    }
    // Update is called once per frame
    void Update()
    {
        if (gameState == states.setup)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                player1Ready = true;
                P1Control.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                player2Ready = true;
                P2Control.SetActive(false);
            }
        }
    }
    public void Player1Scored()
    {
        player2Control.canMove = false;
        P1Scored = false;

        Destroy(ballObject);
        P1Goal.Play();
        homeScore = homeScore + 1;
        UpdateScore();
    }
    public void Player2Scored()
    {
        player1Control.canMove = false;
        P2Scored = false;

        Destroy(ballObject);
        P2Goal.Play();
        awayScore = awayScore + 1;
        UpdateScore();
    }
    IEnumerator StartNewRound()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(particleObject1);
        Destroy(particleObject2);
        StartCoroutine(GameStart());
    }
    public void UpdateScore()
    {
        scoreText.text = homeScore.ToString() + " : " + awayScore.ToString();
    }
    public void ResetVariables()
    {
        Player1.transform.position = Player1Startpos;
        Player2.transform.position = Player2Startpos;
    }
}
