
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject endGameScreen;

    bool gameHasEnded = false;
    void Start()
    {
        endGameScreen.SetActive(false);
    }
    public void EndGame()
    {
        if(gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("GAME OVER");
            endGameScreen.SetActive(true);
        }
        
    }
}
