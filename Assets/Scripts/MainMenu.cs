using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

  //public void PlayGame()
  //{
  //  SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
  //}
  public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
  public void GoToHighScores()
    {
        SceneManager.LoadScene(2);
    }
    public void GoToControls()
    {
        SceneManager.LoadScene(3);
    }

    public void QuitGame()
  {
    Debug.Log("QUIT!");
    Application.Quit();
  }

  //public void ReplayGame()
  //{
  //  SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  //}

  public void ReturnToMainMenu()
  {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        SceneManager.LoadScene(0);
  }



}

