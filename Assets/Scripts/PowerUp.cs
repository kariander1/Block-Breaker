using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    // [SerializeField] Sprite PowerUpSprite;
    [SerializeField] int rarity;
    [SerializeField] AudioClip powerGainedSound;
    [SerializeField] float scaleFactor;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.gameObject.name == "Paddle")
        {
            switch (tag)
            {
                case "PowerUp_Expand":
                    expand(collision);
                    break;
                case "PowerUp_Shorten":
                    shorten(collision);
                    break;
                
            }
            destroyPowerUp();
        }
        else if(collision.gameObject.name == "Left Wall" || collision.gameObject.name == "Right Wall")
        {
            Vector2 velocity = GetComponent<Rigidbody2D>().velocity;
            GetComponent<Rigidbody2D>().velocity = new Vector2(-velocity.x,velocity.y);
        }
        else if (collision.gameObject.name == "Top Wall")
        {
            Vector2 velocity = GetComponent<Rigidbody2D>().velocity;
            GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x, -velocity.y);
        }
    }
    private void shorten(Collider2D collision)
    {
        collision.gameObject.transform.localScale -= new Vector3(scaleFactor, 0, 0);
       
    }
    private void expand(Collider2D collision)
    {
        collision.gameObject.transform.localScale += new Vector3(scaleFactor, 0, 0);
        
    }
    private void destroyPowerUp()
    {
        AudioSource.PlayClipAtPoint(powerGainedSound, Camera.main.transform.position);
        Destroy(gameObject);
    }
     
}
