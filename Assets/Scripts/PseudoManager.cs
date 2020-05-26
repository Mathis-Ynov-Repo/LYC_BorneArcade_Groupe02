using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PseudoManager : MonoBehaviour
{

    public string Player1Name = "Player 1";
    public string Player2Name = "Player 2";
    
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    public void SetPlayer1Pseudo(string pseudo)
    {
        this.Player1Name = pseudo;
    }
    public void SetPlayer2Pseudo(string pseudo)
    {
        this.Player2Name = pseudo;
    }
    public string GetPlayer1Pseudo()
    {
        return Player1Name;
    }
    public string GetPlayer2Pseudo()
    {
        return Player2Name;
    }
}
