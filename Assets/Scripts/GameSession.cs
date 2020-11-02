using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameSession : MonoBehaviour
{

    [Range(0.1f, 5f)] [SerializeField] float gameSpeed = 1f;
    [SerializeField] int scorePerBlock = 10;
    [SerializeField] int lives = 3;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI velocitydebug;
    [SerializeField] int blocksForPowerupMin;
    [SerializeField] int blocksForPowerupMax;
    [SerializeField] bool debug_enabled = false;
    [SerializeField] public bool auto_play = false;
    [SerializeField] PowerUp[] PowerUps;
    //   [SerializeField] bool fast_forward = false;

    //State variables
    [SerializeField] int currentScore = 0;
    [SerializeField] int currentBlocksForPowerUp;
    GameObject fastforwardImage;
    // Start is called before the first frame update
    private void Awake()
    {
        int gameStatusCount = FindObjectsOfType<GameSession>().Length;
        if (gameStatusCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    public void updateDebug(Vector2 velocity)
    {
        velocitydebug.gameObject.SetActive(debug_enabled);
        velocitydebug.text = "Velocity X :" + velocity.x.ToString() + "\nVelocity Y :" + velocity.y.ToString();

    }

    void Start()
    {
        GenerateBlocksCountForPowerup();
        this.scoreText.text = currentScore.ToString();
        updateDebug(new Vector2());
        fastforwardImage = GameObject.Find("Fast Forward");
        fastforwardImage.SetActive(false);
    }
    private void GenerateBlocksCountForPowerup()
    {
        this.currentBlocksForPowerUp = Random.Range(blocksForPowerupMin, blocksForPowerupMax + 1);
    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            debug_enabled = !debug_enabled;
            velocitydebug.gameObject.SetActive(debug_enabled);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {

            gameSpeed = 3f - gameSpeed;

            fastforwardImage.SetActive(!fastforwardImage.active);
        }
        else if(Input.GetKeyDown(KeyCode.P))
        {
            this.auto_play =! this.auto_play;
        }

        Time.timeScale = gameSpeed;
    }

    public void AddToScore()
    {
        this.currentBlocksForPowerUp--;
        if (this.currentBlocksForPowerUp == 0)
        {
            GeneratePowerUp();
            GenerateBlocksCountForPowerup();
        }
        this.currentScore += scorePerBlock;
        this.scoreText.text = currentScore.ToString();
    }
    private void GeneratePowerUp()
    {
        int length = PowerUps.Length;
        PowerUp powerUp = PowerUps[Random.Range(0, length)];
        Ball b = FindObjectOfType<Ball>();
         

        PowerUp newp =  Instantiate(powerUp,b.transform.position, b.transform.rotation);
        Vector2 newVel = b.GetComponent<Rigidbody2D>().velocity;
        newVel = new Vector2(-newVel.x, Mathf.Abs(newVel.y));
        newp.GetComponent<Rigidbody2D>().velocity = newVel;


    }
    public void Resetgame()
    {
        Destroy(gameObject);
    }
}
