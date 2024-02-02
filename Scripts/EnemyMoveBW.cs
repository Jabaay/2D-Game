using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveBW : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Random.Range(5f, 8f) * Time.deltaTime);
    }
}
