using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject[] powerUpPrefab;
    private GameManager GM; // empty variable

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>(); // before coroutines
    }


    // Update is called once per frame
    void Update()
    {
        
        
    }


    public void StartSpawn()
    {
        StartCoroutine(EnemySpawn());
        StartCoroutine(PowerUpSpawn());
    }


    IEnumerator EnemySpawn()
    {
        // sequence - line after line
        // selection - if statements for decisions
        // iteration - looping, repeating code
        while (!GM.gameOver)
        {
            Instantiate(enemyPrefab, new Vector3(Random.Range(-6.5f, 6.5f), 6.5f, 0), Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(3.0f, 5.0f));
        }
    }


    IEnumerator PowerUpSpawn()
    {
        while (true)
        {
            // inclusive and excluvisve
            Instantiate(powerUpPrefab[Random.Range(0, powerUpPrefab.Length)], new Vector3(Random.Range(-6.5f, 6.5f), 6.5f, 0), Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(3.0f, 5.0f));
        }

    }



}
