using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private int upTime;
    private int cooldown;
    private Player parent;

    public void setUpTime(int UpTime)
    {
        this.upTime = UpTime;
    }
    public void setCooldown(int cooldown)
    {
        this.cooldown = cooldown;
    }
    public void setParent(Player player)
    {
        this.parent = player;
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
    void OnTriggerEnter2D (Collider2D collision)
    {
        if(collision.gameObject.tag == "projectile")
        {
            if (collision.gameObject.GetComponent<Projectile>().getShooter() != parent)
            {
                parent.score += 5;
            }
        } else
        {
            return;
        }
    }
}
