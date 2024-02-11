using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMaster : MonoBehaviour
{

    [SerializeField] private GameObject[] enemyPrefabsFW;
    [SerializeField] private GameObject[] enemyPrefabsBW;

    private Vector3[] enemySpawnPos; // array length of 2, spawnPos[0] is FW, spawnPos[1] is BW

    public PlayerScript ps;


    // Start is called before the first frame update
    void Start()
    {
        // enemyBossHealth = 20f;

        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();

        enemySpawnPos = new Vector3[] 
        {   
            new Vector3(Random.Range(0.75f, 3.97f), -11f, 0), // FW, travel lanes
            new Vector3(Random.Range(-3.97f, -0.75f), 11f, 0), // BW, opposing lanes
        };

        StartCoroutine(EnemySpawn());
    }


    IEnumerator EnemySpawn()
    {
        while (GameObject.FindGameObjectWithTag("Player") != null)
        {
            // spawn FW enemies
            Instantiate(enemyPrefabsFW[Random.Range(0, enemyPrefabsFW.Length)], enemySpawnPos[0], Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(1f, 2f));

            // spawn BW enemies
            Instantiate(enemyPrefabsBW[Random.Range(0, enemyPrefabsBW.Length)], enemySpawnPos[1], Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(0f, 2f));
        }

    }



}
