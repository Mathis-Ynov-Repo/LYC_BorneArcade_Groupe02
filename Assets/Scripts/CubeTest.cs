using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTest : MonoBehaviour
{
    public Transform pfhealthBar;
    private HealthSystem healthSystem = new HealthSystem(80);

    void Start()
    {

        Transform healthBarTransform = Instantiate(pfhealthBar, transform.position - transform.forward * 15f + transform.right * 2.5f, Quaternion.Euler(90, transform.eulerAngles.y, transform.eulerAngles.z));
        HealthBar healthBar = healthBarTransform.GetComponent<HealthBar>();
        healthBar.transform.parent = transform;

        healthBar.Setup(healthSystem);
    }

    void Update()
    {
        
    }
    public void TakeDamage(int damage)
    {
        healthSystem.Damage(damage);
        if (healthSystem.GetHealth() == 0)
        {
            Die();
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }
}
