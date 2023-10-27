using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    // instance field
    [SerializeField] private int powerUpID;

    private float powerUpSpeed = 5.0f;

    [SerializeField] private AudioClip powerUpClip;

    // script communication, getting something from one script into another


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * powerUpSpeed * Time.deltaTime);

        // removes objects when not used
        if (transform.position.y < -7.0f)
        {
            Destroy(gameObject); // destroy powerup
        }
    }

    /*
     * 
     * 
     * @param collision what we run into
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("We got hit by " + collision.name);

        // ensure hit by player
        if (collision.CompareTag("Player"))
        {
            // check the tag of the object we collided with, if it is the Player do this
            // reach out to Player scipt and look at inispector components
            Player p = collision.GetComponent<Player>();


            if (p != null)
            {

                if (powerUpID == 1)
                {
                    p.TripleShotOn(); // separate the timer from the regular program
                }
                else if (powerUpID == 2) { 
                    p.SpeedBoostOn();
                } else {
                    p.ShieldOn();
                }

            }

            // play the sound
            AudioSource.PlayClipAtPoint(powerUpClip, Camera.main.transform.position, 1.0f);

            Destroy(gameObject); // destroy the powerup object


        }

    }


}
