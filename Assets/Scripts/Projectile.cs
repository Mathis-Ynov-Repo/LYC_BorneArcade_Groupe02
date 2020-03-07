using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public int damage = 10;
    public float speed = 5;
    private string shooterTag;
    private string targetTag;
    private Rigidbody2D theRB;
    private Player shooter;
    // Start is called before the first frame update
    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
        if(shooterTag == "Player1")
        {
            targetTag = "Player2";
        } else
        {
            targetTag = "Player1";
        }
    }

    // Update is called once per frame
    void Update()
    {
        theRB.velocity = transform.right * speed;
        //theRB.velocity = new Vector2(speed * transform.localScale.x, 0);
        //transform.Translate(Vector2.right * Time.deltaTime * speed);
    }
    public void setSpeed(float speed)
    {
        this.speed = speed;
    }
    public void setShooterTag(string shooterTag)
    {
        this.shooterTag = shooterTag;
    }
    public void setShooter(Player player)
    {
        this.shooter = player;
    }
    public Player getShooter()
    {
        return shooter;
    }
    /*
    void OnCollisionEnter2D(Collision2D collision)
    {
        //next - check if we have collided with anything but player/enemy
        if (collision.gameObject.tag == "Ennemy")
        {
            collision.gameObject.GetComponent<CubeTest>().TakeDamage(damage);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Shield")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<SpaceShip>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    */
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == targetTag)
        {
            collision.gameObject.GetComponent<SpaceShip>().TakeDamage(damage, shooter);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == shooterTag)
        {
            return;
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
