using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{

    public float initialSpeed = 10f;

    public float speedIncrease = 0.2f;

    public Text playerText;

    public Text opponentText;

    private int hitCounter;

    private Rigidbody2D rb;

    public AudioClip paddleHitSound;
    public AudioClip wallHitSound;
    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>(); 
        Invoke("StartBall", 2f);
    }

    void Update()
    {
        
    }

    private void FixedUpdate(){
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, initialSpeed
        + (speedIncrease*hitCounter));
    }

    private void StartBall(){
        rb.velocity = new Vector2(-1, 0)*(initialSpeed +
        speedIncrease * hitCounter);
    }

    private void RestartBall(){
        rb.velocity = new Vector2(0, 0);
        transform.position = new Vector2(0, 0);
        hitCounter = 0;
        Invoke("StartBall", 2f);
    }

    private void PlayerBounce(Transform obj){
        hitCounter++;

        Vector2 ballPosition = transform.position;
        Vector2 playerPosition = obj.position;

        float xDirection;
        float yDirection;

        if(transform.position.x > 0){
            xDirection = -1;
        }else {
            xDirection = 1;
        }

        yDirection = (ballPosition.y = playerPosition.y)/obj.GetComponent<Collider2D>().bounds.size.y;

        if(yDirection==0){
            yDirection = 0.25f;
        }

        rb.velocity = new Vector2(xDirection, yDirection) * (initialSpeed + 
        (speedIncrease * hitCounter));
    }



    private void OnCollisionEnter2D(Collision2D other) 
{
    if (other.gameObject.name == "Player" || other.gameObject.name == "AI") 
    {
        PlayerBounce(other.transform);
        audioSource.PlayOneShot(paddleHitSound); 
    } 
    else if (other.gameObject.name == "Wall") 
    {
        audioSource.PlayOneShot(wallHitSound);
    } 
    else if (other.gameObject.name == "speedObstacle") 
    {
        IncreaseSpeed();
    }
}


    private void IncreaseSpeed(){
            initialSpeed += 4f; // Increase speed by a fixed amount
            hitCounter++; // Optionally increment hitCounter
            Debug.Log("Speed Increased: " + initialSpeed); // Debugging line to check speed
        }


    private void OnTriggerEnter2D(Collider2D other){
        if(transform.position.x > 0){
            RestartBall();
            opponentText.text = (int.Parse(opponentText.text)+1).ToString();
        }else if(transform.position.x < 0){
            RestartBall();
            playerText.text = (int.Parse(s: playerText.text)
                + 1).ToString();
        }else if (other.gameObject.name=="speedObstacle"){
                IncreaseSpeed();
                Destroy(other.gameObject); // Destroy the obstacle after the effect
        }
    }
}
