using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int score;
    private int stocks = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetStocks(int stocks)
    {
        this.stocks = stocks;
    }
    public int GetStocks()
    {
        return this.stocks;
    }
}
