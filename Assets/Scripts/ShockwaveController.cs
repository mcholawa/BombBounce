using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveController : MonoBehaviour
{
    public float expansionSpeed = 5f; // Adjust the speed of expansion
    public float duration = 1.5f; // Adjust the duration of the shockwave
    public float blastForce = 10f; // Adjust the force applied to the player ball
    public string playerBallTag = "Player"; // Set this to the tag of your player ball

    private void Start()
    {
        // Start the shockwave animation
        StartCoroutine(AnimateShockwave());
    }

    private IEnumerator AnimateShockwave()
    {
        float elapsedTime = 0f;
        Vector3 initialScale = transform.localScale;

        // Expand the shockwave
        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(initialScale, initialScale * 4f, elapsedTime / duration);
            elapsedTime += Time.deltaTime * expansionSpeed;
            yield return null;
        }

        // Ensure the shockwave reaches its full size
        transform.localScale = initialScale * 4f;

        // Wait for a short moment
        yield return new WaitForSeconds(0.2f);

        // Fade out the shockwave
        while (transform.localScale.x > 0.1f)
        {
            transform.localScale = Vector3.Lerp(Vector3.zero, transform.localScale, 1 - elapsedTime / duration);
            elapsedTime += Time.deltaTime * expansionSpeed;
            yield return null;
        }

        // Ensure the shockwave disappears completely
        transform.localScale = Vector3.zero;

        // Destroy the shockwave object
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
{
    // Check if the colliding object has the specified tag
    if (other.CompareTag(playerBallTag))
    {
        // The shockwave has collided with the player ball
        Rigidbody2D playerRigidbody = other.GetComponent<Rigidbody2D>();

        if (playerRigidbody != null)
        {
            float currentGravityScale = playerRigidbody.gravityScale;
            playerRigidbody.gravityScale = 0;
            // Set the velocity to zero to ignore gravitational velocity
            playerRigidbody.velocity = Vector2.zero;

            // Calculate the force direction away from the bomb
            Vector2 forceDirection = (other.transform.position - transform.position).normalized;

            // Calculate the force magnitude based on distance from the bomb
            float distance = Vector2.Distance(other.transform.position, transform.position);
            float adjustedForce = blastForce / distance;

            // Apply the force to the player ball
            playerRigidbody.AddForce(adjustedForce * forceDirection, ForceMode2D.Impulse);
            playerRigidbody.gravityScale = currentGravityScale;
            Destroy(gameObject);
        }

        // You can add other actions here if needed
        Debug.Log("Shockwave collided with the player ball!");
    }
}

}