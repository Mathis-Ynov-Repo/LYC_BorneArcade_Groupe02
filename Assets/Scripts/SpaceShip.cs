﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpaceShip : MonoBehaviour
{
    //attributes

    public int id;
    //private int lifePoints = 100;
    private HealthSystem healthSystem = new HealthSystem(100);
    public float movementSpeed;
    private float baseMovementSpeed = 6;
    public float projectileSpeed;
    private float baseProjectileSpeed = 15;
    private string color;
    private int shieldUpTime = 3;
    private float nextFireTime = 0;
    private float nextShootingTime = 0;
    private float fireRate = 0.15f;
    private int maxAmmo = 10;
    private int currentAmmo;
    private int reloadTime = 5;
    private int invicibilityTime = 2;
    private bool isReloading = false;
    private bool isShielded = false;
    public bool isInvincible = false;
    private int shieldCD = 10;
    public Transform pfhealthBar;
    public Player player;
    public Player opponent;

    public Shield shield;

    public Projectile projectile;
    public Transform shootingPoint;

    //Database
    public DB db;

    //Text

    public Text ammoText;
    public Text maxAmmoText;
    public Text ShieldRemainingCooldownText;
    public Text StocksLeftText;

    public Text WinnerText;

    //variable
    Coroutine ReloadCoroutine = null;


    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = maxAmmo;
        
        StocksLeftText.text = player.GetStocks().ToString();

        Transform healthBarTransform = Instantiate(pfhealthBar, transform.position + transform.right * -1.1f, Quaternion.Euler(0, 0, 90));

        healthBarTransform.parent = transform;
        HealthBar healthBar = healthBarTransform.GetComponent<HealthBar>();


        healthBar.Setup(healthSystem);

        movementSpeed = baseMovementSpeed;
        projectileSpeed = baseProjectileSpeed;

    }

    // Update is called once per frame
    void Update()
    {
        Move(movementSpeed);

        var remainingTime = nextFireTime - Time.time;
        if (remainingTime <= 0)
        {
            ShieldRemainingCooldownText.fontSize = 15;
            ShieldRemainingCooldownText.text = "Shield Up";
        }
        else
        {
            ShieldRemainingCooldownText.fontSize = 22;
            ShieldRemainingCooldownText.text = remainingTime.ToString("0.0");
        }

        if (currentAmmo == 0)
        {
            ammoText.fontSize = 15;
            ammoText.text = "RLD";
        }
        else
        {
            ammoText.fontSize = 22;
            ammoText.text = currentAmmo.ToString();
        }

        maxAmmoText.text = maxAmmo.ToString();

        if (isInvincible)
        {
            return;
        }

        if (isReloading)
        {
            if (Input.GetButtonDown("FireShield " + id))
            {
                StartCoroutine(Protect());
                return;
            }
            else
            {
                return;
            }

        }
        if (currentAmmo <= 0)
        {
            ReloadCoroutine = StartCoroutine(Reload());
            return;
        }

        if (Input.GetButtonDown("FireProjectile " + id))
        {
            if (Time.time > nextShootingTime)
            {
                Shoot();
                nextShootingTime = Time.time + fireRate;
            }

        }
        if (Input.GetButtonDown("FireShield " + id))
        {
            StartCoroutine(Protect());
        }

    }
    void Shoot()
    {
        currentAmmo--;

        Projectile projectileObject = Instantiate(projectile, shootingPoint.position, shootingPoint.rotation);
        projectileObject.setSpeed(projectileSpeed);
        projectileObject.setShooterTag(gameObject.tag);
        projectileObject.setShooter(this.player);
    }
    IEnumerator Reload()
    {

        isReloading = true;
        Debug.Log("Reloading..");

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        isReloading = false;
    }
    IEnumerator Invicible()
    {
        isInvincible = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        transform.GetChild(1).gameObject.SetActive(false);
        yield return new WaitForSeconds(invicibilityTime);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        transform.GetChild(1).gameObject.SetActive(true);
        isInvincible = false;
        if (isReloading)
        {
            isReloading = false;
            StopCoroutine(ReloadCoroutine);
        }
        nextFireTime = 0;
        currentAmmo = maxAmmo;
    }
    IEnumerator Protect()
    {
        if (Time.time > nextFireTime)
        {

            Quaternion rotation = transform.rotation;
            var position = transform.position;
            Shield shieldObject = Instantiate(shield, position, rotation);
            shieldObject.transform.parent = transform;
            shieldObject.setUpTime(shieldUpTime);
            shieldObject.tag = "Shield";
            shieldObject.setParent(player);
            isShielded = true;
            nextFireTime = Time.time + shieldCD;
            yield return new WaitForSeconds(shieldUpTime);
            isShielded = false;
        }
        else
        {
            Debug.Log("Ya pas shield");
        }

    }
    public void TakeDamage(int damage, Player opponent)
    {
        if (!isShielded && !isInvincible)
        {
            opponent.score += 10;
            healthSystem.Damage(damage);
            movementSpeed = movementSpeed * 1.05f;
            projectileSpeed = projectileSpeed * 1.1f;
            if (healthSystem.GetHealth() == 0 && player.GetStocks() == 1)
            {
                StocksLeftText.text = "0";
                Die();
            }
            else if (healthSystem.GetHealth() == 0)
            {
                //stocks -= 1;
                player.SetStocks(player.GetStocks() - 1);
                StocksLeftText.text = player.GetStocks().ToString();
                StartCoroutine(Invicible());
                healthSystem = new HealthSystem(100);
                movementSpeed = baseMovementSpeed;
                projectileSpeed = baseProjectileSpeed;
                var healthbar = transform.Find("healthbar 1(Clone)");
                healthbar.Find("Bar").localScale = new Vector3(healthSystem.GetHealthPercent(), 1);
                HealthBar healthBar = healthbar.GetComponent<HealthBar>();

                healthBar.Setup(healthSystem);
            }
        }

    }
    public void TakeDamage(int damage)
    {
        if (!isShielded && !isInvincible)
        {
            healthSystem.Damage(damage);
            movementSpeed = movementSpeed * 1.05f;
            projectileSpeed = projectileSpeed * 1.1f;
            if (healthSystem.GetHealth() == 0 && player.GetStocks() == 1)
            {
                StocksLeftText.text = "0";
                Die();
            }
            else if (healthSystem.GetHealth() == 0)
            {
                //stocks -= 1;
                player.SetStocks(player.GetStocks() - 1);
                StocksLeftText.text = player.GetStocks().ToString();
                StartCoroutine(Invicible());
                healthSystem = new HealthSystem(100);
                movementSpeed = baseMovementSpeed;
                projectileSpeed = baseProjectileSpeed;
                var healthbar = transform.Find("healthbar 1(Clone)");
                healthbar.Find("Bar").localScale = new Vector3(healthSystem.GetHealthPercent(), 1);
                HealthBar healthBar = healthbar.GetComponent<HealthBar>();

                healthBar.Setup(healthSystem);
            }
        }

    }
    void Die()
    {
        db.InsertScore(player.GetPseudo(), player.score);
        db.InsertScore(opponent.GetPseudo(), opponent.score);
        WinnerText.text = opponent.GetPseudo() + " WINS";
        FindObjectOfType<SpaceShip>().isInvincible = true;

        Destroy(gameObject);
        FindObjectOfType<GameManager>().EndGame();
    }

    public void Move(float speed)
    {
        float horizontal = Input.GetAxisRaw("Horizontal " + id);
        float vertical = Input.GetAxisRaw("Vertical " + id);

        Vector3 direction = new Vector3(horizontal, vertical, 0.0f);
        if (direction != Vector3.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.15F);
        }
        transform.Translate(direction * Time.deltaTime * speed, Space.World);
    }
}
