using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private int upTime;
    private int cooldown;

    public void setUpTime(int UpTime)
    {
        this.upTime = UpTime;
    }
    public void setCooldown(int cooldown)
    {
        this.cooldown = cooldown;
    }
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, upTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
