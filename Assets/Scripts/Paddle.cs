using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] private float screenWidthInUnits = 16f;
    [SerializeField] private float minScreenPos = 1f;
    [SerializeField] private float maxScreenPos = 15f;

    GameSession gameSession;
    Ball ball;
    // Start is called before the first frame update
    void Start()
    {
        this.gameSession = FindObjectOfType<GameSession>();
        this.ball = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameSession.auto_play)
        {
      
            transform.position =new Vector2(ball.transform.position.x,transform.position.y);
        }
        else
        {
            float mousePos = Mathf.Clamp((Input.mousePosition.x / Screen.width) * screenWidthInUnits, this.minScreenPos, this.maxScreenPos);
            Vector2 paddlePosition = new Vector2(mousePos, transform.position.y);
            transform.position = paddlePosition;
        }
    //    Debug.Log(Input.mousePosition);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<AudioSource>().Play();
        
    }
}
