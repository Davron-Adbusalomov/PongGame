using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public bool isPlayerA = false;  // True if this is Player A (W/S controls)
    public bool isAIEnabled = false;  // True if Player B is AI-controlled
    public GameObject ball;  // Reference to the ball for AI to track

    private Rigidbody2D rb;
    private Vector2 playerMovement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Determine control logic for Player A or Player B (AI or Player)
        if (isPlayerA)
        {
            // Player A uses W and S keys for movement
            playerMovement = new Vector2(0, GetPlayerAInput());
        }
        else
        {
            if (isAIEnabled)
            {
                // AI control for Player B
                PaddleAIController();
            }
            else
            {
                // Player B uses Up and Down arrow keys for movement
                playerMovement = new Vector2(0, GetPlayerBInput());
            }
        }
    }

    private float GetPlayerAInput()
    {
        // "W" moves up, "S" moves down for Player A
        if (Input.GetKey(KeyCode.W))
        {
            return 1f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            return -1f;
        }
        else
        {
            return 0f;
        }
    }

    private float GetPlayerBInput()
    {
        // "Up Arrow" moves up, "Down Arrow" moves down for Player B
        if (Input.GetKey(KeyCode.UpArrow))
        {
            return 1f;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            return -1f;
        }
        else
        {
            return 0f;
        }
    }

    private void PaddleAIController()
    {
        if (ball != null)
        {
            // Simple AI follows the ball
            if (ball.transform.position.y > transform.position.y + 0.5f)
            {
                playerMovement = new Vector2(0, 1);
            }
            else if (ball.transform.position.y < transform.position.y - 0.5f)
            {
                playerMovement = new Vector2(0, -1);
            }
            else
            {
                playerMovement = new Vector2(0, 0);  // Stop moving when aligned with the ball
            }
        }
    }

    private void FixedUpdate()
    {
        // Apply the movement to the paddle
        rb.velocity = playerMovement * speed;
    }
}
