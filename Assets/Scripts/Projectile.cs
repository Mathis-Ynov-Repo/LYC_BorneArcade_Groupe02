using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public int damage = 10;
    private int speed = 5;
    private Rigidbody2D theRB;
    // Start is called before the first frame update
    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        theRB.velocity = transform.right * speed;
        //theRB.velocity = new Vector2(speed * transform.localScale.x, 0);
        //transform.Translate(Vector2.right * Time.deltaTime * speed);
    }
    public void setSpeed(int speed)
    {
        this.speed = speed;
    }
    void OnCollisionEnter(Collision collision)
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
    void OnTriggerEnter2D(Collider2D collider2D)
    {
        Destroy(gameObject);
    }
}
