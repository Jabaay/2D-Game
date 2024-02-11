using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    private float bulletSpeed = 20f;

    public PlayerScript ps;

    private CapsuleCollider2D playerCollider;
    private CapsuleCollider2D bulletCollider;

    // Start is called before the first frame update
    void Start()
    {
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        BulletCollisionIgnore();
    }

    // Update is called once per frame
    void Update()
    {
        BulletMovement();
    }


    private void BulletMovement()
    {
        transform.Translate(Vector3.up * bulletSpeed * Time.deltaTime);

        // removes objects when not used
        if (transform.position.y > 9f)
        {
            Destroy(gameObject); // destroy Bullet if out of bounds
        }
    }

    private void BulletCollisionIgnore()
    {
        playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<CapsuleCollider2D>();
        bulletCollider = GetComponent<CapsuleCollider2D>();

        if (playerCollider != null && bulletCollider != null)
        {
            Physics2D.IgnoreCollision(playerCollider, bulletCollider);
        }
    }


}

