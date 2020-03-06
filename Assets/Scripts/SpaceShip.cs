using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpaceShip : MonoBehaviour
{
    private int id;
    public int stocks;
    //private int lifePoints = 100;
    private HealthSystem healthSystem = new HealthSystem(100);
    public float movementSpeed;
    public float baseMovementSpeed = 100;
    public float projectileSpeed;
    public float baseProjectileSpeed;
    private string color;
    public int shieldUpTime;
    private float nextFireTime = 0;
    private int maxAmmo = 10;
    private int currentAmmo;
    private int reloadTime = 5;
    private int invicibilityTime = 2;
    private bool isReloading = false;
    private bool isShielded = false;
    public bool isInvincible = false;
    public int shieldCD;
    public Transform pfhealthBar;

    public Shield shield;

    public Projectile projectile;
    public Transform shootingPoint;

    public Text ammoText;
    public Text maxAmmoText;
    public Text ShieldRemainingCooldownText;
    public Text StocksLeftText;

    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = maxAmmo;

        Transform healthBarTransform = Instantiate(pfhealthBar, transform.position + transform.right * -1.1f, Quaternion.Euler(0, 0, 90));

        healthBarTransform.parent = transform;
        HealthBar healthBar = healthBarTransform.GetComponent<HealthBar>();

        healthBar.Setup(healthSystem);

        //Physics.IgnoreLayerCollision(8, 8);
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
        StocksLeftText.text = stocks.ToString();

        if (isInvincible)
        {
            return;
        }

        if (isReloading)
        {
            if (Input.GetButtonUp("Fire2"))
            {
                StartCoroutine(Protect());
            }
            else
            {
                return;
            }

        }
        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
        
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        if (Input.GetButtonUp("Fire2"))
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
        currentAmmo = maxAmmo;
    }
    IEnumerator Protect()
    {
        if (Time.time > nextFireTime)
        {
            //GameObject prefab = Resources.Load("bouclier") as GameObject;

            Quaternion rotation = transform.rotation;
            var position = transform.position;
            //GameObject shieldObject = Instantiate(prefab, position, rotation);
            Shield shieldObject = Instantiate(shield, position, rotation);
            shieldObject.transform.parent = transform;
            //Shield shield = shieldObject.GetComponent<Shield>();
            shieldObject.setUpTime(shieldUpTime);
            shieldObject.tag = "Shield";
            isShielded = true;
            nextFireTime = Time.time + shieldCD;
            yield return new WaitForSeconds(shieldUpTime);
            isShielded = false;
        } else
        {
            Debug.Log("Ya pas shield");
        }

    }
    public void TakeDamage(int damage)
    {
        if(!isShielded && !isInvincible)
        {
            healthSystem.Damage(damage);
            movementSpeed = movementSpeed * 1.05f;
            projectileSpeed = projectileSpeed * 1.1f;
            if (healthSystem.GetHealth() == 0 && stocks == 1)
            {
                StocksLeftText.text = "0";
                Die();
            }
            else if (healthSystem.GetHealth() == 0)
            {
                stocks -= 1;
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
        Destroy(gameObject);
    }

    public void Move(float speed)
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

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
