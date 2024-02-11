using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileScript : MonoBehaviour
{

    private float missileSpeed = 15f;

    public PlayerScript ps;

    private CapsuleCollider2D playerCollider;
    private CapsuleCollider2D missileCollider;


    // Start is called before the first frame update
    void Start()
    {
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        MissileCollisionIgnore();
    }

    // Update is called once per frame
    void Update()
    {
        MissileMovement();
    }


    private void MissileMovement()
    {
        transform.Translate(Vector3.up * missileSpeed * Time.deltaTime);

        // removes objects when not used
        if (transform.position.y > 9f)
        {
            //remove items
            Destroy(gameObject); // destroy laser if tripleshot is null
        }
    }


    private void MissileCollisionIgnore()
    {
        playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<CapsuleCollider2D>();
        missileCollider = GetComponent<CapsuleCollider2D>();

        if (playerCollider != null && missileCollider != null)
        {
            Physics2D.IgnoreCollision(playerCollider, missileCollider);
        }
    }


}

