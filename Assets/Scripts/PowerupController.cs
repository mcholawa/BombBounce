using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupController : MonoBehaviour
{
     // Enum to define different types of powerups
    public enum PowerupType
    {
        Type1,
        Type2,
        Type3
        // Add more types as needed
    }
    // Variable to store the type of the powerup
    public PowerupType type;
    // Array of sprites for each powerup type (for future use)
    public Sprite[] powerupSprites;
    void Start()
    {
        // Generate a random type for the powerup
        type = (PowerupType)Random.Range(0, System.Enum.GetValues(typeof(PowerupType)).Length);
       
        // Set sprite based on the type 
        GetComponent<SpriteRenderer>().sprite = powerupSprites[(int)type];
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Get the mouse position in the world
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Check if the mouse click is over the powerup
            if (GetComponent<Collider2D>().OverlapPoint(mousePosition))
            {
                HandleClick();
                Destroy(gameObject);
            }
        }
    }
    void HandleClick()
    {
        GameManager.Instance.powerUpCollected(type);
    }
      void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
        Destroy(gameObject);
        }
    }
}
