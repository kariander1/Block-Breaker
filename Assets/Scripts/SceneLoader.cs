using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
  public void LoadNextScene()
    {
        int current_scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(current_scene + 1);

    }
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
        FindObjectOfType<GameSession>().Resetgame();
        
        

    }
    public void Quit()
    {
        Application.Quit();
    }
}
