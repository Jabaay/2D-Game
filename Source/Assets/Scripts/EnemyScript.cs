using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    public float enemyHealth;

    public PlayerScript ps;

    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = 10f;
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyBounds();
    }


    // Set the playable area.
    private void EnemyBounds()
    {
        // destroy when out of bounds
        if (Mathf.Abs(transform.position.y) > 11f)
        {
            Destroy(gameObject);
        }
    }



    // Takes care of the collisions between Player and Enemy.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if Player hits Enemy
        if (collision.CompareTag("Player"))
        {

            if (enemyHealth >= 5)
            {
                enemyHealth -= 5;
            }
            else
            {
                ps.SetPlayerStats();
                Destroy(gameObject); // destroy Enemy
                ps.killCount += 1;
            }

            if (ps.shieldOn == false && ps.healthBar.value <= 5)
            {
                ps.SetHealthBarVal(0);
                // ps.playerExplosion.SetActive(true);
                Destroy(collision.gameObject); // destroy Player
                // play death animation
                // show death screen/menu
                Time.timeScale = 0; // stop the game
            }
            else
            {
                if (ps.shieldOn == true)
                {
                    if (ps.shieldBar.value == 10)
                    {
                        ps.SetShieldBarVal(5);
                    } 
                    else if (ps.shieldBar.value == 5)
                    {
                        ps.shield.SetActive(false);
                        ps.SetShieldBarVal(0);
                    }
                }
                else
                {
                    ps.healthBar.value -= 5;
                }
            }
        }

        // if Bullet hits Enemy
        if (collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject); // destroy Bullet

            if (enemyHealth >= 5) // enemy health -5 if >= 5 
            {
                enemyHealth -= 5;
            }
            else // else destroy it (< 5) and set Player stats
            {
                ps.SetPlayerStats();
                Destroy(gameObject);  // destroy Enemy
                ps.killCount += 1;
                ps.score *= 5;
            }
        }

        // if Missile hits Enemy
        if (collision.CompareTag("Missile")) // simply destroy it and set Player stats
        {
            ps.SetPlayerStats();
            Destroy(collision.gameObject);
            Destroy(gameObject); // destroy enemy
            ps.killCount += 1;
            ps.score *= 5;
        }

    }


}
