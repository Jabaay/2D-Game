using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private float enemySpeed = 3.0f;

    public GameObject explosionAnimation;

    private UIManager UI;

    [SerializeField] private AudioClip boomClip;

    // Start is called before the first frame update
    void Start()
    {
       UI = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * enemySpeed * Time.deltaTime);

        // recycle enemies (bad AI)
        if (transform.position.y <= -6.2f)
        {
            // float float, inclusive/inclusive
            // int int, inclusive/exclusive
            float xPos = Random.Range(-8.12f, 8.12f);

            transform.position = new Vector3(xPos, 2.36f, 0); // reset the enemy position if not destroyed
        }


    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("laser")) // laser or tripleshot
        {
            if (collision.transform.parent != null) // triple
            {
                Destroy(collision.transform.parent.gameObject);
            }
            else
            {
                Destroy(collision.gameObject); // regular
            }
        }


        if (collision.CompareTag("Player"))
        {

            // script communication 

            Player P = GameObject.Find("Player(Clone)").GetComponent<Player>();
            // Player P = collision.GetComponent<Player>();

            if (P != null)
            {
                P.Damage();
            }
        }


        // run explosion animation
        Instantiate(explosionAnimation, transform.position, Quaternion.identity);

        if (UI != null)
        {
            UI.UpdateScore();
        }

        // play sound
        AudioSource.PlayClipAtPoint(boomClip, Camera.main.transform.position, 1.0f);

        Destroy(gameObject);

    }







}
