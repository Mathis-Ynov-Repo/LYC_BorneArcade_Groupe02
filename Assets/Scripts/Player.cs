using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int score;
    public Text PseudoText;
    public Text ScoreText;
    private string pseudo;
    private int stocks = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = score.ToString();
    }
    public void SetStocks(int stocks)
    {
        this.stocks = stocks;
    }
    public void SetPseudo(string pseudo)
    {
        this.pseudo = pseudo;
    }
    public string GetPseudo()
    {
        return this.pseudo;
    }
    public int GetStocks()
    {
        return this.stocks;
    }
}
