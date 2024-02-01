using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject ballPrefab;
    public GameObject bombPrefab; 
    public GameObject coinPrefab; 
    public float scoringMultiplier = 1f;
    public TextMeshProUGUI scoreText; 
    public TextMeshProUGUI coinText;
    private GameObject ballInstance;
    private float timer = 0f;
    private int score = 0;
    public int coin = 0;
     private bool isSpawningBombs = false;
     private bool isSpawningCoins = false;
    void Start()
    {
        StartGame();
        StartBombSpawning();
        StartCoinSpawning();
    }
void Awake()
    {
        // Set the instance to this GameManager script when it's first created
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // If an instance already exists, destroy this new one
        }
    }
    void Update()
    {
        if (ballInstance != null && !ballInstance.GetComponent<BallController>().IsGameOver())
        {
            // Update the timer
            timer += Time.deltaTime;

            // Update the score based on the timer (you can customize the scoring logic)
            score = Mathf.FloorToInt(timer * scoringMultiplier);

            // Update the UI with the current score
            UpdateScoreUI();
        }
    }

    void UpdateScoreUI()
    {
        // Update the UI with the current score
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
     void UpdateCoinUI()
    {
        // Update the UI with the current score
        if (coinText != null)
        {
            coinText.text = "Coins: " + coin;
        }
    }
    //RESTARTING AFTER THE GAME IS OVER
    public void RestartGame()
    {
        // // Reset game state
        // timer = 0f;
        // score = 0;
        // UpdateScoreUI();

        // // Destroy the current ball instance
        // if (ballInstance != null)
        // {
        //     Destroy(ballInstance);
        // }

        // Start a new game
        StopBombSpawning();
        StopCoinSpawning();
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    //INITIAL SETUP
    void StartGame()
    {
        // Instantiate a new ball
        ballInstance = Instantiate(ballPrefab, new Vector3(0f,5f,0f), Quaternion.identity);
    }
    //BOMBSPAWNING
    void StartBombSpawning()
    {
        if (!isSpawningBombs)
        {
            isSpawningBombs = true;
            StartCoroutine(SpawnBombsRoutine());
        }
    }
    
    void StopBombSpawning()
    {
        if (isSpawningBombs)
        {
            isSpawningBombs = false;
            StopCoroutine(SpawnBombsRoutine());
        }
    }

    IEnumerator SpawnBombsRoutine()
    {
        while (isSpawningBombs)
        {
            // Adjust the position of bombs as needed
            Vector3 bombPosition = new Vector3(Random.Range(-2f, 2f), 6f, 0f);
            Instantiate(bombPrefab, bombPosition, Quaternion.identity);

            // Adjust the interval between bomb spawns
            float interval = Random.Range(0.3f, 1f);
            yield return new WaitForSeconds(interval);
        }
    }
    //COINSPAWNING
        void StartCoinSpawning()
        {
            if (!isSpawningCoins)
            {
                isSpawningCoins = true;
                StartCoroutine(SpawnCoinsRoutine());
            }
        }
        void StopCoinSpawning()
    {
        if (isSpawningCoins)
        {
            isSpawningCoins = false;
            StopCoroutine(SpawnCoinsRoutine());
        }
    }
    IEnumerator SpawnCoinsRoutine()
    {
        while (isSpawningCoins)
        {
            // Adjust the position of bombs as needed
            Vector3 coinPosition = new Vector3(Random.Range(-2f, 2f), 6f, 0f);
            Instantiate(coinPrefab, coinPosition, Quaternion.identity);

            // Adjust the interval between bomb spawns
            float interval = Random.Range(1f, 3f);
            yield return new WaitForSeconds(interval);
        }
    }
    public void coinCollected(){
        coin++;
        UpdateCoinUI();
    }

}