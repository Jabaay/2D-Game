using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveFW : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Random.Range(5f, 8f) * Time.deltaTime);
    }
}
