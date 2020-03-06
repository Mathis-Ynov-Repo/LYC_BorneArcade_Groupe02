using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    float curTime = 0;
    float nextDamage = 1;
    public List<GameObject> listOfObjects;
    // Start is called before the first frame update
    void Start()
    {
        listOfObjects = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if( listOfObjects.Count > 0)
        {
            if (curTime <= 0)
            {
                foreach (GameObject go in listOfObjects)
                {
                    go.gameObject.GetComponent<SpaceShip>().TakeDamage(10);
                }
                curTime = nextDamage;
            }
            else
            {

                curTime -= Time.deltaTime;
            }
        }
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2")
        {
            listOfObjects.Add(collision.gameObject);
        }
    }
    /*void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(collision);
        if (curTime <= 0)
        {
            foreach (GameObject go in listOfObjects)
            {

              //if (go.transform == collision.transform)
               //{
                go.gameObject.GetComponent<SpaceShip>().TakeDamage(10);
            //go.gameObject.GetComponent<SpaceShip>().TakeDamage(1);
                //}
            }
            curTime = nextDamage;
        }
        else
        {

            curTime -= Time.deltaTime;
        }
    }*/
    void OnTriggerExit2D(Collider2D collision)
    {
        listOfObjects.Remove(collision.gameObject);
    }
}
