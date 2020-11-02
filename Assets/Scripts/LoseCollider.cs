using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            Cursor.visible = true;
            AudioSource aud = GetComponent<AudioSource>();
            //aud.PlayOneShot()
            AudioSource.PlayClipAtPoint(aud.clip, Camera.main.transform.position);
            SceneManager.LoadScene("Game Over");
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }
}
