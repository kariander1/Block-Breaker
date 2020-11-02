using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour
{
    // config params
    [SerializeField] AudioClip breakSound;
    [SerializeField] AudioClip crackSound;
    [SerializeField] AudioClip bounceSound;
    [SerializeField] GameObject blockSparklesVFX;
    
    [SerializeField] int maxHits;
    [SerializeField] Sprite[] hitSprites;

    // cached reference
    Level level;
    GameSession gameStatus;

    // state variables
    [SerializeField] int timesHit; //TODO only for debug

    private void Start()
    {
        this.level= FindObjectOfType<Level>();
        this.gameStatus= FindObjectOfType<GameSession>();
       // if (tag == "Breakable")
         //   level.countBreakableBlocks(); Removed just to remove errors
    }
    private void HandleHit()
    {
        timesHit++;
        if (timesHit == maxHits)
        {
            destroyBlock();
        }
        else
        {
            AudioSource.PlayClipAtPoint(crackSound, Camera.main.transform.position);
            ShowNextHitSprite();
        }
    }
    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHit - 1;
        GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (tag == "Breakable")
        {
            HandleHit();
        }
        else if (tag == "Unbreakable")
        {

            AudioSource.PlayClipAtPoint(bounceSound, Camera.main.transform.position);
        }

    }
    private void destroyBlock()
    {
        this.gameStatus.AddToScore();
        TriggerSparklesVFX();

        AudioSource aud = GetComponent<AudioSource>();
        AudioSource.PlayClipAtPoint(aud.clip, Camera.main.transform.position);

        generateScore();
        //    gameObject.SetActive(false);
        //  level.blockDestroy();  Removed just to remove errors
        Destroy(gameObject);
    }
    private void generateScore()
    {
        //GameObject newText = new GameObject("text", typeof(RectTransform));
        //var newTextComp = newText.AddComponent<Text>();
        ////Text newText = transform.gameObject.AddComponent<Text>(); //This is the old code, it's generates a Null Exception
        //newTextComp.text = "10";
        //newTextComp.fontSize = 16;


        //newText.transform.SetParent(transform);
        //Instantiate(text, transform.position, transform.rotation);
    }
    private void TriggerSparklesVFX()
    {

         
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 1f);
    }
}
