using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public GameObject shockwavePrefab; // Assign your shockwave prefab in the inspector
    public float cooldownTime = 1f; // Set the cooldown time in seconds
    public float cooldownLeft;
    private bool isCooldown = false;
    private Rigidbody2D rb;
    private int clickCount = 0; // Counter for clicks
    void Start()
    {
        // Get the Rigidbody2D component on Start
        cooldownLeft = cooldownTime;
        rb = GetComponent<Rigidbody2D>();
    }
/////////////////UPDATE/////////////
    void Update()
    {
        // Check if the left mouse button is clicked and cooldown is not active
        if (Input.GetMouseButtonDown(0) && !isCooldown)
        {
            // Get the mouse position in the world
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Check if the mouse click is over the bomb
            if (GetComponent<Collider2D>().OverlapPoint(mousePosition))
            {
                // The bomb was clicked
                HandleClick();
                 // Increment the click count // CLOUSE FOR MAX clickCount
                clickCount++;
                if (clickCount >= 3)
                {
                    Destroy(gameObject);
                }
                // Start the cooldown timer
                StartCooldown();
            }
        }
        // Update the cooldown timer
        if (isCooldown)
        {
            cooldownLeft -= Time.deltaTime;

            // Check if cooldown is complete
            if (cooldownLeft <= 0f)
            {
                // Reset cooldown time, activate gravity, and reset velocity to zero
                isCooldown = false;
                cooldownLeft = cooldownTime;
                rb.gravityScale = 1f; // Reset gravity to default
                //rb.velocity = Vector2.zero; // Reset velocity to zero
            }
        }
    }
////////////////////////////////////////////////
    void HandleClick()
    {
        // Instantiate the shockwave at the bomb's position
        Instantiate(shockwavePrefab, transform.position, Quaternion.identity);

        // Handle any other logic for the bomb click here
        //Debug.Log("Bomb clicked!");

        // Disable gravity during cooldown
        rb.gravityScale = 0f;

        // Reset velocity to zero to stop immediately
        rb.velocity = Vector2.zero;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
        Destroy(gameObject);
        }
    }
    void StartCooldown()
    {
        // Set the cooldown flag to true
        isCooldown = true;
    }
}