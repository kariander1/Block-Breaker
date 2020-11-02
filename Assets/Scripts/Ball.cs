using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Ball : MonoBehaviour
{
    [SerializeField] Paddle paddle1;
    [SerializeField] float velocityX = 2f;
    [SerializeField] float velocityY = 6f;
    [SerializeField] float velocityXadd = 0.1f;
    [SerializeField] float velocityYadd = 0.1f;
    


    Vector2 paddleToBallVector;
    bool hasStarted = false;
    // Start is called before the first frame update
    void Start()
    {
        paddleToBallVector = (Vector2) (transform.position- this.paddle1.transform.position);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();
        }

    }
    private void LaunchOnMouseClick()
    {
        if(Input.GetMouseButtonDown(0))
        {

            GetComponent<Rigidbody2D>().velocity = new Vector2(velocityX, velocityY);
            hasStarted = true;
        }
    }
    void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        this.transform.position = paddlePos + this.paddleToBallVector;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name.StartsWith("Paddle"))
        {
            GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity + new Vector2(this.velocityXadd,this.velocityYadd);
            FindObjectOfType<GameSession>().updateDebug(GetComponent<Rigidbody2D>().velocity);


        }
     //   GetComponent<AudioSource>().Play();
    }
}
