using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{

    void Start()
    {

    }
/////////////////UPDATE/////////////
    void Update()
    {
        // Check if the left mouse button is clicked and cooldown is not active
        if (Input.GetMouseButtonDown(0))
        {
            // Get the mouse position in the world
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Check if the mouse click is over the bomb
            if (GetComponent<Collider2D>().OverlapPoint(mousePosition))
            {
                HandleClick();
                Destroy(gameObject);
            }
        }
    }
////////////////////////////////////////////////
    void HandleClick()
    {

        GameManager.Instance.coinCollected();
    }

}