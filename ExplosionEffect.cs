using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // remove the animation
        // Destroy(gameObject, 2.633f); // simple way to detroy it

        // updates length based on actual animation
        Destroy(gameObject, GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
