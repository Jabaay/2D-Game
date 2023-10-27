// This program is to control player movement, and set player bounds.


using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Class name must match the file name
// : MonoBehaviour - inherits from something


public class Player : MonoBehaviour
{

    // instances
    // SerializeField - pokes thru to Unity, but still private
    [SerializeField] private float speed = 10.0f;
    private float fireRate = 0.15f;
    private float canFire = 0.0f;
    [SerializeField] private int lives = 3;

    [SerializeField] public bool canTripleShot = false;
    [SerializeField] private bool canShield = false;

    public GameObject laserPrefab;
    public GameObject tripleShotPrefab;
    public GameObject playerExplosionAnimation;
    public GameObject shield;
    [SerializeField] private GameObject[] engines;

    private AudioSource laserAudio;

    private UIManager UI;
    private SpawnManager SM;
    private GameManager GM;


    // Start is called before the first frame update
    // runs before the first frame only
    // method - block of code
    void Start()
    {

        // script communication
        // go to hierarchy to find UIManager
        UI = GameObject.Find("UIManager").GetComponent<UIManager>();

        GM = GameObject.Find("GameManager").GetComponent<GameManager>();

        // Debug.Log("Hello"); // prints the debug message

        Debug.Log(transform.position);

        // set starting position
        transform.position = new Vector3(-1.78f, -0.13f, 0);

        lives = 3;
        if (UI != null)
        {
            UI.UpdateLives(lives);
        }


        SM = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        laserAudio = GetComponent<AudioSource>();

        if (SM != null)
        {
            SM.StartSpawn();
        }

        // controller = gameObject.AddComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (GM.gameOver)
        {
            Destroy(this.gameObject);
        }

        // Debug.Log("World");

        Move(); // for player movement
        Bounds(); // for checking the boundary
        Shoot(); // for shooting the laser

    }

    // For player movement
    // three parts to every method: heading, body, call
    private void Move()
    {

        // GetAxis returns -1 to 1 based on keyboard
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        // move up and adjusted for clock time(instead of fps)
        transform.Translate(Vector3.up * speed * verticalInput * Time.deltaTime);
        transform.Translate(Vector3.right * speed * horizontalInput * Time.deltaTime);

    }

    /*
     * Sets the bounds.
     */
    private void Bounds()
    {

        // boundary for moving vertically
        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }

        else if (transform.position.y < -4.15f)
        {
            transform.position = new Vector3(transform.position.x, -4.15f, 0);
        }

        // boundary for moving horizontally 
        if (transform.position.x > 9.5f)
        {
            transform.position = new Vector3(-9.5f, transform.position.y, 0);
        }

        else if (transform.position.x < -9.5f)
        {
            transform.position = new Vector3(9.5f, transform.position.y, 0);
        }
    }

    /*
     * Shoots lasers.
     */
    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time > canFire)
            {
                if (!canTripleShot)
                {
                    // clone a laser at the ship's position with the original rotation
                    Instantiate(laserPrefab, transform.position + new Vector3(0, 0.75f, 0), Quaternion.identity);
                    // instead of Quternion, laserPrefab.transform.rotation
                }
                else
                {
                    Instantiate(tripleShotPrefab, transform.position + new Vector3(0, 0.75f, 0), Quaternion.identity);
                }

                // play the sound
                laserAudio.Play();

                // increment cool down
                canFire = Time.time + fireRate; // jump internal time by fireRate
            }
        }
    } // end of Shoot()


    /*
     * 
     */
    public IEnumerator TripleShotOff()
    {
        yield return new WaitForSeconds(5.0f); // count down 5 seconds and set signal when it is done

        canTripleShot = false;
    }


    /*
     * 
     */
    public void TripleShotOn()
    {
        canTripleShot = true;

        StartCoroutine(TripleShotOff());
    }


    /*
     * 
     */
    public void SpeedBoostOn()
    {
        speed = 15.0f;

        StartCoroutine(SpeedBoostOff());
    }


    /*
     * 
     */
    public IEnumerator SpeedBoostOff()
    {
        yield return new WaitForSeconds(5.0f); // count down 5 seconds and set signal when it is done

        speed = 10.0f;
    }



    public void ShieldOn()
    {
        canShield = true;

        shield.SetActive(true);
    }



    public void Damage()
    {
        if (!canShield)
        {
            lives--; // lose a life

            if (lives == 2)
            {
                engines[0].SetActive(true);
            }
            else if (lives == 1)
            {
                engines[1].SetActive(true);
            }

            if (UI != null)
                UI.UpdateLives(lives);
        }
        else
        {
            canShield = false;
            shield.SetActive(false);
        }

        // death
        if (lives <= 0)
        {
            // end the game
            GM.gameOver = true;

            UI.ShowTitleScreen();

            Instantiate(playerExplosionAnimation, transform.position, Quaternion.identity);

            Destroy(gameObject); // destroy the player object
        }

    }


}
