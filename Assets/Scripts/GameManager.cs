
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject endGameScreen;

    bool gameHasEnded = false;
    void Start()
    {
        endGameScreen.SetActive(false);
        var players = FindObjectsOfType<Player>();
        var pseudoManager = FindObjectOfType<PseudoManager>();
        if (players[0].name == "Player1")
        {
            players[0].SetPseudo(pseudoManager.GetPlayer1Pseudo());
            players[1].SetPseudo(pseudoManager.GetPlayer2Pseudo());
        } else
        {
            players[1].SetPseudo(pseudoManager.GetPlayer1Pseudo());
            players[0].SetPseudo(pseudoManager.GetPlayer2Pseudo());
        }
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
