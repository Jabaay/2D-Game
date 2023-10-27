using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    private float speed = 20.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector3.up * speed * Time.deltaTime);

        // removes objects when not used
        if (transform.position.y > 6.5f)
        {
            if (transform.parent == null)
            {
                //remove items
                Destroy(gameObject); // destroy laser if tripleshot is null
            } else
            {
                Destroy(transform.parent.gameObject); // else destroy tripleshot
            }
        }

    }
}
