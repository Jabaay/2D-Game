using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    private Rigidbody2D rb;

    private float speed = 6.5f;
    private float rotationSpeed = 200f;
    private float driftFactor = 0.9f;

    private AudioSource audioSource;

    [SerializeField] AudioClip bulletAudio;
    [SerializeField] AudioClip missileAudio;
    [SerializeField] AudioClip driftAudio;
    [SerializeField] AudioClip lowPowerAudio;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject missilePrefab;

    // Images and Sliders/Bars
    [SerializeField] public GameObject healthIcon;
    [SerializeField] public GameObject powerIcon;
    [SerializeField] public GameObject shieldIcon;

    [SerializeField] public Slider healthBar;
    [SerializeField] public Slider powerBar;
    [SerializeField] public Slider shieldBar;

    [SerializeField] private GameObject[] damageFlames;
    [SerializeField] private GameObject[] nitro;
    [SerializeField] public GameObject shield;
    // SerializeField] public GameObject playerExplosion;

    [SerializeField] public Text killCounter;

    public int killCount;
    public int score;
    public bool shieldOn;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        SetHealthBarVal(10);
        SetPowerBarVal(0);
        transform.position = new Vector3(2.42f, -8.33f, 0);
        killCount = 0;
    }


    // Update is called once per frame
    void Update()
    {
        Debug.Log("Player HP: " + healthBar.value);
        Debug.Log("Player Speed: " + speed);
        Move();
        Bounds();
        Attack();
        Boost();
        Shield();
        Damage();
        killCounter.text = "X" + killCount;
    }


    // Moves the player.
    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Basic car movement
        transform.Translate(Vector3.up * speed * verticalInput * Time.deltaTime);
        transform.Translate(Vector3.right * speed * horizontalInput * Time.deltaTime);

        // Drifting when the spacebar is pressed
        if (Input.GetKey(KeyCode.Space))
        {
            // Apply a drifting factor to reduce the car's linear velocity
            rb.velocity = rb.velocity * driftFactor;

            // Rotate the car based on user input
            float rotation = -horizontalInput * rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.forward, rotation);

            if (Input.GetKeyDown(KeyCode.Space))
                audioSource.PlayOneShot(driftAudio);
        }
    }


    // Set the playable area.
    private void Bounds()
    {

        // boundary for moving vertically
        if (transform.position.y > 11)
        {
            transform.position = new Vector3(transform.position.x, -11, 0);
        }

        else if (transform.position.y < -11)
        {
            transform.position = new Vector3(transform.position.x, 11, 0);
        }

        // boundary for moving horizontally 
        if (transform.position.x > 3.97f)
        {
            transform.position = new Vector3(3.97f, transform.position.y, 0);
        }

        else if (transform.position.x < -3.97f)
        {
            transform.position = new Vector3(-3.97f, transform.position.y, 0);
        }
    }


    // Attacks enemies.
    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // clone a laser at the ship's position with the original rotation
            Instantiate(bulletPrefab, transform.position, transform.rotation);
            audioSource.PlayOneShot(bulletAudio);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (powerBar.value >= 5f)
            {
                Instantiate(missilePrefab, transform.position, transform.rotation);
                audioSource.PlayOneShot(missileAudio);
                SetPowerBarVal(powerBar.value - 5f); // each missile costs 5 power points
            }
            else
            {
                audioSource.PlayOneShot(lowPowerAudio);
                powerIcon.SetActive(false);
                Invoke("TogglePowerIconOn", 0.3f);
            }
        }
    }


    // Helper methods for Attack().
    // Toggles HUD on.
    private void TogglePowerIconOn()
    {
        powerIcon.SetActive(true);
    }



    // Boost.
    private void Boost()
    {
        if (speed <= 6.5f)
        {
            for (int i = 0; i < nitro.Length; i++)
            {
                nitro[i].SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (powerBar.value < 3)
            {
                audioSource.PlayOneShot(lowPowerAudio);
                powerIcon.SetActive(false);
                Invoke("TogglePowerIconOn", 0.3f);
            }
            else
            {
                for (int i = 0; i < nitro.Length; i++)
                {
                    nitro[i].SetActive(true);
                }
                SetPowerBarVal(powerBar.value -= 3);
                StartCoroutine(BoostUp());
                speed *= 1.5f;
            }
        }
    }

    // Helper method for Boost().
    // Each boost lasts 2 seconds.
    IEnumerator BoostUp()
    {
        yield return new WaitForSeconds(2f);
    }


    // Shields.
    private void Shield()
    {
        if (shieldBar.value == 0)
        {
            shield.SetActive(false);
            shieldOn = false;
        }
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (powerBar.value >= 5)
            {
                if (shieldOn == true)
                {
                    SetShieldBarVal(shieldBar.value);
                    audioSource.PlayOneShot(lowPowerAudio);
                    powerIcon.SetActive(false);
                    shieldIcon.SetActive(false);
                    Invoke("TogglePowerIconOn", 0.3f);
                    Invoke("ToggleShieldIconOn", 0.3f);
                }
                else
                {
                    SetPowerBarVal(powerBar.value - 5);
                    SetShieldBarVal(shieldBar.value + 5);
                    shield.SetActive(true);
                    shieldOn = true;
                }
            }
            else
            {
                shieldIcon.SetActive(false);
                Invoke("ToggleShieldIconOn", 0.3f);
                audioSource.PlayOneShot(lowPowerAudio);
            }
        }
    }

    // Helper methods for Shield().
    // Toggles HUD on.
    private void ToggleShieldIconOn()
    {
        shieldIcon.SetActive(true);
    }

    // Helper methods for Shield().
    // Toggles HUD on.
    private void ToggleShieldBarOn()
    {
        shieldBar.enabled = true;
    }


    private void Damage()
    {
        int randomIndex = Random.Range(0, 1);
        int remainingIndex;

        if (randomIndex == 0)
        {
            remainingIndex = 1;
        } else
        {
            remainingIndex = 0;
        }

        if (healthBar.value == 10)
        {
            damageFlames[randomIndex].SetActive(false);
            damageFlames[remainingIndex].SetActive(false);
        }
        else if (healthBar.value < 10 && healthBar.value >= 5)
        {
            damageFlames[randomIndex].SetActive(true);
            damageFlames[remainingIndex].SetActive(false);
            speed = 5.5f;
        } else if (healthBar.value < 5 && healthBar.value > 0)
        {
            damageFlames[randomIndex].SetActive(true);
            damageFlames[remainingIndex].SetActive(true);
            speed = 3.9f;
        }
    }


    // Sets Bars/Sliders' values.
    public void SetHealthBarVal(float newValue)
    {
        healthBar.value = newValue;
    }

    public void SetPowerBarVal(float newValue)
    {
        powerBar.value = newValue;
    }

    public void SetShieldBarVal(float newValue)
    {
        shieldBar.value = newValue;
    }


    // Set Player stats (Health, Power, and Shield).
    public void SetPlayerStats()
    {

        // making sure Player isn't null before setting the stats
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            // Player health
            if (healthBar.value <= 10 && healthBar.value >= 8) // cap it to 10 if b/w 8 and 10
            {
                SetHealthBarVal(10);
            }
            else // else add 2
            {
                healthBar.value += 2;
            }

            // Player power
            if (powerBar.value <= 10 && powerBar.value >= 8) // cap it to 10 if b/w 8 and 10
            {
                SetPowerBarVal(10);
            }
            else // else add 2
            {
                powerBar.value += 2;
            }
        }
    }


}
