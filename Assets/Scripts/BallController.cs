using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Vector3 startPosition;
    private bool gameOver = false;

    void Start()
    {
        startPosition = transform.position;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            // Ball has collided with the ground, trigger game over or restart.
            Debug.Log("Ball has collided with the ground");
            GameOver();
        }
    }
    public bool IsGameOver(){
        return gameOver;
    }
    void GameOver()
    {
        gameOver = true;

        // Implement any game over logic (e.g., show a game over screen)

        // Access the GameManager script to restart the game
        GameManager.Instance.RestartGame();
    }
}